using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using WpfProject.Account;
using WpfProject.DAL;
using WpfProject.Helpers;
using WpfProject.Models;

namespace WpfProject
{
    public partial class Startup : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            var context = DataContextAccesor.GetDataContext();

            context.Database.EnsureCreated();

            if (await context.Categories.AnyAsync() == false) // drop bazy
            {
                List<Category> categoryList = new List<Category>()
                {
                    new Category{Name = "AGD"},
                    new Category{Name = "TV"},
                    new Category{Name = "Telefony"},
                    new Category{Name = "Komputery"},
                    new Category{Name = "Konsole" },
                    new Category{Name = "Gadżety" }
                };
                List<Product> productList = new List<Product>();

                string directory = Path.Combine(Directory.GetCurrentDirectory(), "Image");
                var path = Directory.GetFiles(directory);

                foreach (var item in path)
                {
                    using (FileStream stream = new FileStream(item, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, (int)stream.Length);

                        productList.Add(new Product { Name = "lodowka", Price = 239.22M, Description = "Super Lodowka", Photo = buffer, Sale = true });
                    }
                }
                await context.Categories.AddRangeAsync(categoryList);
                await context.Products.AddRangeAsync(productList);

                await context.SaveChangesAsync();

                if (await context.Users.AnyAsync() == false)
                {
                    User admin = new User
                    {
                        Name = "admin",
                        Password = "admin",
                        Role = AppRole.Admin
                    };

                    await AccountManager.Register(admin);
                }
            }


            MainWindow w = new MainWindow();
            w.Show();
        }
    }
}
