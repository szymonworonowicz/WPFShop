using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WpfProject.Models;

namespace WpfProject.DAL
{
    public class DataContext :DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserData> UserData { get; set; }
        public DbSet<Adres> Adreses { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Order { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connectioString = $"Filename ={Path.Combine(Directory.GetCurrentDirectory(), "Database.sqlite")}";

            optionsBuilder.UseSqlite(connectioString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(x => new { x.OrderId, x.ProductId });

            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.SubCategory)
                .WithMany(x => x.SubCategories)
                .HasForeignKey(x => x.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(x => x.Category)
                    .WithMany(x => x.Products)
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasMany(x => x.Ordered)
                    .WithOne(x => x.Order)
                    .HasForeignKey(x => x.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.UserData)
                    .WithMany(x => x.Orders)
                    .HasForeignKey(x => x.UserDataId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

        }

    }
}
