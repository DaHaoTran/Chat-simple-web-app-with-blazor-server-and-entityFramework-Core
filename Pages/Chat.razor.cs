using ChatSimple.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Net.WebRequestMethods;
using static System.Reflection.Metadata.BlobBuilder;

namespace ChatSimple.Pages
{
    public partial class Chat
    {
        private List<Message> _messages { get; set; } = new();
        [SupplyParameterFromForm]
        private Message _message { get; set; } = new();

        private HubConnection hubConnection;

        protected override async Task OnInitializedAsync()
        {

            hubConnection = new HubConnectionBuilder()
                .WithUrl(_nav.ToAbsoluteUri("/broadcastHub"))
                .Build();

            hubConnection.On("ReceiveMessage", () =>
            {
                CallLoadData();
                InvokeAsync(StateHasChanged);
            });

            await hubConnection.StartAsync();

            await LoadData();
        }

        private void CallLoadData()
        {
            Task.Run(async () =>
            {
                await LoadData();
            });
        }

        protected async Task LoadData()
        {
            _messages = await _testSvc.GetMessages();
            await InvokeAsync(StateHasChanged);
        }

        Task SendMessage() => hubConnection.SendAsync("SendMessage");

        public bool IsConnected =>
            hubConnection.State == HubConnectionState.Connected;

        public void Dispose()
        {
            _ = hubConnection.DisposeAsync();
        }

        private async void Post()
        {
            _message.email = LoginState.email;
            _message.time = DateTime.Now;
            _message.name = LoginState.name;
            await _testSvc.AddNewMessage(_message);
            _message = new();
            if (IsConnected) await SendMessage();
            _nav.NavigateTo("/chat");
        }
    }
}
