namespace Gossip.UseCases.GetProfileData;

public class GetProfileDataUseCase
{
    public async Task<Result<GetProfileDataResponse>> Do(GetProfileDataPayload payload)
    {
        return Result<GetProfileDataResponse>.Success(null);
    }
}