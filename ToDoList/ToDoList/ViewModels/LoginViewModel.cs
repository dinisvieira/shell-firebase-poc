using System.Threading.Tasks;
using ToDoList.Services;
using ToDoList.Views;
using Xamarin.Forms;

namespace ToDoList.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        public Command LoginCommand { get; }

        public LoginViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            LoginCommand = new Command(OnLoginClicked);

            _ = CheckLogin();
        }

        private async Task CheckLogin()
        {
            if (await _authenticationService.HasValidToken())
            {
                await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private async void OnLoginClicked(object obj)
        {
            try
            {
                IsBusy = true;

                if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password)) { return; }

                var loggedIn = await _authenticationService.SignInWithEmailAndPasswordAsync(Email, Password);

                if (loggedIn)
                {
                    Email = string.Empty;
                    Password = string.Empty;

                    await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
