using JobPortal.Domain.Entities;
using JobPortal.Persistence.BookmarkJobs;
using JobPortal.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace JobPortal.Persistence
{
    public class DataContext: DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;

            Database.EnsureCreated();
        }

        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Resume> Resumes { get; set; }

        public DbSet<Employer> Employers { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }

        public DbSet<BookmarkJob> Bookmarks { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }



        public void Save()
        {
            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new JobPostingConfiguration().Configure(builder.Entity<JobPosting>());
            new JobSeekerConfiguration().Configure(builder.Entity<JobSeeker>());
            new BookmarkJobConfiguration().Configure(builder.Entity<BookmarkJob>());
            new RatingConfiguration().Configure(builder.Entity<Rating>());

        }
    }
}
