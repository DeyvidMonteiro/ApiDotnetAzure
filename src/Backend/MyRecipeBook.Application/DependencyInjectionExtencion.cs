using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.UseCases.Dashboard;
using MyRecipeBook.Application.UseCases.Login.DoLogin;
using MyRecipeBook.Application.UseCases.Recipe.Delete;
using MyRecipeBook.Application.UseCases.Recipe.Filter;
using MyRecipeBook.Application.UseCases.Recipe.Generate;
using MyRecipeBook.Application.UseCases.Recipe.GetById;
using MyRecipeBook.Application.UseCases.Recipe.Image;
using MyRecipeBook.Application.UseCases.Recipe.Register;
using MyRecipeBook.Application.UseCases.Recipe.Update;
using MyRecipeBook.Application.UseCases.Users.ChangePassword;
using MyRecipeBook.Application.UseCases.Users.Delete;
using MyRecipeBook.Application.UseCases.Users.Profile;
using MyRecipeBook.Application.UseCases.Users.Registrar;
using MyRecipeBook.Application.UseCases.Users.Update;
using Sqids;

namespace MyRecipeBook.Application;

public static class DependencyInjectionExtencion
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAutoMapper(services);
        AddEncoder(services, configuration);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {

        services.AddScoped(option => new AutoMapper.MapperConfiguration(autoMapperOptions =>
        {
            var sqids = option.GetService<SqidsEncoder<long>>()!;

            autoMapperOptions.AddProfile(new AutoMapping(sqids));
        }).CreateMapper());
    }

    private static void AddEncoder(IServiceCollection services, IConfiguration configuration)
    {
        var sqids = new SqidsEncoder<long>(new()
        {
            MinLength = 3,
            Alphabet = configuration.GetValue<string>("Settings:IdCryptographyAlphabet")!
        });

        services.AddSingleton(sqids);
    }


    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUseruseCase, RegisterUseruseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUserProfilerUseCase, GetUserProfileUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        services.AddScoped<IRegisterRecipeUseCase, RegisterRecipeUseCase>();
        services.AddScoped<IFilterRecipeUseCase, FilterRecipeUseCase>();
        services.AddScoped<IGetRecipeByIdUseCase, GetRecipeByIdUseCase>();
        services.AddScoped<IDeleteRecipeUseCase, DeleteRecipeUseCase>();
        services.AddScoped<IUpdateRecipeUseCase, UpdateRecipeUseCase>();
        services.AddScoped<IGetDashboardUseCase, GetDashboardUseCase>();
        services.AddScoped<IGenerateRecipeUseCase, GenerateRecipeUseCase>();
        services.AddScoped<IAddUpdateImageCoverUserCase, AddUpdateImageCoverUserCase>();

    }

}
