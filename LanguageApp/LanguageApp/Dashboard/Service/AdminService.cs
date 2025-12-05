using LanguageApp.Dashboard.Interface;
using Microsoft.EntityFrameworkCore;

namespace LanguageApp.Dashboard.Service;

public class AdminService : IAdminService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public AdminService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    // Super Admin can add roles
    public async Task<bool> AddRoleAsync(string roleName)
    {
        if (await _roleManager.RoleExistsAsync(roleName))
            return false;

        var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
        return result.Succeeded;
    }

    // Upgrade user to Admin
    public async Task<bool> PromoteToAdminAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        if (!await _roleManager.RoleExistsAsync("Admin"))
            await _roleManager.CreateAsync(new IdentityRole("Admin"));

        var result = await _userManager.AddToRoleAsync(user, "Admin");
        return result.Succeeded;
    }

    // Remove Admin role from user
    public async Task<bool> RemoveAdminAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        if (!await _userManager.IsInRoleAsync(user, "Admin"))
            return false;

        var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
        return result.Succeeded;
    }

    public async Task<List<UserProfileResponse>> GetAllAdminsAsync()
    {
        var adminRole = await _roleManager.FindByNameAsync("Admin");

        if (adminRole == null)
            return new List<UserProfileResponse>();

        var adminUserIds = await _context.UserRoles
            .Where(ur => ur.RoleId == adminRole.Id)
            .Select(ur => ur.UserId)
            .ToListAsync();

        var adminUsers = await _userManager.Users
            .Where(u => adminUserIds.Contains(u.Id))
            .ToListAsync();

        return adminUsers.Adapt<List<UserProfileResponse>>();
    }

}

