using System;
using System.Collections.Generic;
using WpfProject.Helpers;

namespace WpfProject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int UserDataId { get; set; }
        public string OrderOption { get; set; }
        public decimal OrderAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Date { get; set; }

        public virtual UserData UserData { get; set; }

        public virtual ICollection<OrderItem> Ordered { get; set; }

    }
}
