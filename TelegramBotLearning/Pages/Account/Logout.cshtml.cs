using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TelegramBotLearning.Models;

namespace TelegramBotLearning.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            // Очищаем сессию
            HttpContext.Session.Clear();
            
            // Выполняем выход
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Пользователь вышел из системы.");
            
            return RedirectToPage("/Index");
        }
    }
} 