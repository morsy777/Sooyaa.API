namespace LanguageApp.Dashboard.Interface;

public interface IAdminService
{
    Task<bool> AddRoleAsync(string roleName);
    Task<bool> PromoteToAdminAsync(string userId);
    Task<bool> RemoveAdminAsync(string userId);
    Task<List<UserProfileResponse>> GetAllAdminsAsync();
}
