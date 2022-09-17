using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pomodoro.Components
{
    internal sealed class TimerButton: CheckBox
    {
        public TimerButton()
        {
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalAlignment = HorizontalAlignment.Center;

            FontSize = 100;

            Width = 500;
            Height = 150;

            Content = "Start";
        }
    }
}
