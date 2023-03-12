using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _config;
    public JwtTokenService(IConfiguration config)
    {
        _config = config;
    }
    public JwtSecurityToken GetJwtToken(List<Claim> claims)
    {
        var secretKey = _config["Jwt:SecretKey"] ?? throw new NullReferenceException("Could not find secret key");
        var expiry = string.IsNullOrEmpty(_config["Jwt:ExpiryInMinutes"]) ? 30 : double.Parse(_config["Jwt:ExpiryInMinutes"]!);
        var tokenIssuer = _config["Jwt:Issuer"] ?? "";
        var audience = _config["Jwt:Audience"] ?? "";

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var token = new JwtSecurityToken(
            issuer: tokenIssuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiry),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    public bool Revoke(string token)
    {
        throw new NotImplementedException();
    }
}