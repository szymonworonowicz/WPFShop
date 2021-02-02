using System.Collections.Generic;

namespace WpfProject.Models
{
    public class Adres
    {
        public int AdresId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string HomeNumber { get; set; }
        public int OrderId { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
