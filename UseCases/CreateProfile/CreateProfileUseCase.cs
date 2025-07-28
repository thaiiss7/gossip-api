namespace Gossip.UseCases;

// public class CreateProfileUseCase(
//     IProfilesService profilesService,
//     IPasswordService passwordService
// )
// {
//     public async Task<Result<CreateProfileResponse>> Do(CreateProfilePayload payload)
//     {
//         var profile = new Profile {
//             Bio = payload.Bio,
//             Email = payload.Email,
//             Username = payload.Username,
//             Password = passwordService.Hash(payload.Password)
//         };

//         await profilesService.Create(profile);

//         return Result<CreateProfileResponse>.Success(new());
//     }
// }