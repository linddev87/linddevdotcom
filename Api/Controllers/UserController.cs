using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IClaimsService _claimsService;
    private readonly IJwtTokenService _jwtTokenService;

    public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IClaimsService claimsService, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _claimsService = claimsService;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto req){
        var user = await _userManager.FindByEmailAsync(req.Email);
        
        if(user == null || await _userManager.CheckPasswordAsync(user, req.Password) == false){
            return Unauthorized(new UserLoginResponseDto(){
                Succeeded = false,
                Message = "Login failed"
            });
        }

        var claims = await _claimsService.GetUserClaimsAsync(user);
        var token = _jwtTokenService.GetJwtToken(claims);

        return Ok(new UserLoginResponseDto(){
            Succeeded = true,
            Token = new TokenDto(){
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            }
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto req)
    {
        IdentityResult result;

        ApplicationUser newUser = new()
        {
            Email = req.Email,
            UserName = req.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        result = await _userManager.CreateAsync(newUser, req.Password);

        if (!result.Succeeded)
            return Conflict(new UserRegistrationResponseDto
            {
                Succeeded = result.Succeeded,
                Error = "Registration failed"
            });

        result = await _userManager.AddToRoleAsync(newUser, "Unassigned");

        return CreatedAtAction(nameof(Register), new UserRegistrationResponseDto { Succeeded = true });
    }
}