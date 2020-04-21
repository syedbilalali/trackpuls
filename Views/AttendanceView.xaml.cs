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
using trackpuls.Models;
using System.Windows.Threading;
using System.Windows.Interop;
using trackpuls.ViewModels;
using trackpuls.Helper;

namespace trackpuls.Views
{
    /// <summary>
    /// Interaction logic for AttendanceView.xaml
    /// </summary>
    public partial class AttendanceView : UserControl
    {
        public int logOffTime = 1;
        public AttendanceView()
        {
            InitializeComponent();
            DataContextChanged += DataContextChangedHandler;
         //   InitializeAutoLogoffFeature();

        }
        void DataContextChangedHandler(object sender, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = e.NewValue as AttendanceViewModel;

            if (viewModel != null)
            {
               // viewModel. += (sender, args) => { someMethod(); }
            }
        }
        private void InitializeAutoLogoffFeature()
        {
            Window data = Window.GetWindow(this);
            HwndSource windowSpecificOSMessageListener = HwndSource.FromHwnd(new WindowInteropHelper(data).Handle);
            windowSpecificOSMessageListener.AddHook(new HwndSourceHook(CallBackMethod));
            AutoLogOffHelper.LogOffTime = logOffTime;
            AutoLogOffHelper.MakeAutoLogOffEvent += new AutoLogOffHelper.MakeAutoLogOff(AutoLogOffHelper_MakeAutoLogOffEvent);
            AutoLogOffHelper.StartAutoLogoffOption();
           // string time = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
           // tblStatus.Text = "Timer is started at " + ": " + time;
        }
        void AutoLogOffHelper_MakeAutoLogOffEvent()
        {
         //   Logoff();
        }
        private IntPtr CallBackMethod(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            //  Listening OS message to test whether it is a user activity
            if ((msg >= 0x0200 && msg <= 0x020A) || (msg <= 0x0106 && msg >= 0x00A0) || msg == 0x0021)
            {
                AutoLogOffHelper.ResetLogoffTimer();
                string time = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
             //   tblStatus.Text = "Timer is reset on user activity at " + ": " + time;
             //   tblLastUserActivityTypeOSMessage.Text = "Last user activity type OS message the application considers " + ": 0x" + msg.ToString("X");
            }
            else
            {
            //    tblLastOSMessage.Text = "Last OS Message " + ":  0x" + msg.ToString("X");
                // For debugging purpose
                // If this auto logoff does not work for some user activity, you can detect the integer code of that activity  using the following line.
                //Then All you need to do is adding this integer code to the above if condition.
                System.Diagnostics.Debug.WriteLine(msg.ToString());
            }
            return IntPtr.Zero;
        }
    }
    
}
