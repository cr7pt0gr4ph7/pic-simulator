using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.AvalonEdit.Rendering;
using ICSharpCode.AvalonEdit.Document;
using System.Windows.Media;
using System.Windows;

namespace PicSim.UI.Helper
{
	public class ColorizeAvalonEdit : DocumentColorizingTransformer
	{
		protected override void ColorizeLine(DocumentLine line)
		{
			int lineStartOffset = line.Offset;
			string text = CurrentContext.Document.GetText(line);
			int start = 0;
			int index;
			while ((index = text.IndexOf("AvalonEdit", start)) >= 0)
			{
				base.ChangeLinePart(
					lineStartOffset + index, // startOffset
					lineStartOffset + index + 10, // endOffset
					(VisualLineElement element) => {
						// This lambda gets called once for every VisualLineElement
						// between the specified offsets.
						Typeface tf = element.TextRunProperties.Typeface;
						// Replace the typeface with a modified version of
						// the same typeface
						element.TextRunProperties.SetTypeface(new Typeface(
							tf.FontFamily,
							FontStyles.Italic,
							FontWeights.Bold,
							tf.Stretch
						));
					});
				start = index + 1; // search for next occurrence
			}
		}
	}
}


/*
 * References
 * - http://avalonedit.net/documentation/html/1e7e98ad-31d6-1298-0676-5035a879aff4.htm
 * - http://danielgrunwald.de/coding/AvalonEdit/rendering.php
 */