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


namespace WhatAndWhenData
{
    public class WhatAndWhenContext : IdentityDbContext<IdentityUser>
    {
       


        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<PriorityEntity> Priorities { get; set; }

        


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var db = System.IO.Path.Join(path, "whatandwhen.db");
            options.UseSqlite($"Data Source={db}");
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


           

            // Konfiguracja relacji między CommentEntity a TaskEntity
            /* modelBuilder.Entity<CommentEntity>()
                   .HasOne(c => c.Task)
                   .WithMany(t => t.Comments)
                   .HasForeignKey(c => c.TaskId)
                   .OnDelete(DeleteBehavior.Restrict);

             /*


               // Dodawanie użytkowników i ról
               string ADMIN_ID = Guid.NewGuid().ToString();
               string ROLE_ID = Guid.NewGuid().ToString();

               // dodanie roli administratora
               modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
               {
                   Name = "admin",
                   NormalizedName = "ADMIN",
                   Id = ROLE_ID,
                   ConcurrencyStamp = ROLE_ID
               });
             zżytkownika
               var admin = new IdentityUser
               {
                   Id = ADMIN_ID,
                   Email = "adminuser@whatandwhen.com",
                   EmailConfirmed = true,
                   UserName = "adminuser@whatandwhen.com",
                   NormalizedUserName = "ADMINUSER@WHATANDWHEN.COM",
                   NormalizedEmail = "ADMINUSER@WHATANDWHEN.COM"
               };

               // haszowanie hasła
               PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
               admin.PasswordHash = ph.HashPassword(admin, "S3cretPassword");


               // zapisanie użytkownika
               modelBuilder.Entity<IdentityUser>().HasData(admin);

               // przypisanie roli administratora użytkownikowi
               modelBuilder.Entity<IdentityUserRole<string>>()
               .HasData(new IdentityUserRole<string>
               {
                   RoleId = ROLE_ID,
                   UserId = ADMIN_ID
               });

               string STUDENT_ROLE_ID = Guid.NewGuid().ToString();
               modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
               {
                   Name = "student",
                   NormalizedName = "STUDENT",
                   Id = STUDENT_ROLE_ID,
                   ConcurrencyStamp = STUDENT_ROLE_ID
               });

               // Utworzenie użytkownika testowego jako studenta
               string STUDENT_USER_ID = Guid.NewGuid().ToString();
               var studentUser = new IdentityUser
               {
                   Id = STUDENT_USER_ID,
                   Email = "studentuser@whatandwhen.com",
                   EmailConfirmed = true,
                   UserName = "studentuser@whatandwhen.com",
                   NormalizedUserName = "STUDENTUSER@WHATANDWHEN.COM",
                   NormalizedEmail = "STUDENTUSER@WHATANDWHEN.COM"
               };

               // Hashowanie hasła

               studentUser.PasswordHash = ph.HashPassword(studentUser, "StudentPassword123");

               // Zapisanie użytkownika
               modelBuilder.Entity<IdentityUser>().HasData(studentUser);

               // Przypisanie roli studenta użytkownikowi
               modelBuilder.Entity<IdentityUserRole<string>>()
               .HasData(new IdentityUserRole<string>
               {
                   RoleId = STUDENT_ROLE_ID,
                   UserId = STUDENT_USER_ID
               });
               */
        }
    }
}
