using Gossip.Models;

namespace Gossip.Services;

public interface IProfilesService
{
    Task<Profile> FindByLogin(string login);
    Task<Guid> Create(Profile profile);
}