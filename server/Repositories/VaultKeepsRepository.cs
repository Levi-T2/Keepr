

namespace Keepr.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }
    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        string sql = @"SELECT * FROM vaultKeeps WHERE id = @vaultKeepId;";
        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
        return vaultKeep;
    }
    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
        INSERT INTO vaultKeeps
        (creatorId, vaultId, keepId)
        VALUES (@CreatorId, @VaultId, @KeepId);
        
        SELECT * FROM vaultKeeps WHERE id = LAST_INSERT_ID();";

        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).FirstOrDefault();
        return vaultKeep;
    }
    internal List<VKResponse> GetKeepsInVault(int vaultId)
    {
        string sql = @"
        SELECT vks.*, kps.*, acc.* 
        FROM vaultKeeps vks
        JOIN keeps kps ON kps.id = vks.keepId
        JOIN accounts acc ON kps.creatorId = acc.id
        WHERE vks.vaultId = @vaultId;";

        List<VKResponse> keepsInVault = _db.Query<VaultKeep, VKResponse, Profile, VKResponse>(sql, (vaultKeep, vkResponse, profile) =>
        {
            vkResponse.VaultKeepId = vaultKeep.Id;
            vkResponse.AccountId = vaultKeep.CreatorId;
            vkResponse.Creator = profile;
            return vkResponse;
        }, new { vaultId }).ToList();
        return keepsInVault;
    }
    internal void DeleteVaultKeep(int vaultKeepId)
    {
        string sql = @"DELETE FROM vaultKeeps WHERE id = @vaultKeepId LIMIT 1;";
        _db.Execute(sql, new { vaultKeepId });
    }
}