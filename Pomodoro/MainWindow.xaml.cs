using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Pomodoro
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new PomodoroApp(
                pomodoroModeGrid, 
                pomodoroTimer, pomodoroCheck, 
                new TimeSpan(0,0,30), new TimeSpan(0,0,15),
                Resources);
        }
    }

    internal enum PomodoroMode
    {
        Active, ShortBreak, LongBreak
    }

    internal class PomodoroApp
    {
        //Changed pomodoro background pomodoroModeGrid

        private PomodoroMode _pomodoroMode = PomodoroMode.Active;
        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        private TimeSpan _breakTime;
        private TimeSpan _activeTime;
        private TimeSpan _time;

        private Label _pomodoroTimer;
        private Grid _pomodorGrid;
        private CheckBox _pomodoroCheck;
        private ResourceDictionary _resourcesDictionary;

        public PomodoroApp(Grid pomodoroGrid,
            Label pomodoroTimer,CheckBox pomodoroCheck, 
            TimeSpan time, TimeSpan breakTime, ResourceDictionary resourcesDictionary)
        {
            _pomodoroTimer = pomodoroTimer;
            _pomodoroCheck = pomodoroCheck;
            _pomodorGrid = pomodoroGrid;
            _resourcesDictionary = resourcesDictionary;

            _time = time;

            _breakTime = breakTime;
            _activeTime = time;

            _pomodoroCheck.Checked += CheckedHandler;
            _pomodoroCheck.Unchecked += UncheckedHandler;

            _dispatcherTimer.Tick += UpdateTime;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private void UpdateTime(object? sender, EventArgs e)
        {
            _time -= new TimeSpan(0, 0, 1);

            SwitcherMode();

            string minutes = _time.Minutes.ToString();
            string seconds = _time.Seconds.ToString();

            if (minutes.Length == 1) minutes = "0" + minutes;
            if (seconds.Length == 1) seconds = "0" + seconds;

            _pomodoroTimer.Content = $"{minutes}:{seconds}";
        }

        private void SwitchToActiveMode()
        {
            _pomodorGrid.Background = new SolidColorBrush(Color.FromRgb(237, 59, 59));
            _pomodoroCheck.Template = _resourcesDictionary["startButtonTemplateActive"] as ControlTemplate;
            _time = _activeTime;
            _pomodoroMode = PomodoroMode.Active;
        }

        private void SwitchToBreakMode()
        {
            _pomodorGrid.Background = new SolidColorBrush(Color.FromRgb(52, 192, 235));
            _pomodoroCheck.Template = _resourcesDictionary["startButtonTemplateShortBreak"] as ControlTemplate;
            _time = _breakTime;
            _pomodoroMode = PomodoroMode.ShortBreak;
        }

        private void SwitcherMode()
        {
            if (_time.Minutes == 0 && _time.Seconds == 0)
            {
                if (_pomodoroMode == PomodoroMode.Active) SwitchToBreakMode();
                else if (_pomodoroMode == PomodoroMode.ShortBreak) SwitchToActiveMode();
                _dispatcherTimer.Stop();
                _pomodoroCheck.IsChecked = false;
            }
        }

        private void UncheckedHandler(object sender, RoutedEventArgs e)
        {
            _pomodoroCheck.Content = "Start";
            _dispatcherTimer.Stop();
        }

        private void CheckedHandler(object sender, RoutedEventArgs e)
        {
            _pomodoroCheck.Content = "Stop";
            _dispatcherTimer.Start();
        }
    }
}
