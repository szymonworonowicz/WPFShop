﻿using System.Collections.Generic;
using WpfProject.Helpers;

namespace WpfProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public AppRole Role { get; set; }


        public int? UserDataId { get; set; }

        public virtual UserData UserData { get; set; }
        public ICollection<OrderItem> Orders { get; set; }

    }
}
