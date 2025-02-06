using FluentValidation;
using SoftwareCatalog.Api.Techs.Endpoints;

namespace SoftwareCatalog.Api.Techs;

public static class Extensions
{
    public static IServiceCollection AddTechServices(this IServiceCollection services)
    {
        services.AddScoped<IValidator<TechCreateModel>, TechCreateModelValidator>();

        services.AddAuthorizationBuilder()
            .AddPolicy(CONSTANTS.AUTHORIZATION.POLICIES.CAN_GET_TECHS, p =>
            {
                p.RequireRole("manager");
                p.RequireRole("software-center");
            });

        return services;
    }

    public static IEndpointRouteBuilder MapTechs(this IEndpointRouteBuilder routes)
    {
        var group = routes
            .MapGroup("techs")
            .WithTags("Software Center API Techs")
            .WithDescription("The cool technicians that manage all software.");

        group.MapPost("/", AddTech.CreateTech).RequireAuthorization(CONSTANTS.AUTHORIZATION.POLICIES.CAN_GET_TECHS);
        group.MapGet("/{id}", GetTechs.GetSingleTech).RequireAuthorization(CONSTANTS.AUTHORIZATION.POLICIES.CAN_GET_TECHS);
        group.MapGet("/", GetTechs.GetAllTechsAsync).RequireAuthorization(CONSTANTS.AUTHORIZATION.POLICIES.CAN_GET_TECHS);

        return group;
    }
}
