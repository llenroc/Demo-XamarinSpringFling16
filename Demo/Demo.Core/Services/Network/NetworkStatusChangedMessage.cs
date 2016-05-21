using MvvmCross.Plugins.Messenger;

namespace Demo.Core.Services.Network
{
    /// <summary>
    /// Clase para la notificación del cambio de red
    /// </summary>
    public class NetworkStatusChangedMessage : MvxMessage
    {
        /// <summary>
        /// Enumable del estado de la red
        /// </summary>
        public INetworkService Status { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="networkService">Tipo de servicio</param>
        public NetworkStatusChangedMessage(INetworkService networkService) : base(networkService)
        {
            this.Status = networkService;
        }
    }
}
