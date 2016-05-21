using MvvmCross.Plugins.Messenger;
using System;

namespace Demo.Core.Services.Network
{
    public interface INetworkService
    {
        /// <summary>
        /// Propiedad para verificar si la red está activa
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Propiedad para verificar si es conexión por WiFi
        /// </summary>
        bool IsWifi { get; }

        /// <summary>
        /// Propiedad para verificar si es red celular
        /// </summary>
        bool IsMobile { get; }

        /// <summary>
        /// Método para suscribirte al cambio de red
        /// </summary>
        /// <param name="action">Acción a realizar</param>
        /// <returns></returns>
        MvxSubscriptionToken Subscribe(Action<NetworkStatusChangedMessage> action);
    }
}
