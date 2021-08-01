using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using VirtualDesktop.hotkey;

namespace VirtualDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //InitializeComponent();
            Console.WriteLine("Starting here!");
            HotKeyManager.SetupSystemHook();
            HotKeyManager.AddHotKey(ModifierKeys.Alt, Key.Left, () => { staLeftSwitch(); });
            HotKeyManager.AddHotKey(ModifierKeys.Alt, Key.Right, () => { staRightSwitch(); });
            //Thread.Sleep(Timeout.Infinite);

            Closing += MainWindow_Closing;
            Console.WriteLine("Finished!");
        }

        private void staLeftSwitch()
        {
            var thread = new Thread(switchDesktopLeft);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void staRightSwitch()
        {
            var thread = new Thread(switchDesktopLeft);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void switchDesktopLeft(object state)
        {
            //File.WriteAllText(@"C:\Users\dev\Desktop\test.log", "LEFT FIRED!");
            bool wrapdesktops = true;
            if (wrapdesktops && (VirtualDesktop.Desktop.FromDesktop(VirtualDesktop.Desktop.Current) == 0)) { VirtualDesktop.Desktop.FromIndex(VirtualDesktop.Desktop.Count - 1).MakeVisible(); }
            else { VirtualDesktop.Desktop.Current.Left.MakeVisible(); }
            //Testing
            VirtualDesktop.Desktop.Current.SetForeground("Spotify");
        }

        private void switchDesktopRight(object state)
        {
            //File.WriteAllText(@"C:\Users\dev\Desktop\test.log", "RIGHT FIRED!");
            bool wrapdesktops = true;
            if (wrapdesktops && (VirtualDesktop.Desktop.FromDesktop(VirtualDesktop.Desktop.Current) == 0)) { VirtualDesktop.Desktop.FromIndex(VirtualDesktop.Desktop.Count + 1).MakeVisible(); }
            else { VirtualDesktop.Desktop.Current.Right.MakeVisible(); }
            //Testing
            VirtualDesktop.Desktop.Current.SetForeground("Spotify");
            
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e){
            HotKeyManager.ShutdownSystemHook();
        }
    }
}
