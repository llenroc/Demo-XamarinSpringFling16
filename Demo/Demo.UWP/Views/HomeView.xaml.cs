using MvvmCross.WindowsUWP.Views;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace Demo.UWP.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    [MvxRegion("FrameContent")]
    public sealed partial class HomeView : BaseView
    {
        //Make a place to store the timer
        private readonly DispatcherTimer timer;

        public HomeView()
        {
            this.InitializeComponent();
            //Configure the timer
            timer = new DispatcherTimer
            {
                //Set the interval between ticks (in this case 2 seconds to see it working)
                Interval = TimeSpan.FromSeconds(5)
            };

            //Change what's displayed when the timer ticks
            timer.Tick += ChangeImage;
            //Start the timer
            timer.Start();
        }

        private void ChangeImage(object sender, object e)
        {
            //Get the number of items in the flip view
            var totalItems = flipView.Items.Count;
            int newItemIndex = -1;
            //Figure out the new item's index (the current index plus one, if the next item would be out of range, go back to zero)
            if (flipView.SelectedIndex != -1)
                newItemIndex = (flipView.SelectedIndex + 1) % totalItems;

            //Set the displayed item's index on the flip view
            flipView.SelectedIndex = newItemIndex;
        }

        /// <summary>
        /// When the user changes the item displayed manually, reset the timer so we don't advance at the remainder of the last interval
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Since the page is configured before the timer is, check to make sure that we've actually got a timer
            if (!ReferenceEquals(timer, null))
            {
                timer.Stop();
                timer.Start();
            }
        }
    }
}
