using Microsoft.AspNetCore.Identity;

namespace TelegramBotLearning.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
} 