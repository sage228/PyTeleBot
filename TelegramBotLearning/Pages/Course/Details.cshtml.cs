using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TelegramBotLearning.Data;
using TelegramBotLearning.Models;
using Microsoft.AspNetCore.Identity;

namespace TelegramBotLearning.Pages.Course
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Models.Course Course { get; set; }
        public bool IsInstructor { get; set; }
        public bool IsStudent { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Course = await _context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Lessons)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Course == null)
            {
                return NotFound();
            }

            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    IsInstructor = await _userManager.IsInRoleAsync(user, "Instructor");
                    IsStudent = await _userManager.IsInRoleAsync(user, "Student");
                }
            }

            return Page();
        }
    }
} 