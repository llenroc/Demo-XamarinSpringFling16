using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Demo.Core.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using System;
using System.Threading.Tasks;

namespace Demo.Droid.Views.Fragments
{
    [MvxFragment(typeof(ShellViewModel), Resource.Id.navigation_frame)]
    [Register("demo.droid.views.fragments.MenuFragment")]
    public class MenuFragment : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private NavigationView navigationView;
        private IMenuItem previousMenuItem;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(Resource.Layout.fragment_navigation, null);

            navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_view);
            navigationView.SetNavigationItemSelectedListener(this);
            navigationView.Menu.FindItem(Resource.Id.nav_home).SetChecked(true);

            return view;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            if (item != previousMenuItem)
            {
                previousMenuItem?.SetChecked(false);
            }

            item.SetCheckable(true);
            item.SetChecked(true);

            previousMenuItem = item;

            Navigate(item.ItemId);

            return true;
        }

        private async Task Navigate(int itemId)
        {
            ((HomeView)Activity).DrawerLayout.CloseDrawers();

            // add a small delay to prevent any UI issues
            await Task.Delay(TimeSpan.FromMilliseconds(250));

            switch (itemId)
			{
				case Resource.Id.nav_home:
					ViewModel.HomeCommand.Execute();
					break;
				case Resource.Id.nav_artists:
					ViewModel.ArtistCommand.Execute();
					break;
				case Resource.Id.nav_albums:
					ViewModel.AlbumCommand.Execute();
					break;
				case Resource.Id.nav_tracks:
					ViewModel.TrackCommand.Execute();
					break;
			}
        }
    }
}