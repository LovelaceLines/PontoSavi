using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Context;

namespace PontoSavi.Infra.Data.Repositories;

public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
    private readonly AppDbContext _context;

    public CompanyRepository(AppDbContext context) : base(context) =>
        _context = context;

    public async Task<QueryResult<Company>> Query(CompanyFilter filter)
    {
        var query = _context.Companies.AsNoTracking().AsQueryable();

        if (!filter.Search.IsNullOrEmpty())
            query = query.Where(u =>
                u.Name.ToLower().Contains(filter.Search!.ToLower()) ||
                u.TradeName.ToLower().Contains(filter.Search!.ToLower()) ||
                u.CNPJ.ToLower().Contains(filter.Search!.ToLower()));

        if (filter.Id.HasValue) query = query.Where(u => u.Id == filter.Id);

        if (!filter.Name.IsNullOrEmpty()) query = query.Where(u => u.Name!.ToLower().Contains(filter.Name!.ToLower()));
        if (!filter.TradeName.IsNullOrEmpty()) query = query.Where(u => u.TradeName!.ToLower().Contains(filter.TradeName!.ToLower()));
        if (!filter.CNPJ.IsNullOrEmpty()) query = query.Where(u => u.CNPJ!.ToLower().Contains(filter.CNPJ!.ToLower()));

        if (filter.IdDescOrderSort.HasValue)
            query = filter.IdDescOrderSort.Value ? query.OrderByDescending(u => u.Id) : query.OrderBy(u => u.Id);
        if (filter.NameDescOrderSort.HasValue)
            query = filter.NameDescOrderSort.Value ? query.OrderByDescending(u => u.Name) : query.OrderBy(u => u.Name);
        if (filter.TradeNameDescOrderSort.HasValue)
            query = filter.TradeNameDescOrderSort.Value ? query.OrderByDescending(u => u.TradeName) : query.OrderBy(u => u.TradeName);
        if (filter.CNPJDescOrderSort.HasValue)
            query = filter.CNPJDescOrderSort.Value ? query.OrderByDescending(u => u.CNPJ) : query.OrderBy(u => u.CNPJ);

        var totalCount = await query.CountAsync();

        var companies = await query
            .Skip(filter.PageIndex * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new QueryResult<Company>(companies, totalCount);
    }

    public async Task<bool> ExistsById(int id) =>
        await _context.Companies.AsNoTracking().AnyAsync(c => c.Id == id);

    public async Task<Company> GetById(int id) =>
        await _context.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id) ??
            throw new AppException("Empresa não encontrada!", HttpStatusCode.NotFound);
}
