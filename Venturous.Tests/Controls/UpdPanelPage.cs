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
        private WatElement _ddlDropdown = Auto();

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

        public void SelectDropdownOption(string optionValue)
        {
            _ddlDropdown.SelectOption(optionValue);
        }

        public bool IsAppearOnThirdShown
        {
            get
            {
                return Element.CanFind("lblAppearOnThird");
            }
        }
    }

}