using System;
using System.Threading.Tasks;

namespace Demo.Core.Services.Message
{
    /// <summary>
    /// Interfaz que define métodos para el menejo de alertas en la aplicación.
    /// </summary>
    public interface IMessageService
    {
        #region Metodos
        
        /// <summary>
        /// Método para mostrar un mensaje de alerta
        /// </summary>
        /// <param name="message">Mensaje a mostrar</param>
        /// <param name="done">Accion al terminar de mostrar el mensaje</param>
        /// <param name="title">Titulo del mensaje</param>
        /// <param name="okButton">Nombre del botón OK</param>
		void Alert(string message, Action done = null, string title = "", string okButton = "OK");

        /// <summary>
        /// Método para mostrar un mensaje de alerta asincrono
        /// </summary>
        /// <param name="message">Mensaje a mostrar</param>
        /// <param name="title">Titulo del mensaje</param>
        /// <param name="okButton">Nombre del botón OK</param>
        /// <returns></returns>
		Task AlertAsync(string message, string title = "", string okButton = "OK");
        
        #endregion
    }
}
