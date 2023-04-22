using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Interfaces;

namespace EmployeeManagementAPI.Repositories;

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

    public ICollection<T> GetAll()
    {
        return _context.Set<T>().ToArray();
    }

    public T? GetById(int id)
    {
        return _context.Set<T>().FirstOrDefault(entity => entity.Id == id);
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
