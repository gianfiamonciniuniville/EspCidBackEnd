namespace EspCid.Domain.Interfaces;

public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T Get(int id);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);   
}