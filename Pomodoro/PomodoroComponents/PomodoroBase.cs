using Pomodoro.Components;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Pomodoro.PomodoroComponents
{
    internal abstract class PomodoroBase : TabItem
    {
        protected Timer _timer;
        protected TimerButton _button;
        protected TimerContainer _container;

        private ControlTemplate _timerTemplate;
        public ControlTemplate TimerTemplate
        {
            get { return _timerTemplate; }
            set
            {
                _timerTemplate = value;
                _timer.Template = value;
            }
        }

        private ControlTemplate _buttonTemplate;
        public ControlTemplate ButtonTemplate
        {
            get { return _buttonTemplate; }
            set
            {
                _buttonTemplate = value;
                _button.Template = value;
            }
        }

        //Adapter
        public event Timer.NotifyTimerStopHandler NotifyTimerStopEvent
        {
            add { _timer.NotifyTimerStopEvent += value; }
            remove { _timer.NotifyTimerStopEvent -= value; }
        }

        public event Timer.TimerDelegate TimerTickEvent
        {
            add { _timer.TimerTickEvent += value; }
            remove { _timer.TimerTickEvent -= value; }
        }

        public void InvokeTimerTickEvent() => _timer.InvokeTimerTickEvent();

        public PomodoroBase(object header, TimeSpan time)
        {
            Header = header;

            _timer = new Timer(time);
            _button = new TimerButton();
            _container = new TimerContainer();

            _button.Checked += CheckedButtonHandler;
            _button.Unchecked += UncheckedButtonHandler;
            _timer.NotifyTimerStopEvent += NotifyTimerStopEventHandler;

            SealTimerComponents();

            Content = _container;
        }

        public void TimerStop() => _button.IsChecked = false;
        public void TimerStart() => _button.IsChecked = true;
        public void TimerReset() => NotifyTimerStopEventHandler();

        protected virtual void NotifyTimerStopEventHandler()
        {
            _timer.Reset();
            _button.IsChecked = false;
        }

        protected virtual void CheckedButtonHandler(object sender, RoutedEventArgs e)
        {
            _timer.Start();
            _button.Content = "Stop";
        }
        protected virtual void UncheckedButtonHandler(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _button.Content = "Start";
        }

        private void SealTimerComponents()
        {
            _container.PutElement(_timer, 0, Put.Row);
            _container.PutElement(_timer, 0, Put.Column);

            _container.PutElement(_button, 1, Put.Row);
            _container.PutElement(_button, 0, Put.Column);
        }


    }
}
