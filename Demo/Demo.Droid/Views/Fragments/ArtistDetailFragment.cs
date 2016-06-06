using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Demo.Core.Models;
using Demo.Core.ViewModels;
using FFImageLoading;
using FFImageLoading.Views;
using MvvmCross.Droid.Shared.Attributes;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Demo.Droid.Views.Fragments
{
    [MvxFragment(typeof(ShellViewModel), Resource.Id.content_frame, true)]
    [Register("demo.droid.views.fragments.ArtistDetailFragment")]
    public class ArtistDetailFragment : BaseFragment<ArtistDetailViewModel>
    {
        protected override int FragmentId => Resource.Layout.ArtistDetailFragment;

        public ImageViewAsync Image;
        public TextView Description;
        private View loader;
        private ProgressBar progress;

        public static ArtistDetailFragment NewInstance(MArtist selectedArtist)
        {
            var detailsFrag = new ArtistDetailFragment { Arguments = new Bundle() };
            detailsFrag.Arguments.PutString("current_artist", JsonConvert.SerializeObject(selectedArtist));
            return detailsFrag;
        }

        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            if (this.Arguments.GetString("current_artist") != null)
            {
                ViewModel.ArtistParam = JsonConvert.DeserializeObject<MArtist>(Arguments.GetString("current_artist"));

                await Task.Run(async () =>
                {
                    await ViewModel.LoadArtistDetail();
                });

                if (ViewModel.Artist != null && ViewModel.ArtistParam != null)
                {
                    Description.Text = ViewModel.Artist.Biography.Content;
					ImageService.Instance.LoadUrl(ViewModel.Artist.Image).Into(Image);
                }
            }
            else
            {
                if (ViewModel.ArtistParam != null)
                    ImageService.Instance.LoadUrl(ViewModel.ArtistParam.Image).Into(Image);
                
            }

            this.Activity.Title = ViewModel.ArtistParam.Name;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            loader = view.FindViewById<View>(Resource.Id.loader);
			Image = view.FindViewById<ImageViewAsync>(Resource.Id.artistImageView);
            progress = view.FindViewById<ProgressBar>(Resource.Id.progressBar);
            Description = view.FindViewById<TextView>(Resource.Id.artistDescTextView);

            if (container == null)
            {
                // Currently in a layout without a container, so no reason to create our view.
                return null;
            }

            return view;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.IsLoading))
            {
                showLoader(ViewModel.IsLoading);
            }
        }

        protected void showLoader(bool IsLoading)
        {
            if (IsLoading)
                loader.Visibility = ViewStates.Visible;
            else
                loader.Visibility = ViewStates.Gone;
        }
    }
}