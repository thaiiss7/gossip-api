namespace Gossip.Services.JWT;

public interface IJWTService
{
    string CreateToken(ProfileToAuth data);
}