namespace Gossip.UseCases.PublishPost;

public class PublishPostUseCase
{
    public async Task<Result<PublishPostResponse>> Do(PublishPostPayload payload)
    {
        return Result<PublishPostResponse>.Success(null);
    }
}