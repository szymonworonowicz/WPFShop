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
        public DbSet<UserData> UserDatas { get; set; }
        public DbSet<Adres> Adreses { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

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
                entity.HasKey(x => new { x.Id, x.ProductId });
            });
        }

    }
}
