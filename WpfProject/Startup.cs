using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfProject.Account;
using WpfProject.DAL;
using WpfProject.Helpers;
using WpfProject.ImagesHelpers;
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




                string directory = Path.Combine(Directory.GetCurrentDirectory(), "Image");
                var path = Directory.GetFiles(directory);

                await context.Categories.AddRangeAsync(categoryList);


                await context.SaveChangesAsync();

                List<Category> subCategoryList = new List<Category>()
                {
                    new Category{Name="Pralki",SubCategoryId=categoryList[0].Id},
                    new Category{Name="Zmywarki",SubCategoryId=categoryList[0].Id},
                    new Category{Name="Lodowki",SubCategoryId=categoryList[0].Id},
                    new Category{Name="Ekspresy",SubCategoryId=categoryList[0].Id},
                    new Category{Name="Kino domowe",SubCategoryId=categoryList[1].Id},
                    new Category{Name="Telewizory",SubCategoryId=categoryList[1].Id},
                    new Category{Name="DVD",SubCategoryId=categoryList[1].Id},
                    new Category{Name="Akcesoria RTV",SubCategoryId=categoryList[1].Id},
                    new Category{Name="Smartfony",SubCategoryId=categoryList[2].Id},
                    new Category{Name="Tablety",SubCategoryId=categoryList[2].Id},
                    new Category{Name="Telefony Stacjonarne",SubCategoryId=categoryList[2].Id},
                    new Category{Name="PC",SubCategoryId=categoryList[3].Id},
                    new Category{Name="Laptopy",SubCategoryId=categoryList[3].Id},
                    new Category{Name="Notebooki",SubCategoryId=categoryList[3].Id},
                    new Category{Name="Drukarki",SubCategoryId=categoryList[3].Id},
                    new Category{Name="Urządzenia peryferyjne",SubCategoryId=categoryList[3].Id},
                    new Category{Name="X-Box",SubCategoryId=categoryList[4].Id},
                    new Category{Name="Playstation",SubCategoryId=categoryList[4].Id},
                    new Category{Name="Nintendo",SubCategoryId=categoryList[4].Id},
                    new Category{Name="Smartwatche",SubCategoryId=categoryList[5].Id},
                    new Category{Name="Czytniki E-bookow",SubCategoryId=categoryList[5].Id},
                    new Category{Name="Nawigacje GPS",SubCategoryId=categoryList[5].Id},
                };

                await context.Categories.AddRangeAsync(subCategoryList);


                await context.SaveChangesAsync();

                List<Product> productList = new List<Product>();
                string json = "";
                using (StreamReader str = new StreamReader("generated.json"))
                {
                    json = str.ReadToEnd();
                }
                productList = JsonSerializer.Deserialize<List<Product>>(json);
                Random rand = new Random(); //losowanie obrazka
                ImageResizer resizer = new ImageResizer();
                foreach (var product in productList)
                {
                    var item = path[rand.Next(0, path.Length)];


                    product.Photo = resizer.resize(item);
                    product.CategoryId = subCategoryList[product.CategoryId.Value - 1].Id;
                }

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

                    AccountManager.RegisterAdmin(admin);

                    //User user = new User
                    //{
                    //    Name = "user",
                    //    Password = "user",
                    //    Role = AppRole.User
                    //};

                    //AccountManager.Register(user);
                }
            }


            MainWindow w = new MainWindow();
            w.Show();
        }
    }
}
