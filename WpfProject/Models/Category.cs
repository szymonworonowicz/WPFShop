using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WpfProject.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int? SubCategoryId { get; set; }
        public override string ToString()
        {
            return Name;
        }

        public virtual ICollection<Category> SubCategories { get; set; }

        public virtual Category SubCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
