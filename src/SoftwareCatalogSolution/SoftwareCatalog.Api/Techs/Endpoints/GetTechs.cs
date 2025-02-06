using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Techs.Endpoints;

public static class GetTechs
{
    public static async Task<Ok<IReadOnlyList<TechResponseModel>>> GetAllTechsAsync(
        [FromServices] IDocumentSession session,
        [FromServices] IHttpContextAccessor _httpContextAccessor
    )
    {
        var items = await session
            .Query<TechEntity>()
            .ProjectToModel()
            .ToListAsync();

        return TypedResults.Ok(items);
    }

    public static async Task<Results<Ok<TechResponseModel>, NotFound>> GetSingleTech(
        [FromRoute] Guid id,
        [FromServices] IDocumentSession session,
        [FromServices] IHttpContextAccessor _httpContextAccessor
    )
    {
        var response = await session
            .Query<TechEntity>()
            .Where(x => x.Id == id)
            .ProjectToModel()
            .SingleOrDefaultAsync();

        return response switch
        {
            null => TypedResults.NotFound(),
            _ => TypedResults.Ok(response)
        };
    }
}