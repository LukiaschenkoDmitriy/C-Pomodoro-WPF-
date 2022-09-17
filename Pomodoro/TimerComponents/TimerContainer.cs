using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pomodoro.Components
{
    internal enum Put
    {
        Row, Column
    }

    internal class TimerContainer: Grid
    {
        public TimerContainer()
        {
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            ColumnDefinitions.Add(new ColumnDefinition());
        }

        public void PutElement(UIElement element, int id, Put position)
        {
            if (!Children.Contains(element)) Children.Add(element);
            if (position == Put.Row) Grid.SetRow(element, id);
            else Grid.SetColumn(element, id);
        }
    }
}
