using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatAndWhenData.Entities;

namespace WhatAndWhenData
{
    public class WhatAndWhenContext : DbContext
    {
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WhatAndWhenContext>
        {
            public WhatAndWhenContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<WhatAndWhenContext>();
                // Użyj właściwego connection stringa z pliku konfiguracyjnego lub ustawienia domyślnego
                optionsBuilder.UseSqlite("Data Source=your_database.db"); // Zmień na odpowiedni connection string

                return new WhatAndWhenContext(optionsBuilder.Options);
            }
        }
        public WhatAndWhenContext(DbContextOptions<WhatAndWhenContext> options)
           : base(options)
        {
        }


        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<PriorityEntity> Priorities { get; set; }
        public DbSet<SubtaskEntity> Subtasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var path = System.IO.Path.Join(Environment.CurrentDirectory, "whatandwhen.db");
            options.UseSqlite($"Data Source={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEntity>().HasData(
                new CategoryEntity() { Id = 1, Name = "Work", Description = "Tasks related to work" },
                new CategoryEntity() { Id = 2, Name = "Personal", Description = "Personal tasks" }
            );

            modelBuilder.Entity<PriorityEntity>().HasData(
                new PriorityEntity() { Id = 1, Name = "Low", Level = 1 },
                new PriorityEntity() { Id = 2, Name = "Medium", Level = 2 },
                new PriorityEntity() { Id = 3, Name = "High", Level = 3 }
            );
        }
    }
}
