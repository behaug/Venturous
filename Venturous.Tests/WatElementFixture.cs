using System;
using NUnit.Framework;
using Venturous.Tests.Controls;
using WatiN.Core.Exceptions;

namespace Venturous.Tests
{
    [TestFixture, RequiresSTA]
    public class WatElementFixture
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
        public void GetText_OnInputs_ReturnsValue()
        {
            _app.OpenTextAndValue();
            var page = _app.TextAndValuePage;

            Assert.That(page.Text1.Text, Is.EqualTo("value"));
            Assert.That(page.Text2.Text, Is.EqualTo("value"));
            Assert.That(page.Button1.Text, Is.EqualTo("value"));
            Assert.That(page.Textarea1.Text, Is.EqualTo("text"));
            Assert.That(page.TextDisabled.Text, Is.EqualTo("value"));
        }

        [Test]
        public void GetText_OnCheckbox_ReturnsEmpty()
        {
            _app.OpenTextAndValue();

            Assert.That(_app.TextAndValuePage.Check1.Text, Is.Empty);
            Assert.That(_app.TextAndValuePage.Radio1.Text, Is.Empty);
        }

        [Test]
        public void GetText_OnNonInputs_ReturnsInnerText()
        {
            _app.OpenTextAndValue();
            Assert.That(_app.TextAndValuePage.Span1.Text, Is.EqualTo("text"));
        }

        [Test]
        public void SetText_OnNonInputs_Throws()
        {
            _app.OpenTextAndValue();
            Assert.Throws<Exception>(() => _app.TextAndValuePage.Span1.Text = "whatever");
        }

        [Test]
        public void SetText_OnCheckbox_Throws()
        {
            _app.OpenTextAndValue();
            Assert.Throws<Exception>(() => _app.TextAndValuePage.Check1.Text = "whatever");
            Assert.Throws<Exception>(() => _app.TextAndValuePage.Radio1.Text = "whatever");
        }

        [Test]
        public void SetText_OnInputs_SetsValue()
        {
            _app.OpenTextAndValue();
            var page = _app.TextAndValuePage;

            page.Text1.Text = "1";
            page.Text2.Text = "2";
            page.Button1.Text = "3";
            page.Textarea1.Text = "4";

            Assert.That(page.Text1.Text, Is.EqualTo("1"));
            Assert.That(page.Text2.Text, Is.EqualTo("2"));
            Assert.That(page.Button1.Text, Is.EqualTo("3"));
            Assert.That(page.Textarea1.Text, Is.EqualTo("4"));
        }

        [Test]
        public void SetText_OnDisabledInput_Throws()
        {
            _app.OpenTextAndValue();
            Assert.Throws<Exception>(() => _app.TextAndValuePage.TextDisabled.Text = "whatever");
        }

        [Test]
        public void Click_OnDisabledButton_Throws()
        {
            _app.OpenTextAndValue();
            Assert.Throws<ElementDisabledException>(() => _app.TextAndValuePage.ButtonDisabled.Click());
        }

        [Test]
        public void CanFindId_WhenElementExists_ReturnsTrue()
        {
            _app.OpenDefault();
            Assert.IsTrue(_app.DefaultPage.HasTopMenu);
        }

        [Test]
        public void CanFindId_WhenElementDoesntExists_ReturnsFalse()
        {
            _app.OpenDefault();
            Assert.IsFalse(_app.DefaultPage.HasFlamingLogo);
        }

        [Test]
        public void SelectOption_Dropdown_ByValue()
        {
            _app.OpenDefault();
            _app.DefaultPage.Dropdown.SelectOption("item3");
            Assert.That(_app.DefaultPage.DropdownValue, Is.EqualTo("item3"));
        }

        [Test]
        public void SelectOption_Dropdown_ByIndex()
        {
            _app.OpenDefault();
            _app.DefaultPage.Dropdown.SelectOption(2);
            Assert.That(_app.DefaultPage.DropdownValue, Is.EqualTo("item3"));
        }

        [Test]
        public void SelectOption_Listbox()
        {
            _app.OpenDefault();
            _app.DefaultPage.Listbox.SelectOption("item3");
            Assert.That(_app.DefaultPage.ListboxValue, Is.EqualTo("item3"));
        }

        [Test]
        public void SelectOption_Multiselect()
        {
            _app.OpenDefault();
            _app.DefaultPage.Multiselect.SelectOption("item3");
            _app.DefaultPage.Multiselect.SelectOption(1); // item2
            Assert.That(_app.DefaultPage.MultiselectValue, Is.EqualTo("item2,item3"));
        }
    }
}