using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Pomodoro.Components
{
    public static class TimeSpanInstruments
    {
        public static string ConverteTimeSpanToClockStringFormat(TimeSpan time)
        {
            string minutes = (time.Minutes.ToString().Length == 1) ? "0" + time.Minutes.ToString() : time.Minutes.ToString();
            string seconds = (time.Seconds.ToString().Length == 1) ? "0" + time.Seconds.ToString() : time.Seconds.ToString();

            return $"{minutes}:{seconds}";
        }
    }

    internal sealed class Timer: Label
    {
        #region Private Fields
        private DispatcherTimer _dispatcherTime = new DispatcherTimer();
        private TimeSpan _constTime = new TimeSpan(0, 20, 0);

        private TimeSpan _currentTime;

        public delegate void NotifyTimerStopHandler();
        public event NotifyTimerStopHandler NotifyTimerStopEvent;

        public delegate void TimerDelegate(TimeSpan timer);
        public event TimerDelegate TimerTickEvent;

        #endregion
        #region ctor
        public Timer(TimeSpan time)
        {
            _constTime = time;
            _currentTime = _constTime;

            InitializingDispatcherTimer();
            InitializingContents();
        }
        #endregion
        #region Property
        public TimeSpan Time
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
                Content = TimeSpanInstruments.ConverteTimeSpanToClockStringFormat(value);
            }
        }
        #endregion
        #region Event Adapter
        public event EventHandler DispatherTimerTick
        {
            add { _dispatcherTime.Tick += value; }
            remove { _dispatcherTime.Tick -= value; }
        }
        #endregion
        #region DispatcherTimer
        #region Methods
        public void InvokeTimerTickEvent() => TimerTickEvent.Invoke(Time);
        public void Start() => _dispatcherTime.Start();
        public void Stop() => _dispatcherTime.Stop();
        public void Reset()
        {
            _dispatcherTime.Stop();
            Time = _constTime;
        }
        private void InitializingDispatcherTimer()
        {
            _dispatcherTime.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTime.Tick += SingleSubtraction;
        }
        #endregion
        #region Event Handlers
        private void SingleSubtraction(object? sender, EventArgs e) 
        {
            Time -= new TimeSpan(0, 0, 1);

            if (Time.Minutes == 0 && Time.Seconds == 0)
            {
                NotifyTimerStopEvent.Invoke();
            }

            TimerTickEvent?.Invoke(Time);
        } 
        #endregion
        #endregion
        #region Label
        private void InitializingContents()
        {
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;

            FontSize = 200;

            Content = TimeSpanInstruments.ConverteTimeSpanToClockStringFormat(_currentTime);
        }
        #endregion
    }
}
