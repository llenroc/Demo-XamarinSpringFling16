using Android.OS;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Demo.Droid.Views.Fragments
{
    public abstract class BaseFragment : MvxFragment
    {
        private Android.Support.V7.Widget.Toolbar toolbar;
        private MvxActionBarDrawerToggle drawerToogle;

        protected BaseFragment()
        {
            RetainInstance = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(FragmentId, null);
            toolbar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
				toolbar.SetNavigationIcon(Resource.Drawable.menu);
                ((HomeView)Activity).SetSupportActionBar(toolbar);
                ((HomeView)Activity).SupportActionBar.SetDisplayHomeAsUpEnabled(true);

                drawerToogle = new MvxActionBarDrawerToggle(Activity,
                    ((HomeView)Activity).DrawerLayout,
                    toolbar,
                    Resource.String.drawer_open,
                    Resource.String.drawer_close);
                ((HomeView)Activity).DrawerLayout.SetDrawerListener(drawerToogle);
            }

            return view;
        }

        protected abstract int FragmentId { get; }
    }

    public abstract class BaseFragment<TViewModel> : BaseFragment where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}