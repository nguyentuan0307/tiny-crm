using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Domain.ProductAggregate.Specifications;

public class ProductCodeExactMatchSpecification : Specification<Product>
{
    private readonly string _code;

    public ProductCodeExactMatchSpecification(string code)
    {
        _code = code;
    }

    public override Expression<Func<Product, bool>> ToExpression()
    {
        if (string.IsNullOrWhiteSpace(_code)) return product => true;

        return product => product.Code.ToUpper() == _code.ToUpper();
    }
}