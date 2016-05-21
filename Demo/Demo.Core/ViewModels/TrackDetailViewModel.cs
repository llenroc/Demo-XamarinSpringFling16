using Demo.Core.Models;
using Demo.Core.Services.Message;
using Demo.Core.Services;
using Demo.Core.Services.Network;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;

namespace Demo.Core.ViewModels
{
    /// <summary>
    /// Define propiedades, métodos y comandos para el manejo de la pantalla detalle de canción.
    /// </summary>
    public class TrackDetailViewModel : BaseViewModel
    {
        
        #region Constructor

        public TrackDetailViewModel
            (IDataService dataService, IMessageService messageService, INetworkService networkService)
        {
            DataService = dataService;
            MessageService = messageService;
            NetworkService = networkService;
        }

        #endregion

        #region Properties

        private MTrack trackParam;
        public MTrack TrackParam
        {
            get { return trackParam; }
            set { SetProperty(ref trackParam, value); }
        }

        private MTrack track;
        public MTrack Track
        {
            get { return track; }
            set { SetProperty(ref track, value); }
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
        /// Se ejecuta al cargar la página de detalle de canción.
        /// </summary>
        private MvxCommand<MTrack> loadTrackDetailCommand;
        public MvxCommand<MTrack> LoadTrackDetailCommand
        {
            get
            {
                return loadTrackDetailCommand ?? (loadTrackDetailCommand = new MvxCommand<MTrack>(async (loadTrackDetailCommand) =>
                {
                    if (loadTrackDetailCommand == null) return;

                    await LoadTrackDetail();
                }));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Se ejecuta al inicio para obtener los parámetros enviados desde TrackViewModel
        /// </summary>
        /// <param name="track">Canción seleccionado.</param>
        public async void Init(MTrack track)
        {
            if (track == null || string.IsNullOrEmpty(track.Name) || string.IsNullOrEmpty(track.ArtistName))
                return;

            this.TrackParam = track;
            await LoadTrackDetail();
        }

        /// <summary>
        /// Obtiene información de la canción seleccionado en la pantalla anterior.
        /// </summary>
        /// <returns></returns>
        public async Task LoadTrackDetail()
        {
            IsLoading = true;
            var data = await DataService.GetTrackInfo(TrackParam.Name, TrackParam.ArtistName);
            if (data != null)
            {
                Track = new MTrack();
                Track = data;
                Track.Image = TrackParam.Image;
            }
            else
            {
                ErrorMsg = "No se encontraron resultados para ésta búsqueda";
                IsErrorMsgVisible = true;
            }

            IsLoading = false;
        }


        #endregion
    }
}
