using System.Net;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Repositories;

namespace PontoSavi.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;

    public CompanyService(ICompanyRepository repository) =>
        _repository = repository;

    public async Task<QueryResult<Company>> Query(CompanyFilter filter) =>
        await _repository.Query(filter);

    public async Task<Company> GetById(int id) =>
        await _repository.GetById(id);

    public async Task<Company> Create(Company company)
    {
        // TODO if (await _repository.ExistsByCNPJ(company.CNPJ)) throw new AppException("CNPJ já cadastrado!", HttpStatusCode.BadRequest);
        // TODO if (await _repository.ExistsByName(company.Name)) throw new AppException("Nome já cadastrado!", HttpStatusCode.BadRequest);

        return await _repository.Add(company);
    }

    public async Task<Company> Update(Company newCompany)
    {
        var oldCompany = await _repository.GetById(newCompany.Id);

        // if (oldCompany.CNPJ != newCompany.CNPJ) throw new AppException("CNPJ não pode ser alterado!", HttpStatusCode.BadRequest);
        // if (oldCompany.Name != newCompany.Name) throw new AppException("Nome não pode ser alterado!", HttpStatusCode.BadRequest);

        oldCompany.TradeName = newCompany.TradeName;

        return await _repository.Update(oldCompany);
    }

    public async Task<Company> Delete(int id)
    {
        var company = await _repository.GetById(id);
        return await _repository.Remove(company);
    }
}
