using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfProject.DAL;
using WpfProject.Models;

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
            this.WindowStyle = WindowStyle.None;

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
            }
            if (photourl != "")
                using (FileStream stream = new FileStream(photourl, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, (int)stream.Length);
                    newProduct.Photo = buffer;
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
            if (add)
            {
                context.Products.Add(newProduct);

            }
            else
            {
                try
                {
                    context.Attach(newProduct).State = EntityState.Modified;
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (DbUpdateConcurrencyException ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {

                }

            }
            context.SaveChanges();
            this.Close();
        }

    }
}
