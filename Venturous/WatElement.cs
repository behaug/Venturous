using System;
using Venturous.Infrastructure;
using WatiN.Core;
using WFind = WatiN.Core.Find;

namespace Venturous
{
    /// <summary>
    /// Represents an HTML element in a web page.
    /// </summary>
    public class WatElement
    {
        private readonly Document _document;
        private Div _root;
        private CustomNativeElement _nativeElement;
        private readonly By _finder;
        private readonly WatElement _parent;

        internal WatElement(Document document)
            : this(By.Body, null, false)
        {
            _document = document;
            RefreshElement();
        }

        internal WatElement(By finder, WatElement parent, bool checkExists)
        {
            _finder = finder;
            _parent = parent;

            if (parent != null)
                _document = parent._document;

            if (checkExists)
                RefreshElement();
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

                _nativeElement = new CustomNativeElement(_document.Body.NativeElement, _document);
                _root = ElementFactory.CreateElement<Div>(_document.DomContainer, _nativeElement);
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

        private TControl CreateControl<TControl>(WatElement element) where TControl : WatControl, new()
        {
            var control = new TControl();
            control.InitializeControl(element);
            return control;
        }

        private WatElement CreateElement(By finder)
        {
            return new WatElement(finder, this, true);
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

        /// <summary>Finds an element by id</summary>
        public WatElement Find(string id)
        {
            return Find(By.Id(id));
        }

        /// <summary>Finds the given element</summary>
        public WatElement Find(By finder)
        {
            return CreateElement(finder);
        }

        /// <summary>Finds a control by id</summary>
        public TControl Find<TControl>(string id) where TControl : WatControl, new()
        {
            return Find<TControl>(By.Id(id));
        }

        /// <summary>Finds the given control</summary>
        public TControl Find<TControl>(By finder) where TControl : WatControl, new()
        {
            return CreateControl<TControl>(Find(finder));
        }

        /// <summary>Returns whether an element with the given id exists</summary>
        public bool CanFind(string id)
        {
            return CanFind(By.Id(id));
        }

        /// <summary>Returns whether the given element exists</summary>
        public bool CanFind(By finder)
        {
            RefreshElement();
            return _root.Element(finder.Constraint).Exists;
        }

        /// <summary>Clicks on the element</summary>
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

        /// <summary>Selects the option with the given value in a select list</summary>
        public void SelectOption(string value)
        {
            RootAs<SelectList>().SelectByValue(value);
            _nativeElement.WaitForComplete();
        }

        /// <summary>Selects the option with the given index in a select list</summary>
        public void SelectOption(int index)
        {
            var selectList = RootAs<SelectList>();
            selectList.SelectByValue(selectList.Option(WFind.ByIndex(index)).Value);
            _nativeElement.WaitForComplete();
        }
    }
}