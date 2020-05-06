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
using ControlzEx;
using MaterialDesignThemes.Wpf;

namespace trackpuls.Views
{
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : UserControl
    {
        public TaskView()
        {
            InitializeComponent();
        }
        private void BtnSelectCompany_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            PackIcon plybtn = button.FindName("icon") as PackIcon;
            if(plybtn.Kind == PackIconKind.PlayCircle) {

                plybtn.Kind = PackIconKind.PauseCircle;
                plybtn.Foreground = Brushes.Red;
            }
            else {
                plybtn.Kind = PackIconKind.PlayCircle;
                plybtn.Foreground = Brushes.Blue;
            }
        }
    }
}
