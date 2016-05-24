using Demo.Core.Models;
using Demo.Core.Services.Message;
using Demo.Core.Services;
using Demo.Core.Services.Network;
using MvvmCross.Core.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Demo.Core.ViewModels
{
    /// <summary>
    ///  Define propiedades, métodos y comandos para el manejo de la pantalla Canciones.
    /// </summary>
    public class TrackViewModel : BaseViewModel
    {
       
        #region Constructor

        public TrackViewModel
            (IDataService dataService, IMessageService messageService, INetworkService networkService)
        {
            DataService = dataService;
            MessageService = messageService;
            NetworkService = networkService;
        }

        #endregion

        #region Properties

        private ObservableCollection<MTrack> tracks;
        public ObservableCollection<MTrack> Tracks
        {
            get { return tracks; }
            set { SetProperty(ref tracks, value); }
        }

        private MTrack selectedTrack;
        public MTrack SelectedTrack
        {
            get { return selectedTrack; }
            set { SetProperty(ref selectedTrack, value); }
        }

        private string trackParam;
        public string TrackParam
        {
            get { return trackParam; }
            set { SetProperty(ref trackParam, value); }
        }
        private string artistParam;
        public string ArtistParam
        {
            get { return artistParam; }
            set { SetProperty(ref artistParam, value); }
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

        private MvxCommand selectedTrackCommand;
        public MvxCommand SelectedTrackCommand
        {
            get { return selectedTrackCommand ?? (selectedTrackCommand = new MvxCommand(NavigateToTrack)); }
        }

        private MvxCommand searchTrackCommand;
        public MvxCommand SearchTrackCommand
        {
            get
            {
                return searchTrackCommand ?? (searchTrackCommand = new MvxCommand(async () =>
                {
                    if (!NetworkService.IsConnected)
                    {
                        await MessageService.AlertAsync("Hubo un error. Por favor, verifica tu conexión a internet e inténtalo nuevamente", "Aviso", "Aceptar");
                        return;
                    }
                    if (string.IsNullOrEmpty(TrackParam) || string.IsNullOrEmpty(ArtistParam))
                    {
                        await MessageService.AlertAsync("Debes ingresar criterios válidos.", "Aviso", "OK");
                        return;
                    }

                    await SearchTracks();
                }));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Realiza una búsqueda de una canción específica y devuelve una lista de canciones relacionadas (si existe).
        /// </summary>
        private async Task SearchTracks()
        {
            IsLoading = true;

            var data = await DataService.SearchTrack(TrackParam,ArtistParam);
            if (data != null)
                Tracks = new ObservableCollection<MTrack>(data);
            else
            {
                ErrorMsg = "No se encontraron resultados para esta búsqueda.";
                IsErrorMsgVisible = true;
            }

            IsLoading = false;
        }

        /// <summary>
        /// Navega a la pantalla del detalle del canción.
        /// </summary>
        /// <returns></returns>
        private async void NavigateToTrack()
        {
            if (SelectedTrack == null) return;

            if (!NetworkService.IsConnected)
            {
                await MessageService.AlertAsync("Hubo un error al obtener información. Inténtalo nuevamente.", "Aviso", "Aceptar");
                return;
            }

            ShowViewModel<TrackDetailViewModel>(SelectedTrack);
        }

        #endregion
    }
}
