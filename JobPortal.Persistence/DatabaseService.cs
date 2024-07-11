﻿using JobPortal.Application.Interfaces;
using JobPortal.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace JobPortal.Persistence
{
    public class DatabaseService: DbContext, IDatabaseService
    {
        private readonly IConfiguration _configuration;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;

            Database.EnsureCreated();
        }

        public DbSet<JobPosting> JobPostings { get; set; }

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
            
        }
    }
}
