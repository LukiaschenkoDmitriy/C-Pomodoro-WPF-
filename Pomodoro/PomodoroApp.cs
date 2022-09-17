using System.Windows;
using System.Windows.Controls;

namespace Pomodoro
{
    internal sealed class PomodoroApp : TabControl
    {
        private PomodoroBase[] _pomodoroWindows = new PomodoroBase[3];
        private int[] _patternSwitchMode = new int[] { 0, 1, 0, 1, 0, 2 };
        private int _currentPatternPosition = 0;

        public PomodoroApp(ResourceDictionary resources)
        {
            ControlTemplate timerTextTemplate = resources["timerTemplate"] as ControlTemplate;
            ControlTemplate timerButtonActiveTemplate = resources["buttonActiveTemplate"] as ControlTemplate;
            ControlTemplate timerButtonTemplateShortBreak = resources["buttonShortBreakTemplate"] as ControlTemplate;
            ControlTemplate timerButtonTemplateLongBreak = resources["buttonLongBreakTemplate"] as ControlTemplate;

            _pomodoroWindows[0] = new PomodoroActive(new System.TimeSpan(0, 20, 0));
            _pomodoroWindows[1] = new PomodoroShortBreak(new System.TimeSpan(0, 5, 0));
            _pomodoroWindows[2] = new PomodoroLongBreak(new System.TimeSpan(0, 15, 0));

            _pomodoroWindows[0].ButtonTemplate = timerButtonActiveTemplate;
            _pomodoroWindows[1].ButtonTemplate = timerButtonTemplateShortBreak;
            _pomodoroWindows[2].ButtonTemplate = timerButtonTemplateLongBreak;

            foreach(object obj in _pomodoroWindows)
            {
                PomodoroBase downCastObj = (PomodoroBase)obj;

                downCastObj.TimerTemplate = timerTextTemplate;
                downCastObj.NotifyTimerStopEvent += SwitchToOtherPomodoro;

                Items.Add(obj);
            }
        }

        //Switch pomodoro modes
        private void SwitchToOtherPomodoro()
        {
            _currentPatternPosition++;
            if (_currentPatternPosition == _patternSwitchMode.Length) { _currentPatternPosition = 0; }

            SelectedIndex = _patternSwitchMode[_currentPatternPosition];
            ((PomodoroBase)SelectedItem).TimerStart();
        }
    }
}
