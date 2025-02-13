using Microsoft.EntityFrameworkCore;
using QuizApp.Entities;

namespace QuizApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; } 
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Choice> Choices { get; set; }
    }
}
