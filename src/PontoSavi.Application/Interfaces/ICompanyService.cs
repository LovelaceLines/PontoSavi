using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Application.Interfaces;

public interface ICompanyService
{
    Task<QueryResult<Company>> Query(CompanyFilter filter);
    Task<Company> GetById(int id);
    Task<Company> Create(Company company);
    Task<Company> Update(Company company);
    Task<Company> Delete(int id);
}
