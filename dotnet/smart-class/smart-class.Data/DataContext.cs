﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using smart_class.Core.Entities;
using System.Diagnostics;
using File = smart_class.Core.Entities.File;

namespace smart_class.Data
{
    public class DataContext(IConfiguration configuration) : DbContext
    {
        private readonly IConfiguration _configuration = configuration;
        public DbSet<Institution> Institution { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Lesson> Lesson { get; set; }
        public DbSet<File> File { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["DbConnectionString"]);
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Institution>()
                .HasMany(i => i.Admins)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Institution>()
                .HasMany(i => i.Teachers)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Institution>()
                .HasMany(i => i.Groups)
                .WithOne(g => g.Institution) // נניח ש-Group יש לו Property בשם Institution
                .HasForeignKey(g => g.InstitutionId) // כאן אתה קובע את המפתח הזר
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
