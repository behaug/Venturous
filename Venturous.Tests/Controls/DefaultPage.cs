using System;
using NUnit.Framework;

namespace Venturous.Tests.Controls
{
    class DefaultPage : WatPage
    {
        public void ClickServerErrorButton()
        {
            Element.Find("btnServerError").Click();
        }

        public TestMenu TopMenu
        {
            get { return Element.Find<TestMenu>("topMenu"); }
        }

        public bool HasTopMenu
        {
            get { return Element.CanFind("topMenu"); }
        }

        public bool HasFlamingLogo
        {
            get { return Element.CanFind("flaming_logo"); }
        }

        public WatElement Dropdown
        {
            get { return Element.Find("dropdown"); }
        }

        public string DropdownValue
        {
            get { return Element.Find("dropdown_value").Text; }
        }

        public WatElement Listbox
        {
            get { return Element.Find("listbox"); }
        }

        public string ListboxValue
        {
            get { return Element.Find("listbox_value").Text; }
        }

        public WatElement Multiselect
        {
            get { return Element.Find("multiselect"); }
        }

        public string MultiselectValue
        {
            get { return Element.Find("multiselect_value").Text; }
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