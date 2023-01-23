using System.Linq;
using EurekaBot.Domain.Entities.Shared.Primitives;

namespace EurekaBot.Infrastructure.Persistence.Specifications.Shared;

public interface ISpecification<T> where T : Entity
{
    IQueryable<T> ApplySpecification(Specification<T> specification, bool trackChanges = default);
}
