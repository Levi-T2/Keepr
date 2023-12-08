


namespace Keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _VaultKeepsRepository;
    private readonly VaultsService _VaultsService;
    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService)
    {
        _VaultKeepsRepository = vaultKeepsRepository;
        _VaultsService = vaultsService;
    }
    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        Vault fetchedVault = _VaultsService.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
        if (fetchedVault.CreatorId != vaultKeepData.CreatorId)
        {
            throw new Exception("You can't put a keep in a vault that isn't yours.");
        }
        else
        {
            VaultKeep vaultKeep = _VaultKeepsRepository.CreateVaultKeep(vaultKeepData);
            return vaultKeep;
        }
    }

    internal List<VKResponse> GetKeepsInVault(int vaultId, string userId)
    {
        Vault fetchedVault = _VaultsService.GetVaultById(vaultId, userId);
        List<VKResponse> keepsInVault = _VaultKeepsRepository.GetKeepsInVault(vaultId);
        return keepsInVault;
    }
    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        VaultKeep vaultKeep = _VaultKeepsRepository.GetVaultKeepById(vaultKeepId);
        if (vaultKeep == null)
        {
            throw new Exception("The id supplied was invalid.");
        }
        return vaultKeep;
    }
    internal string DeleteVaultKeep(int vaultKeepId, string userId)
    {
        VaultKeep fetchedVaultKeep = GetVaultKeepById(vaultKeepId);
        if (fetchedVaultKeep.CreatorId != userId)
        {
            throw new Exception("You lack the authorization to delete this VaultKeep.");
        }
        _VaultKeepsRepository.DeleteVaultKeep(vaultKeepId);
        return "The vaultKeep has been removed from the database.";

    }
}