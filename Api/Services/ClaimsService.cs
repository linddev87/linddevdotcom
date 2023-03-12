using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

public class ClaimsService : IClaimsService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ClaimsService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<List<Claim>> GetUserClaimsAsync(ApplicationUser user)
    {
        var claims = new List<Claim>(){
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var roles = await _userManager.GetRolesAsync(user);

        foreach(var role in roles){
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }
}