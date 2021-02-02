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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connectioString = $"Filename ={Path.Combine(Directory.GetCurrentDirectory(), "Database.sqlite")}";

            optionsBuilder.UseSqlite(connectioString);
        }
    }
}
