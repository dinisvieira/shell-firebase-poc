using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Xamarin.Essentials;

namespace ToDoList.Services
{
    internal class AuthenticationService : IAuthenticationService
    {

        /*
              apiKey: "AIzaSyD2mhCOJJICZn97joAurPPcnCOJ6_z4l9k",
              authDomain: "todolist-e2543.firebaseapp.com",
              databaseURL: "https://todolist-e2543-default-rtdb.firebaseio.com",
              projectId: "todolist-e2543",
              storageBucket: "todolist-e2543.appspot.com",
              messagingSenderId: "1009085131762",
              appId: "1:1009085131762:web:b2b9dc36c2848d479dd96b",
              measurementId: "G-RQ1C7YF3R6"
         */

        public static string FirebaseClient = "https://todolist-e2543.firebaseapp.com";
        public static string FirebaseSecret = "AIzaSyD2mhCOJJICZn97joAurPPcnCOJ6_z4l9k";
        
        public async Task<bool> SignInWithEmailAndPasswordAsync(string email, string password)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(FirebaseSecret));

            var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);

            try
            {
                await SecureStorage.SetAsync("firebase_token",auth.FirebaseToken);
                await SecureStorage.SetAsync("firebase_refresh_token",auth.RefreshToken);
                await SecureStorage.SetAsync("email", email);
                await SecureStorage.SetAsync("user_id",auth.User.LocalId);

                return true;
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }

            return false;
        }

        public async Task<bool> HasValidToken()
        {
            try
            {
                var firebaseToken = await SecureStorage.GetAsync("firebase_token");
                var refreshToken = await SecureStorage.GetAsync("firebase_refresh_token");
                var email = await SecureStorage.GetAsync("email");
                var userId = await  SecureStorage.GetAsync("user_id");
                if (!string.IsNullOrWhiteSpace(firebaseToken) && !string.IsNullOrWhiteSpace(refreshToken) && !string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(userId))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }

            return false;
        }
    }
}
