﻿using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Application.Interfaces;

public interface IDayOffService
{
    Task<QueryResult<DayOff>> Query(DayOffFilter filter);
    Task<DayOff> GetById(int id, int companyId);
    Task<DayOff> GetByDate(DateTime date, int companyId);
    Task<DayOff> Create(DayOff dayOff);
    Task<DayOff> Update(DayOff dayOff);
    Task<DayOff> Delete(DayOff dayOff);
}
