using System.Collections.Generic;

namespace WpfProject.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int AdresId { get; set; }

        public virtual Adres Adres { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
