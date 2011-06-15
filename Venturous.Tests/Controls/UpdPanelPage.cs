namespace Venturous.Tests.Controls
{
    class UpdPanelPage : WatPage
    {
        public PanelDiv PanelDiv
        {
            get { return Element.Find<PanelDiv>("panelDiv"); }
        }
    }

    class PanelDiv : WatControl
    {
        public void ClickButton()
        {
            Element.Find("btnTest").Click();
        }

        public string LabelText
        {
            get { return Element.Find("lblText").Text; }
        }

        public string ClickAndGetUpdatedText()
        {
            ClickButton();
            return LabelText;
        }
    }

}