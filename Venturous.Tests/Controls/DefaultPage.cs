using System;
using NUnit.Framework;

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

        public WatElement Dropdown
        {
            get { return Element.FindId("dropdown"); }
        }

        public string DropdownValue
        {
            get { return Element.FindId("dropdown_value").Text; }
        }

        public WatElement Listbox
        {
            get { return Element.FindId("listbox"); }
        }

        public string ListboxValue
        {
            get { return Element.FindId("listbox_value").Text; }
        }

        public WatElement Multiselect
        {
            get { return Element.FindId("multiselect"); }
        }

        public string MultiselectValue
        {
            get { return Element.FindId("multiselect_value").Text; }
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