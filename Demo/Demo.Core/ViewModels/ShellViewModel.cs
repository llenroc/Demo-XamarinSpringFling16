using Demo.Core.ViewModels;
using MvvmCross.Core.ViewModels;

namespace Demo.Core.ViewModels
{
    /// <summary>
    /// Implementa métodos y comandos para el menú hamburguesa (para Windows y iOS).
    /// </summary>
    public class ShellViewModel : BaseViewModel
    {
        
        #region Constructor

        public ShellViewModel()
        {
        }

        #endregion

        #region Commands
        /// <summary>
        /// Navega a la pantalla Home.
        /// </summary>
        private MvxCommand homeCommand;
        public MvxCommand HomeCommand
        {
            get { return homeCommand ?? (homeCommand = new MvxCommand(() => ShowViewModel<HomeViewModel>())); }
        }

        /// <summary>
        /// Navega a la pantalla Artistas.
        /// </summary>
        private MvxCommand artistCommand;
        public MvxCommand ArtistCommand
        {
            get { return artistCommand ?? (artistCommand = new MvxCommand(() => ShowViewModel<ArtistViewModel>())); }
        }

		/// <summary>
		/// Navega a la pantalla Álbumes.
		/// </summary>
		private MvxCommand albumCommand;
		public MvxCommand AlbumCommand
		{
			get { return albumCommand ?? (albumCommand = new MvxCommand(() => ShowViewModel<AlbumViewModel>())); }
		}

        /// <summary>
        /// Navega a la pantalla Canciones.
        /// </summary>
        private MvxCommand trackCommand;
        public MvxCommand TrackCommand
        {
            get { return trackCommand ?? (trackCommand = new MvxCommand(() => ShowViewModel<TrackViewModel>())); }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Permite inicializar el menú hamburguesa y mostrar la pantalla de Home (para Android).
        /// </summary>
        public void ShowMenu()
        {
            ShowViewModel<HomeViewModel>();
            ShowViewModel<MenuViewModel>();
        }
        #endregion

    }
}
