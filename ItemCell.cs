using System;
using Xamarin.Forms;

namespace wherever
{
	public class ItemCell : ViewCell
	{
		public ItemCell ()
		{
			var label = new Label {
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand
			};

			label.SetBinding (Label.TextProperty, "Nombre");

			var tick = new Image {
				Source = FileImageSource.FromFile ("check.png"),
				HorizontalOptions = LayoutOptions.End
			};

			tick.SetBinding (Image.IsVisibleProperty, "Hecho");

			var layout = new StackLayout {
				Padding = new Thickness (20, 0, 20, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { label, tick }
			};

			View = layout;
		}
	}
}
