using System;
using System.Windows.Media;

namespace Pomodoro.PomodoroComponents
{
    internal sealed class PomodoroShortBreak : PomodoroBase
    {
        public PomodoroShortBreak(TimeSpan time) : base("Short Break", time)
        {
            _container.Background = new SolidColorBrush(Color.FromRgb(52, 192, 235));
        }
    }
}
