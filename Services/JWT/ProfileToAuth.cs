namespace Gossip.Services.JWT;

public record ProfileToAuth(
    Guid ProfileId,
    string Username
);