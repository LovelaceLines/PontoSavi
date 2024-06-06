using System.Net;

using PontoSavi.Application.Interfaces;
using PontoSavi.Domain.DTOs;
using PontoSavi.Domain.Entities;
using PontoSavi.Domain.Exceptions;
using PontoSavi.Domain.Filters;
using PontoSavi.Domain.Repositories;

namespace PontoSavi.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;

    public CompanyService(ICompanyRepository repository) =>
        _repository = repository;

    public async Task<QueryResult<CompanyDTO>> Query(CompanyFilter filter) =>
        await _repository.Query(filter);

    public async Task<Company> GetById(int id) =>
        await _repository.GetById(id);

    public async Task<Company> GetByPublicId(string publicId) =>
        await _repository.GetByPublicId(publicId);

    public async Task<Company> Create(Company company)
    {
        // TODO if (await _repository.ExistsByCNPJ(company.CNPJ)) throw new AppException("CNPJ já cadastrado.", HttpStatusCode.BadRequest);
        // TODO if (await _repository.ExistsByName(company.Name)) throw new AppException("Nome já cadastrado.", HttpStatusCode.BadRequest);

        return await _repository.Add(company);
    }

    public async Task<Company> Update(Company newCompany)
    {
        var oldCompany = await _repository.GetByPublicId(newCompany.PublicId);

        if (oldCompany.CNPJ != newCompany.CNPJ) throw new AppException("CNPJ não pode ser alterado.", HttpStatusCode.BadRequest);
        if (oldCompany.Name != newCompany.Name) throw new AppException("Nome não pode ser alterado.", HttpStatusCode.BadRequest);

        oldCompany.TradeName = newCompany.TradeName;

        return await _repository.Update(oldCompany);
    }

    public async Task<Company> Delete(string publicId)
    {
        if (!await _repository.ExistsByPublicId(publicId)) throw new AppException("Empresa não encontrada.", HttpStatusCode.NotFound);

        var company = await _repository.GetByPublicId(publicId);
        return await _repository.Remove(company);
    }
}
