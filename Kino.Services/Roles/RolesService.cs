using Kino.Core.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Kino.Services.Roles
{
    public class RolesService(RoleManager<IdentityRole> roleManager) : IRolesService
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task<IdentityRole?> AddRole(RoleViewModel roleViewModel)
        {
            var role = new IdentityRole(roleViewModel.RoleName);
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
                return role;

            return null;
        }
    }
}
