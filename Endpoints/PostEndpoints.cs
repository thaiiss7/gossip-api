using System.Security.Claims;
using Gossip.UseCases.DeletePost;
using Gossip.UseCases.PublishPost;
using Microsoft.AspNetCore.Mvc;

namespace Gossip.Endpoints;

public static class PostEndpoints
{
    public static void ConfigurePostEndpoints(this WebApplication app)
    {
        app.MapGet("post/{id}", (string id) =>
        {

        });

        app.MapPost("post", async (
            [FromBody]PublishPostPayload payload,
            [FromServices]PublishPostUseCase useCase) =>
        {
            var result = await useCase.Do(payload);
            
            return (result.IsSuccess, result.Reason) switch
            {
                (false, "Post not found") => Results.NotFound(),
                (false, _) => Results.BadRequest(),
                (true, _) => Results.Ok(result.Data)
            };
        });

        app.MapDelete("post/{id}", async (string id, 
            HttpContext http,
            [FromServices]DeletePostUseCase useCase) =>
        {
            var claim = http.User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = Guid.Parse(claim.Value);
            var postId = Guid.Parse(id);
            var payload = new DeletePostPayload(postId, userId);
            var result = await useCase.Do(payload);

            return (result.IsSuccess, result.Reason) switch
            {
                (false, "Post not found") => Results.NotFound(),
                (false, _) => Results.BadRequest(),
                (true, _) => Results.Ok()
            };
        }).RequireAuthorization();
    }
}