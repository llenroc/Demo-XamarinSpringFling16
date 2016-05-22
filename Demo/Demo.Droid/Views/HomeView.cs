using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Demo.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Demo.Droid.Views
{
    [Activity(
       Theme = "@style/AppTheme",
       LaunchMode = LaunchMode.SingleTop,
       MainLauncher = true,
       ScreenOrientation = ScreenOrientation.Portrait
       )]
    public class HomeView : MvxCachingFragmentCompatActivity<ShellViewModel>
    {
        public DrawerLayout DrawerLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.HomeView);

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);

            if (savedInstanceState == null)
                ViewModel.ShowMenu();

        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    DrawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}