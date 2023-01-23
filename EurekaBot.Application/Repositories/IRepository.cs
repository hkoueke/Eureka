namespace EurekaBot.Application.Repositories;

public interface IRepository<in T>
{
    void Add(T entityToAdd);

    void Update(T entityToUpdate);

    void Remove(T entityToRemove);
}
