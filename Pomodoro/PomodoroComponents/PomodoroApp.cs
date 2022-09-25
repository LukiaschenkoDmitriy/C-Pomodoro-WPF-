using System.Windows;
using System.Windows.Controls;
using System;
using Pomodoro.NotificationComponents;

namespace Pomodoro.PomodoroComponents
{
    internal sealed class PomodoroApp : TabControl
    {
        private PomodoroBase[] _pomodoroWindows = new PomodoroBase[3];
        private int[] _patternSwitchMode = new int[] { 0, 2, 0, 1, 0, 2 };
        private int _currentPatternPosition = 0;

        public PomodoroApp(ResourceDictionary resources)
        {
            ControlTemplate timerTextTemplate = resources["timerTemplate"] as ControlTemplate;
            ControlTemplate timerButtonActiveTemplate = resources["buttonActiveTemplate"] as ControlTemplate;
            ControlTemplate timerButtonTemplateShortBreak = resources["buttonShortBreakTemplate"] as ControlTemplate;
            ControlTemplate timerButtonTemplateLongBreak = resources["buttonLongBreakTemplate"] as ControlTemplate;

            TimeSpan activeTime = new(0, 25, 0);
            TimeSpan shortbreakTime = new(0, 5, 0);
            TimeSpan longbreakTime = new(0, 15, 0);

            _pomodoroWindows[0] = new PomodoroActive(activeTime);
            _pomodoroWindows[1] = new PomodoroShortBreak(shortbreakTime);
            _pomodoroWindows[2] = new PomodoroLongBreak(longbreakTime);

            _pomodoroWindows[0].ButtonTemplate = timerButtonActiveTemplate;
            _pomodoroWindows[1].ButtonTemplate = timerButtonTemplateShortBreak;
            _pomodoroWindows[2].ButtonTemplate = timerButtonTemplateLongBreak;

            PomodoroNotifications pomodoroNotifications = new(activeTime, shortbreakTime, longbreakTime);

            _pomodoroWindows[0].TimerTickEvent += pomodoroNotifications.PomodoroActiveNotifications;
            _pomodoroWindows[1].TimerTickEvent += pomodoroNotifications.PomodoroShortBreakNotifications;
            _pomodoroWindows[2].TimerTickEvent += pomodoroNotifications.PomodoroLongBreakNotifications;

            foreach (object obj in _pomodoroWindows)
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
            _pomodoroWindows[_patternSwitchMode[_currentPatternPosition]].InvokeTimerTickEvent();
        }
    }
}
