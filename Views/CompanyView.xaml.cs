using System;
using System.Collections.Generic;
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
    }
    public class Company {
        
        public string ID { get; set; }
        public string Name { get; set;  }
    }
}
