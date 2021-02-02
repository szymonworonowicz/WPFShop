using System;
using System.Collections.Generic;
using System.Text;
using WpfProject.Models;

namespace WpfProject.Helpers
{
    public class CartHelper
    {
        private static List<OrderItem> OrderItems { get;  set; }
        private static DateTime CartCreated;

        private CartHelper() { }

        public static void AddToCart(Product p, int Count)
        {
            if (OrderItems == null)
                OrderItems = new List<OrderItem>();
            CartCreated = DateTime.Now;

            OrderItem item = new OrderItem { Product = p, Count = Count };

            OrderItems.Add(item);
        }

        public static List<OrderItem> getCart()
        {
            DateTime now = DateTime.Now;

            if (CartCreated.AddMinutes(30) > now)
                return OrderItems;
            return null;
        }
        public static void resetCart()
        {
            OrderItems = null;
        }
    }
}
