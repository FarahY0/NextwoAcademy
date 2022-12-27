using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nextwo.Models;
using Nextwo.Models.ViewModel;

namespace Nextwo.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
       
    }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course>   Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCourse>()
                  .HasKey(m => new { m.UserId, m.CourseId });
        }

    }
}
