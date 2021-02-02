using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using WpfProject.DAL;
using WpfProject.Models;

namespace WpfProject
{
    public partial class Startup : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Application app = new Application();

            var context = DataContextAccesor.GetDataContext();

            context.Database.EnsureCreated();

            if (context.Categories.Any() == false) // drop bazy
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
                context.Categories.AddRange(categoryList);
                context.Products.AddRange(productList);
                context.SaveChanges();
            }


            MainWindow w = new MainWindow();
            w.Show();
        }
    }
}
