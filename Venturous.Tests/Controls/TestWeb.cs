namespace Venturous.Tests.Controls
{
    class TestWeb : WatApplication
    {
        private const string BaseUrl = "http://localhost/Venturous.TestWeb/";

        public TestWeb(BrowserType browserType) 
            : base(browserType)
        {
        }

        public UpdPanelPage UpdPanelFrame
        {
            get { return Browser.Frame<UpdPanelPage>(0); }
        }

        public void OpenDefault()
        {
            Browser.GoTo(BaseUrl);
        }

        public DefaultPage DefaultPage
        {
            get { return Browser.Page<DefaultPage>(); }
        }

        public void OpenFrameSet()
        {
            Browser.GoTo(BaseUrl + "frameset.htm");
        }

        public FrameContentPage RightFrame
        {
            get { return Browser.Frame<FrameContentPage>(1); }
        }

        public void OpenFrameContent()
        {
            Browser.GoTo(BaseUrl + "framecontent.htm");
        }

        public FrameContentPage FrameContentPage
        {
            get { return Browser.Page<FrameContentPage>(); }
        }

        public void OpenUpdPanel()
        {
            Browser.GoTo(BaseUrl + "UpdPanel.aspx");
        }

        public UpdPanelPage UpdPanelPage
        {
            get { return Browser.Page<UpdPanelPage>(); }
        }

        public void OpenSlowBoot()
        {
            Browser.GoTo(BaseUrl + "SlowBoot.aspx");
        }

        public SlowBootPage SlowBootPage
        {
            get { return Browser.Page<SlowBootPage>(); }
        }

        public void OpenTextAndValue()
        {
            Browser.GoTo(BaseUrl + "TextAndValue.aspx");
        }

        public TextAndValuePage TextAndValuePage
        {
            get { return Browser.Page<TextAndValuePage>(); }
        }
    }
}