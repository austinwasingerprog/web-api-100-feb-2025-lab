using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace SoftwareCatalog.Api.Techs.Endpoints;

public static class AddTech
{
    public static async Task<Results<Created<TechResponseModel>, BadRequest<IDictionary<string, string[]>>>> CreateTech(
        [FromBody] TechCreateModel request,
        [FromServices] IValidator<TechCreateModel> validator,
        [FromServices] IDocumentSession session,
        [FromServices] IHttpContextAccessor _httpContextAccessor
    )
    {
        var validations = await validator.ValidateAsync(request);
        if (!validations.IsValid)
        {
            return TypedResults.BadRequest(validations.ToDictionary());
        }

        var sub = _httpContextAccessor.HttpContext?.User?.Identity?.Name?.ToString() ?? string.Empty;
        var entity = new TechEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Sub = sub
        };
        session.Store(entity);
        await session.SaveChangesAsync();
        var response = entity.MapToModel();

        return TypedResults.Created($"/techs/{response.Id}", response);
    }
}