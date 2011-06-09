using System;

namespace Venturous
{
    /// <summary>
    /// Thrown when the application under test returns a server error
    /// </summary>
    public class ServerErrorException : Exception
    {
        public ServerErrorException(string url, string message, string html)
            : base(string.Format("A server error occurred for url {0}: {1}", url, message))
        {
            Html = html;
        }

        public string Html { get; private set; }
    }
}