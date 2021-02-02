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
                string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam mi felis, pellentesque nec suscipit vitae, ullamcorper sit amet lorem. Nullam posuere elit eget lectus hendrerit, in vulputate ex aliquam. Aliquam cursus, dui quis semper placerat, libero ipsum pharetra ante, a efficitur justo nunc eu purus. Vivamus at sapien vitae justo sagittis ullamcorper. Sed eu odio at elit dapibus blandit ut vel lectus. Suspendisse sit amet lacinia lorem. Morbi vestibulum luctus volutpat. Mauris egestas augue vitae eleifend molestie. Suspendisse nec magna felis. Nulla ligula nunc, aliquet non hendrerit eu, pulvinar ac neque. Vivamus ex turpis, tempor ut viverra ac, facilisis eu ligula.";
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
                int i = 0;


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
                foreach (var item in path)
                {
                    using (FileStream stream = new FileStream(item, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, (int)stream.Length);

                        if (++i % 2 == 0)
                        {
                            productList.Add(new Product { Name = "lodowka", Price = 239.22M, Description = lorem, Photo = buffer, Sale = 0, StanMagazynowy = 20, CategoryId = subCategoryList[i].Id });
                        }
                        else
                        {
                            productList.Add(new Product { Name = "lodowka", Price = 239.22M, Description = lorem, Photo = buffer, Sale = 20, StanMagazynowy = 20, CategoryId = subCategoryList[i].Id });
                        }


                    }
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
