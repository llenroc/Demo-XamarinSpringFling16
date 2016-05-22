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
using Android.Widget;
using Demo.Core.ViewModels;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Shared.Attributes;

namespace Demo.Droid.Views.Fragments
{
    [MvxFragment(typeof(ShellViewModel), Resource.Id.content_frame, true)]
    [Register("demo.droid.views.fragments.AlbumFragment")]
    public class AlbumFragment : BaseFragment<AlbumViewModel>
    {        
        private View loader;
        private SearchView searchView;
        private ProgressBar progress;

        protected override int FragmentId => Resource.Layout.AlbumFragment;
        

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            this.Activity.Title = "Albumes";
        }

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            
            //Inflate the recyclerviews
            loader = view.FindViewById<View>(Resource.Id.loader);
            progress = view.FindViewById<ProgressBar>(Resource.Id.progressBar);
            searchView = view.FindViewById<SearchView>(Resource.Id.albumSearchView);

            searchView.QueryTextSubmit += SearchView_QueryTextSubmit;
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            return view;
        }

        private void SearchView_QueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e)
        {
            ViewModel.ParamSearch = searchView.Query;
            ViewModel.SearchAlbumCommand.Execute();
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

        public override void OnDestroy()
        {
            base.OnDestroy();
            ViewModel.Albums = null;
        }
    }
}
