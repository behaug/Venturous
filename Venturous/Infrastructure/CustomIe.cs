using SHDocVw;
using WatiN.Core;
using WatiN.Core.Native.InternetExplorer;

namespace Venturous.Infrastructure
{
    /// <summary>
    /// Custom subclass of IE to make it throw on server errors
    /// </summary>
    class CustomIe : IE
    {
        #region ErrorDetails
        private class ErrorDetails
        {
            public readonly string Url;
            public readonly int StatusCode;

            public ErrorDetails(string url, int statusCode)
            {
                Url = url;
                StatusCode = statusCode;
            }
        }
        #endregion

        #region AttachToCustomIeHelper
        private class AttachToCustomIeHelper : AttachToIeHelper
        {
            protected override IE CreateBrowserInstance(IEBrowser browser)
            {
                return new CustomIe(browser);
            }
        }
        #endregion

        static CustomIe()
        {
            RegisterAttachToHelper(typeof(CustomIe), new AttachToCustomIeHelper());
        }

        private ErrorDetails _error;

        private CustomIe(IEBrowser browser)
            : base(browser)
        {
            var internetExplorer = (InternetExplorerClass)InternetExplorer;
            internetExplorer.BeforeNavigate += BeforeNavigate;
            internetExplorer.NavigateError += NavigateError;
        }

        public CustomIe()
        {
            var internetExplorer = (InternetExplorerClass)InternetExplorer;
            internetExplorer.BeforeNavigate += BeforeNavigate;
            internetExplorer.NavigateError += NavigateError;
        }

        public override void WaitForComplete(int waitForCompleteTimeOut)
        {
            base.WaitForComplete(waitForCompleteTimeOut);

            if (_error != null)
                throw new ServerErrorException(_error.Url, Title, Html);
        }

        void BeforeNavigate(string url, int flags, string targetFrameName, ref object postData, string headers, ref bool cancel)
        {
            _error = null;
        }

        void NavigateError(object pDisp, ref object url, ref object frame, ref object statusCode, ref bool cancel)
        {
            _error = new ErrorDetails((string)url, (int)statusCode);
        }
    }
}