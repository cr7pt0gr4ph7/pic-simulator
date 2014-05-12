using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace PicSim.UI.Behaviors
{
    public class AddCurrentLineRendererBehavior : Behavior<TextEditor>
    {
        #region HighlightedLine Dependency Property

        public int HighlightedLine
        {
            get { return (int)GetValue(HighlightedLineProperty); }
            set { SetValue(HighlightedLineProperty, value); }
        }

        public static readonly DependencyProperty HighlightedLineProperty =
            DependencyProperty.Register("HighlightedLine", typeof(int), typeof(AddCurrentLineRendererBehavior), new PropertyMetadata(new PropertyChangedCallback(HighlighedLine_Changed)));

        private static void HighlighedLine_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (AddCurrentLineRendererBehavior)d;

            if (self.m_renderer != null)
                self.m_renderer.HighlightedLine = (int)e.NewValue;
        }

        #endregion

        private CurrentLineRenderer m_renderer;

        protected override void OnAttached()
        {
            m_renderer = new CurrentLineRenderer(AssociatedObject) { HighlightedLine = this.HighlightedLine };
            AssociatedObject.TextArea.TextView.BackgroundRenderers.Add(m_renderer);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextArea.TextView.BackgroundRenderers.Remove(m_renderer);
            m_renderer = null;
        }

        private class CurrentLineRenderer : IBackgroundRenderer
        {
            private readonly TextEditor m_editor;
            private int m_highlightedLine;

            public CurrentLineRenderer(TextEditor editor)
            {
                Ensure.ArgumentNotNull(editor, "editor");
                m_editor = editor;
            }

            public KnownLayer Layer
            {
                get { return KnownLayer.Background; }
            }

            public void Draw(TextView textView, DrawingContext drawingContext)
            {
                if (m_editor.Document == null) return;

                textView.EnsureVisualLines();

                var lineNo = HighlightedLine;
                if (lineNo < 0 || lineNo >= m_editor.Document.LineCount) return;
                
                var line = m_editor.Document.GetLineByNumber(lineNo);
                if (line != null)
                {
                    foreach (var rect in BackgroundGeometryBuilder.GetRectsForSegment(textView, line))
                    {
                        drawingContext.DrawRectangle(
                            new SolidColorBrush(Color.FromArgb(0x40, 0, 0, 0xFF)), null,
                            new Rect(rect.Location, new Size(textView.ActualWidth, rect.Height)));
                    }
                }
            }

            public int HighlightedLine
            {
                get { return m_highlightedLine; }
                set
                {
                    if (m_highlightedLine != value)
                    {
                        m_highlightedLine = value;
                        // TODO Refresh
                    }
                }
            }
        }
    }
}
