namespace Gossip.UseCases.GetProfileData;

public record GetProfileDataPost(
    string Title,
    string Text,
    DateTime Date
);

public record GetProfileDataResponse(
    string Username,
    string Bio,
    ICollection<GetProfileDataPost> Posts
);