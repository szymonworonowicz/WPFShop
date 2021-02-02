using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WpfProject.Models
{
    public class OrderItem : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        private int count;
        public int Count 
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
                OnPropertyChanged("Amount");
            }
        }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                new PropertyChangedEventArgs(property));
        }
    }
}
