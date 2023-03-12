using System.Security.Claims;

public interface IClaimsService{
    Task<List<Claim>> GetUserClaimsAsync(ApplicationUser user);
}