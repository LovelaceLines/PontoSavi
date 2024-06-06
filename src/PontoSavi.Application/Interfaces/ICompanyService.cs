using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;

namespace PontoSavi.Application.Interfaces;

public interface ICompanyService
{
    Task<QueryResult<CompanyDTO>> Query(CompanyFilter filter);
    Task<Company> GetById(int id);
    Task<Company> GetByPublicId(string publicId);
    Task<Company> Create(Company company);
    Task<Company> Update(Company newCompany);
    Task<Company> Delete(string publicId);
}
