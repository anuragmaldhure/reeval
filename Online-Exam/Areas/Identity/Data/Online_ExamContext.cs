using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Online_Exam.Models;
//using Online_Exam.Areas.Identity.Data;

namespace Online_Exam.Data
{
    public class Online_ExamContext : IdentityDbContext<Online_ExamUser>
    {
        public Online_ExamContext(DbContextOptions<Online_ExamContext> options)
            : base(options)
        {
        }
        public DbSet<Online_ExamUser> ApplicationUsers { get; set; }

        // DbSet properties for each entity from ApplicationDbContext
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<SectionResult> SectionResults { get; set; }


        // Configuring relationships and model properties
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Exam -> Section (cascade delete enabled)
            modelBuilder.Entity<Exam>()
                .HasMany(e => e.Sections)
                .WithOne(s => s.Exam)
                .HasForeignKey(s => s.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Section -> Question (cascade delete enabled)
            modelBuilder.Entity<Section>()
                .HasMany(s => s.Questions)
                .WithOne(q => q.Section)
                .HasForeignKey(q => q.SectionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Question -> Option (cascade delete enabled)
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Options)
                .WithOne(o => o.Question)
                .HasForeignKey(o => o.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // ExamResult -> User (cascade delete enabled)
            modelBuilder.Entity<ExamResult>()
                .HasOne(er => er.User)
                .WithMany(u => u.ExamResults)
                .HasForeignKey(er => er.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ExamResult -> Exam (disable cascade delete)
            modelBuilder.Entity<ExamResult>()
                .HasOne(er => er.Exam)
                .WithMany(e => e.ExamResults)
                .HasForeignKey(er => er.ExamId)
                .OnDelete(DeleteBehavior.NoAction);

            // UserAnswer -> ExamResult (cascade delete enabled)
            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.ExamResult)
                .WithMany(er => er.UserAnswers)
                .HasForeignKey(ua => ua.ResultId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserAnswer -> Question (disable cascade delete)
            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.Question)
                .WithMany(q => q.UserAnswers)
                .HasForeignKey(ua => ua.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            // UserAnswer -> Option (disable cascade delete)
            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.SelectedOption)
                .WithMany(o => o.UserAnswers)
                .HasForeignKey(ua => ua.SelectedOptionId)
                .OnDelete(DeleteBehavior.NoAction);

            // SectionResult -> ExamResult (cascade delete enabled)
            modelBuilder.Entity<SectionResult>()
                .HasOne(sr => sr.ExamResult)
                .WithMany(er => er.SectionResults)
                .HasForeignKey(sr => sr.ExamResultId)
                .OnDelete(DeleteBehavior.Cascade);

            // SectionResult -> Section (disable cascade delete to avoid multiple cascade paths)
            modelBuilder.Entity<SectionResult>()
                .HasOne(sr => sr.Section)
                .WithMany(s => s.SectionResults)
                .HasForeignKey(sr => sr.SectionId)
                .OnDelete(DeleteBehavior.NoAction);  // Disable cascade delete


        }
    }
}
