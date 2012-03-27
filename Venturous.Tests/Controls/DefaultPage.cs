using System;
using NUnit.Framework;

namespace Venturous.Tests.Controls
{
    class DefaultPage : WatPage
    {
        private WatElement _btnServerError = Auto();
        private WatElement _dropdownValue = Auto("dropdown_value");
        private WatElement _listboxValue = Auto("listbox_value");
        private WatElement _multiselectValue = Auto("multiselect_value");
        private WatElement _openWidow = Auto("open_window");

        public TestMenu TopMenu = Auto<TestMenu>();
        public WatElement Dropdown = Auto();
        public WatElement Listbox = Auto();
        public WatElement Multiselect = Auto();
        public WatElement TxtInput = Auto();

        public void ClickServerErrorButton()
        {
            _btnServerError.Click();
        }

        public bool HasTopMenu
        {
            get { return Element.CanFind("topMenu"); }
        }

        public bool HasTopMenuLowercase
        {
            get { return Element.CanFind("topmenu"); }
        }

        public bool HasFlamingLogo
        {
            get { return Element.CanFind("flaming_logo"); }
        }

        public string DropdownValue
        {
            get { return _dropdownValue.Text; }
        }

        public string ListboxValue
        {
            get { return _listboxValue.Text; }
        }

        public string MultiselectValue
        {
            get { return _multiselectValue.Text; }
        }

        public void ClickOpenWidowLink()
        {
            _openWidow.Click();
        }
    }

    class TestMenu : WatControl
    {
        public void ClickGoogle()
        {
            Element.Find(By.Attribute("resKey", "mnuItem1")).Find(By.Tag("a")).Click();
        }

        public void ClickBing()
        {
            Element.Find(By.Attribute("resKey", "mnuItem2")).Find(By.Tag("span")).Click();
        }
    }

}