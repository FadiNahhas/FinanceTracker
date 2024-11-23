using Microsoft.AspNetCore.Identity;

namespace PersonalFinanceTracker.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public float Budget { get; set; }
}