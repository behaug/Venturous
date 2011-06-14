using System;
using Venturous.Infrastructure;
using WatiN.Core;
using WatiN.Core.Native;

namespace Venturous
{
    /// <summary>
    /// Represents an HTML element in a web page.
    /// </summary>
    public class WatElement
    {
        private Document _document;
        private Div _root;
        private INativeElement _nativeElement;
        private Finder _finder;
        private WatElement _parent;

        private void InitializeElement(Document document, Finder finder, WatElement parent)
        {
            _document = document;
            _finder = finder;
            _parent = parent;
            RefreshElement();
        }

        internal void InitializeElement(Document document)
        {
            _finder = Finder.Body;
            _document = document;
            _nativeElement = new CustomNativeElement(document.Body.NativeElement, _document);
            _root = ElementFactory.CreateElement<Div>(_document.DomContainer, _nativeElement);
        }

        private string FullFindText()
        {
            if (_parent == null)
                return _finder.Description;

            return _parent.FullFindText() + " " + _finder.Description;
        }

        public override string ToString()
        {
            return FullFindText();
        }

        protected void RefreshElement()
        {
            if (_parent == null)
            {
                if (_nativeElement != null && _nativeElement.IsElementReferenceStillValid())
                    return;

                InitializeElement(_document);
                return;
            }

            _parent.RefreshElement();

            if (_nativeElement != null && _nativeElement.IsElementReferenceStillValid())
                return;

            var element = _parent._root.Element(_finder.Constraint);
            if (!element.Exists)
                throw new Exception("Element not found: \"" + FullFindText() + "\"");

            _nativeElement = new CustomNativeElement(element.NativeElement, _document);
            _root = ElementFactory.CreateElement<Div>(_document.DomContainer, _nativeElement);
        }

        /// <summary>Finds a control by id, where the id ends with the given value.</summary>
        public TControl FindId<TControl>(string id) where TControl : WatControl, new()
        {
            return CreateControl<TControl>(FindId(id));
        }

        /// <summary>Finds an element by id, where the id ends with the given value.</summary>
        public WatElement FindId(string id)
        {
            return CreateElement(Finder.ById(id));
        }

        /// <summary>Returns whether an element can be found where the id ends with the given value.</summary>
        public bool CanFindId(string id)
        {
            return CanFind(Finder.ById(id));
        }

        /// <summary>Finds a control by CSS class name.</summary>
        public TControl FindClass<TControl>(string cssClass) where TControl : WatControl, new()
        {
            return CreateControl<TControl>(FindClass(cssClass));
        }

        /// <summary>Finds an element by CSS class name.</summary>
        public WatElement FindClass(string cssClass)
        {
            return CreateElement(Finder.ByClass(cssClass));
        }

        /// <summary>Returns whether an element can be found by CSS class name.</summary>
        public bool CanFindClass(string cssClass)
        {
            return CanFind(Finder.ByClass(cssClass));
        }

        /// <summary>Finds a control by attribute value.</summary>
        public TControl FindAttribute<TControl>(string attributeName, string value) where TControl : WatControl, new()
        {
            return CreateControl<TControl>(FindAttribute(attributeName, value));
        }

        /// <summary>Finds an element by attribute value.</summary>
        public WatElement FindAttribute(string attributeName, string value)
        {
            return CreateElement(Finder.ByAttribute(attributeName, value));
        }

        /// <summary>Returns whether an element can be found by attribute value.</summary>
        public bool CanFindAttribute(string attributeName, string value)
        {
            return CanFind(Finder.ByAttribute(attributeName, value));
        }

        /// <summary>Finds a control by tag name, taking the first one it finds.</summary>
        public TControl FindTag<TControl>(string tagName) where TControl : WatControl, new()
        {
            return CreateControl<TControl>(FindTag(tagName));
        }

        /// <summary>Finds an element by tag name, taking the first one it finds.</summary>
        public WatElement FindTag(string tagName)
        {
            return CreateElement(Finder.ByTagName(tagName));
        }

        /// <summary>Returns whether an element can be found by tag name.</summary>
        public bool CanFindTag(string tagName)
        {
            return CanFind(Finder.ByTagName(tagName));
        }

        /// <summary>Finds a control by tag name, taking the one at the given index.</summary>
        public TControl FindTag<TControl>(string tagName, int index) where TControl : WatControl, new()
        {
            return CreateControl<TControl>(FindTag(tagName, index));
        }

        /// <summary>Finds an element by tag name, taking the one at the given index.</summary>
        public WatElement FindTag(string tagName, int index)
        {
            return CreateElement(Finder.ByTagName(tagName, index));
        }

        /// <summary>Returns whether an element can be found by tag name with the given index.</summary>
        public bool CanFindTag(string tagName, int index)
        {
            return CanFind(Finder.ByTagName(tagName, index));
        }

        private TControl CreateControl<TControl>(WatElement element) where TControl : WatControl, new()
        {
            var control = new TControl();
            control.InitializeControl(element);
            return control;
        }

        private WatElement CreateElement(Finder finder)
        {
            var element = new WatElement();
            element.InitializeElement(_document, finder, this);
            return element;
        }

        private bool CanFind(Finder finder)
        {
            RefreshElement();
            return _root.Element(finder.Constraint).Exists;
        }

        private TElement RootAs<TElement>() where TElement : Element
        {
            RefreshElement();
            return ElementFactory.CreateElement<TElement>(_document.DomContainer, _nativeElement);
        }

        private Element Root
        {
            get
            {
                RefreshElement();
                return ElementFactory.CreateElement<Element>(_document.DomContainer, _nativeElement);
            }
        }

        /// <summary>Simulates a mouse click on the element</summary>
        public void Click()
        {
            RootAs<Link>().Click();
        }

        /// <summary>Gets or sets the value of the input field, or gets the inner text of the HTML element</summary>
        public string Text
        {
            get 
            {
                var tagName = Root.TagName.ToLower();
                if (tagName == "input" || tagName == "textarea")
                {
                    var type = Root.GetAttributeValue("type").ToLower();
                    if (type == "checkbox" || type == "radio")
                        return ""; // Checkboxes don't have a visible text

                    return RootAs<TextField>().Value;
                }

                return Root.Text;
            }
            set 
            {
                var tagName = Root.TagName.ToLower();
                if (tagName == "input" || tagName == "textarea")
                {
                    var type = Root.GetAttributeValue("type").ToLower();
                    if (type == "checkbox" || type == "radio")
                        throw new Exception("Can not set text on checkbox: " + FullFindText());

                    bool isDisabled = Root.GetAttributeValue("disabled").ToLower() == "disabled";
                    if (isDisabled)
                        throw new Exception("The input element is disabled");

                    RootAs<TextField>().Value = value;
                    return;
                }

                throw new Exception("Can not set text on non-input field: " + FullFindText());
            }
        }
    }
}