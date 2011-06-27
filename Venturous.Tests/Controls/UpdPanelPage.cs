namespace Venturous.Tests.Controls
{
    class UpdPanelPage : WatPage
    {
        public PanelDiv PanelDiv = Auto<PanelDiv>();
    }

    class PanelDiv : WatControl
    {
        private WatElement _btnTest = Auto();
        private WatElement _lblText = Auto();

        public void ClickButton()
        {
            _btnTest.Click();
        }

        public string LabelText
        {
            get { return _lblText.Text; }
        }

        public string ClickAndGetUpdatedText()
        {
            ClickButton();
            return LabelText;
        }
    }

}