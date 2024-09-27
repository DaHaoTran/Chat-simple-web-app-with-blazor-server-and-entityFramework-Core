using ChatSimple.Data;
using ChatSimple.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ChatSimple.Pages
{
    public partial class Index
    {
        private static string state = string.Empty;
        [SupplyParameterFromForm]
        private Account _account { get; set; }
        protected override void OnInitialized()
        {
            _account = new Account();
        }
        public async void CheckLogin()
        {
            if(await _testSvc.Login(_account.email, _account.password))
            {
                LoginState.email = _account.email;
                var acc = await _testSvc.GetAccount(_account.email);
                LoginState.name = acc.username;
                _nav.NavigateTo("/chat");
            } else
            {
                state = "Not Found";
            }
        }
    }
}
