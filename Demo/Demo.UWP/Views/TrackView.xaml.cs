using Demo.Core.ViewModels;
using MvvmCross.WindowsUWP.Views;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace Demo.UWP.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    [MvxRegion("FrameContent")]
    public sealed partial class TrackView : BaseView
    {
        public new TrackViewModel ViewModel
        {
            get { return (TrackViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public TrackView()
        {
            this.InitializeComponent();
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            ViewModel.SearchTrackCommand.Execute();
        }
    }
}
