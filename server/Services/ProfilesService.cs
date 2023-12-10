
namespace Keepr.Services;

public class ProfilesService
{
    private readonly ProfilesRepository _ProfilesRepository;
    public ProfilesService(ProfilesRepository profilesRepository)
    {
        _ProfilesRepository = profilesRepository;
    }


    internal Account GetProfileById(string profileId)
    {
        Account profile = _ProfilesRepository.GetProfileById(profileId);
        if (profile == null)
        {
            throw new Exception($"The Id supplied was invalid or null: {profileId}");
        }
        return profile;
    }
    internal List<Keep> GetKeepsForProfile(string profileId)
    {
        List<Keep> keepList = _ProfilesRepository.GetKeepsForProfile(profileId);
        return keepList;
    }

    internal List<Vault> GetVaultsForProfile(string profileId)
    {
        List<Vault> vaultList = _ProfilesRepository.GetVaultsForProfile(profileId);
        return vaultList;
    }
}