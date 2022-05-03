using Moq;
using NUnit.Framework;
using ToDoList.Services;
using ToDoList.ViewModels;

namespace ToDoList.Tests
{

    [TestFixture]
    public class LoginPageViewModelTest
    {
        private readonly Mock<IAuthenticationService> authenticationServiceMock = new Mock<IAuthenticationService>();

        [TestCase("user", "pass")]
        public void LogInWithValidCreds_LoadingIndicatorShown(string userName, string password)
        {
            LoginViewModel model = CreateViewModelAndLogin(userName, password);

            Assert.IsTrue(model.IsBusy);
        }

        [TestCase("user", "pass")]
        public void LogInWithValidCreds_AuthenticationRequested(string userName, string password)
        {
            CreateViewModelAndLogin(userName, password);

            authenticationServiceMock.Verify(x => x.SignInWithEmailAndPasswordAsync(userName, password), Times.Once);
        }

        [TestCase("", "pass")]
        [TestCase("   ", "pass")]
        [TestCase(null, "pass")]
        public void LogInWithEmptyuserName_AuthenticationNotRequested(string userName, string password)
        {
            CreateViewModelAndLogin(userName, password);

            authenticationServiceMock.Verify(x => x.SignInWithEmailAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        private LoginViewModel CreateViewModelAndLogin(string userName, string password)
        {
            var model = new LoginViewModel(authenticationServiceMock.Object);

            model.Email = userName;
            model.Password = password;

            model.LoginCommand.Execute(null);

            return model;
        }
    }
}