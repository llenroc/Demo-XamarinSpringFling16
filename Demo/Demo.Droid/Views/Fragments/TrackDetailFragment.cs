using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Demo.Core.Models;
using Demo.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;
using Newtonsoft.Json;
using System.Threading.Tasks;
using FFImageLoading;
using FFImageLoading.Views;

namespace Demo.Droid.Views.Fragments
{
    [MvxFragment(typeof(ShellViewModel), Resource.Id.content_frame, true)]
    [Register("demo.droid.views.fragments.TrackDetailFragment")]
    public class TrackDetailFragment : BaseFragment<TrackDetailViewModel>
    {
        protected override int FragmentId => Resource.Layout.TrackDetailFragment;

        private View loader;
		public ImageViewAsync Image;
        private ProgressBar progress;

        public static TrackDetailFragment NewInstance(MTrack selectedTrack)
        {
            var detailsFrag = new TrackDetailFragment { Arguments = new Bundle() };
            detailsFrag.Arguments.PutString("current_track", JsonConvert.SerializeObject(selectedTrack));
            return detailsFrag;
        }

        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            if (this.Arguments.GetString("current_track") != null)
            {
                ViewModel.TrackParam = JsonConvert.DeserializeObject<MTrack>(Arguments.GetString("current_track"));
                await Task.Run(async () =>
                {
                    await ViewModel.LoadTrackDetail();
                });
            }
			if (ViewModel.Track != null && ViewModel.TrackParam != null)
				ImageService.Instance.LoadUrl(ViewModel.Track.Image).Into(Image);

            this.Activity.Title = ViewModel.TrackParam.Name;

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
			Image = view.FindViewById<ImageViewAsync>(Resource.Id.trackImageView);
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