using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WpfProject.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public virtual Product Product { get; set; }

        [NotMapped]
        public decimal Amount
        {
            get
            {
                if(Product !=null)
                {
                    return Product.Price * (decimal)Count;
                }
                return 0;
            }
        }
    }
}
