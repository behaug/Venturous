using System;
using Venturous.Infrastructure;
using WatiN.Core;

namespace Venturous
{
    /// <summary>
    /// Represents the browser and the contents displayed within it.
    /// </summary>
    public class WatBrowser : IDisposable
    {
        private Browser _browser;
        private Type _browserType;

        /// <summary>Instantiates a browser using Internet Explorer as the browser type</summary>
        public WatBrowser()
            : this(BrowserType.IE)
        { }

        /// <summary>Instantiates a browser using the given browser type</summary>
        public WatBrowser(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.IE:
                    _browser = new CustomIe();
                    break;
                case BrowserType.FireFox:
                    _browser = new CustomFireFox();
                    break;
                default:
                    throw new ArgumentException("The given browser type is not supported: " + browserType);
            }

            _browserType = _browser.GetType();
        }

        private WatBrowser(Browser browser)
        {
            _browser = browser;
            _browserType = _browser.GetType();
        }

        /// <summary>Closes the browser</summary>
        public void Dispose()
        {
            _browser.Dispose();
        }

        /// <summary>Returns a given page type</summary>
        public TPage Page<TPage>() where TPage : WatPage, new()
        {
            return CreatePage<TPage>(_browser);
        }

        /// <summary>Returns a given page type inside a given browser frame</summary>
        public TPage Frame<TPage>(int index) where TPage : WatPage, new()
        {
            return CreatePage<TPage>(_browser.Frames[index]);
        }

        /// <summary>Returns a new browser instance for the window with the given title</summary>
        public WatBrowser GetBrowserForWindow(string title)
        {
            var newBrowser = Browser.AttachTo(_browserType, Find.ByTitle(title));
            var windowBrowser = new WatBrowser(newBrowser);
            return windowBrowser;
        }

        private TPage CreatePage<TPage>(Document document) where TPage : WatPage, new()
        {
            var page = new TPage();
            page.InitializePage(document);
            return page;
        }

        /// <summary>Navigates the browser to the given URL</summary>
        public void GoTo(string url)
        {
            _browser.NativeBrowser.NavigateTo(new Uri(url));
            _browser.DomContainer.WaitForComplete(100); // Extra long timeout to allow the application to boot up
        }

        /// <summary>Returns the URL of the current page</summary>
        public string Url
        {
            get { return _browser.Url; }
        }
    }
}