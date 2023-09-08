using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClient
{
    public partial class MainPage : ContentPage
    {
        private readonly HubConnection _hubConnection;
        public MainPage()
        {
            InitializeComponent();

            var baseUrl = "http://localhost";
            //android can't connect to localhost

            if (DeviceInfo.Current.Platform == DevicePlatform.Android) {
                baseUrl = "http://10.0.2.2";
            }   

            _hubConnection= new HubConnectionBuilder().WithUrl($"{baseUrl}:5291/chat").Build();

            _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                lblChat.Text += $"<b>{user}</b>: {message}<br/>";

            });

            Task.Run(() =>
            {
                Dispatcher.Dispatch(async () =>
                {
                    await _hubConnection.StartAsync();
                });
            });
        }
      
        private async void btnSend_Clicked(object sender, EventArgs e)
        {
            await _hubConnection.InvokeCoreAsync("SendMessageToAll", args: new[]
            {
                txtUsername.Text,
                txtMessage.Text
            });

            txtMessage.Text=String.Empty;
        }
    }
}