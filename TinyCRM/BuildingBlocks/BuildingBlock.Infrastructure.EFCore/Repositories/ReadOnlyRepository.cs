using System.Linq.Dynamic.Core;
using BuildingBlock.Domain.Model;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Infrastructure.EFCore.Repositories;

public class ReadOnlyRepository<TDbContext, TEntity> : IReadOnlyRepository<TEntity>
    where TDbContext : BaseDbContext
    where TEntity : GuidEntity
{
    private readonly TDbContext _dbContext;
    private DbSet<TEntity>? _dbSet;

    public ReadOnlyRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected DbSet<TEntity> DbSet => _dbSet ??= _dbContext.Set<TEntity>();

    public Task<TEntity?> GetAnyAsync(ISpecification<TEntity> specification,
        string? includeTables = null)
    {
        var query = DbSet.AsQueryable();

        query = Filter(query, specification);

        query = Include(query, includeTables);

        return query.FirstOrDefaultAsync();
    }

    public Task<List<TEntity>> GetAllAsync(ISpecification<TEntity>? specification = null,
        string? includeTables = null)
    {
        var query = DbSet.AsQueryable();

        if (specification != null) query = Filter(query, specification);

        query = Include(query, includeTables);

        return query.ToListAsync();
    }

    public Task<bool> CheckIfExistAsync(ISpecification<TEntity>? specification = null)
    {
        return specification != null
            ? DbSet.AsNoTracking().AnyAsync(specification.ToExpression())
            : DbSet.AsNoTracking().AnyAsync();
    }

    public async Task<(List<TEntity>, int)> GetFilterAndPagingAsync(ISpecification<TEntity> specification, string sort,
        int skip,
        int take, string? includeTables = null)
    {
        var query = DbSet.AsQueryable();

        query = Include(query, includeTables);

        query = Filter(query, specification);

        var totalCount = await query.CountAsync();

        query = query.OrderBy(sort);

        query = query.Skip(skip).Take(take);

        return (await query.ToListAsync(), totalCount);
    }

    public Task<int> CountAsync(ISpecification<TEntity> specification)
    {
        var query = DbSet.AsQueryable();
        query = Filter(query, specification);
        return query.CountAsync();
    }

    private static IQueryable<TEntity> Filter(IQueryable<TEntity> query, ISpecification<TEntity> specification)
    {
        return query.Where(specification.ToExpression());
    }

    private static IQueryable<TEntity> Include(IQueryable<TEntity> query, string? includeTables = null)
    {
        if (string.IsNullOrEmpty(includeTables)) return query;

        var includeProperties = includeTables.Split(',');

        return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }
}