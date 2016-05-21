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
    /// Define propiedades, métodos y comandos para el manejo de la pantalla de Álbumes.
    /// </summary>
    public class AlbumViewModel : BaseViewModel
    {
        
        #region Constructor

        public AlbumViewModel
            (IDataService dataService, IMessageService messageService, INetworkService networkService)
        {
            DataService = dataService;
            MessageService = messageService;
            NetworkService = networkService;
        }

        #endregion

        #region Properties

        private ObservableCollection<MAlbum> albums;
        public ObservableCollection<MAlbum> Albums
        {
            get { return albums; }
            set { SetProperty(ref albums, value); }
        }

        private MAlbum selectedAlbum;
        public MAlbum SelectedAlbum
        {
            get { return selectedAlbum; }
            set { SetProperty(ref selectedAlbum, value); }
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
        private MvxCommand selectedAlbumCommand;
        public MvxCommand SelectedAlbumCommand
        {
            get { return selectedAlbumCommand ?? (selectedAlbumCommand = new MvxCommand(NavigateToAlbum)); }
        }

        /// <summary>
        /// Comando que se ejecuta al presionar el botón para realizar una búsqueda de un álbum.
        /// </summary>
        private MvxCommand searchAlbumCommand;
        public MvxCommand SearchAlbumCommand
        {
            get
            {
                return searchAlbumCommand ?? (searchAlbumCommand = new MvxCommand(async() =>
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

        private async Task SearchAlbums()
        {
            IsLoading = true;
            var data = await DataService.SearchAlbum(ParamSearch);
            if (data != null)
                Albums = new ObservableCollection<MAlbum>(data);
            else
            {
                ErrorMsg = "No se encontraron resultados para esta búsqueda.";
                IsErrorMsgVisible = true;
            }

            IsLoading = false;
        }

        /// <summary>
        /// Navega a la pantalla del detalle del álbum.
        /// </summary>
        /// <returns></returns>
        private async void NavigateToAlbum()
        {
            if (SelectedAlbum == null) return;

            if (NetworkService.IsConnected)
            {
                ShowViewModel<AlbumDetailViewModel>(SelectedAlbum);
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
