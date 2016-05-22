using Demo.Core.Models;
using Demo.Core.Services;
using Demo.Core.Services.Message;
using Demo.Core.Services.Network;
using Demo.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;


namespace Demo.Core.ViewModels
{
    /// <summary>
    /// Define propiedades, métodos y comandos para el manejo de la pantalla del detalle de álbum.
    /// </summary>
    public class AlbumDetailViewModel : BaseViewModel
    {
        
        #region Constructor

        public AlbumDetailViewModel
            (IDataService dataService, IMessageService messageService, INetworkService networkService)
        {
            DataService = dataService;
            MessageService = messageService;
            NetworkService = networkService;
        }

        #endregion

        #region Properties

        private MAlbum albumParam;
        public MAlbum AlbumParam
        {
            get { return albumParam; }
            set { SetProperty(ref albumParam, value); }
        }

        private MAlbum album;
        public MAlbum Album
        {
            get { return album; }
            set { SetProperty(ref album, value); }
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
        /// Se ejecuta cuando carga la pantalla de detalle de álbum.
        /// </summary>
        private MvxCommand<MAlbum> loadAlbumDetailCommand;
        public MvxCommand<MAlbum> LoadAlbumDetailCommand
        {
            get
            {
                return loadAlbumDetailCommand ?? (loadAlbumDetailCommand = new MvxCommand<MAlbum>(async (loadAlbumDetailCommand) =>
                {
                    if (loadAlbumDetailCommand == null) return;

                    await LoadAlbumDetail();
                }));
            }
        }


        #endregion

        #region Methods

        /// <summary>
        /// Se ejecuta al inicio para obtener los parámetros enviados desde AlbumViewModel.
        /// </summary>
        /// <param name="album">Álbum seleccionado,</param>
        public async void Init(MAlbum album)
        {
            if (album == null || string.IsNullOrEmpty(album.Name) || string.IsNullOrEmpty(album.Artist))
                return;

            this.AlbumParam = album;
            await LoadAlbumDetail();
        }


        /// <summary>
        /// Obtiene información del álbum seleccionado en la pantalla anterior.
        /// </summary>
        /// <returns></returns>
        private async Task LoadAlbumDetail()
        {
            IsLoading = true;
            var data = await DataService.GetAlbumInfo(AlbumParam.Name, AlbumParam.Artist);
            if (data != null)
            {
                Album = new MAlbum();
                Album = data;
				Album.Image = AlbumParam.Image;
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
