namespace Keepr.Controllers;


[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
    private readonly KeepsService _KeepsService;
    private readonly Auth0Provider _auth0;
    public KeepsController(KeepsService keepsService, Auth0Provider auth0)
    {
        _KeepsService = keepsService;
        _auth0 = auth0;
    }

    // Get All Keeps
    [HttpGet]
    public ActionResult<List<Keep>> GetAllKeeps()
    {
        try
        {
            List<Keep> keepList = _KeepsService.GetAllKeeps();
            return Ok(keepList);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    // Get Keep By Id
    [HttpGet("{keepId}")]
    public ActionResult<Keep> GetKeepById(int keepId)
    {
        try
        {
            Keep keep = _KeepsService.GetKeepById(keepId);
            return Ok(keep);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    // Create Keep
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keep keep = _KeepsService.CreateKeep(keepData);
            return Ok(keep);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    // Edit Keep
    [Authorize]
    [HttpPut("{keepId}")]
    public async Task<ActionResult<Keep>> EditKeep(int keepId, [FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            string userId = userInfo.Id;
            Keep editedKeep = _KeepsService.EditKeep(keepId, keepData, userId);
            return Ok(editedKeep);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }


    [Authorize]
    [HttpDelete("{keepId}")]
    public async Task<ActionResult<Keep>> DeleteKeep(int keepId)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            string userId = userInfo.Id;
            string message = _KeepsService.DeleteKeep(keepId, userId);
            return Ok(message);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}
