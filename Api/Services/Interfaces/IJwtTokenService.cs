using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public interface IJwtTokenService{
    JwtSecurityToken GetJwtToken(List<Claim> claims);
    bool Revoke(string token);
}