using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Domain.Repositories;

public interface ICompanyRepository : IBaseRepository<Company>
{
    Task<QueryResult<Company>> Query(CompanyFilter filter);
    Task<bool> ExistsById(int id);
    Task<Company> GetById(int id);
}
