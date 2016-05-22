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
    /// Define propiedades, métodos y comandos para el manejo de la pantalla de Artistas
    /// </summary>
    public class ArtistViewModel : BaseViewModel
    {
          
        #region Constructor

        public ArtistViewModel
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
            set{ SetProperty(ref artists, value); }
        }

        private MArtist selectedArtist;
        public MArtist SelectedArtist
        {
            get { return selectedArtist; }
            set { SetProperty(ref selectedArtist, value); }
        }

        private string paramSearch;
        public string ParamSearch
        {
            get { return paramSearch; }
            set { SetProperty(ref paramSearch, value); }
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
        /// Comando que se ejecuta cuando se selecciona un artista de la lista.
        /// </summary>
        private MvxCommand selectedArtistCommand;
        public MvxCommand SelectedArtistCommand
        {
            get { return selectedArtistCommand ?? (selectedArtistCommand = new MvxCommand(NavigateToArtist)); }
        }

        /// <summary>
        /// Comando que se ejecuta al presionar el botón para realizar una búsqueda de un artista.
        /// </summary>
        private MvxCommand searchArtistCommand;
        public MvxCommand SearchArtistCommand
        {
            get
            {
                return searchArtistCommand ?? (searchArtistCommand = new MvxCommand(async () =>
                {
                    if (!NetworkService.IsConnected)
                    {
                        await MessageService.AlertAsync("Hubo un error. Por favor, verifica tu conexión a internet e inténtalo nuevamente", "Aviso", "Aceptar");
                        return;
                    }
                    if (string.IsNullOrEmpty(ParamSearch))
                    {
                        await MessageService.AlertAsync("Ingresa un criterio de búsqueda válido", "Aviso", "OK");
                        return;
                    }

                    await SearchAlbums();
                }));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Realiza una búsqueda de un álbum específico y devuelve una lista de artistas relacionados (si existe).
        /// </summary>
        /// <returns></returns>
        private async Task SearchAlbums()
        {
            IsLoading = true;
            var data = await DataService.SearchArtist(ParamSearch);
            if (data != null)
                Artists = new ObservableCollection<MArtist>(data);
            else
            {
                ErrorMsg = "No se encontraron resultados para esta búsqueda.";
                IsErrorMsgVisible = true;
            }

            IsLoading = false;
        }

        /// <summary>
        /// Navega a la pantalla del detalle del artista.
        /// </summary>
        /// <returns></returns>
        private async void NavigateToArtist()
        {
            if (SelectedArtist == null) return;

            if (NetworkService.IsConnected)
            {
                ShowViewModel<ArtistDetailViewModel>(SelectedArtist);
            }
            else
            {
                await MessageService.AlertAsync("Hubo un error al obtener información. Inténtalo nuevamente.", "Aviso", "Aceptar");
                return;
            }
        }

        #endregion
    }
}
