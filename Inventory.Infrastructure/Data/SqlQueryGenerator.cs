using System.Reflection;

namespace Inventory.Infrastructure.Data;
public static class SqlQueryGenerator
{
    public static string GenerateDeleteQuery<TEntity>()
    {
        var tableName = typeof(TEntity).Name;
        return $"UPDATE {tableName} SET Status = 2 WHERE Id = @Id";
    }

    public static string GenerateUpdateQuery<TEntity>()
    {
        var tableName = typeof(TEntity).Name;
        var setClause = string.Join(", ", typeof(TEntity).GetProperties()
            .Where(p => !p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
            .Select(p => p.Name + " = @" + p.Name));
        return $"UPDATE {tableName} SET {setClause} WHERE Id = @Id";
    }

    public static string GenerateInsertQuery<TEntity>()
    {
        var tableName = typeof(TEntity).Name;
        var properties = typeof(TEntity).GetProperties()
                                        .Where(p => !IsIdentityColumn(p))
                                        .ToArray();

        var columns = string.Join(", ", properties.Select(p => p.Name));
        var values = string.Join(", ", properties.Select(p => "@" + p.Name));

        return $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
    }

    private static bool IsIdentityColumn(PropertyInfo property)
    {
        return property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase);
    }
}
