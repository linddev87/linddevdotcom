using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

public class UtilityService : IUtilityService
{
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public UtilityService(ApplicationDbContext context, RoleManager<ApplicationRole> roleManager)
    {
        _context = context;
        _roleManager = roleManager;
    }
}