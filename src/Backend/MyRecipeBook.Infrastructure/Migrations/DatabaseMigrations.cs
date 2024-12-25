using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Extencions;
using MySqlConnector;

namespace MyRecipeBook.Infrastructure.Migrations;

public static class DatabaseMigrations
{
    public static void migrate(string connectionstring, IServiceProvider serviceProvider)
    {
        Ensuredatabasecreated_MySql(connectionstring);
        Migrationdatabase(serviceProvider);
    }

    private static void Ensuredatabasecreated_MySql(string connectionstring)
    {
        var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionstring);

        var databaseName = connectionStringBuilder.Database;

        connectionStringBuilder.Remove("Database");

        using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);

        var parameters = new DynamicParameters();
        parameters.Add("name", databaseName);

        var records = dbConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", parameters);

        if (records.Any().IsFalse())
        {
            dbConnection.Execute($"CREATE DATABASE {databaseName}");
        }

    }

    private static void Migrationdatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        runner.ListMigrations();

        runner.MigrateUp();
    }

}
