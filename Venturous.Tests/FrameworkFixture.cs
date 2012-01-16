using System;
using NUnit.Framework;
using Venturous.Tests.Controls;

namespace Venturous.Tests
{
    [TestFixture, RequiresSTA]
    public class FrameworkFixture
    {
        private TestWeb _app;

        [TestFixtureSetUp]
        public void Setup()
        {
            _app = new TestWeb(BrowserType.FireFox);
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            _app.Dispose();
        }

        [Test]
        public void WaitsForUpdatePanel()
        {
            _app.OpenUpdPanel();
            Assert.That(_app.UpdPanelPage.PanelDiv.ClickAndGetUpdatedText(), Is.EqualTo("I was clicked"));
        }

        [Test]
        public void WaitsForUpdatePanelAfterDropdownSelection()
        {
            _app.OpenUpdPanel();
            _app.UpdPanelPage.PanelDiv.SelectDropdownOption("3");
            Assert.That(_app.UpdPanelPage.PanelDiv.IsAppearOnThirdShown);
        }

        [Test]
        public void ThrowsOnServerError()
        {
            _app.OpenDefault();
            Assert.Throws<ServerErrorException>(() => _app.DefaultPage.ClickServerErrorButton());
        }

        [Test]
        public void CanFindElementsInsideFrames()
        {
            _app.OpenFrameSet();
            Assert.That(_app.RightFrame.MessageDiv1.MessageText, Is.EqualTo("Wrapper1 message"));
        }

        [Test]
        public void CanFindByAttribute()
        {
            _app.OpenDefault();
            _app.DefaultPage.TopMenu.ClickGoogle();
            Assert.That(_app.Browser.Url, Is.StringContaining("google"));
        }

        [Test]
        public void ClickTriggersJavaScriptClickHandler()
        {
            _app.OpenDefault();
            _app.DefaultPage.TopMenu.ClickBing();
            Assert.That(_app.Browser.Url, Is.StringContaining("bing"));
        }

        [Test]
        public void WaitsForUpdatePanelInsideFrame()
        {
            _app.OpenFrameSet();
            Assert.That(_app.UpdPanelFrame.PanelDiv.ClickAndGetUpdatedText(), Is.EqualTo("I was clicked"));
        }

        [Test]
        public void FindsChildElementScopedInsideParent()
        {
            _app.OpenFrameContent();
            Assert.That(_app.FrameContentPage.MessageDiv1.MessageText, Is.EqualTo("Wrapper1 message"));
            Assert.That(_app.FrameContentPage.MessageDiv2.MessageText, Is.EqualTo("Wrapper2 message"));
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
    }
}
