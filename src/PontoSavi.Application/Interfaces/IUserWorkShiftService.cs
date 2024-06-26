﻿using PontoSavi.Domain.Entities;

namespace PontoSavi.Application.Interfaces;

public interface IUserWorkShiftService
{
    Task<List<WorkShift>> GetByUserId(int userId, int companyId);
    Task<UserWorkShift> Create(UserWorkShift userWorkShift);
    Task<UserWorkShift> Delete(UserWorkShift userWorkShift);
}
