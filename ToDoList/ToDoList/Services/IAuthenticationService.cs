using System.Threading.Tasks;

namespace ToDoList.Services
{
    public interface IAuthenticationService
    {
        Task<bool> SignInWithEmailAndPasswordAsync(string email, string password);
        Task<bool> HasValidToken();
    }
}
