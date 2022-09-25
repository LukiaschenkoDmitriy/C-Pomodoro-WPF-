using System;
using System.Windows.Media;

namespace Pomodoro.PomodoroComponents
{
    internal sealed class PomodoroActive : PomodoroBase
    {
        public PomodoroActive(TimeSpan time) : base("Pomodoro", time)
        {
            _container.Background = new SolidColorBrush(Color.FromRgb(237, 59, 59));
        }
    }
}
