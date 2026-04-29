using Microsoft.AspNetCore.Identity;

namespace SampleMvcApp.Models;

public class ApplicationUser : IdentityUser
{
    public string DisplayName { get; set; } = string.Empty;
}
