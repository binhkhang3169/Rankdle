using Microsoft.EntityFrameworkCore;

namespace APIRanked.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<TypeQuiz> TypeQuizzes { get; set; }
        public DbSet<Daily> Dailies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Table configurations
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<TypeQuiz>().ToTable("TypeQuizzes");
            modelBuilder.Entity<Daily>().ToTable("Dailies");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Quiz>().ToTable("Quizzes");
            modelBuilder.Entity<UserAnswer>().ToTable("UserAnswers");

            // Primary keys
            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
            modelBuilder.Entity<TypeQuiz>().HasKey(t => t.TypeId);
            modelBuilder.Entity<Daily>().HasKey(d => d.DailyId);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Quiz>().HasKey(q => q.QuizId);
            modelBuilder.Entity<UserAnswer>().HasKey(ua => ua.Id);

            // Relationships

            // Role -> Users (1:N)
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            // TypeQuiz -> Quizzes (1:N)
            modelBuilder.Entity<TypeQuiz>()
                .HasMany(tq => tq.Quizzes)
                .WithOne(q => q.TypeQuiz)
                .HasForeignKey(q => q.TypeId)
                .OnDelete(DeleteBehavior.Cascade);

            // TypeQuiz -> Dailies (1:N)
            modelBuilder.Entity<TypeQuiz>()
                .HasMany(tq => tq.Dailies)
                .WithOne(d => d.TypeQuiz)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.Cascade);

            // User -> UserAnswers (1:N)
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserAnswers)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quiz -> UserAnswers (1:N)
            modelBuilder.Entity<Quiz>()
                .HasMany(q => q.UserAnswers)
                .WithOne(ua => ua.Quiz)
                .HasForeignKey(ua => ua.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            // Properties Configuration

            // User
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            // TypeQuiz
            modelBuilder.Entity<TypeQuiz>()
                .Property(tq => tq.NameType)
                .IsRequired()
                .HasMaxLength(255);

            // Quiz
            modelBuilder.Entity<Quiz>()
                .Property(q => q.Region)
                .IsRequired()
                .HasMaxLength(255);

            // Indexes (if needed)
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
