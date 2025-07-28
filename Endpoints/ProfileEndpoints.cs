using Gossip.UseCases;
using Gossip.UseCases.CreateProfile;
using Gossip.UseCases.GetProfileData;
using Microsoft.AspNetCore.Mvc;

namespace Gossip.Endpoints;

public static class ProfileEndpoints
{
    public static void ConfigureProfileEndpoints(this WebApplication app)
    {
        app.MapGet("profile/{username}", async (
            string username,
            [FromServices]GetProfileDataUseCase useCase) =>
        {
            var payload = new GetProfileDataPayload(username);
            var result = await useCase.Do(payload);

            return (result.IsSuccess, result.Reason) switch
            {
                (false, "User not found")  => Results.NotFound(),
                (false, _) => Results.BadRequest(),
                (true, _) => Results.Ok(result.Data)
            };

        });

        app.MapPost("profile", async (
            [FromBody]CreateProfilePayload payload, 
            [FromServices]CreateProfileUseCase useCase) =>
        {
            var result = await useCase.Do(payload);
            if (result.IsSuccess)
                return Results.Created();
            
            return Results.BadRequest(result.Reason);
        });
    }
}