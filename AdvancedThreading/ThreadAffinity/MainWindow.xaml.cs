using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ThreadAffinity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Thread affinity means that the thread that instantiates an object is the only thread that is allowed to access its members.
        /// A dependency object has thread affinity in .net.  Also UI, that is why we need to implement a dispatcher
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Task.Factory.StartNew(ChangeHelloWorld);
        }

        private void ChangeHelloWorld()
        {
            Thread.Sleep(2500);
            UpdateHelloWorld("Hello new world");
        }

        private void UpdateHelloWorld(string helloNewWorld)
        {
            //will throw InvalidOperationException as another thread owns it
            //txtMessage.Text = "Hello papi";
            Dispatcher.Invoke(() => txtMessage.Text = helloNewWorld);
        }
    }
}
