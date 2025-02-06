using Riok.Mapperly.Abstractions;

namespace SoftwareCatalog.Api.Techs;

[Mapper]
public static partial class TechMappers
{
    public static partial IQueryable<TechResponseModel> ProjectToModel(this IQueryable<TechEntity> entity);

    [MapperIgnoreSource(nameof(TechEntity.Sub))]
    public static partial TechResponseModel MapToModel(this TechEntity entity);
}
