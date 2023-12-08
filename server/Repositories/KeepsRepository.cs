namespace Keepr.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal List<Keep> GetAllKeeps()
    {
        string sql = @"
        SELECT kp.*, acc.* 
        FROM keeps kp 
        JOIN accounts acc ON kp.creatorId = acc.id;";

        List<Keep> keepList = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }).ToList();
        return keepList;
    }
    internal Keep GetKeepById(int keepId)
    {
        string sql = @"
        SELECT kp.*, acc.* 
        FROM keeps kp 
        JOIN accounts acc ON kp.creatorId = acc.id
        WHERE kp.id = @keepId;";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { keepId }).FirstOrDefault();
        return keep;
    }
    internal Keep CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO keeps (name, description, img, creatorId)
        VALUES (@Name, @Description, @Img, @CreatorId);
        
        SELECT kp.*, acc.* 
        FROM keeps kp 
        JOIN accounts acc ON kp.creatorId = acc.id
        WHERE kp.id = LAST_INSERT_ID();";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }
        , keepData).FirstOrDefault();
        return keep;
    }
    internal void EditKeep(Keep ogKeep)
    {
        string sql = @"
        UPDATE keeps
        SET name = @Name, description = @Description
        WHERE id = @Id;";
        _db.Execute(sql, ogKeep);
    }
    internal void DeleteKeep(int keepId)
    {
        string sql = @"DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
        _db.Execute(sql, new { keepId });
    }
}