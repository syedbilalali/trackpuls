using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace trackpuls.Views
{
    /// <summary>
    /// Interaction logic for CompanyView.xaml
    /// </summary>
    public partial class CompanyView : UserControl
    {
        public CompanyView()
        {
            InitializeComponent();
            List<Company> items = new List<Company>();
            items.Add(new Company() { Name = "John Doe", ID = "42" });
            items.Add(new Company() { Name = "Jane Doe", ID = "39" });
            items.Add(new Company() { Name = "Sammy Doe",ID = "13" });
            items.Add(new Company() { Name = "Syed Bilal Ali", ID = "13" });
            items.Add(new Company() { Name = "Syed Aman Ali", ID = "13" });
            ChooseCompany.ItemsSource = items;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello WOrld ");
        }
        private System.Drawing.Image GenerateAvtarImage(String text, Font font, System.Drawing.Color textColor, System.Drawing.Color backColor, string filename)
        {
            //first, create a dummy bitmap just to get a graphics object  
            System.Drawing.Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be  
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object  
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size  
            img = new Bitmap(110, 110);

            drawing = Graphics.FromImage(img);

            //paint the background  
            drawing.Clear(backColor);

            //create a brush for the text  
            System.Drawing.Brush textBrush = new SolidBrush(textColor);

            //drawing.DrawString(text, font, textBrush, 0, 0);  
            drawing.DrawString(text, font, textBrush, new System.Drawing.Rectangle(-2, 20, 200, 110));

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();
            return img;
        }
    }
    public class Company {
        
        public string ID { get; set; }
        public string Name { get; set;  }
    }
    
}
