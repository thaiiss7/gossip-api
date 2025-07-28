namespace Gossip.UseCases.DeletePost;

public class DeletePostUseCase
{
    public async Task<Result<DeletePostResponse>> Do(DeletePostPayload payload)
    {
        return Result<DeletePostResponse>.Success(null);
    }
}