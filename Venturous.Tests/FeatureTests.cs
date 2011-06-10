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
            Assert.Throws<ServerErrorException>(() => _app.DefaultPage.ClickServerErrorButton());
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
            Assert.Throws<Exception>(() => { 
                var messageText = _app.FrameContentPage.MessageDiv1.MessageText; 
            });
        }

        [Test]
        public void WaitsForSlowBoot()
        {
            _app.OpenSlowBoot();
            Assert.That(_app.SlowBootPage.Greeting, Is.EqualTo("Hello"));
        }

        [Test]
        public void GetText()
        {
            _app.OpenTextAndValue();
            var page = _app.TextAndValuePage;

            AssertText(page.Span1, "text");
            AssertText(page.Text1, "value");
            AssertText(page.Text2, "value");
            AssertText(page.Check1, "");
            AssertText(page.Radio1, "");
            AssertText(page.Button1, "value");
            AssertText(page.Textarea1, "text");
        }

        [Test]
        public void SetText()
        {
            _app.OpenTextAndValue();
            var page = _app.TextAndValuePage;

            Assert.Throws<Exception>(() => page.Span1.Text = "text");
            Assert.Throws<Exception>(() => page.Check1.Text = "a");
            Assert.Throws<Exception>(() => page.Radio1.Text = "a");

            page.Text1.Text = "1";
            AssertText(page.Text1, "1");

            page.Text2.Text = "2";
            AssertText(page.Text2, "2");
            
            page.Button1.Text = "3";
            AssertText(page.Button1, "3");

            page.Textarea1.Text = "4";
            AssertText(page.Textarea1, "4");
        }

        private void AssertText(WatElement element, string text)
        {
            Assert.That(element.Text, Is.EqualTo(text), "Wrong text on " + element);
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

        public void OpenSlowBoot()
        {
            Browser.GoTo(BaseUrl + "SlowBoot.aspx");
        }

        public void OpenTextAndValue()
        {
            Browser.GoTo(BaseUrl + "TextAndValue.aspx");
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

        public SlowBootPage SlowBootPage
        {
            get { return Browser.Page<SlowBootPage>(); }
        }

        public TextAndValuePage TextAndValuePage
        {
            get { return Browser.Page<TextAndValuePage>(); }
        }
    }

    class TextAndValuePage : WatPage
    {
        public WatElement Span1 { get { return Element.FindId("span1"); } }
        public WatElement Text1 { get { return Element.FindId("text1"); } }
        public WatElement Text2 { get { return Element.FindId("text2"); } }
        public WatElement Check1 { get { return Element.FindId("check1"); } }
        public WatElement Radio1 { get { return Element.FindId("radio1"); } }
        public WatElement Button1 { get { return Element.FindId("button1"); } }
        public WatElement Textarea1 { get { return Element.FindId("textarea1"); } }
    }

    class SlowBootPage : WatPage
    {
        public string Greeting
        {
            get { return Element.FindId("greeting").Text; }
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
