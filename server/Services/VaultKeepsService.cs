


namespace Keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _VaultKeepsRepository;
    private readonly VaultsService _VaultsService;
    private readonly KeepsService _KeepsService;
    private readonly KeepsRepository _KeepsRepository;
    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService, KeepsService keepsService, KeepsRepository keepsRepository)
    {
        _VaultKeepsRepository = vaultKeepsRepository;
        _VaultsService = vaultsService;
        _KeepsService = keepsService;
        _KeepsRepository = keepsRepository;
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
            int keepId = vaultKeep.KeepId;
            IncrementKeptCount(keepId);
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
        else
        {
            int keepId = fetchedVaultKeep.KeepId;
            DecreaseKeptCount(keepId);
            _VaultKeepsRepository.DeleteVaultKeep(vaultKeepId);
            return "The vaultKeep has been removed from the database.";
        }
    }

    internal Keep IncrementKeptCount(int keepId)
    {
        Keep fetchedKeep = _KeepsService.GetKeepById(keepId);
        fetchedKeep.Kept++;
        _KeepsRepository.UpdateKeptCount(fetchedKeep);
        return fetchedKeep;
    }
    internal void DecreaseKeptCount(int keepId)
    {
        Keep fetchedKeep = _KeepsService.GetKeepById(keepId);
        fetchedKeep.Kept--;
        _KeepsRepository.UpdateKeptCount(fetchedKeep);
        return;
    }
}