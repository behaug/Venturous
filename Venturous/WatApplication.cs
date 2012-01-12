using System;
using WatiN.Core;

namespace Venturous
{
    /// <summary>
    /// Base class for implementing custom application classes
    /// </summary>
    public abstract class WatApplication : IDisposable
    {
        private WatBrowser _browser;

        protected WatApplication(BrowserType browserType)
        {
            _browser = new WatBrowser(browserType);
        }

        /// <summary>Sets the number of seconds to wait for a request to complete. The default is 30 sec. Increase this if the application is sometimes too slow.</summary>
        public void SetWaitForCompleteTimeout(int seconds)
        {
            Settings.WaitForCompleteTimeOut = seconds;
            Settings.WaitUntilExistsTimeOut = seconds;
        }

        public WatBrowser Browser
        {
            get { return _browser; }
        }

        /// <summary>Opens the given URL, and handles login.</summary>
        public void Open(string url)
        {
            _browser.GoTo(url);

            if (IsLoginPage())
                LogIn();
        }

        /// <summary>Returns whether the current page is a login page.</summary>
        protected virtual bool IsLoginPage()
        {
            return false;
        }

        /// <summary>Logs in using the current page as a login page.</summary>
        protected virtual void LogIn()
        {
        }

        /// <summary>Closes the browser.</summary>
        public void Dispose()
        {
            _browser.Dispose();
        }
    }
}