using System.Threading;
using System.Threading.Tasks;

namespace EurekaBot.Application.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
