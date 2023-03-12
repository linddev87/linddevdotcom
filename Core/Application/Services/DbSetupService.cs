using Microsoft.AspNetCore.Identity;
using Core.Application.Interfaces;

namespace Core.Application.Services{
    public class DbSetupService : IDbSetupService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public DbSetupService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task Init()
        {
            try{
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new ApplicationRole("Admin"));
    
            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new ApplicationRole("User"));

            if (!await _roleManager.RoleExistsAsync("Unassigned"))
                await _roleManager.CreateAsync(new ApplicationRole("Unassigned"));
            }
            catch(Exception e){
                throw;
            }
        }
    }
}
