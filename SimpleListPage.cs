using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace wherever
{
	public class SimpleListPage : ContentPage
	{
		ListView listView;

		public SimpleListPage ()
		{
			Title = "List";

			NavigationPage.SetHasNavigationBar (this, true);

			listView = new ListView {
				RowHeight = 40,
				ItemTemplate = new DataTemplate (typeof(TodoItemCell))
			};

			// These commented-out lines were used to test the ListView prior to integrating the database
//			listView.ItemsSource = new string [] { "Prueba 1", "Prueba 2", "Prueba 3", "Prueba 4", "Prueba 5" };
//			listView.ItemsSource = new TodoItem [] { 
//				new TodoItem {Name = "Prueba 1", Done=true}, 
//				new TodoItem {Name = "Prueba 2"},
//				new TodoItem {Name = "Prueba 3", Done=true}, 
//				new TodoItem {Name = "Prueba 4"},
//				new TodoItem {Name = "Prueba 5", Done=true}
//			};

			listView.ItemSelected += (sender, e) => {
				var todoItem = (TodoItem)e.SelectedItem;
				var todoPage = new TodoItemPage ();
				todoPage.BindingContext = todoItem;
				Navigation.PushAsync (todoPage);
			};

			var layout = new StackLayout ();
			if (Device.OS == TargetPlatform.WinPhone) { // WinPhone doesn't have the title showing
				layout.Children.Add (new Label {
					Text = "Todo",
					FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
					FontAttributes = FontAttributes.Bold
				});
			}
			layout.Children.Add (listView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;


			ToolbarItem tbi = null;
			if (Device.OS == TargetPlatform.iOS) {
				tbi = new ToolbarItem ("+", null, () => {
					var todoItem = new TodoItem ();
					var todoPage = new TodoItemPage ();
					todoPage.BindingContext = todoItem;
					Navigation.PushAsync (todoPage);
				}, 0, 0);
			}
			if (Device.OS == TargetPlatform.Android) { // BUG: Android doesn't support the icon being null
				tbi = new ToolbarItem ("+", "plus", () => {
					var todoItem = new TodoItem ();
					var todoPage = new TodoItemPage ();
					todoPage.BindingContext = todoItem;
					Navigation.PushAsync (todoPage);
				}, 0, 0);
			}

			if (Device.OS == TargetPlatform.WinPhone) {
				tbi = new ToolbarItem ("Add", "add.png", () => {
					var todoItem = new TodoItem ();
					var todoPage = new TodoItemPage ();
					todoPage.BindingContext = todoItem;
					Navigation.PushAsync (todoPage);
				}, 0, 0);
			}

			ToolbarItems.Add (tbi);

			if (Device.OS == TargetPlatform.iOS) {
				var tbi2 = new ToolbarItem ("?", null, () => {
					var todos = App.Database.GetItemsNotDone ();
					var tospeak = "";
					foreach (var t in todos)
						tospeak += t.Name + " ";
					if (tospeak == "")
						tospeak = "there are no tasks to do";

					DependencyService.Get<ITextToSpeech> ().Speak (tospeak);
				}, 0, 0);
				ToolbarItems.Add (tbi2);
			}
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			listView.ItemsSource = App.Database.GetItems ();
		}
	}
}
