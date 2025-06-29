using Kino.Core.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Kino.Services.Roles
{
    public interface IRolesService
    {
        Task<IdentityRole?> AddRole(RoleViewModel roleViewModel);
    }
}
