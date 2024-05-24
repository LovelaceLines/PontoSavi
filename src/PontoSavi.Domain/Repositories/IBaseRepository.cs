namespace PontoSavi.Domain.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<List<T>> GetAll();
    Task<T> GetById(dynamic id);
    Task<int> GetCount();
    Task<bool> ExistsById(params object?[]? keyValues);
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<T> Remove(T entity);
    Task<T> RemoveById(dynamic id);

    Task BeginTransaction();
    Task CommitTransaction();
    Task RollbackTransaction();
}