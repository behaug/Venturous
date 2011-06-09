using System;
using NUnit.Framework;

namespace Venturous.Tests
{
    [TestFixture, RequiresSTA]
    public class FeatureTests
    {
        private TestApplication _app;

        [SetUp]
        public void Setup()
        {
            _app = new TestApplication(BrowserType.FireFox);
        }

        [TearDown]
        public void Teardown()
        {
            _app.Dispose();
        }

        [Test]
        public void UpdatePanel()
        {
            _app.OpenUpdPanel();
            Assert.That(_app.UpdPanelPage.PanelDiv.ClickAndGetUpdatedText(), Is.EqualTo("I was clicked"));
        }

        [Test]
        public void ServerError()
        {
            _app.OpenDefault();
            var ex = Assert.Throws<ServerErrorException>(() => _app.DefaultPage.ClickServerErrorButton());
            //Trace.WriteLine(ex.ToString());
        }

        [Test]
        public void Frames()
        {
            _app.OpenFrameSet();
            Assert.That(_app.RightFrame.MessageDiv1.MessageText, Is.EqualTo("Wrapper1 message"));
        }

        [Test]
        public void UpdatePanelInsideFrame()
        {
            _app.OpenFrameSet();
            Assert.That(_app.UpdPanelFrame.PanelDiv.ClickAndGetUpdatedText(), Is.EqualTo("I was clicked"));
        }

        [Test]
        public void ChildSelection()
        {
            _app.OpenFrameContent();
            Assert.That(_app.FrameContentPage.MessageDiv1.MessageText, Is.EqualTo("Wrapper1 message"));
            Assert.That(_app.FrameContentPage.MessageDiv2.MessageText, Is.EqualTo("Wrapper2 message"));
        }

        [Test]
        public void SelectByAttribute()
        {
            _app.OpenDefault();
            _app.DefaultPage.TopMenu.ClickGoogle();
            Assert.That(_app.Browser.Url, Is.StringContaining("google"));
        }

        [Test]
        public void ElementOnClickHandler()
        {
            _app.OpenDefault();
            _app.DefaultPage.TopMenu.ClickBing();
            Assert.That(_app.Browser.Url, Is.StringContaining("bing"));
        }

        [Test]
        public void ThrowsOnMissingElement()
        {
            _app.OpenDefault();
            string messageText;
            Assert.Throws<Exception>(() => messageText = _app.FrameContentPage.MessageDiv1.MessageText);
        }
    }

    class TestApplication : WatApplication
    {
        private const string BaseUrl = "http://localhost/Venturous.TestWeb/";

        public TestApplication(BrowserType browserType) 
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

        public void OpenFrameSet()
        {
            Browser.GoTo(BaseUrl + "frameset.htm");
        }

        public void OpenFrameContent()
        {
            Browser.GoTo(BaseUrl + "framecontent.htm");
        }

        public void OpenUpdPanel()
        {
            Browser.GoTo(BaseUrl + "UpdPanel.aspx");
        }

        public DefaultPage DefaultPage
        {
            get { return Browser.Page<DefaultPage>(); }
        }

        public FrameContentPage RightFrame
        {
            get { return Browser.Frame<FrameContentPage>(1); }
        }

        public FrameContentPage FrameContentPage
        {
            get { return Browser.Page<FrameContentPage>(); }
        }

        public UpdPanelPage UpdPanelPage
        {
            get { return Browser.Page<UpdPanelPage>(); }
        }
    }

    class DefaultPage : WatPage
    {
        public void ClickServerErrorButton()
        {
            Element.FindId("btnServerError").Click();
        }

        public TestMenu TopMenu
        {
            get { return Element.FindId<TestMenu>("topMenu"); }
        }
    }

    class UpdPanelPage : WatPage
    {
        public PanelDiv PanelDiv
        {
            get { return Element.FindId<PanelDiv>("panelDiv"); }
        }
    }

    class PanelDiv : WatControl
    {
        public void ClickButton()
        {
            Element.FindId("btnTest").Click();
        }

        public string LabelText
        {
            get { return Element.FindId("lblText").Text; }
        }

        public string ClickAndGetUpdatedText()
        {
            ClickButton();
            return LabelText;
        }
    }

    class TestMenu : WatControl
    {
        public void ClickGoogle()
        {
            Element.FindAttribute("resKey", "mnuItem1").FindTag("a").Click();
        }

        public void ClickBing()
        {
            Element.FindAttribute("resKey", "mnuItem2").FindTag("span").Click();
        }
    }

    class FrameContentPage : WatPage
    {
        public MessageDiv MessageDiv1
        {
            get { return Element.FindId<MessageDiv>("wrapper1"); }
        }

        public MessageDiv MessageDiv2
        {
            get { return Element.FindId<MessageDiv>("wrapper2"); }
        }
    }

    class MessageDiv : WatControl
    {
        public string MessageText
        {
            get { return Element.FindId("message").Text; }
        }
    }
}
