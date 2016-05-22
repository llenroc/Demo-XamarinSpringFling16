using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Demo.Core.Models;
using FFImageLoading;
using FFImageLoading.Views;
using System;
using System.Collections.ObjectModel;

namespace Demo.Droid.Adapters
{
    public class TrackAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        ObservableCollection<MTrack> Tracks;


        public TrackAdapter(ObservableCollection<MTrack> tracks)
        {
            Tracks = tracks;
        }

        public override int ItemCount
        {
            get
            {
                return Tracks.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        //Must override, this inflates our Layout and instantiates and assigns
        //it to the ViewHolder.
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Inflate our CrewMemberItem Layout
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_hubtracks_item, parent, false);

            //Create our ViewHolder to cache the layout view references and register
            //the OnClick event.
            var viewHolder = new ViewHolder(itemView, OnClick);

            return viewHolder;
        }

        //This will fire any event handlers that are registered with our ItemClick
        //event.
        private void OnClick(int position)
        {
            if (ItemClick != null)
            {
                ItemClick(this, position);
            }
        }

        //Must override, this is the important one.  This method is used to
        //bind our current data to your view holder.  Think of this as the equivalent
        //of GetView for regular Adapters.
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as ViewHolder;

            var track = Tracks[position];

            //Bind our data from our data source to our View References
            viewHolder.Name.Text = track.Name;
            viewHolder.ArtistName.Text = track.ArtistName;
            ImageService.Instance.LoadUrl(track.Image).Into(viewHolder.Image);
        }


        public class ViewHolder : RecyclerView.ViewHolder
        {
            public ImageViewAsync Image { get; set; }
            public TextView Name { get; set; }
            public TextView ArtistName { get; set; }

            public ViewHolder(View itemView, Action<int> listener)
                : base(itemView)
            {
                //Creates and caches our views defined in our layout
				Image = itemView.FindViewById<ImageViewAsync>(Resource.Id.imageImageView);
                Name = itemView.FindViewById<TextView>(Resource.Id.nameTextView);
                ArtistName = itemView.FindViewById<TextView>(Resource.Id.artistTextView);

                // Detect user clicks on the item view and report which item
                // was clicked (by position) to the listener:
                itemView.Click += (sender, e) => listener(base.Position);
            }
        }
    }
}