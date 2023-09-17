using BuildingBlock.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.CQRS.Queries.Requests;
using Sales.Application.DTOs;

namespace Sales.API.Controllers;

[ApiController]
[Route("api/leads")]
public class LeadController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeadController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<FilterAndPagingResultDto<LeadDto>>> GetAllAsync(
        [FromQuery] FilterAndPagingLeadsDto dto)
    {
        var leads = await _mediator.Send(new FilterAndPagingLeadsQuery(dto));

        return Ok(leads);
    }

    [HttpGet("{id:guid}")]
    [ActionName(nameof(GetByIdAsync))]
    public async Task<ActionResult<LeadDto>> GetByIdAsync(Guid id)
    {
        var lead = await _mediator.Send(new GetLeadQuery(id));

        return Ok(lead);
    }
}