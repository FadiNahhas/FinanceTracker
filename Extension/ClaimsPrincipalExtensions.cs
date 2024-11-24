using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Extension;

public static class ClaimsPrincipalExtensions
{
    public static async Task<string> GetNameAsync(this ClaimsPrincipal user, UserManager<ApplicationUser> userManager)
    {
        var appUser = await userManager.GetUserAsync(user);
        
        return appUser == null ? string.Empty : appUser.Name;
    }
}