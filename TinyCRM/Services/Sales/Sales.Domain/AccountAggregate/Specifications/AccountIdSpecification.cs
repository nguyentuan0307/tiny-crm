using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;

namespace Sales.Domain.AccountAggregate.Specifications;

public class AccountIdSpecification : Specification<Account>
{
    private readonly Guid _id;

    public AccountIdSpecification(Guid id)
    {
        _id = id;
    }

    public override Expression<Func<Account, bool>> ToExpression()
    {
        return account => account.Id == _id;
    }
}