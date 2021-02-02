using System;
using System.Collections.Generic;
using System.Text;

namespace WpfProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public decimal Price { get; set; }

        public byte [] Photo { get; set; }
        public string Description { get; set; }
        public bool Sale { get; set; }

        public virtual Category Category { get; set; }
    }
}
