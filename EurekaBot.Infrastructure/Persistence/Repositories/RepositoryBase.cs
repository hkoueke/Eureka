using System.Linq;
using EurekaBot.Application.Repositories;
using EurekaBot.Domain.Entities.Shared.Primitives;
using EurekaBot.Infrastructure.Persistence.Specifications.Shared;

namespace EurekaBot.Infrastructure.Persistence.Repositories;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity>, ISpecification<TEntity>
    where TEntity : Entity
{
    private readonly ApplicationDbContext _dbContext;

    protected RepositoryBase(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Remove(TEntity entityToRemove)
    {
        _dbContext.Set<TEntity>().Remove(entityToRemove);
    }

    public void Add(TEntity entityToAdd)
    {
        _dbContext.Set<TEntity>().Add(entityToAdd);
    }

    public void Update(TEntity entityToUpdate)
    {
        _dbContext.Set<TEntity>().Update(entityToUpdate);
    }

    public IQueryable<TEntity> ApplySpecification(
        Specification<TEntity> specification, 
        bool trackChanges = false)
    {
        return SpecificationEvaluator
            .GetQuery(_dbContext.Set<TEntity>(), specification, trackChanges);
    }
}
