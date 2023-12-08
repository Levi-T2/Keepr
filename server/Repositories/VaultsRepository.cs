

using Microsoft.Extensions.Primitives;

namespace Keepr.Repositories;

public class VaultsRepository
{
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }
    internal Vault GetVaultById(int vaultId)
    {
        string sql = @"
        SELECT vlt.*, acc.*
        FROM vaults vlt
        JOIN accounts acc ON vlt.creatorId = acc.id
        WHERE vlt.id = @vaultId;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { vaultId }).FirstOrDefault();
        return vault;
    }
    internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
        INSERT INTO vaults
        (name, description, img, isPrivate ,creatorId)
        VALUES (@Name, @Description, @Img, @IsPrivate, @CreatorId);
        
        SELECT vlt.*, acc.* 
        FROM vaults vlt
        JOIN accounts acc ON vlt.creatorId = acc.id
        WHERE vlt.id = LAST_INSERT_ID();";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, vaultData).FirstOrDefault();
        return vault;
    }
    internal void EditVault(Vault ogVault)
    {
        string sql = @"
        UPDATE vaults
        SET name = @Name, description = @Description, isPrivate = @IsPrivate
        WHERE id = @Id;";
        _db.Execute(sql, ogVault);
    }
    internal void DeleteVault(int vaultId)
    {
        string sql = @"DELETE FROM vaults WHERE id = @vaultId LIMIT 1;";
        _db.Execute(sql, new { vaultId });
    }
}