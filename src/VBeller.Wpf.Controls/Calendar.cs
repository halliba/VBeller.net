using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VBeller.Wpf.Controls
{
    public class Calendar : System.Windows.Controls.Calendar
    {
        public static readonly DependencyProperty InvertedScrollProperty;
        public static readonly DependencyProperty IsScrollEnabledProperty;

        public bool InvertedScroll
        {
            get => (bool)GetValue(InvertedScrollProperty);
            set => SetValue(InvertedScrollProperty, value);
        }

        public bool IsScrollEnabled
        {
            get => (bool)GetValue(IsScrollEnabledProperty);
            set => SetValue(IsScrollEnabledProperty, value);
        }

        public Calendar()
        {
            MouseWheel += OnMouseWheel;
        }

        static Calendar()
        {
            InvertedScrollProperty = DependencyProperty.Register(nameof(InvertedScroll),
                typeof(bool), typeof(Calendar), new FrameworkPropertyMetadata(false));
            IsScrollEnabledProperty = DependencyProperty.Register(nameof(IsScrollEnabled),
                typeof(bool), typeof(Calendar), new FrameworkPropertyMetadata(false));
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs mouseWheelEventArgs)
        {
            if (!IsScrollEnabled) return;

            var up = InvertedScroll ? -1 : 1;
            var down = InvertedScroll ? 1 : -1;

            var delta = mouseWheelEventArgs.Delta;
            switch (DisplayMode)
            {
                case CalendarMode.Month:
                    DisplayDate = delta < 0 ? DisplayDate.AddMonths(up) : DisplayDate.AddMonths(down);
                    break;
                case CalendarMode.Year:
                    DisplayDate = delta < 0 ? DisplayDate.AddYears(up) : DisplayDate.AddYears(down);
                    break;
                case CalendarMode.Decade:
                    DisplayDate = delta < 0 ? DisplayDate.AddYears(up * 10) : DisplayDate.AddYears(down * 10);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}