using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WpfProject.DAL;
using WpfProject.Models;

namespace WpfProject.Profiler
{
    class DbAccessorService
    {
        private static ObservableCollection<Product> products;
        private static ObservableCollection<Category> categories;
        private static ObservableCollection<Order> orders;
        private static ObservableCollection<User> users;

        public static ObservableCollection<Product> getProducts()
        {
            if (products == null)
            {
                using (var context = new DataContext())
                {
                    var productslist = context.Products.Include(x => x.Category)
                        .ThenInclude(x => x.SubCategory)
                        .Where(x => x.StanMagazynowy>0)
                        .ToList();
                    products = new ObservableCollection<Product>(productslist);
                }


            }

            return products;
        }

        public static ObservableCollection<Category> getCategories()
        {
            if (categories == null)
            {
                using (var context = new DataContext())
                {
                    var categorieslist = context.Categories.Where(x => x.SubCategoryId == null)
                        .Include(x => x.SubCategories)
                        .ToList();

                    categories = new ObservableCollection<Category>(categorieslist);
                }


            }

            return categories;
        }

        public static ObservableCollection<Order> getOrders()
        {
            if (orders == null)
            {
                using (var context = new DataContext())
                {
                    var orderslist = context.Order.Include(x => x.Ordered).ThenInclude(x => x.Product).Include(x => x.UserData).ThenInclude(x => x.Adres).ToList();
                    orders = new ObservableCollection<Order>(orderslist);
                }

            }

            return orders;
        }

        public static ObservableCollection<User> getUsers()
        {
            if (users == null)
            {
                using (var context = new DataContext())
                {
                    var userslist = context.Users.Include(x => x.UserData).ToList();
                    users = new ObservableCollection<User>(userslist);
                }

            }

            return users;
        }

        internal static void DeleteProduct(Product product)
        {
            products.Remove(product);

            Task t = new Task(() =>
            {
                using (var context = new DataContext())
                {
                    context.Products.Remove(product);
                    context.SaveChangesAsync();
                }

            });

            t.Start();
        }

        internal static void AddProduct(Product product)
        {
            products.Add(product);
            Task t = new Task(() =>
            {
                using (DataContext context = new DataContext())
                {
                    product.Category = null;
                    context.Add(product);
                    context.SaveChanges();
                }
            });
            t.Start();
        }

        public static void updateProduct(Product product)
        {
            int i = 0;
            for (i = 0; i < products.Count; i++)
            {
                if (products[i] == product)
                {
                    break;
                }
            }
            products[i] = product;
            Task t = new Task(() =>
            {
                using (DataContext context = new DataContext())
                {
                    try
                    {
                        context.Products.Update(product);
                        context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            });
            t.Start();
        }

        public static void updateOrder(Order order)
        {
            Task t = new Task(() =>
            {
                using (DataContext context = new DataContext())
                {
                    context.Order.Update(order);
                    context.SaveChanges();
                }
            });
            t.Start();
        }
        public static void DeleteOrder(Order order )
        {
            orders.Remove(order);
            Task t = new Task(() =>
            {
                using (DataContext context = new DataContext())
                {
                    context.Order.Remove(order);
                    context.SaveChanges(); 
                }
            });

            t.Start();
        }
    }
}
