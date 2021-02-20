using CostApp.Data.Identity;
using CostApp.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostApp.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
            .HasData(
                new Category { Id = 1, Name = "Food" },
                new Category { Id = 2, Name = "Home" },
                new Category { Id = 3, Name = "Cosmetics" },
                new Category { Id = 4, Name = "Sweets" },
                new Category { Id = 5, Name = "Presents" },
                new Category { Id = 6, Name = "Bills" },
                new Category { Id = 7, Name = "Shopping" },
                new Category { Id = 8, Name = "Faculty" },
                new Category { Id = 9, Name = "Drugs" },
                new Category { Id = 10, Name = "Various" }
            );
        }
    }
}
