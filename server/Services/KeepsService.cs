


namespace Keepr.Services;

public class KeepsService
{
    private readonly KeepsRepository _KeepsRepository;
    public KeepsService(KeepsRepository keepsRepository)
    {
        _KeepsRepository = keepsRepository;
    }

    internal List<Keep> GetAllKeeps()
    {
        List<Keep> keepList = _KeepsRepository.GetAllKeeps();
        return keepList;
    }
    internal Keep GetKeepById(int keepId)
    {
        Keep keep = _KeepsRepository.GetKeepById(keepId);
        if (keep == null)
        {
            throw new Exception($"{keepId} (keepId) || is invalid or null.");
        }
        else
        {
            return keep;
        }
    }
    internal Keep CreateKeep(Keep keepData)
    {
        Keep keep = _KeepsRepository.CreateKeep(keepData);
        return keep;
    }
    internal Keep EditKeep(int keepId, Keep keepData, string userId)
    {
        Keep ogKeep = GetKeepById(keepId);
        if (ogKeep.CreatorId != userId)
        {
            throw new Exception("You are not authorized to edit another users keep.");
        }

        ogKeep.Name = keepData.Name ?? ogKeep.Name;
        ogKeep.Description = keepData.Description ?? ogKeep.Description;

        _KeepsRepository.EditKeep(ogKeep);
        return ogKeep;
    }
    internal string DeleteKeep(int keepId, string userId)
    {
        Keep fetchedKeep = GetKeepById(keepId);
        if (fetchedKeep.CreatorId != userId)
        {
            throw new Exception("You are not authorized to delete another users keep.");
        }
        else
        {
            _KeepsRepository.DeleteKeep(keepId);
            return $"{fetchedKeep.Name} has been deleted from the database.";
        }
    }
}