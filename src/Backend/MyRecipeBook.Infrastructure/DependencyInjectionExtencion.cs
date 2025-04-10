﻿using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Criptography;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Domain.Services.OpenAI;
using MyRecipeBook.Domain.ValueObjects;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositorios;
using MyRecipeBook.Infrastructure.Extensions;
using MyRecipeBook.Infrastructure.Security.Cryptography;
using MyRecipeBook.Infrastructure.Security.Tokens.Access.Generator;
using MyRecipeBook.Infrastructure.Security.Tokens.Access.Validator;
using MyRecipeBook.Infrastructure.Services.LoggedUser;
using MyRecipeBook.Infrastructure.Services.OpenAI;
using OpenAI.Chat;
using System.Reflection;

namespace MyRecipeBook.Infrastructure;

public static class DependencyInjectionExtencion
{
    public static void AddAInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddPasswordEncripter(services, configuration);
        AddRepositories(services);
        AddLoggedUser(services);
        AddTokens(services, configuration);

        if (configuration.IsUnitTestEnviroment())
            return;

        AddDbContext_MySqlServer(services, configuration);
        AddFluentMigrator_MySql(services, configuration);
    }
    private static void AddDbContext_MySqlServer(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();
        var serverversion = new MySqlServerVersion(new Version(8, 0, 35));

        services.AddDbContext<MyRecipeBookDbContext>(dbContextOptions =>
        {
            dbContextOptions.UseMySql(connectionString, serverversion);
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
        services.AddScoped<IRecipeWriteOnlyRepository, RecipeRepository>();
        services.AddScoped<IRecipeReadOnlyRepository, RecipeRepository>();
        services.AddScoped<IRecipeUpdateOnlyRepository, RecipeRepository>();
    }

    private static void AddFluentMigrator_MySql(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options
            .AddMySql5()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.Load("MyRecipeBook.Infrastructure")).For.All();
        });
    }
    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        services.AddScoped<IAccessTokenValidator>(options => new JwtTokenValidator(signingKey!));
    }
    private static void AddLoggedUser(IServiceCollection services)
    {
        services.AddScoped<ILoggedUser, LoggedUser>();
    }
    private static void AddPasswordEncripter(IServiceCollection services, IConfiguration configuration)
    {
        var additionKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");

        services.AddScoped<IPasswordEncripter>(option => new Sha512Encripter(additionKey!));
    }

    private static void AddOpenAI(IServiceCollection services, IConfiguration configuration)
    {
        //services.AddScoped<IGenerateRecipeAI, ChatGptServices>();

        //var key = configuration.GetValue<string>("Settings:OpenAI:ApiKey");

        //services.AddScoped(c => new ChatClient(MyRecipeBookRuleConstants.CHAT_MODEL, apiKey)));

        //sprint 4 aula 141

    }
}
