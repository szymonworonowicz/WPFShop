using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WpfProject.DAL;
using WpfProject.Models;

namespace WpfProject.Profiler
{
    class DbAccessorService
    {
        private static List<Product> products;
        private static List<Category> categories;
        private static List<Order> orders;
        private static List<User> users;

        public static List<Product> getProducts()
        {
            if (products == null)
            {
                using (var context = new DataContext())
                {
                    products = context.Products.Include(x => x.Category).ThenInclude(x => x.SubCategory).ToList();
                }


            }

            return products;
        }

        public static List<Category> getCategories()
        {
            if (products == null)
            {
                using (var context = new DataContext())
                {
                    categories = context.Categories.Where(x => x.SubCategoryId == null).Include(x => x.SubCategories).ToList();
                }


            }

            return categories;
        }

        public static List<Order> getOrders()
        {
            if (orders == null)
            {
                using (var context = new DataContext())
                {
                    orders = context.Order.Include(x => x.Ordered).Include(x => x.UserData).ThenInclude(x => x.Adres).ToList();
                }

            }

            return orders;
        }

        public static List<User> getUsers()
        {
            if (users == null)
            {
                using (var context = new DataContext())
                {
                    users = context.Users.Include(x => x.UserData).ToList();
                }

            }

            return users;
        }
    }
}
