using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Demo.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;

namespace Demo.Droid.Views.Fragments
{
    [MvxFragment(typeof(ShellViewModel), Resource.Id.content_frame, true)]
    [Register("demo.droid.views.fragments.ArtistFragment")]
    public class ArtistFragment : BaseFragment<ArtistViewModel>
    {
        protected override int FragmentId => Resource.Layout.ArtistFragment;
        private View loader;
        private SearchView searchView;
        private ProgressBar progress;

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            this.Activity.Title = "Artistas";
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            
            //Inflate the recyclerviews
            loader = view.FindViewById<View>(Resource.Id.loader);
            progress = view.FindViewById<ProgressBar>(Resource.Id.progressBar);
            searchView = view.FindViewById<SearchView>(Resource.Id.artistSearchView);

            searchView.QueryTextSubmit += SearchView_QueryTextSubmit;
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            return view;
        }

		public override void OnDestroyView()
		{
			// Borramos el contenido de nuestra lista Artistas al navegar a otra pantalla
			base.OnDestroyView();
			this.ViewModel.Artists = null;
		}

        private void SearchView_QueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e)
        {            
            ViewModel.ParamSearch = searchView.Query;
            ViewModel.SearchArtistCommand.Execute();

			InputMethodManager imm = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
			imm.HideSoftInputFromWindow(searchView.WindowToken, 0);
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.IsLoading))
            {
                ShowLoader(ViewModel.IsLoading);
            }
        }

        protected void ShowLoader(bool IsLoading)
        {
            if (IsLoading)
                loader.Visibility = ViewStates.Visible;
            else
                loader.Visibility = ViewStates.Gone;
        }      

    }
}