using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Pomodoro.NotificationComponents
{
    internal class PomodoroNotifications
    {
        private TimeSpan _active;
        private TimeSpan _shortbreak;
        private TimeSpan _longBreak;

        public PomodoroNotifications(TimeSpan active, TimeSpan shortbreak, TimeSpan longbreak)
        {
            (_active, _shortbreak, _longBreak) = (active, shortbreak, longbreak);
        }

        internal void PomodoroActiveNotifications(TimeSpan time)
        {
            if (time.Minutes == 4 && time.Seconds == 0)
            {
                new ToastContentBuilder()
                                .AddText("Left 4 minutes!")
                                .AddInlineImage(new Uri(Path.Combine(Environment.CurrentDirectory, @"assets\notificationImages\pomodoroActive", "pomodoro4minutes.png")))
                                .Show();
            }

            if (time.Minutes == 0 && time.Seconds == 1)
            {
                new ToastContentBuilder()
                .AddText("Pomodoro time is over!")
                .AddInlineImage(new Uri(Path.Combine(Environment.CurrentDirectory, @"assets\notificationImages\pomodoroActive", "pomodoroend.png")))
                .Show();
            }

            if (time.Minutes == 1 && time.Seconds == 0)
            {
                new ToastContentBuilder()
                                .AddText("Left 1 minutes!")
                                .AddInlineImage(new Uri(Path.Combine(Environment.CurrentDirectory, @"assets\notificationImages\pomodoroActive", "pomodoroLast1minute.png")))
                                .Show();
            }
        }

        internal void PomodoroShortBreakNotifications(TimeSpan time)
        {
            if (time.Minutes == 0 && time.Seconds == 1)
            {
                new ToastContentBuilder()
                .AddText("Pomodoro time is over!")
                .AddInlineImage(new Uri(Path.Combine(Environment.CurrentDirectory, @"assets\notificationImages\shortBreak", "shortbreakend.png")))
                .Show();
            }

            if (time.Minutes == 1 && time.Seconds == 0)
            {
                new ToastContentBuilder()
                                .AddText("Left 1 minutes!")
                                .AddInlineImage(new Uri(Path.Combine(Environment.CurrentDirectory, @"assets\notificationImages\shortBreak", "shortbreakLast1minute.png")))
                                .Show();
            }
        }

        internal void PomodoroLongBreakNotifications(TimeSpan time)
        {
            if (time.Minutes == 0 && time.Seconds == 1)
            {
                new ToastContentBuilder()
                .AddText("Pomodoro time is over!")
                .AddInlineImage(new Uri(Path.Combine(Environment.CurrentDirectory, @"assets\notificationImages\longbreak", "longbreakend.png")))
                .Show();
            }

            if (time.Minutes == 1 && time.Seconds == 0)
            {
                new ToastContentBuilder()
                                .AddText("Left 1 minutes!")
                                .AddInlineImage(new Uri(Path.Combine(Environment.CurrentDirectory, @"assets\notificationImages\longbreak", "longbreakLast1minute.png")))
                                .Show();
            }

            if (time.Minutes == 2 && time.Seconds == 0)
            {
                new ToastContentBuilder()
                                .AddText("Left 2 minutes!")
                                .AddInlineImage(new Uri(Path.Combine(Environment.CurrentDirectory, @"assets\notificationImages\longbreak", "pomodorolongbreak2minutes.png")))
                                .Show();
            }
        }
    }
}
