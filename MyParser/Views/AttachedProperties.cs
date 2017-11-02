using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyParser.Views
{
    class ListboxAttachedProperties
    {
        public static readonly DependencyProperty autoScrollProperty =
            DependencyProperty.RegisterAttached("AutoScroll", typeof(bool), typeof(ListBox), new PropertyMetadata(false));

        public static void SetAutoScroll(ListBox element, bool value)
        {
            element.SetValue(autoScrollProperty, value);
            if (value)
            {
                element.SelectionChanged += Element_SelectionChanged;
            }
            else
            {
                element.SelectionChanged -= Element_SelectionChanged;
            }
        }        

        public static bool GetAutoScroll(ListBox element)
        {
            return (bool)element.GetValue(autoScrollProperty);            
        }

        private static void Element_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)sender;
            listBox.ScrollIntoView(listBox.SelectedItem);
        }
    }
}
