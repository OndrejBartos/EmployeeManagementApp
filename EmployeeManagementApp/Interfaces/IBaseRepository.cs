using System.Linq.Expressions;

namespace EmployeeManagementAPI.Interfaces;

public interface IBaseRepository<T> where T : class, IModel
{
    public ICollection<T> GetAll(Expression<Func<T, object>>? include = null);
    public T? GetById(int id, Expression<Func<T, object>>? include = null);
    public bool Create(T entity);
    public bool Update(T entity);
    public bool Delete(int id);
    public bool Exists(int id);
}
