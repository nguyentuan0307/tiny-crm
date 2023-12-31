using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.LeadAggregate.Enums;

namespace Sales.Domain.LeadAggregate.Specifications;

public class LeadStatusFilterSpecification : Specification<Lead>
{
    private readonly LeadStatus? _status;

    public LeadStatusFilterSpecification(LeadStatus? status)
    {
        _status = status;
    }

    public override Expression<Func<Lead, bool>> ToExpression()
    {
        if (_status == null) return lead => true;

        return lead => lead.Status == _status;
    }
}