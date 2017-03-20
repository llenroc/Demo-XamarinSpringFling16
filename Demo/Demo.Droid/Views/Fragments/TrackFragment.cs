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
    [Register("demo.droid.views.fragments.TrackFragment")]
    public class TrackFragment : BaseFragment<TrackViewModel>
    {
        protected override int FragmentId => Resource.Layout.TrackFragment;
        private View loader;
        private SearchView trackSearchView;
        private SearchView artistSearchView;
        private ProgressBar progress;

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            this.Activity.Title = "Canciones";
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            
            //Inflate the recyclerviews
            loader = view.FindViewById<View>(Resource.Id.loader);
            progress = view.FindViewById<ProgressBar>(Resource.Id.progressBar);
            trackSearchView = view.FindViewById<SearchView>(Resource.Id.trackSearchView);
            artistSearchView = view.FindViewById<SearchView>(Resource.Id.artistSearchView);

            trackSearchView.QueryTextSubmit += SearchView_QueryTextSubmit; 
            artistSearchView.QueryTextSubmit += SearchView_QueryTextSubmit; 
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            return view;
        }

		public override void OnDestroyView()
		{
			// Borramos el contenido de nuestra lista Tracks al navegar a otra pantalla
			base.OnDestroyView();
			ViewModel.Tracks = null;
		}

        private void SearchView_QueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e)
        {            
            ViewModel.TrackParam = trackSearchView.Query;
            ViewModel.ArtistParam = artistSearchView.Query;
            ViewModel.SearchTrackCommand.Execute();

			InputMethodManager imm = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
			imm.HideSoftInputFromWindow(artistSearchView.WindowToken, 0);
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