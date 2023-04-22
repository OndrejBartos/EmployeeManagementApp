namespace EmployeeManagementAPI.Interfaces;

public interface IBaseRepository<T> where T : IModel
{
    public ICollection<T> GetAll();
    public T? GetById(int id);
    public bool Create(T entity);
    public bool Update(T entity);
    public bool Delete(int id);
    public bool Exists(int id);
}
