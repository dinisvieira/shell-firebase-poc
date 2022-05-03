using ToDoList.Services;
using ToDoList.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            var authService = DependencyService.Get<IAuthenticationService>();
            this.BindingContext = new LoginViewModel(authService);
        }
    }
}