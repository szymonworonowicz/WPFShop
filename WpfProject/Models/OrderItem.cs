using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfProject.Models
{
    public class OrderItem : INotifyPropertyChanged
    {
        [Key]
        public int ProductId { get; set; }
        [Key]
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
                if (Product != null)
                {
                    if(Product.Sale !=0)
                    {
                        return decimal.Parse(Product.SalePrice) * (decimal)Count;
                    }
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
