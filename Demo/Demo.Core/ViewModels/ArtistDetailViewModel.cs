using Demo.Core.Models;
using Demo.Core.Services;
using Demo.Core.Services.Message;
using Demo.Core.Services.Network;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;

namespace Demo.Core.ViewModels
{
    /// <summary>
    /// Define propiedades, métodos y comandos para el manejo de la pantalla del detalle de artista.
    /// </summary>
    public class ArtistDetailViewModel : BaseViewModel
    {
        
        #region Constructor

        public ArtistDetailViewModel
            (IDataService dataService, IMessageService messageService, INetworkService networkService)
        {
            DataService = dataService;
            MessageService = messageService;
            NetworkService = networkService;
        }

        #endregion

        #region Properties

        private MArtist artistParam;
        public MArtist ArtistParam
        {
            get { return artistParam; }
            set { SetProperty(ref artistParam, value); }
        }

        private MArtist artist;
        public MArtist Artist
        {
            get { return artist; }
            set { SetProperty(ref artist, value); }
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

        // <summary>
        /// Se ejecuta cuando carga la pantalla de detalle de artista.
        /// </summary>
        private MvxCommand<MArtist> loadArtistDetailCommand;
        public MvxCommand<MArtist>LoadArtistDetailCommand
        {
            get
            {
                return loadArtistDetailCommand ?? (loadArtistDetailCommand = new MvxCommand<MArtist>(async (loadArtistDetailCommand) =>
                {
                    if (loadArtistDetailCommand == null) return;

                    await LoadArtistDetail();
                }));
            }
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Se ejecuta al inicio para obtener los parámetros enviados desde ArtistViewModel.
        /// </summary>
        /// <param name="artist">Artista seleccionado.</param>
        public async void Init(MArtist artist)
        {
            if (artist == null || string.IsNullOrEmpty(artist.Name))
                return;

            this.ArtistParam = artist;
            await LoadArtistDetail();
        }

        /// <summary>
        /// Obtiene información del artista seleccionado en la pantalla anterior.
        /// </summary>
        /// <returns></returns>
        public async Task LoadArtistDetail()
        {
            IsLoading = true;
            var data = await DataService.GetArtistInfo(ArtistParam.Name);
            if (data != null)
            {
                Artist = new MArtist();
                Artist = data;
            }
            else
            {
                ErrorMsg = "No se encontraron resultados para ésta búsqueda.";
                IsErrorMsgVisible = true;
            }

            IsLoading = false;
        }

        #endregion
        
    }
}
