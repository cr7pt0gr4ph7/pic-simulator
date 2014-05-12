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
using System.Windows.Interactivity;
using System.Windows.Media;

namespace PicSim.UI.Behaviors
{
    public class AddBreakpointMarginBehavior : Behavior<TextEditor>
    {
        private BreakpointMargin m_breakpointMargin;

        protected override void OnAttached()
        {
            m_breakpointMargin = new BreakpointMargin();
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
            private const int DIAMETER = 8;

            protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
            {
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
                base.OnTextViewChanged(oldTextView, newTextView);
                this.InvalidateVisual();
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                var textView = this.TextView;
                var renderSize = this.RenderSize;

                if (textView != null && textView.VisualLinesValid)
                {
                    foreach (var line in textView.VisualLines)
                    {
                        var lineNo = line.FirstDocumentLine.LineNumber;

                        if (lineNo % 3 == 0)
                        {
                            var xCoord = renderSize.Width / 2;
                            var yCoord = line.GetTextLineVisualYPosition(line.TextLines[0], VisualYPosition.TextMiddle);
                            var position = new Point(xCoord, yCoord);
                            var diameter = textView.DefaultLineHeight;

                            drawingContext.DrawEllipse(SystemColors.ControlBrush, null, position, diameter / 2, diameter / 2);
                        }
                    }
                }
            }
        }
    }
}
