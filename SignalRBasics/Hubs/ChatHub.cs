using Microsoft.AspNetCore.SignalR;

namespace SignalRBasics.Hubs
{
    /// <summary>
    /// ChatHub
    /// Класс хаба.
    /// </summary>
    public class ChatHub:Hub
    {
        /// <summary>
        /// SendMessageToAll
        /// Метод принимающий имя отправителя и сообщение, и рассылающий данные всем подключенным к чату клиентам.
        /// </summary>
        /// <param name="user">Имя пользователя отправителя.</param>
        /// <param name="message">Текст сообщения.</param>
        /// <returns></returns>
        public async Task SendMessageToAll(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage",user, message);
        }
    }
}
