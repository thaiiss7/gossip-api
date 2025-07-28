using Gossip.Models;

namespace Gossip.Services.Profiles;

public interface IProfilesService
{
    Task<Profile> FindByLogin(string login);
    Task<Guid> Create(Profile profile);
}