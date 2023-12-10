


namespace Keepr.Repositories;

public class ProfilesRepository
{
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
        _db = db;
    }


    internal Account GetProfileById(string profileId)
    {
        string sql = @"SELECT * FROM accounts WHERE id = @profileId;";
        Account profile = _db.QueryFirstOrDefault<Account>(sql, new { profileId });
        return profile;
    }
    internal List<Keep> GetKeepsForProfile(string profileId)
    {
        string sql = @"SELECT * FROM keeps
        WHERE creatorId = @profileId;";

        List<Keep> keepList = _db.Query<Keep>(sql, new { profileId }).ToList();
        return keepList;
    }
    internal List<Vault> GetVaultsForProfile(string profileId)
    {
        string sql = @"SELECT * FROM vaults
        WHERE creatorId = @profileId AND isPrivate = false;";

        List<Vault> vaultList = _db.Query<Vault>(sql, new { profileId }).ToList();
        return vaultList;
    }
}