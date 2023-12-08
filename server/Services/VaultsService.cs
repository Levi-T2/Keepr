

namespace Keepr.Services;

public class VaultsService
{
    private readonly VaultsRepository _VaultsRepository;
    public VaultsService(VaultsRepository vaultsRepository)
    {
        _VaultsRepository = vaultsRepository;
    }
    internal Vault GetVaultById(int vaultId, string userId)
    {
        Vault vault = _VaultsRepository.GetVaultById(vaultId);
        if (vault == null)
        {
            throw new Exception($"{vaultId} (vaultId) || is null or invalid.");
        }
        else
        {
            if (vault.IsPrivate == true && vault.CreatorId != userId)
            {
                throw new Exception("This vault is private!");
            }
            else
            {
                return vault;
            }
        }
    }
    internal Vault CreateVault(Vault vaultData)
    {
        Vault vault = _VaultsRepository.CreateVault(vaultData);
        return vault;
    }
    internal Vault EditVault(int vaultId, string userId, Vault vaultData)
    {
        Vault ogVault = GetVaultById(vaultId, userId);
        if (ogVault.CreatorId != userId)
        {
            throw new Exception("You do not have authorization to edit another users vault.");
        }
        else
        {
            ogVault.Name = vaultData.Name ?? ogVault.Name;
            ogVault.Description = vaultData.Description ?? ogVault.Description;
            ogVault.IsPrivate = vaultData.IsPrivate ?? ogVault.IsPrivate;

            _VaultsRepository.EditVault(ogVault);
            return ogVault;
        }
    }
    internal string DeleteVault(int vaultId, string userId)
    {
        Vault fetchedVault = GetVaultById(vaultId, userId);
        if (fetchedVault.CreatorId != userId)
        {
            throw new Exception("You do not have the authorization to delete another users vault.");
        }
        else
        {
            _VaultsRepository.DeleteVault(vaultId);
            return $"{fetchedVault.Name} has been deleted from the dataBase";
        }
    }
}