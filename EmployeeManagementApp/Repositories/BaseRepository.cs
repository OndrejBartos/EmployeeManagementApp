using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeManagementAPI.Repositories;

/// <summary>
/// Implementation of the base repository interface that contains all CRUD operations.
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public class BaseRepository<T> : IBaseRepository<T> where T : class, IModel
{
    protected readonly DataContext _context;

    protected BaseRepository(DataContext context)
    {
        _context = context;
    }

    public bool Create(T entity)
    {
        _context.Set<T>().Add(entity);
        return Save();
    }

    public bool Delete(int id)
    {
        var entity = GetById(id);

        if (entity is null)
            return false;

        _context.Set<T>().Remove(entity);
        return Save();
    }

    public bool Exists(int id)
    {
        return _context.Set<T>().Any(entity => entity.Id == id);
    }

    public ICollection<T> GetAll(Expression<Func<T, object>>? include = null)
    {
        var query = _context.Set<T>().AsQueryable();

        if (include != null)
        {
            query = query.Include(include);
        }

        return query.ToArray();
    }

    public T? GetById(int id, Expression<Func<T, object>>? include = null)
    {
        var query = _context.Set<T>().AsQueryable();

        if (include != null)
        {
            query = query.Include(include);
        }

        return query.FirstOrDefault(entity => entity.Id == id);
    }

    public bool Update(T entity)
    {
        if (entity is null)
            return false;

        _context.Set<T>().Update(entity);
        return Save();
    }

    protected bool Save()
    {
        try
        {
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
