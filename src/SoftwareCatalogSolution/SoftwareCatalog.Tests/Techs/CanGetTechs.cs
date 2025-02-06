using Alba;
using Alba.Security;
using System.Security.Claims;

namespace SoftwareCatalog.Tests.Techs;

[Trait("Category", "System")]
[Trait("Feature", "Techs")]
public class CanGetTechs
{
    [Fact]
    public async Task GetsA200WhenGettingAllTechs()
    {
        // Arrange
        var fakeIdentity = new AuthenticationStub().WithName("TechBro")
            .With(new Claim(ClaimTypes.Role, "manager"))
            .With(new Claim(ClaimTypes.Role, "software-center"));
        var host = await AlbaHost.For<Program>(fakeIdentity);

        // Act
        var response = await host.Scenario(api =>
        {
            api.Get.Url("/techs");

            // Assert
            api.StatusCodeShouldBeOk();
        });
    }
}
