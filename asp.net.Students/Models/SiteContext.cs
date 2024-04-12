using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace asp.net.Students.Models
{
    public class SiteContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public SiteContext(DbContextOptions<SiteContext> options) : base(options) { }

        public virtual DbSet<Info> Infos { get; set; }
        public virtual DbSet<MapInfo> MapInfos { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }
        public virtual DbSet<QuizQuestion> QuizQuestions { get; set; }
        public virtual DbSet<QuizOption> QuizOptions { get; set; }
        public virtual DbSet<QuizStudentAnswer> QuizStudentAnswers { get; set; }
        public virtual DbSet<QuizStudentResult> QuizStudentResults { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole<int>>()
                .HasData(
                new List<IdentityRole<int>>()
                {
                    new() {Id = 1, Name = "Admin",NormalizedName = "ADMIN"},
                    new() {Id = 2, Name = "Teacher",NormalizedName = "TEACHER"},
                    new() {Id = 3, Name = "Student",NormalizedName = "STUDENT"},
                    new() {Id = 4, Name = "Guest",NormalizedName = "GUEST"},
                });
        }

    }
}
