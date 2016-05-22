using MvvmCross.Binding.BindingContext;
using CoreGraphics;
using Foundation;
using UIKit;
using Demo.Core.ViewModels;
using Cirrious.FluentLayouts.Touch;
using MvvmCross.iOS.Support.SidePanels;
using Demo.iOS.Views.Base;

namespace Demo.iOS.Menu
{
	[Register("MenuView")]
	[MvxPanelPresentation(MvxPanelEnum.Left, MvxPanelHintType.ActivePanel, false)]
	public class MenuView : BaseViewController<MenuViewModel>
	{
		public MenuView()
		{ 
			
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var scrollView = new UIScrollView(View.Frame)
			{
				ShowsHorizontalScrollIndicator = false,
				AutoresizingMask = UIViewAutoresizing.FlexibleHeight
			};

			// create a binding set for the appropriate view model
			var set = this.CreateBindingSet<MenuView, MenuViewModel>();

			var homeButton = new UIButton(new CGRect(0, 100, 320, 40));
			homeButton.SetTitle("Home", UIControlState.Normal);
			homeButton.BackgroundColor = UIColor.White;
			homeButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			set.Bind(homeButton).To(vm => vm.HomeCommand);

			var artistButton = new UIButton(new CGRect(0, 100, 320, 40));
			artistButton.SetTitle("Settings", UIControlState.Normal);
			artistButton.BackgroundColor = UIColor.White;
			artistButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			set.Bind(artistButton).To(vm => vm.ArtistCommand);

			var albumButton = new UIButton(new CGRect(0, 100, 320, 40));
			albumButton.SetTitle("Help & Feedback", UIControlState.Normal);
			albumButton.BackgroundColor = UIColor.White;
			albumButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			set.Bind(albumButton).To(vm => vm.AlbumCommand);

			var trackButton = new UIButton(new CGRect(0, 100, 320, 40));
			trackButton.SetTitle("Help & Feedback", UIControlState.Normal);
			trackButton.BackgroundColor = UIColor.White;
			trackButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			set.Bind(trackButton).To(vm => vm.TrackCommand);

			set.Apply();

			Add(scrollView);

			View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

			View.AddConstraints(
				scrollView.AtLeftOf(View),
				scrollView.AtTopOf(View),
				scrollView.WithSameWidth(View),
				scrollView.WithSameHeight(View));

			scrollView.Add(homeButton);
			scrollView.Add(artistButton);
			scrollView.Add(albumButton);
			scrollView.Add(trackButton);

			scrollView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

			var constraints = scrollView.VerticalStackPanelConstraints(new Margins(20, 10, 20, 10, 5, 5), scrollView.Subviews);
			scrollView.AddConstraints(constraints);
		}

		public override void ViewWillAppear(bool animated)
		{
			Title = "Left Menu View";
			base.ViewWillAppear(animated);

			NavigationController.NavigationBarHidden = true;
		}
	}
}

