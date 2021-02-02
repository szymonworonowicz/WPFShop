using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfProject.Models;

namespace WpfProject.DialogWindow
{
    /// <summary>
    /// Interaction logic for InvoiceDlg.xaml
    /// </summary>
    public partial class InvoiceDlg : Window
    {
        private Order order { get; set; }
        public InvoiceDlg(Order order)
        {
            InitializeComponent();
            this.order = order;
            DataContext = order;

            CommandBinding PrintBinding = new CommandBinding();
            PrintBinding.Command = ApplicationCommands.Print;
            PrintBinding.Executed += PrintCommandExecuted;
            this.CommandBindings.Add(PrintBinding);

            CommandBinding SaveBinding = new CommandBinding();
            SaveBinding.Command = ApplicationCommands.Save;
            SaveBinding.Executed += SaveCommandExecuted;
            this.CommandBindings.Add(SaveBinding);

        }

        private void SaveCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Save();
        }

        private void PrintCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Print();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }
        private void Save()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.DefaultExt = ".jpg";
            saveFileDialog.Filter = "Image (.jpg)|*.jpg";
            this.Save_btn.Visibility = Visibility.Hidden;
            this.Print_btn.Visibility = Visibility.Hidden;
            if (saveFileDialog.ShowDialog().Value)
            {
                string filename = saveFileDialog.FileName;
                //get the dimensions of the ink control
                int width = (int)InvoiceContentGrid.ActualWidth;
                int height = (int)InvoiceContentGrid.ActualHeight;
                //render ink to bitmap
                RenderTargetBitmap rtb =
                new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Default);
                rtb.Render(InvoiceContentGrid);

                if(File.Exists(filename))
                {
                    File.SetAttributes(filename, FileAttributes.Normal);
                    File.Delete(filename);
                }
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(rtb));
                    encoder.Save(fs);
                }

            }
            this.Close();
        }

        private void Print()
        {
            PrintDialog printDialog = new PrintDialog();
            this.Height = printDialog.PrintableAreaHeight;
            this.Width = printDialog.PrintableAreaWidth;
            
            if(printDialog.ShowDialog().Value)
            {
                printDialog.PrintVisual(this, $"faktura nr {order.Id}");
            }

        }
    }
}
