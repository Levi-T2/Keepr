namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly ProfilesService _ProfilesService;
    public ProfilesController(ProfilesService profilesService)
    {
        _ProfilesService = profilesService;
    }

    // Get Profile By Id
    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetProfileById(string profileId)
    {
        try
        {
            Account profile = _ProfilesService.GetProfileById(profileId);
            return Ok(profile);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    // Get Keeps for profile
    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetKeepsForProfile(string profileId)
    {
        try
        {
            List<Keep> keepList = _ProfilesService.GetKeepsForProfile(profileId);
            return Ok(keepList);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    // Get Vaults for profile
    [HttpGet("{profileId}/vaults")]
    public ActionResult<List<Vault>> GetVaultsForProfile(string profileId)
    {
        try
        {
            List<Vault> vaultList = _ProfilesService.GetVaultsForProfile(profileId);
            return Ok(vaultList);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

}