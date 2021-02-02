﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WpfProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
