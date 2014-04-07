using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PicSim.UI.Views
{
    public static class AvalonEditHelpers
    {
        #region TextBinding Attached Dependency Property

        public static string GetTextBinding(DependencyObject obj)
        {
            return (string)obj.GetValue(TextBindingProperty);
        }

        public static void SetTextBinding(DependencyObject obj, string value)
        {
            obj.SetValue(TextBindingProperty, value);
        }

        // Using a DependencyProperty as the backing store for TextBinding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBindingProperty =
            DependencyProperty.RegisterAttached("TextBinding", typeof(string), typeof(AvalonEditHelpers), new PropertyMetadata(new PropertyChangedCallback(OnTextBindingChanged)));

        private static void OnTextBindingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textEditor = (TextEditor)d;
            textEditor.Document = new TextDocument((string)e.NewValue);
        }
        
        #endregion
    }
}
