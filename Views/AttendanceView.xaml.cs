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
    /// Interaction logic for AttendanceView.xaml
    /// </summary>
    public partial class AttendanceView : UserControl
    {
        public AttendanceView()
        {
            InitializeComponent();
            List<Company> items = new List<Company>();
            items.Add(new Company() { Name = "John Doe", ID = "42" });
            items.Add(new Company() { Name = "Jane Doe", ID = "39" });
            ChooseCompany.ItemsSource = items;
        }
    }
    
}
