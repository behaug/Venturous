using System.Text.RegularExpressions;
using WatiN.Core;
using WatiN.Core.Native.Mozilla;

namespace Venturous.Infrastructure
{
    /// <summary>
    /// Custom subclass of FireFox to make it throw on server errors
    /// </summary>
    class CustomFireFox : FireFox
    {
        #region AttachToCustomFireFoxHelper
        private class AttachToCustomFireFoxHelper : AttachToFireFoxHelper
        {
            protected override FireFox  CreateBrowserInstance(FFBrowser browser)
            {
 	             return new CustomFireFox(browser);
            }
        }
        #endregion

        static CustomFireFox()
        {
            RegisterAttachToHelper(typeof(CustomFireFox), new AttachToCustomFireFoxHelper());
        }

        public CustomFireFox()
        {}

        public CustomFireFox(FFBrowser browser)
            :base(browser)
        {}

        public override void WaitForComplete(int waitForCompleteTimeOut)
        {
            base.WaitForComplete(waitForCompleteTimeOut);

            if (Regex.IsMatch(Html, "Server Error in '([^']+)' Application."))
                throw new ServerErrorException(Url, Title, Html);
        }
    }
}