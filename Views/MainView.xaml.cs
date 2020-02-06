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
using System.Windows.Shapes;
using trackpuls.ViewModels;

namespace trackpuls.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            
        }
        //-------------Window Button------------//

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CloseBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Close.Source = new BitmapImage(new Uri(@"../Assets/1+.png", UriKind.Relative));
        }
        private void CloseBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            Close.Source = new BitmapImage(new Uri(@"../Assets/1.png", UriKind.Relative));
        }
        private void MinimiseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void MinimiseBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Minimise.Source = new BitmapImage(new Uri(@"../Assets/2+.png", UriKind.Relative));
        }
        private void MinimiseBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            Minimise.Source = new BitmapImage(new Uri(@"../Assets/2.png", UriKind.Relative));
        }
        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void PlusBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Plus.Source = new BitmapImage(new Uri(@"../Assets/3+.png", UriKind.Relative));
        }
        private void PlusBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            Plus.Source = new BitmapImage(new Uri(@"../Assets/3.png", UriKind.Relative));
        }
        //-------------Window Button------------//
    }
}
