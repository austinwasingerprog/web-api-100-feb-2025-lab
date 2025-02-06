namespace SoftwareCatalog.Api.Techs;

public class TechEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sub { get; set; } = string.Empty;
    public string? Email { get; set; } = null;
    public string? PhoneNumber { get; set; } = null;
}
