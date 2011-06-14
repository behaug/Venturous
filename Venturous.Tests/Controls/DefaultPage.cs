namespace Venturous.Tests.Controls
{
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

        public bool HasTopMenu
        {
            get { return Element.CanFindId("topMenu"); }
        }

        public bool HasFlamingLogo
        {
            get { return Element.CanFindId("flaming_logo"); }
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

}