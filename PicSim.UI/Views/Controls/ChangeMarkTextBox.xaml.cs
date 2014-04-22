﻿using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PicSim.UI.Views
{
	/// <summary>
	/// Interaction logic for ChangeMarkTextBlock.xaml
	/// </summary>
	public partial class ChangeMarkTextBox : UserControl
	{
		public string Text
		{
			get
			{
				return box.Text;
			}

			set
			{
				if (value != box.Text)
				{
					box.Text = value;
					Animate();
				}
			}
		}

		public Color _Color = Colors.Transparent;
		public Color Color
		{
			get
			{
				return _Color;
			}

			set
			{
				panel.Background = new SolidColorBrush(value);
				_Color = value;
			}
		}

		public ChangeMarkTextBox()
		{
			InitializeComponent();
		}

		private void Animate()
		{
			panel.Background = new SolidColorBrush(Color);

			var anim = new ColorAnimation()
			{
				From = Colors.Red,
				To = Color,
				Duration = TimeSpan.FromMilliseconds(1500),
			};

			panel.Background.BeginAnimation(SolidColorBrush.ColorProperty, anim);
		}
	}
}
