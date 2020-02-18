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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using trackpuls.ViewModels;
using MaterialDesignThemes.Wpf;

namespace trackpuls.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private readonly Storyboard _storyboard;
        public MainView()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += MainView_MouseLeftButtonDown;
           

        }
        private void MainView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        //-------------Window Button------------//
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!AlreadyFaded)
            {
                AlreadyFaded = true;
             //   e.Cancel = true;
                var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(2));
                anim.Completed += new EventHandler(anim_Completed);
                this.BeginAnimation(UIElement.OpacityProperty, anim);
            }
            //this.Close();
            System.Windows.Application.Current.Shutdown();
           //x System.Windows.Application.Current.Exit();
        }
        private void CloseBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Close.Source = new BitmapImage(new Uri(@"../Assets/1+.ico", UriKind.Relative));
        }
        private void CloseBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            Close.Source = new BitmapImage(new Uri(@"../Assets/1.ico", UriKind.Relative));
        }
        private void MinimiseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void MinimiseBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Minimise.Source = new BitmapImage(new Uri(@"../Assets/2+.ico", UriKind.Relative));
        }
        private void MinimiseBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            Minimise.Source = new BitmapImage(new Uri(@"../Assets/2.ico", UriKind.Relative));
        }
        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void PlusBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Plus.Source = new BitmapImage(new Uri(@"../Assets/3+.ico", UriKind.Relative));
        }
        private void PlusBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            Plus.Source = new BitmapImage(new Uri(@"../Assets/3.ico", UriKind.Relative));
        }
        //-------------Window Button------------//
        private bool closeCompleted = false;


        private void FormFadeOut_Completed(object sender, EventArgs e)
        {
            closeCompleted = true;
          //   this.Close();
        }

        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{

        //    if (!closeCompleted)
        //    {
        //        FormFadeOut.Begin();
        //        e.Cancel = true;
        //    }
        //}
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AlreadyFaded = false;
        }

        bool AlreadyFaded;
        //private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    if (!AlreadyFaded)
        //    {
        //        AlreadyFaded = true;
        //        e.Cancel = true;
        //        var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(1));
        //        anim.Completed += new EventHandler(anim_Completed);
        //        this.BeginAnimation(UIElement.OpacityProperty, anim);
        //    }
        //}

        void anim_Completed(object sender, EventArgs e)
        {
            this.Close();
        }
        private void OnWindowStateChanged(object sender, EventArgs e)
        {
            MessageBox.Show(" On dbl click Window State  ");


        }
    }
}
