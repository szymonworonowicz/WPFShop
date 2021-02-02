using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WpfProject.DAL
{
    public class DataContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connectioString = $"Filename ={Path.Combine(Directory.GetCurrentDirectory(), "Database.sqlite")}";

            optionsBuilder.UseSqlite(connectioString);
        }
    }
}
