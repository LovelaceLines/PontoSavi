using Microsoft.EntityFrameworkCore;
using System.Net;

using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Repositories;
using PontoSavi.Infra.Data.Context;

namespace PontoSavi.Infra.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public BaseRepository(AppDbContext context) =>
        _context = context;

    public virtual async Task<List<T>> GetAll() =>
        await _context.Set<T>().AsNoTracking().ToListAsync();

    public virtual async Task<T> GetById(dynamic id) =>
        await _context.Set<T>().FindAsync(id) ??
            throw new AppException("Entidade não encontrada!", HttpStatusCode.NotFound);

    public virtual async Task<bool> ExistsById(params object?[]? keyValues) =>
        await _context.Set<T>().FindAsync(keyValues) != null;

    public virtual async Task<int> GetCount() =>
        await _context.Set<T>().CountAsync();

    public virtual async Task<T> Add(T entity)
    {
        try
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex) { throw CatchError(ex); }
    }

    public virtual async Task<T> Update(T entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex) { throw CatchError(ex); }
    }

    public virtual async Task<T> Remove(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<T> RemoveById(dynamic id)
    {
        T entity = await GetById(id);
        _context.Remove(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task BeginTransaction() =>
        await _context.Database.BeginTransactionAsync();

    public async Task CommitTransaction() =>
        await _context.Database.CommitTransactionAsync();

    public async Task RollbackTransaction() =>
        await _context.Database.RollbackTransactionAsync();

    public Exception CatchError(Exception ex)
    {
        var message = ex.InnerException?.Message ?? ex.Message;

        if (message.Contains("UNIQUE constraint")) throw new AppException("Valor já existe!", HttpStatusCode.Conflict);
        throw new Exception(message);
    }
}