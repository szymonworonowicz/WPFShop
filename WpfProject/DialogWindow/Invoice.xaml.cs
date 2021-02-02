using Microsoft.Win32;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.XPath;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using System.Windows.Shapes;
using WpfProject.Models;
using Image = MigraDoc.DocumentObjectModel.Shapes.Image;
using System.Threading;

namespace WpfProject.DialogWindow
{
    /// <summary>
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class Invoice : Window
    {

        readonly static MigraDoc.DocumentObjectModel.Color TableBorder = new MigraDoc.DocumentObjectModel.Color(81, 125, 192);
        readonly static MigraDoc.DocumentObjectModel.Color TableBlue = new MigraDoc.DocumentObjectModel.Color(235, 240, 249);
        readonly static MigraDoc.DocumentObjectModel.Color TableGray = new MigraDoc.DocumentObjectModel.Color(242, 242, 242);


        Document document;
        readonly XmlDocument invoice;
        readonly XPathNavigator navigator;
        TextFrame addressFrame;
        TextFrame PriceFrame;
        MigraDoc.DocumentObjectModel.Tables.Table table;

        private Order order { get; set; }
        public Invoice(Order order)
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
                using (FileStream fs = File.Create(saveFileDialog.FileName))
                {
                    //if (System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower() == "pdf")
                    //{
                    this.document = new Document();

                    this.document.Info.Title = "A sample invoice";
                    this.document.Info.Subject = "Demonstrates how to create an invoice.";
                    this.document.Info.Author = "Stefan Lange";

                    DefineStyle();

                    CreatePage();

                    FillContent();

                    MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "MigraDoc.mdddl");

                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    PdfDocumentRenderer renderer = new PdfDocumentRenderer(false, PdfSharp.Pdf.PdfFontEmbedding.Always);
                    renderer.Document = document;


                    renderer.RenderDocument();

                    Thread.Sleep(5000);
                    // Save the document...
                    string filename = saveFileDialog.FileName;
                    renderer.PdfDocument.Save(filename);
                    //}
                }

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
            priceParagraph.AddText($"Cena całkowita {order.OrderAmount}");
            paragraph.AddLineBreak();
            priceParagraph.AddText($"Rodzaj przesyłki {order.OrderOption}");
            paragraph.AddLineBreak();
            priceParagraph.AddText($"Cena z przesyłką {order.Amount}");
            paragraph.AddLineBreak();


            foreach (var orderItem in order.Ordered)
            {
                Row row = new Row();
                row.TopPadding = 1.5;
                row.Cells[0].Shading.Color = TableGray;
                row.Cells[0].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[1].Shading.Color = TableGray;
                row.Cells[2].Shading.Color = TableGray;
                row.Cells[3].Shading.Color = TableGray;

                row.Cells[0].AddParagraph(orderItem.Product.Name);
                row.Cells[1].AddParagraph(orderItem.Count.ToString());
                row.Cells[2].AddParagraph(orderItem.Product.Price.ToString());
                row.Cells[3].AddParagraph(orderItem.Amount.ToString());

                this.table.SetEdge(0, this.table.Rows.Count - 1, 4, 1, Edge.Box, BorderStyle.Single, 0.75);
            }

        }


        private void CreatePage()
        {
            // Each MigraDoc document needs at least one section.
            MigraDoc.DocumentObjectModel.Section section = this.document.AddSection();

            // Put a logo in the header
            Image image = section.Headers.Primary.AddImage("../Image_Icons/logo1.png");
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Right;
            image.WrapFormat.Style = WrapStyle.Through;

            // Create footer
            MigraDoc.DocumentObjectModel.Paragraph paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText(">ul.Pralki 5, 13-342 Lodówka");
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Create the text frame for the address
            this.addressFrame = section.AddTextFrame();
            this.addressFrame.Height = "3.0cm";
            this.addressFrame.Width = "7.0cm";
            this.addressFrame.Left = ShapePosition.Left;
            this.addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.addressFrame.Top = "5.0cm";
            this.addressFrame.RelativeVertical = RelativeVertical.Page;

            this.PriceFrame = section.AddTextFrame();
            this.addressFrame.Height = "3.0cm";
            this.addressFrame.Width = "7.0cm";
            this.addressFrame.Left = ShapePosition.Right;
            this.addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.addressFrame.Top = "5.0cm";
            this.addressFrame.RelativeVertical = RelativeVertical.Page;

            // Put sender in address frame
            paragraph = addressFrame.AddParagraph("ul.Pralki 5, 13-342 Lodówka");
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 7;
            paragraph.Format.SpaceAfter = 3;

            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "8cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText("INVOICE", TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddText("Białystok, ");
            paragraph.AddDateField("dd.MM.yyyy");

            // Create the item table
            this.table = section.AddTable();
            this.table.Style = "Table";
            this.table.Borders.Color = TableBorder;
            this.table.Borders.Width = 0.25;
            this.table.Borders.Left.Width = 0.5;
            this.table.Borders.Right.Width = 0.5;
            this.table.Rows.LeftIndent = 0;

            // Before you can add a row, you must define the columns
            Column column = this.table.AddColumn("1cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = this.table.AddColumn("2.5cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = this.table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = this.table.AddColumn("3.5cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = this.table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = this.table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = TableBlue;
            row.Cells[0].AddParagraph("Nazwa");
            row.Cells[0].Format.Font.Bold = false;
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

