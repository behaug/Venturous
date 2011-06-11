namespace Venturous.Tests.Controls
{
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

}