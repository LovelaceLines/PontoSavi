using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Domain.Repositories;

public interface ICompanyRepository : IBaseRepository<Company>
{
    Task<QueryResult<CompanyDTO>> Query(CompanyFilter filter);
    Task<bool> ExistsById(int id);
    Task<bool> ExistsByPublicId(string publicId);
    Task<bool> ExistsByName(string name);
    Task<bool> ExistsByCNPJ(string cnpj);
    Task<Company> GetById(int id);
    Task<Company> GetByPublicId(string publicId);
    Task<Company> GetByName(string name);
    Task<Company> GetByCNPJ(string cnpj);
}
