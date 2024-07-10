using Inventory.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Common.Application.Interfaces;
using Inventory.Domain.Entities;

namespace Inventory.Infrastructure.Repositories;
public abstract class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
    where TEntity : BaseEntity
    where TContext : DbContext
{
    private readonly TContext _context;

    public GenericRepository(TContext context)
    {
        _context = context;
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener la entidad con id {id}: {ex.Message}", ex);
        }
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        try
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener todas las entidades: {ex.Message}", ex);
        }
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var query = SqlQueryGenerator.GenerateInsertQuery<TEntity>();
        var connectionString = _context.Database.GetConnectionString();

        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(query, entity);
            }
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al agregar la entidad: {ex.Message}", ex);
        }
    }

    public async Task<TEntity> DeleteAsync(int id)
    {
        try
        {
            var entity = await GetByIdAsync(id);
            var query = SqlQueryGenerator.GenerateDeleteQuery<TEntity>();
            var connectionString = _context.Database.GetConnectionString();
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al eliminar la entidad con id {id}: {ex.Message}", ex);
        }
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        try
        {
            var query = SqlQueryGenerator.GenerateUpdateQuery<TEntity>();
            var connectionString = _context.Database.GetConnectionString();
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(query, entity);
            }

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al actualizar la entidad: {ex.Message}", ex);
        }
    }
}
