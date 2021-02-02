using Microsoft.Win32;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Image = MigraDoc.DocumentObjectModel.Shapes.Image;

namespace WpfProject.DialogWindow
{
    /// <summary>
    /// Interaction logic for InvoiceDlg.xaml
    /// </summary>
    public partial class InvoiceDlg : Window
    {
        readonly static MigraDoc.DocumentObjectModel.Color TableBorder = new MigraDoc.DocumentObjectModel.Color(81, 125, 192);
        readonly static MigraDoc.DocumentObjectModel.Color TableBlue = new MigraDoc.DocumentObjectModel.Color(235, 240, 249);
        readonly static MigraDoc.DocumentObjectModel.Color TableGray = new MigraDoc.DocumentObjectModel.Color(242, 242, 242);


        Document document;
        TextFrame addressFrame;
        TextFrame PriceFrame;
        MigraDoc.DocumentObjectModel.Tables.Table table;

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
            saveFileDialog.Filter = "Pdf Files (*.pdf)|*.pdf|All Files(*.*) | *.* ";
            this.Save_btn.Visibility = Visibility.Hidden;
            this.Print_btn.Visibility = Visibility.Hidden;
            if (saveFileDialog.ShowDialog().Value)
            {
                //if (System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower() == "pdf")
                //{
                this.document = new Document();

                this.document.Info.Title = $"Faktura nr {order.Date.Year}/{order.Id}";
                this.document.Info.Subject = $"Faktura nr {order.Date.Year}/{order.Id}";
                this.document.Info.Author = "RTV&AGD";

                DefineStyle();

                CreatePage();
                FillContent();


                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
                renderer.Document = document;

                renderer.RenderDocument();

                // Save the document...
                string filename = saveFileDialog.FileName;
                renderer.PdfDocument.Save(filename);


                Process process = new Process();
                process.StartInfo = new ProcessStartInfo()
                {
                    FileName = "explorer",
                    Arguments = filename,

                };
                process.Start();

            }
            this.Close();

        }
        private void FillContent()
        {
            //XPathNavigator item = SelectItem("/invoice/to");
            MigraDoc.DocumentObjectModel.Paragraph paragraph = addressFrame.AddParagraph();
            paragraph.AddText("Dane Zamawiajacego :");
            paragraph.AddLineBreak();
            paragraph.AddText($"Imię i nazwisko: {order.UserData.Name} {order.UserData.Surname}");
            paragraph.AddLineBreak();
            paragraph.AddText($"Adres: ul. {order.UserData.Adres.Street} {order.UserData.Adres.HomeNumber}, {order.UserData.Adres.PostCode} {order.UserData.Adres.City}");

            MigraDoc.DocumentObjectModel.Paragraph priceParagraph = PriceFrame.AddParagraph();
            priceParagraph.AddText($"Cena całkowita: \n {order.OrderAmount} zł \n ");
            //paragraph.AddLineBreak();
            priceParagraph.AddText($"Rodzaj przesyłki: \n {order.OrderOption}  \n");
            //paragraph.AddLineBreak();
            priceParagraph.AddText($"Cena z przesyłką: \n {order.Amount} zł\n ");
            //paragraph.AddLineBreak();


            foreach (var orderItem in order.Ordered)
            {
                Row row = table.AddRow();
                row.TopPadding = 1.5;

                row.Cells[0].Shading.Color = TableGray;
                row.Cells[0].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[1].Shading.Color = TableGray;
                row.Cells[2].Shading.Color = TableGray;
                row.Cells[3].Shading.Color = TableGray;

                row.Cells[0].AddParagraph(orderItem.Product.Name);
                row.Cells[1].AddParagraph(orderItem.Count.ToString());
                row.Cells[2].AddParagraph($"{orderItem.Product.Price} zł");
                row.Cells[3].AddParagraph($"{orderItem.Amount} zł");

                this.table.SetEdge(0, this.table.Rows.Count - 1, 4, 1, Edge.Box, BorderStyle.Single, 0.75);
            }

        }

        private void CreatePage()
        {
            // Each MigraDoc document needs at least one section.
            MigraDoc.DocumentObjectModel.Section section = this.document.AddSection();

            // Put a logo in the header
            Image image = section.Headers.Primary.AddImage("Image_Icons/logo1.png");
            image.Height = "2.5cm";
            image.Width = "5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Right;
            image.WrapFormat.Style = WrapStyle.Through;

            // Create the text frame for the address
            this.addressFrame = section.AddTextFrame();
            this.addressFrame.Height = "8.0cm";
            this.addressFrame.Width = "10.0cm";
            this.addressFrame.Left = ShapePosition.Left;
            this.addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.addressFrame.Top = "4cm";
            this.addressFrame.RelativeVertical = RelativeVertical.Page;

            this.PriceFrame = section.AddTextFrame();
            this.PriceFrame.Height = "5.0cm";
            this.PriceFrame.Width = "5.0cm";
            this.PriceFrame.Left = ShapePosition.Right;
            this.PriceFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.PriceFrame.Top = "4.0cm";
            this.PriceFrame.RelativeVertical = RelativeVertical.Page;

            // Put sender in address frame
            //= addressFrame.AddParagraph("ul.Pralki 5, 13-342 Lodówka");
            //paragraph.Format.Font.Name = "Times New Roman";
            //paragraph.Format.Font.Size = 7;
            //paragraph.Format.SpaceAfter = 3;

            // Add the print date field
            MigraDoc.DocumentObjectModel.Paragraph paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "8cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText($"Faktura nr {order.Date.Year}/{order.Id}", TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddText("Białystok, ");
            paragraph.AddDateField("dd.MM.yyyy");

            paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText($"\u00a9 2021 RTV&AGD \n ul.Pralki 5, 13-342 Lodówka \n Data wydruku: {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")} ");
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Create the item table
            this.table = section.AddTable();
            table.Format.Alignment = ParagraphAlignment.Justify;
            table.Borders.Color = MigraDoc.DocumentObjectModel.Colors.Black;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 5;
            table.TopPadding = 10;
            table.BottomPadding = 10;

            // Before you can add a row, you must define the columns
            Column column = this.table.AddColumn("7.5cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = this.table.AddColumn("1.5cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = this.table.AddColumn("3.5cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = this.table.AddColumn("3.5cm");
            column.Format.Alignment = ParagraphAlignment.Right;


            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Left;
            row.Format.Font.Bold = true;
            row.Shading.Color = TableBlue;
            row.Cells[0].AddParagraph("Nazwa");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;

            row.Cells[1].AddParagraph("Ilość");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;

            row.Cells[2].AddParagraph("Cena/szt");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[2].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;

            row.Cells[3].AddParagraph("Wartosc");
            row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[3].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;



            this.table.SetEdge(0, 0, 4, 1, Edge.Box, BorderStyle.Single, 0.75, MigraDoc.DocumentObjectModel.Color.Empty);

        }

        private void DefineStyle()
        {
            // Get the predefined style Normal.
            MigraDoc.DocumentObjectModel.Style style = this.document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Verdana";

            style = this.document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = this.document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called Table based on style Normal
            style = this.document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = 9;

            // Create a new style called Reference based on style Normal
            style = this.document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);


        }
        private void Print()
        {
            PrintDialog printDialog = new PrintDialog();
            this.Height = printDialog.PrintableAreaHeight;
            this.Width = printDialog.PrintableAreaWidth;

            if (printDialog.ShowDialog().Value)
            {
                printDialog.PrintVisual(this, $"faktura nr {order.Id}");
            }

        }
    }
}
