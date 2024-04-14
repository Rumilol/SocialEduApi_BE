using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialEduApi.Models.Identity;
using SocialEduApi.Models.Entities;

namespace SocialEduApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
           
    

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<InstitutionRole> InstitutionRoles { get; set; }
        public DbSet<ProjectSubmission> ProjectSubmissions { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<SavedUser> SavedUsers { get; set; }
        public DbSet<SavedUsersFolder> SavedUsersFolders { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubmissionFolder> SubmissionFolders { get; set; }
        public DbSet<SubmissionFolderContains> SubmissionFolderContainses { get; set; }
        public DbSet<UserInstitutionBelongs> UserInstitutionBelongses { get; set; }
        public DbSet<UserOnSubject> UsersOnSubjects { get; set; }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<IdentityRole> AspNetRoles { get; set; }
        
    }
}
