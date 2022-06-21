using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;


namespace WebApp.Data
{
    public class DataContext  :  DbContext 
    {
       
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
        }
        public DbSet<Student> Student { get ; set ; }

        public DbSet<Course> Course { get; set; }

        public DbSet<Faculty> Faculty { get; set; }

        public DbSet<Employee> Employee { get;  set; }

        public DbSet<Member> Member { get; set; }

        public DbSet<StudentCourse> StudentCourse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>();
            modelBuilder.Entity<Course>();
            modelBuilder.Entity<Faculty>();
            modelBuilder.Entity<Employee>();
            modelBuilder.Entity<Member>();
            modelBuilder.Entity<StudentCourse>();
        }
    }
}
