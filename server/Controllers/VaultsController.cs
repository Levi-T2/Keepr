using Microsoft.AspNetCore.Http.HttpResults;

namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
    private readonly VaultsService _VaultsService;
    private readonly Auth0Provider _auth0;
    private readonly VaultKeepsService _VaultKeepsService;
    public VaultsController(VaultsService vaultsService, Auth0Provider auth0, VaultKeepsService vaultKeepsService)
    {
        _VaultsService = vaultsService;
        _auth0 = auth0;
        _VaultKeepsService = vaultKeepsService;
    }
    // Get Vault By Id
    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _VaultsService.GetVaultById(vaultId, userInfo?.Id);
            return Ok(vault);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
    // Create Vault
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            Vault vault = _VaultsService.CreateVault(vaultData);
            return Ok(vault);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
    // Edit Vault
    [Authorize]
    [HttpPut("{vaultId}")]
    public async Task<ActionResult<Vault>> EditVault(int vaultId, [FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            string userId = userInfo.Id;
            Vault editedVault = _VaultsService.EditVault(vaultId, userId, vaultData);
            return Ok(editedVault);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
    // Delete Vault
    [Authorize]
    [HttpDelete("{vaultId}")]
    public async Task<ActionResult<Vault>> DeleteVault(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            string userId = userInfo.Id;
            string message = _VaultsService.DeleteVault(vaultId, userId);
            return Ok(message);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
    // Get Keeps In Vault
    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<VKResponse>>> GetKeepsInVault(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            List<VKResponse> keepsInVault = _VaultKeepsService.GetKeepsInVault(vaultId, userInfo?.Id);
            return Ok(keepsInVault);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

}