using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatAndWhenData.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace WhatAndWhenData
{
    public class WhatAndWhenContext : IdentityDbContext<IdentityUser>
    {
       


        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<PriorityEntity> Priorities { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));

            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var db = System.IO.Path.Join(path, "whatandwhen.db");
            optionsBuilder.UseSqlite($"Data Source={db}");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
