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
                    new Category{Name = "AGD" ,SubCategoryId=null},
                    new Category{Name = "TV" ,SubCategoryId=null},
                    new Category{Name = "Telefony" ,SubCategoryId=null},
                    new Category{Name = "Komputery" ,SubCategoryId=null},
                    new Category{Name = "Konsole" ,SubCategoryId=null},
                    new Category{Name = "Gadżety",SubCategoryId=null },
                };
               

                List<Product> productList = new List<Product>();

                string directory = Path.Combine(Directory.GetCurrentDirectory(), "Image");
                var path = Directory.GetFiles(directory);
                int i = 0;

                foreach (var item in path)
                {
                    using (FileStream stream = new FileStream(item, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, (int)stream.Length);

                        if (++i % 2 == 0)
                        {
                            productList.Add(new Product { Name = "lodowka", Price = 239.22M, Description = "Super Lodowka", Photo = buffer, Sale = 0, StanMagazynowy = 20 });
                        }
                        else
                        {
                            productList.Add(new Product { Name = "lodowka", Price = 239.22M, Description = "Super Lodowka", Photo = buffer, Sale = 20, StanMagazynowy = 20 });
                        }


                    }
                }
                await context.Categories.AddRangeAsync(categoryList);

                await context.Products.AddRangeAsync(productList);

                await context.SaveChangesAsync();

                List<Category> subCategoryList = new List<Category>()
                {
                    new Category{Name="Pralki",SubCategoryId=productList[0].Id},
                    new Category{Name="Zmywarki",SubCategoryId=productList[0].Id},
                    new Category{Name="Lodowki",SubCategoryId=productList[0].Id},
                    new Category{Name="Ekspresy",SubCategoryId=productList[0].Id},
                    new Category{Name="Kino domowe",SubCategoryId=productList[1].Id},
                    new Category{Name="Telewizory",SubCategoryId=productList[1].Id},
                    new Category{Name="DVD",SubCategoryId=productList[1].Id},
                    new Category{Name="Akcesoria RTV",SubCategoryId=productList[1].Id},
                    new Category{Name="Smartfony",SubCategoryId=productList[2].Id},
                    new Category{Name="Tablety",SubCategoryId=productList[2].Id},
                    new Category{Name="Telefony Stacjonarne",SubCategoryId=productList[2].Id},
                    new Category{Name="PC",SubCategoryId=productList[2].Id},
                    new Category{Name="Laptopy",SubCategoryId=productList[3].Id},
                    new Category{Name="Notebooki",SubCategoryId=productList[3].Id},
                    new Category{Name="Drukarki",SubCategoryId=productList[3].Id},
                    new Category{Name="Urządzenia peryferyjne",SubCategoryId=productList[3].Id},
                    new Category{Name="X-Box",SubCategoryId=productList[4].Id},
                    new Category{Name="Playstation",SubCategoryId=productList[4].Id},
                    new Category{Name="Nintendo",SubCategoryId=productList[4].Id},
                    new Category{Name="Smartwatche",SubCategoryId=productList[5].Id},
                    new Category{Name="Czytniki E-bookow",SubCategoryId=productList[5].Id},
                    new Category{Name="Nawigacje GPS",SubCategoryId=productList[5].Id},
                };

                await context.Categories.AddRangeAsync(subCategoryList);

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
