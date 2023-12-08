namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
    private readonly VaultKeepsService _VaultKeepsService;
    private readonly Auth0Provider _auth0;

    public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0)
    {
        _VaultKeepsService = vaultKeepsService;
        _auth0 = auth0;
    }

    // Create VaultKeep
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            vaultKeepData.CreatorId = userInfo.Id;
            VaultKeep vaultKeep = _VaultKeepsService.CreateVaultKeep(vaultKeepData);
            return Ok(vaultKeep);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [Authorize]
    [HttpDelete("{vaultKeepId}")]
    public async Task<ActionResult<VaultKeep>> DeleteVaultKeep(int vaultKeepId)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            string userId = userInfo.Id;
            string message = _VaultKeepsService.DeleteVaultKeep(vaultKeepId, userId);
            return Ok(vaultKeepId);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

}