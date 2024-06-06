using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

using PontoSavi.Domain.DTOs;
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

    public async Task<QueryResult<CompanyDTO>> Query(CompanyFilter filter)
    {
        var query = _context.Companies.AsNoTracking().AsQueryable();

        if (!filter.Search.IsNullOrEmpty())
            query = query.Where(u =>
                u.Name.ToLower().Contains(filter.Search!.ToLower()) ||
                u.TradeName.ToLower().Contains(filter.Search!.ToLower()) ||
                u.CNPJ.ToLower().Contains(filter.Search!.ToLower()));

        if (!filter.PublicId.IsNullOrEmpty()) query = query.Where(u => u.PublicId == filter.PublicId);
        if (!filter.Name.IsNullOrEmpty()) query = query.Where(u => u.Name!.ToLower().Contains(filter.Name!.ToLower()));
        if (!filter.TradeName.IsNullOrEmpty()) query = query.Where(u => u.TradeName!.ToLower().Contains(filter.TradeName!.ToLower()));
        if (!filter.CNPJ.IsNullOrEmpty()) query = query.Where(u => u.CNPJ!.ToLower().Contains(filter.CNPJ!.ToLower()));

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
            .Select(c => new CompanyDTO(c))
            .ToListAsync();

        return new QueryResult<CompanyDTO>(companies, totalCount);
    }

    public Task<bool> ExistsByCNPJ(string cnpj) =>
        _context.Companies.AsNoTracking().AnyAsync(c => c.CNPJ == cnpj);

    public Task<bool> ExistsById(int id) =>
        _context.Companies.AsNoTracking().AnyAsync(c => c.Id == id);

    public Task<bool> ExistsByName(string name) =>
        _context.Companies.AsNoTracking().AnyAsync(c => c.Name == name);

    public Task<bool> ExistsByPublicId(string publicId) =>
        _context.Companies.AsNoTracking().AnyAsync(c => c.PublicId == publicId);

    public Task<Company> GetByCNPJ(string cnpj) =>
        _context.Companies.AsNoTracking().FirstAsync(c => c.CNPJ == cnpj);

    public Task<Company> GetById(int id) =>
        _context.Companies.AsNoTracking().FirstAsync(c => c.Id == id);

    public Task<Company> GetByName(string name) =>
        _context.Companies.AsNoTracking().FirstAsync(c => c.Name == name);

    public Task<Company> GetByPublicId(string publicId) =>
        _context.Companies.AsNoTracking().FirstAsync(c => c.PublicId == publicId);
}
