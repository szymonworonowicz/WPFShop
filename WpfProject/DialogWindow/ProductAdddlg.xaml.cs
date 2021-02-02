using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfProject.DAL;
using WpfProject.ImagesHelpers;
using WpfProject.Models;
using WpfProject.Profiler;

namespace WpfProject.DialogWindow
{
    /// <summary>
    /// Interaction logic for ProductAdddlg.xaml
    /// </summary>
    public partial class ProductAdddlg : Window
    {
        private DataContext context;
        private List<Category> categories;
        public Product newProduct { get; set; }

        public bool add { get; set; }
        public ProductAdddlg()
        {
            InitializeComponent();
            newProduct = new Product();
            add = true;

        }
        public ProductAdddlg(Product product)
        {
            InitializeComponent();
            newProduct = product;
            AddProduct.Content = "Edytuj Produkt";
            add = false;
        }
        private void Add_Product_Loaded(object sender, RoutedEventArgs e)
        {
            context = DataContextAccesor.GetDataContext();
            categories = context.Categories.Where(x => x.SubCategoryId == null).Include(x => x.SubCategories).ToList();
            CategoryCombo.ItemsSource = categories;

            //CategoryCombo.SelectedIndex = 0;
            MainGrid.DataContext = newProduct;
            PhotoName.Content = "";
        }
        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            string photourl = "";
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == true)
            {
                photourl = dialog.FileNames[0];
                var path = photourl.Split('\\');
                PhotoName.Content = path[path.Length-1];
            }

            if (photourl != "")
            {
                ImageResizer resizer = new ImageResizer();
                newProduct.Photo = resizer.resize(photourl);
            }

        }

        private void Category_Selection_Changed(object sender, SelectionChangedEventArgs e)
        {
            //SubCategoryCombo.SelectedIndex = -1;
            SubCategoryCombo.ItemsSource = categories[CategoryCombo.SelectedIndex].SubCategories;
            SubCategoryCombo.SelectedIndex = 0;

        }

        private void SubCategory_Selection_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (SubCategoryCombo.SelectedIndex != -1)
            {
                var category = SubCategoryCombo.SelectedItem as Category;
                newProduct.CategoryId = category.Id;
            }

        }

        private void Add_Product_Click(object sender, RoutedEventArgs e)
        {
            if (newProduct.Photo.Length != 0)
                if (add)
                {
                    DbAccessorService.AddProduct(newProduct);
                }
                else
                {
                    DbAccessorService.updateProduct(newProduct);
                    
                }
            this.Close();
        }

    }
}
