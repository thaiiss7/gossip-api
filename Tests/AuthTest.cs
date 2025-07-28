using Gossip.Models;
using Gossip.Services.JWT;
using Gossip.Services.Password;
using Gossip.Services.Profiles;
using Gossip.UseCases.Login;
using Moq;
using Xunit;

namespace Gossip.Tests;

public class AuthTest
{
    [Theory]
    [InlineData("minhasenhaA", "minhasenhaA", true)]
    [InlineData("1232132132131", "asdafafafd", false)]
    [InlineData("", "a", false)]
    [InlineData("acomplexpasswordhaha123", "acomplexpasswordhaha123", true)]
    [InlineData(null, "acomplexpasswordhaha123", false)]
    public void TestPasswordHash(string senha1, string senha2, bool result)
    {
        var hasher = new PBKDF2PasswordService();
        var hash = hasher.Hash(senha1);
        var ok = hasher.Compare(senha2, hash);
        Assert.Equal(result, ok);
    }

    [Fact]
    public async Task TestLogin()
    {
        var profileService = new Mock<IProfilesService>();
        profileService.Setup(s => s.FindByLogin("trevis"))
            .ReturnsAsync(new Profile {
                Username = "trevis",
                Password = "hash_da_senha"
            });
        var passworService = new Mock<IPasswordService>();
        passworService.Setup(s => s.Compare("cristianS2", "hash_da_senha"))
            .Returns(true);
        var jwtSevice = new Mock<IJWTService>();

        var useCase = new LoginUseCase(
            profileService.Object,
            passworService.Object,
            jwtSevice.Object
        );

        var result = await useCase.Do(new LoginPayload("trevis", "cristianS2"));
        Assert.True(result.IsSuccess);
    }
}