using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public byte[] Photo { get; set; }
        public string Description { get; set; }
        public int Sale { get; set; }
        public DateTime AddedDate { get; set; }
        public int StanMagazynowy { get; set; }
        public int? CategoryId { get; set; }

        [NotMapped]
        public string SalePrice
        {
            get
            {
                if (Sale != 0)
                    return Math.Round((1.0M - (decimal)Sale / 100.0M) * (decimal)Price, 2).ToString();
                return "";
            }

        }


        public virtual Category Category { get; set; }
    }
}
