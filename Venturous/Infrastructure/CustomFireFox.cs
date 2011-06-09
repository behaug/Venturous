using System.Text.RegularExpressions;
using WatiN.Core;

namespace Venturous.Infrastructure
{
    /// <summary>
    /// Custom subclass of FireFox to make it throw on server errors
    /// </summary>
    class CustomFireFox : FireFox
    {
        public override void WaitForComplete(int waitForCompleteTimeOut)
        {
            base.WaitForComplete(waitForCompleteTimeOut);

            if (Regex.IsMatch(Html, "Server Error in '([^']+)' Application."))
                throw new ServerErrorException(Url, Title, Html);
        }
    }
}