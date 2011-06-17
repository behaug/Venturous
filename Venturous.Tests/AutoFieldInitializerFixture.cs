using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Venturous.Infrastructure;

namespace Venturous.Tests
{
    [TestFixture]
    public class AutoFieldInitializerFixture
    {
        public class TopMenuControl : WatControl
        {
            public WatElement MenuItem1 = Auto();
        }

        public class TestControl : WatControl
        {
            private WatElement _btnSave = Auto();
            public WatElement TxtName = Auto();
            public WatElement CustomId = Auto("custom_id");
            public WatElement CustomFind = Auto(By.Class("someclass"));
            public TopMenuControl TopMenu = Auto<TopMenuControl>();

            public void ClickSave()
            {
                _btnSave.Click();
            }

            public WatElement BtnSave
            {
                get { return _btnSave; }
            }
        }

        private AutoFieldInitializer _initializer;

        [SetUp]
        public void SetUp()
        {
            _initializer = new AutoFieldInitializer();
        }

        [Test]
        public void InitializeAutoFields_PrivateElement_IsInitialized()
        {
            var control = new TestControl();
            _initializer.InitializeAutoFields(control);
            Assert.That(control.BtnSave, Is.Not.Null);
        }

        [Test]
        public void InitializeAutoFields_PublicElement_IsInitialized()
        {
            var control = new TestControl();
            _initializer.InitializeAutoFields(control);
            Assert.That(control.TxtName, Is.Not.Null);
        }

        [Test]
        public void InitializeAutoFields_PublicControl_IsInitialized()
        {
            var control = new TestControl();
            _initializer.InitializeAutoFields(control);
            Assert.That(control.TopMenu, Is.Not.Null);
        }

        [Test]
        public void InitializeAutoFields_ControlWithAutoFields_NestedAutoFieldsAreInitialized()
        {
            var control = new TestControl();
            _initializer.InitializeAutoFields(control);
            Assert.That(control.TopMenu.MenuItem1, Is.Not.Null);
        }

        [Test]
        public void InitializeAutoFields_ElementWithCustomId_GivenIdIsUsed()
        {
            var control = new TestControl();
            _initializer.InitializeAutoFields(control);
            Assert.That(control.CustomId.ToString(), Is.StringContaining("custom_id"));
        }

        [Test]
        public void InitializeAutoFields_ElementWithCustomFind_GivenFindIsUsed()
        {
            var control = new TestControl();
            _initializer.InitializeAutoFields(control);
            Assert.That(control.CustomFind.ToString(), Is.StringContaining("someclass"));
        }

        [Test]
        public void GetElementId_NoUnderscore_ReturnsSame()
        {
            Assert.That(_initializer.GetElementId("txtInput"), Is.EqualTo("txtInput"));
        }

        [Test]
        public void GetElementId_WithUnderscore_RemovesUnderscore()
        {
            Assert.That(_initializer.GetElementId("_txtInput"), Is.EqualTo("txtInput"));
        }
    }
}
