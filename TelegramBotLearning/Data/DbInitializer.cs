using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TelegramBotLearning.Models;

namespace TelegramBotLearning.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("Начало инициализации базы данных");

            // Создаем роли, если они не существуют
            string[] roleNames = { "Instructor", "Student" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    logger.LogInformation("Создание роли: {RoleName}", roleName);
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Создаем тестового преподавателя
            var instructorEmail = "instructor@example.com";
            var instructor = await userManager.FindByEmailAsync(instructorEmail);
            if (instructor == null)
            {
                logger.LogInformation("Создание преподавателя: {Email}", instructorEmail);
                instructor = new ApplicationUser
                {
                    UserName = "instructor@example.com",
                    Email = "instructor@example.com",
                    EmailConfirmed = true,
                    FirstName = "Иван",
                    LastName = "Иванов"
                };
                var result = await userManager.CreateAsync(instructor, "Instructor123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(instructor, "Instructor");
                    logger.LogInformation("Преподаватель успешно создан");
                }
                else
                {
                    logger.LogError("Ошибка при создании преподавателя: {Errors}", 
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            // Создаем тестового студента
            var studentEmail = "student@example.com";
            var student = await userManager.FindByEmailAsync(studentEmail);
            if (student == null)
            {
                logger.LogInformation("Создание студента: {Email}", studentEmail);
                student = new ApplicationUser
                {
                    UserName = studentEmail,
                    Email = studentEmail,
                    EmailConfirmed = true,
                    FirstName = "Петр",
                    LastName = "Петров"
                };
                var result = await userManager.CreateAsync(student, "Student123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(student, "Student");
                    logger.LogInformation("Студент успешно создан");
                }
                else
                {
                    logger.LogError("Ошибка при создании студента: {Errors}", 
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            // Добавляем тестовые курсы, если их нет
            if (!context.Courses.Any())
            {
                logger.LogInformation("Добавление тестовых курсов");
                var courses = new List<Course>
                {
                    new Course
                    {
                        Title = "Основы создания Telegram ботов на Python",
                        Description = "Изучите базовые принципы создания Telegram ботов с использованием Python и библиотеки python-telegram-bot.",
                        InstructorId = instructor.Id,
                        ImageUrl = "/images/course1.png",
                        Price = 0,
                        CreatedAt = DateTime.UtcNow
                    },
                    new Course
                    {
                        Title = "Продвинутые техники разработки Telegram ботов",
                        Description = "Углубленный курс по созданию сложных Telegram ботов с использованием современных практик и паттернов.",
                        InstructorId = instructor.Id,
                        ImageUrl = "/images/course2.png",
                        Price = 0,
                        CreatedAt = DateTime.UtcNow
                    }
                };

                try
                {
                    logger.LogInformation("ID преподавателя: {InstructorId}", instructor.Id);
                    context.Courses.AddRange(courses);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Тестовые курсы успешно добавлены");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Ошибка при добавлении тестовых курсов: {Message}", ex.Message);
                    throw; // Пробрасываем исключение дальше для обработки на уровне приложения
                }
            }
            else
            {
                logger.LogInformation("Курсы уже существуют в базе данных");
                var existingCourses = context.Courses.ToList();
                foreach (var course in existingCourses)
                {
                    logger.LogInformation("Существующий курс: {Title}, ID: {Id}", course.Title, course.Id);
                }
            }

            logger.LogInformation("Инициализация базы данных завершена");
        }
    }
} 