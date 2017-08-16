using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Examen.Automatizacion
{
    public class LoginPage
    {
        const string url = "http://localhost:57264/"; // Ejecutar el proyecto angular sin depurar

        private readonly IWebDriver _driver;

        #region Products Page Elements
        [FindsBy(How = How.CssSelector, Using = "a[ui-sref='login']")]
        private IWebElement loginLink = null;

        [FindsBy(How = How.CssSelector, Using = "input#userName")]
        private IWebElement userNameText = null;

        [FindsBy(How = How.CssSelector, Using = "input#password")]
        private IWebElement passwordText = null;

        [FindsBy(How = How.CssSelector, Using = "button[type='submit']")]
        private IWebElement loginButton = null;


        [FindsBy(How = How.CssSelector, Using = ".alert.alert-danger")]
        private IWebElement divAlert = null;
        #endregion

        public LoginPage()
        {
            _driver = Driver.Instance;
            PageFactory.InitElements(_driver, this);
        }

        public void GoToUrl()
        {
            Driver.Instance.Navigate().GoToUrl(url);
        }

        public void GoToLogin()
        {
            loginLink.Click();
        }

        public void RegisterDataLogin_OK()
        {
            userNameText.SendKeys("daniel@quintana.com");
            passwordText.SendKeys("prueba");
        }

        public void RegisterDataLogin_ERROR()
        {
            userNameText.SendKeys("daniel@quintana.com");
            passwordText.SendKeys("prueba2");
        }

        public void SignIn()
        {
            loginButton.Click();
        }

        public bool GetLoginLink()
        {
            return loginLink == null;
        }

        public bool GetDivAlert()
        {
            return divAlert != null;
        }

    }
}
