using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Configuration;
using System.Windows.Media.Imaging;
using System.IO;

namespace Logoff_ScreenSaver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer Timer = new DispatcherTimer();   //Timer
        public int LogOffAfter;                                 //Time to wait before logoff
        public int ElapsedTime = 0;                             //Elapsed time
        public Point MousePoint { get; set; }                   //Mouse pointer starting location

        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\{ConfigurationManager.AppSettings.Get("LogoImage")}"))
            {
                BitmapImage bmi = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}\\{ConfigurationManager.AppSettings.Get("LogoImage")}"));
                Logo.Source = bmi;
            }

            Int32.TryParse(ConfigurationManager.AppSettings.Get("LogOffTimeInSeconds"), out LogOffAfter);   //Parse logoff wait time.
            SignOutTimer.Content = LogOffAfter + " Seconds";                                                //Write wait time to label
            MousePoint = MouseInterop.GetCursorPosition();                                                  //Get cursor position
            System.Diagnostics.Debug.WriteLine("start {0}", MousePoint);

            //Check for a button press
            KeyDown += (o, args) =>
            {
                Timer.Stop();
                Application.Current.Shutdown();
            };
            
            //Check for mouse movement.
            MouseMove += (o, args) =>
            {
                var currentMoustPoint = Mouse.GetPosition(this);
                System.Diagnostics.Debug.WriteLine("end {0}", currentMoustPoint);
                var distance = Math.Sqrt(Math.Pow(currentMoustPoint.X - MousePoint.X, 2) + Math.Pow(currentMoustPoint.Y - MousePoint.Y, 2));
                System.Diagnostics.Debug.WriteLine(distance);
                if (distance > 5)
                {
                    Timer.Stop();
                    Application.Current.Shutdown();
                }
            };

            //Update timer and close if 0
            Timer.Tick += (o, args) =>
            {
                ElapsedTime++;
                SignOutTimer.Content = (LogOffAfter - ElapsedTime) + " Seconds";
                if (LogOffAfter - ElapsedTime <= 0)
                {
                    Timer.Stop();
                    LogoffInterop.WindowsLogOff();
                }           
            };

            //1 second interval
            Timer.Interval = new TimeSpan(0, 0, 1);
            //start timer
            Timer.Start();
        }
    }
}
