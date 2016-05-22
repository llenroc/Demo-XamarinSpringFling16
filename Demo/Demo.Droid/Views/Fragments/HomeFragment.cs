using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Demo.Droid.Adapters;
using Demo.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Demo.Droid.Views.Fragments
{
    [MvxFragment(typeof(ShellViewModel), Resource.Id.content_frame, true)]
    [Register("demo.droid.views.fragments.HomeFragment")]
    public class HomeFragment : BaseFragment<HomeViewModel>
    {

        private RecyclerView trackList;
        private RecyclerView artistList;
        private RecyclerView.LayoutManager layoutManager1;
        private RecyclerView.LayoutManager layoutManager2;
        private TrackAdapter trackAdapter;
        private ArtistAdapter artistAdapter;
        private View loader;
        private ProgressBar progress;

        protected override int FragmentId => Resource.Layout.HomeFragment;

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.IsLoading))
            {
                ShowLoader(ViewModel.IsLoading);
            }
            if (e.PropertyName == nameof(ViewModel.IsErrorMsgVisible))
            {

            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            //Inflate the recyclerviews
            loader = view.FindViewById<View>(Resource.Id.loader);
            progress = view.FindViewById<ProgressBar>(Resource.Id.progressBar);
            artistList = view.FindViewById<RecyclerView>(Resource.Id.artistListView);
            trackList = view.FindViewById<RecyclerView>(Resource.Id.trackListView);

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            //show the RecyclerView in a horizontal list.
            layoutManager1 = new LinearLayoutManager(container.Context, LinearLayoutManager.Horizontal, false);

            //show the RecyclerView in a horizontal list.
            layoutManager2 = new LinearLayoutManager(container.Context, LinearLayoutManager.Horizontal, false);

            artistList.SetLayoutManager(layoutManager1);
            trackList.SetLayoutManager(layoutManager2);

            return view;
        }

        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            this.Activity.Title = "Principal";

            await Task.Run(async () =>
            {
                await ViewModel.LoadHome();
            });
            // await ViewModel.LoadHome();

            if (ViewModel.Tracks != null && ViewModel.Tracks.Count > 0)
            {
                trackAdapter = new TrackAdapter(ViewModel.Tracks);
                trackAdapter.ItemClick += TrackAdapter_ItemClick;
                trackList.SetAdapter(trackAdapter);
            }
            if (ViewModel.Artists != null && ViewModel.Artists.Count > 0)
            {
                artistAdapter = new ArtistAdapter(ViewModel.Artists);
                artistAdapter.ItemClick += ArtistAdapter_ItemClick;
                artistList.SetAdapter(artistAdapter);
            }
        }

        private void TrackAdapter_ItemClick(object sender, int e)
        {
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;

            var details = FragmentManager.FindFragmentById(Resource.Id.trackDetailFragment) as TrackDetailFragment;
            ViewModel.SelectedTrack = ViewModel.Tracks[e];

            if (details == null)
            {
                // Make new fragment to show this selection.
                details = TrackDetailFragment.NewInstance(ViewModel.SelectedTrack);
                // Execute a transaction, replacing any existing
                // fragment with this one inside the frame.
                var ft = FragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.content_frame, details, "TrackFragTag");
				ft.SetTransition((int)FragmentTransit.EnterMask);
                ft.AddToBackStack("TrackFragTag");
                ft.Commit();
            }
            else
            {
                // Otherwise we need to launch a new Activity to display
                // the dialog fragment with selected artist.
                var intent = new Intent();
                intent.SetClass(Activity, typeof(TrackDetailFragment));
                intent.PutExtra("current_track", JsonConvert.SerializeObject(ViewModel.SelectedTrack));
                StartActivity(intent);
            }
        }

        private void ArtistAdapter_ItemClick(object sender, int e)
        {
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;

            var details = FragmentManager.FindFragmentById(Resource.Id.artistDetailFragment) as ArtistDetailFragment;
            ViewModel.SelectedArtist = ViewModel.Artists[e];

            if (details == null)
            {
                // Make new fragment to show this selection.
                details = ArtistDetailFragment.NewInstance(ViewModel.SelectedArtist);
                // Execute a transaction, replacing any existing
                // fragment with this one inside the frame.
                var ft = FragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.content_frame, details, "ArtistFragTag");
				ft.SetTransition((int)FragmentTransit.FragmentOpen);
                ft.AddToBackStack("ArtistFragTag");
                ft.Commit();
            }
            else
            {
                // Otherwise we need to launch a new Activity to display
                // the dialog fragment with selected artist.
                var intent = new Intent();
                intent.SetClass(Activity, typeof(ArtistDetailFragment));
                intent.PutExtra("current_artist", JsonConvert.SerializeObject(ViewModel.SelectedArtist));
                StartActivity(intent);
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