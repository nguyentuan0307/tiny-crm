using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Domain.ProductAggregate.Specifications;

public class ProductCodePartialMatchSpecification : Specification<Product>
{
    private readonly string _code;

    public ProductCodePartialMatchSpecification(string code)
    {
        _code = code;
    }

    public override Expression<Func<Product, bool>> ToExpression()
    {
        if (string.IsNullOrWhiteSpace(_code)) return product => true;

        return product => product.Code.ToUpper().Contains(_code.ToUpper());
    }
}