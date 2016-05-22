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
using FFImageLoading;
using FFImageLoading.Views;
using Newtonsoft.Json;
using Demo.Core.Models;
using MvvmCross.Droid.Shared.Attributes;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Demo.Droid.Views.Fragments
{
    [MvxFragment(typeof(ShellViewModel), Resource.Id.content_frame, true)]
    [Register("demo.droid.views.fragments.AlbumDetailFragment")]
    public class AlbumDetailFragment : BaseFragment<AlbumDetailViewModel>
    {
		protected override int FragmentId => Resource.Layout.AlbumDetailFragment;

		public ImageViewAsync Image;
        private View loader;
        private ProgressBar progress;
        
        public override  void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
			ImageService.Instance.LoadUrl(ViewModel.Album.Image).Into(Image);
            this.Activity.Title = ViewModel.AlbumParam.Name;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
			Image = view.FindViewById<ImageViewAsync>(Resource.Id.albumImageView);
            loader = view.FindViewById<View>(Resource.Id.loader);
            progress = view.FindViewById<ProgressBar>(Resource.Id.progressBar);

            return view;
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