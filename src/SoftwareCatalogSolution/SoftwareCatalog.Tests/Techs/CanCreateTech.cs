using Alba;
using Alba.Security;
using SoftwareCatalog.Api.Techs;
using System.Security.Claims;

namespace SoftwareCatalog.Tests.Techs;

[Trait("Category", "System")]
[Trait("Feature", "Techs")]
public class CanCreateTechs
{
    [Fact]
    public async Task Gets201WhenCreatingTech()
    {
        // Arrange
        var fakeIdentity = new AuthenticationStub().WithName("TechBro")
            .With(new Claim(ClaimTypes.Role, "manager"))
            .With(new Claim(ClaimTypes.Role, "software-center"));
        var host = await AlbaHost.For<Program>(fakeIdentity);

        var requestModel = new TechCreateModel
        {
            Name = "Mr. Anonymous",
            Email = "anonymous@techbro.net"
        };

        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(requestModel).ToUrl("/techs");
            api.StatusCodeShouldBe(201);
        });
        var postBody = postResponse.ReadAsJson<TechResponseModel>();
        var location = postResponse.Context.Response.Headers.Location.ToString();

        Assert.NotNull(postBody);

        var getResponse = await host.Scenario(api =>
        {
            api.Get.Url(location);
        });

        var getBody = getResponse.ReadAsJson<TechResponseModel>();
        Assert.NotNull(getBody);
    }
}
