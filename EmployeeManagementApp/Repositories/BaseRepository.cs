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

    public async Task<bool> Create(T entity)
    {
        _context.Set<T>().Add(entity);
        return await Save();
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await GetById(id);

        if (entity is null)
            return false;

        _context.Set<T>().Remove(entity);
        return await Save();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Set<T>().AnyAsync(entity => entity.Id == id);
    }

    public async Task<ICollection<T>> GetAll(Expression<Func<T, object>>? include = null)
    {
        var query = _context.Set<T>().AsQueryable();

        if (include != null)
        {
            query = query.Include(include);
        }

        return await query.ToArrayAsync();
    }

    public async Task<T?> GetById(int id, Expression<Func<T, object>>? include = null)
    {
        var query = _context.Set<T>().AsQueryable();

        if (include != null)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task<bool> Update(T entity)
    {
        if (entity is null)
            return false;

        _context.Set<T>().Update(entity);
        return await Save();
    }

    protected async Task<bool> Save()
    {
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
