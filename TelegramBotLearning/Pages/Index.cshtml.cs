using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TelegramBotLearning.Data;
using TelegramBotLearning.Models;
using Microsoft.AspNetCore.Identity;

namespace TelegramBotLearning.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(
            ApplicationDbContext context, 
            ILogger<IndexModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public IList<Models.Course> Courses { get; set; } = new List<Models.Course>();
        public bool IsInstructor { get; set; }
        public bool IsStudent { get; set; }

        public async Task OnGetAsync()
        {
            Courses = await _context.Courses
                .Include(c => c.Instructor)
                .ToListAsync();

            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    IsInstructor = await _userManager.IsInRoleAsync(user, "Instructor");
                    IsStudent = await _userManager.IsInRoleAsync(user, "Student");
                }
            }
        }
    }
}
