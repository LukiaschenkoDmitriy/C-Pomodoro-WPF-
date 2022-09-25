using System;
using System.Windows.Media;

namespace Pomodoro.PomodoroComponents
{
    internal sealed class PomodoroLongBreak : PomodoroBase
    {
        public PomodoroLongBreak(TimeSpan time) : base("Long Break", time)
        {
            _container.Background = new SolidColorBrush(Color.FromRgb(52, 116, 235));
        }
    }
}
