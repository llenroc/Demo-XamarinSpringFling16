using Demo.Core.Models;
using Demo.Core.Services;
using Demo.Core.Services.Message;
using Demo.Core.Services.Network;
using MvvmCross.Core.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Demo.Core.ViewModels
{
    /// <summary>
    /// Define propiedades, métodos y comandos para el manejo de la pantalla Home.
    /// </summary>
    public class HomeViewModel : BaseViewModel
    {
        
        #region Constructor

        public HomeViewModel
            (IDataService dataService, IMessageService messageService, INetworkService networkService)
        {
            DataService = dataService;
            MessageService = messageService;
            NetworkService = networkService;
        }

        #endregion

        #region Properties

        private ObservableCollection<MArtist> artists;
        public ObservableCollection<MArtist> Artists
        {
            get { return artists; }
            set { SetProperty(ref artists, value); }
        }

        private ObservableCollection<MTrack> tracks;
        public ObservableCollection<MTrack> Tracks
        {
            get { return tracks; }
            set { SetProperty(ref tracks, value); }
        }

        private MArtist selectedArtist;
        public MArtist SelectedArtist
        {
            get { return selectedArtist; }
            set { SetProperty(ref selectedArtist, value); }
        }

        private MTrack selectedTrack;
        public MTrack SelectedTrack
        {
            get { return selectedTrack; }
            set { SetProperty(ref selectedTrack, value); }
        }

        private string errorMsg;
        public string ErrorMsg
        {
            get { return errorMsg; }
            set { SetProperty(ref errorMsg, value); }
        }

        private bool isErrorMsgVisible;
        public bool IsErrorMsgVisible
        {
            get { return isErrorMsgVisible; }
            set { SetProperty(ref isErrorMsgVisible, value); }
        }


        #endregion

        #region Commands
        
        /// <summary>
        /// Se ejecuta al cargar la página de Home.
        /// </summary>
        private MvxCommand loadHomeCommand;
        public MvxCommand LoadHomeCommand
        {
            get { return loadHomeCommand ?? (loadHomeCommand = new MvxCommand(async () => await LoadHome())); }
        }

        /// <summary>
        /// Se ejecuta al seleccionar un artista del Slider de Artistas.
        /// </summary>
        private MvxCommand selectedArtistCommand;
        public MvxCommand SelectedArtistCommand
        {
            get { return selectedArtistCommand ?? (selectedArtistCommand = new MvxCommand(NavigateToArtist)); }
        }

        /// <summary>
        /// Se ejecuta al seleccionar una canción de la lista.
        /// </summary>
        private MvxCommand selectedTrackCommand;
        public MvxCommand SelectedTrackCommand
        {
            get { return selectedTrackCommand ?? (selectedTrackCommand = new MvxCommand(NavigateToTrack)); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Obtiene la lista de artistas y canciones Top.
        /// </summary>
        /// <returns></returns>
        public async Task LoadHome()
        {
            IsLoading = true;

            await LoadTopTracks();
            await LoadTopArtists();

            if (Artists == null && Tracks == null)
            {
                ErrorMsg = "No hay información para mostrar";
                IsErrorMsgVisible = true;
            }
            else if (!NetworkService.IsConnected)
            {
                ErrorMsg = "No tienes conexión de red. Verifica e intenta nuevamente";
                IsErrorMsgVisible = true;
            }

            IsLoading = false;
        }

        /// <summary>
        /// Obtiene la lista de canciones más escuchadas.
        /// </summary>
        /// <returns></returns>
        private async Task LoadTopTracks()
        {
            IsLoading = true;
            var data = await DataService.GetTopTracksList();
            if (data != null)
                Tracks = new ObservableCollection<MTrack>(data);
        }

        /// <summary>
        /// Obtiene la lista de artistas más escuchadas.
        /// </summary>
        /// <returns></returns>
        private async Task LoadTopArtists()
        {
            var data = await DataService.GetTopArtistsList();
            if (data != null)
                Artists = new ObservableCollection<MArtist>(data);
            IsLoading = false;
        }

        /// <summary>
        /// Navega a la pantalla de detalle de artista.
        /// </summary>
        private async void NavigateToArtist()
        {
            if (SelectedArtist == null) return;

            if (!NetworkService.IsConnected)
            {
                await MessageService.AlertAsync("Verifica tu conexión a red e intenta nuevamente", "Aviso", "Aceptar");
                return;
            }

            ShowViewModel<ArtistDetailViewModel>(SelectedArtist);
        }

        /// <summary>
        /// Navega a la pantalla de detalle de canción.
        /// </summary>
        private async void NavigateToTrack()
        {
            if (SelectedTrack == null) return;

            if (!NetworkService.IsConnected)
            {
                await MessageService.AlertAsync("Verifica tu conexión a red e intenta nuevamente", "Aviso", "Aceptar");
                return;
            }

            ShowViewModel<TrackDetailViewModel>(SelectedTrack);
        }

        #endregion
    }
}
