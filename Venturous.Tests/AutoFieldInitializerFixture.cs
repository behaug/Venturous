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
        #region Controls

        public class TopMenuControl : WatControl
        {
            public WatElement MenuItem1 = Auto();
            
            public WatElement GetControlElement()
            {
                return Element;
            }
        }

        public class TestControlBase : WatControl
        {
            private WatElement _btnClose = Auto();
            
            public WatElement BtnClose
            {
                get { return _btnClose; }
            }
        }

        public class TestControl : TestControlBase
        {
            private WatElement _btnSave = Auto();
            public WatElement TxtName = Auto();
            public WatElement CustomId = Auto("custom_id");
            public WatElement CustomFind = Auto(By.Class("someclass"));
            public TopMenuControl TopMenu = Auto<TopMenuControl>();
            public TopMenuControl Footer = Auto<TopMenuControl>("footer_div");
            public TopMenuControl Sidebar = Auto<TopMenuControl>(By.Class("sidebar_class"));

            public WatElement BtnSave
            {
                get { return _btnSave; }
            }
        }

        #endregion

        private AutoFieldInitializer _initializer;

        [SetUp]
        public void SetUp()
        {
            _initializer = new AutoFieldInitializer();
        }

        [Test]
        public void InitializeAutoFields_PrivateAutoElement_IsInitialized()
        {
            var control = new TestControl();
            _initializer.InitializeAutoFields(control);
            Assert.That(control.BtnSave.GetType().Name, Is.EqualTo("WatElement"));
        }

        [Test]
        public void InitializeAutoFields_InheritedAutoElement_IsInitialized()
        {
            var control = new TestControl();
            _initializer.InitializeAutoFields(control);
            Assert.That(control.BtnClose.GetType().Name, Is.EqualTo("WatElement"));
        }

        [Test]
        public void InitializeAutoFields_PublicAutoElement_IsInitialized()
        {
            var control = new TestControl();
            _initializer.InitializeAutoFields(control);
            Assert.That(control.TxtName.GetType().Name, Is.EqualTo("WatElement"));
        }

        [Test]
        public void InitializeAutoFields_AutoElementWithCustomId_GivenIdIsUsed()
        {
            var control = new TestControl();
            _initializer.InitializeAutoFields(control);
            Assert.That(control.CustomId.ToString(), Is.StringContaining("custom_id"));
        }

        [Test]
        public void InitializeAutoFields_AutoElementWithCustomFind_GivenFindIsUsed()
        {
            var control = new TestControl();
            _initializer.InitializeAutoFields(control);
            Assert.That(control.CustomFind.ToString(), Is.StringContaining("someclass"));
        }

        [Test]
        public void InitializeAutoFields_AutoControl_IsInitialized()
        {
            var control = new TestControl();
            _initializer.InitializeAutoFields(control);
            Assert.That(control.TopMenu.GetControlElement().GetType().Name, Is.EqualTo("WatElement"));
        }

        [Test]
        public void InitializeAutoFields_AutoControlWithAutoFields_NestedAutoFieldsAreInitialized()
        {
            var control = new TestControl();
            _initializer.InitializeAutoFields(control);
            Assert.That(control.TopMenu.MenuItem1.GetType().Name, Is.EqualTo("WatElement"));
        }

        [Test]
        public void InitializeAutoFields_AutoControlWithCustomId_GivenIdIsUsed()
        {
            var control = new TestControl();
            _initializer.InitializeAutoFields(control);
            Assert.That(control.Footer.GetControlElement().ToString(), Is.StringContaining("footer_div"));
        }

        [Test]
        public void InitializeAutoFields_AutoControlWithCustomFind_GivenFindIsUsed()
        {
            var control = new TestControl();
            _initializer.InitializeAutoFields(control);
            Assert.That(control.Sidebar.GetControlElement().ToString(), Is.StringContaining("sidebar_class"));
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
