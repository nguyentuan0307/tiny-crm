using BuildingBlock.Domain.Model;
using BuildingBlock.Domain.Specifications;

namespace BuildingBlock.Domain.Repositories;

public interface IReadOnlyRepository<TEntity> where TEntity : GuidEntity
{
    Task<TEntity?> GetAnyAsync(ISpecification<TEntity> specification, string? includeTables = null);

    Task<List<TEntity>> GetAllAsync(ISpecification<TEntity>? specification = null, string? includeTables = null);


    Task<bool> CheckIfExistAsync(ISpecification<TEntity>? specification = null);

    Task<(List<TEntity>, int)> GetFilterAndPagingAsync(ISpecification<TEntity> specification,
        string sort, int skip, int take, string? includeTables = null);
    
    Task<int> CountAsync(ISpecification<TEntity> specification);
}