using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using System.Xaml.Permissions;
using System.Xml.Linq;
using Pomodoro.PomodoroComponents;

namespace Pomodoro
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PomodoroApp pomodoroApp = new PomodoroApp(Resources);
            grid.Children.Add(pomodoroApp);
        }
    }
}
