using AutoMapper;
using BuildingBlock.Application.CQRS.Query;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using Sales.Application.CQRS.Queries.LeadQueries.Requests;
using Sales.Application.DTOs.LeadDTOs;
using Sales.Domain.LeadAggregate;
using Sales.Domain.LeadAggregate.Exceptions;
using Sales.Domain.LeadAggregate.Specifications;

namespace Sales.Application.CQRS.Queries.LeadQueries.Handlers;

public class GetLeadQueryHandler : IQueryHandler<GetLeadQuery, LeadDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Lead> _repository;

    public GetLeadQueryHandler(
        IReadOnlyRepository<Lead> leadReadOnlyRepository,
        IMapper mapper
    )
    {
        _repository = leadReadOnlyRepository;
        _mapper = mapper;
    }

    public async Task<LeadDetailDto> Handle(GetLeadQuery request, CancellationToken cancellationToken)
    {
        var leadIdSpecification = new LeadIdSpecification(request.Id);

        var lead = Optional<Lead>.Of(await _repository.GetAnyAsync(leadIdSpecification))
            .ThrowIfNotPresent(new LeadNotFoundException(request.Id)).Get();

        return _mapper.Map<LeadDetailDto>(lead);
    }
}