using MvvmCross.Core.ViewModels;
using System;

namespace Demo.Core.ViewModels
{
    /// <summary>
    /// Define los comandos para  las opciones del menú hamburguesa.
    /// </summary>
    public class MenuViewModel
        : BaseViewModel
    {
        public MenuViewModel()
        {
        }

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
    }
}
