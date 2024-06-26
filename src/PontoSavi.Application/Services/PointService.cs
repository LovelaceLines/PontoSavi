using System.Net;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Repositories;

namespace PontoSavi.Application.Services;

public class PointService : IPointService
{
    private readonly IPointRepository _repository;
    private readonly IUserWorkShiftRepository _userWorkShiftRepository;
    private readonly ICompanyWorkShiftRepository _companyWorkShiftRepository;

    public PointService(IPointRepository repository,
        IUserWorkShiftRepository userWorkShiftRepository,
        ICompanyWorkShiftRepository companyWorkShiftRepository)
    {
        _repository = repository;
        _userWorkShiftRepository = userWorkShiftRepository;
        _companyWorkShiftRepository = companyWorkShiftRepository;
    }

    public async Task<QueryResult<Point>> Query(PointFilter filter) =>
        await _repository.Query(filter);

    public async Task<Point> GetById(int id, int tenantId) =>
        await _repository.GetById(id, tenantId);

    public async Task<Point> GetOpenPoint(int userId, int tenantId) =>
        await _repository.GetOpenPointByUserId(userId, tenantId);

    public async Task<Point> ManualCheckIn(Point point)
    {
        if (await _repository.ExistsOpenPointByUserId(point.UserId, point.TenantId))
            throw new AppException("Usuário já possui um ponto aberto!", HttpStatusCode.BadRequest);

        point.CheckInStatus = PointStatus.ManualCheck;
        point.CheckOut = null;
        point.CheckOutAt = null;
        point.CheckOutStatus = null;
        point.CheckOutDescription = null;

        return await _repository.Add(point);
    }

    public async Task<Point> ManualCheckOut(Point newPoint)
    {
        if (!await _repository.ExistsOpenPointByUserId(newPoint.UserId, newPoint.TenantId))
            throw new AppException("Usuário não possui um ponto aberto!", HttpStatusCode.BadRequest);

        var oldPoint = await _repository.GetById(newPoint.Id, newPoint.TenantId);

        oldPoint.CheckOut = newPoint.CheckOut;
        oldPoint.CheckOutAt = DateTime.Now;
        oldPoint.CheckOutDescription = newPoint.CheckOutDescription;
        oldPoint.CheckOutStatus = PointStatus.ManualCheck;

        return await _repository.Update(oldPoint);
    }

    public async Task<Point> AutoCheckIn(int userId, int tenantId, string? description)
    {
        if (await _repository.ExistsOpenPointByUserId(userId, tenantId))
            throw new AppException("Usuário já possui um ponto aberto!", HttpStatusCode.BadRequest);

        var point = new Point
        {
            UserId = userId,
            CheckIn = DateTime.Now,
            CheckInStatus = PointStatus.AutoCheck,
            CheckInDescription = description,
            TenantId = tenantId,
        };

        return await _repository.Add(point);
    }

    public async Task<Point> AutoCheckOut(int userId, int tenantId, string? description)
    {
        if (!await _repository.ExistsOpenPointByUserId(userId, tenantId))
            throw new AppException("Usuário não possui um ponto aberto!", HttpStatusCode.BadRequest);

        var point = await _repository.GetOpenPointByUserId(userId, tenantId);

        point.CheckOut = DateTime.Now;
        point.CheckOutAt = DateTime.Now;
        point.CheckOutStatus = PointStatus.AutoCheck;
        point.CheckOutDescription = description;

        if (point.CheckInStatus == PointStatus.AutoCheck)
        {
            List<WorkShift> workShifts = await _userWorkShiftRepository.ExistsById(userId, tenantId) ?
                await _userWorkShiftRepository.GetWorkShiftByUserId(userId, tenantId) :
                await _companyWorkShiftRepository.GetWorkShiftByCompanyId(tenantId);

            if (IsStatusApproved(point, workShifts))
            {
                point.CheckInStatus = PointStatus.Approved;
                point.CheckOutStatus = PointStatus.Approved;
            }
        }

        return await _repository.Update(point);
    }

    public static bool IsStatusApproved(Point point, List<WorkShift> workShifts)
    {
        if (point.CheckIn > point.CheckOut || point.CheckIn.Day != point.CheckOut!.Value.Day ||
            point.CheckIn.Month != point.CheckOut!.Value.Month || point.CheckIn.Year != point.CheckOut!.Value.Year)
            return false;

        var pointCheckIn = new TimeOnly(point.CheckIn.Hour, point.CheckIn.Minute, point.CheckIn.Second);
        var pointCheckOut = new TimeOnly(point.CheckOut!.Value.Hour, point.CheckOut!.Value.Minute, point.CheckOut!.Value.Second);

        foreach (var workShift in workShifts)
        {
            var checkIn = workShift.CheckIn;
            var CheckOut = workShift.CheckOut;

            bool isPointCheckInTolerance = checkIn.AddMinutes(-workShift.CheckInToleranceMinutes) <= pointCheckIn &&
                pointCheckIn <= checkIn.AddMinutes(workShift.CheckInToleranceMinutes);
            bool isPointCheckOutTolerance = CheckOut.AddMinutes(-workShift.CheckOutToleranceMinutes) <= pointCheckOut &&
                pointCheckOut <= CheckOut.AddMinutes(workShift.CheckOutToleranceMinutes);

            if (isPointCheckInTolerance && isPointCheckOutTolerance)
                return true;
        }

        return false;
    }

    public async Task<Point> UpdateDescription(Point newPoint)
    {
        var oldPoint = await _repository.GetById(newPoint.Id, newPoint.TenantId);

        oldPoint.CheckInDescription = newPoint.CheckInDescription ?? oldPoint.CheckInDescription;
        oldPoint.CheckOutDescription = newPoint.CheckOutDescription ?? oldPoint.CheckOutDescription;

        return await _repository.Update(oldPoint);
    }

    public async Task<Point> UpdateFull(Point newPoint)
    {
        var oldPoint = await _repository.GetById(newPoint.Id, newPoint.TenantId);

        oldPoint.ManagerId = newPoint.ManagerId ?? oldPoint.ManagerId;
        oldPoint.CheckIn = newPoint.CheckIn;
        oldPoint.CheckInStatus = newPoint.CheckInStatus;
        oldPoint.CheckInDescription = newPoint.CheckInDescription;
        oldPoint.CheckOut = newPoint.CheckOut;
        oldPoint.CheckOutAt = DateTime.Now;
        oldPoint.CheckOutStatus = newPoint.CheckOutStatus;
        oldPoint.CheckOutDescription = newPoint.CheckOutDescription;

        return await _repository.Update(oldPoint);
    }

    public async Task<Point> Approve(int id, int managerId, int tenantId)
    {
        var point = await _repository.GetById(id, tenantId);

        point.CheckInStatus = PointStatus.Approved;
        point.CheckOutStatus = PointStatus.Approved;
        point.ManagerId = managerId;

        return await _repository.Update(point);
    }

    public async Task<Point> Reject(int id, int managerId, int tenantId)
    {
        var point = await _repository.GetById(id, tenantId);

        point.CheckInStatus = PointStatus.Rejected;
        point.CheckOutStatus = PointStatus.Rejected;
        point.ManagerId = managerId;

        return await _repository.Update(point);
    }
}
