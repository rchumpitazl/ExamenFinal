using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examen.Automatizacion.Tests
{
    [TestClass]
    public class LoginPageTests
    {
        private readonly LoginPage _loginPage;

        public LoginPageTests()
        {
            Driver.GetInstance();
            _loginPage = new LoginPage();
        }

        [TestMethod]
        public void Login_OK_Automation()
        {
            _loginPage.GoToUrl();
            _loginPage.GoToLogin();
            _loginPage.RegisterDataLogin_OK();
            _loginPage.SignIn();

            _loginPage.GetLoginLink().Should().BeFalse();

            Driver.CloseInstance();
        }

        [TestMethod]
        public void Login_ERROR_Automation()
        {
            _loginPage.GoToUrl();
            _loginPage.GoToLogin();
            _loginPage.RegisterDataLogin_ERROR();
            _loginPage.SignIn();

            _loginPage.GetDivAlert().Should().BeTrue();

            Driver.CloseInstance();
        }
    }
}
