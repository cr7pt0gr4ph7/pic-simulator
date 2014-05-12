using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace PicSim.UI.Behaviors
{
    public class AddBreakpointMarginBehavior : Behavior<TextEditor>
    {
        #region Breakpoints dependency property

        public ICollection<int> Breakpoints
        {
            get { return (ICollection<int>)GetValue(BreakpointsProperty); }
            set { SetValue(BreakpointsProperty, value); }
        }

        public static readonly DependencyProperty BreakpointsProperty =
            DependencyProperty.Register("Breakpoints", typeof(ICollection<int>), typeof(AddBreakpointMarginBehavior), new PropertyMetadata(new PropertyChangedCallback(Breakpoints_Changed)));

        private static void Breakpoints_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (AddBreakpointMarginBehavior)d;

            var oldValue = e.OldValue as INotifyCollectionChanged;
            var newValue = e.NewValue as INotifyCollectionChanged;

            if (oldValue != null) newValue.CollectionChanged -= self.Breakpoints_CollectionChanged;
            if (newValue != null) newValue.CollectionChanged += self.Breakpoints_CollectionChanged;

            self.m_breakpointMargin.InvalidateVisual();
        }

        private void Breakpoints_CollectionChanged(object target, NotifyCollectionChangedEventArgs e)
        {
            m_breakpointMargin.InvalidateVisual();
        }

        #endregion

        private BreakpointMargin m_breakpointMargin;

        protected override void OnAttached()
        {
            m_breakpointMargin = new BreakpointMargin(() => Breakpoints);
            AssociatedObject.TextArea.LeftMargins.Insert(0, m_breakpointMargin);

            // HACK Fix interaction with ShowLineNumbers
            AssociatedObject.TextArea.LeftMargins.CollectionChanged += LeftMargins_CollectionChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextArea.LeftMargins.CollectionChanged -= LeftMargins_CollectionChanged;
            AssociatedObject.TextArea.LeftMargins.Remove(m_breakpointMargin);
            m_breakpointMargin = null;
        }

        void LeftMargins_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // HACK Fix the interaction with ShowLineNumbers
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var leftMargins = AssociatedObject.TextArea.LeftMargins;
                int breakpointMarginIndex = leftMargins.IndexOf(m_breakpointMargin);

                if (e.NewItems.Contains(m_breakpointMargin)) return;
                if (e.NewStartingIndex >= breakpointMarginIndex) return;

                Dispatcher.InvokeAsync(() => {
                    int current_breakpointMarginIndex = leftMargins.IndexOf(m_breakpointMargin);
                    if (current_breakpointMarginIndex > 0)
                    {
                        leftMargins.Move(current_breakpointMarginIndex, 0);
                    }
                });
            }
        }

        private class BreakpointMargin : AbstractMargin
        {
            private const int MARGIN_WIDTH = 15;

            private readonly Func<ICollection<int>> m_breakpoints;
            private readonly Brush m_brush;

            public BreakpointMargin(Func<ICollection<int>> breakpoints)
            {
                Ensure.ArgumentNotNull(breakpoints, "breakpoints");
                m_breakpoints = breakpoints;
                m_brush = Brushes.Red;
            }

            protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
            {
                // accept clicks even when clicking on the background
                return new PointHitTestResult(this, hitTestParameters.HitPoint);
            }

            protected override Size MeasureOverride(Size availableSize)
            {
                return new Size(MARGIN_WIDTH, 0);
            }

            protected override void OnDocumentChanged(ICSharpCode.AvalonEdit.Document.TextDocument oldDocument, ICSharpCode.AvalonEdit.Document.TextDocument newDocument)
            {
                base.OnDocumentChanged(oldDocument, newDocument);
                this.InvalidateVisual();
            }

            protected override void OnTextViewChanged(ICSharpCode.AvalonEdit.Rendering.TextView oldTextView, ICSharpCode.AvalonEdit.Rendering.TextView newTextView)
            {
                if (oldTextView != null) oldTextView.VisualLinesChanged -= TextView_VisualLinesChanged;
                base.OnTextViewChanged(oldTextView, newTextView);
                if (newTextView != null) newTextView.VisualLinesChanged += TextView_VisualLinesChanged;
                InvalidateVisual();
            }

            private void TextView_VisualLinesChanged(object sender, EventArgs e)
            {
                InvalidateVisual();
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                var textView = this.TextView;
                var renderSize = this.RenderSize;
                var breakpoints = this.m_breakpoints();

                if (breakpoints != null && textView != null && textView.VisualLinesValid)
                {
                    foreach (var line in textView.VisualLines)
                    {
                        var lineNo = line.FirstDocumentLine.LineNumber;

                        if (breakpoints.Contains(lineNo))
                        {
                            var diameter = textView.DefaultLineHeight - 2;
                            var xCoord = diameter / 2;
                            var yCoord = line.GetTextLineVisualYPosition(line.TextLines[0], VisualYPosition.TextMiddle) - textView.VerticalOffset;
                            var position = new Point(xCoord, yCoord);

                            drawingContext.DrawEllipse(m_brush, null, position, diameter / 2, diameter / 2);
                        }
                    }
                }
            }

            protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
            {
                base.OnMouseLeftButtonDown(e);
                var textView = this.TextView;

                if (!e.Handled && textView != null)
                {
                    e.Handled = true;
                    var clickedLine = textView.GetVisualLineFromVisualTop(e.GetPosition(textView).Y + textView.VerticalOffset);
                    if (clickedLine == null) return;
                    var lineNo = clickedLine.FirstDocumentLine.LineNumber;

                    var breakpoints = m_breakpoints();
                    if (breakpoints == null) return;

                    if (breakpoints.Contains(lineNo))
                        breakpoints.Remove(lineNo);
                    else
                        breakpoints.Add(lineNo);
                }
            }
        }
    }
}
