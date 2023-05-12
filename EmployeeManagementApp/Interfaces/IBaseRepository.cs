using System.Linq.Expressions;

namespace EmployeeManagementAPI.Interfaces;

/// <summary>
/// Base repository interface that contains all CRUD operations.
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IBaseRepository<T> where T : class, IModel
{
    public Task<ICollection<T>> GetAll(Expression<Func<T, object>>? include = null);
    public Task<T?> GetById(int id, Expression<Func<T, object>>? include = null);
    public Task<bool> Create(T entity);
    public Task<bool> Update(T entity);
    public Task<bool> Delete(int id);
    public Task<bool> Exists(int id);
}
