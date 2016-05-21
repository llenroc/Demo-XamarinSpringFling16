using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

namespace Demo.Core.Services.Message
{
    /// <summary>
    /// Clase para cargar los Plugins de MvvmCross
    /// </summary>
	public class PluginLoader : IMvxPluginLoader
    {
        #region Properties
        /// <summary>
        /// Instancia del PluginLoader
        /// </summary>
        public static readonly PluginLoader Instance = new PluginLoader();
        #endregion

        #region Methods
        /// <summary>
        /// Método para asegurarse que está cargado el plugin correctamente
        /// </summary>
        public void EnsureLoaded()
        {
            var manager = Mvx.Resolve<IMvxPluginManager>();
            manager.EnsurePlatformAdaptionLoaded<PluginLoader>();
        }
        #endregion
    }
}
