using System.Collections.Specialized;
using System.Drawing;
using WatiN.Core.DialogHandlers;
using WatiN.Core.Native;

namespace Venturous.Infrastructure
{
    /// <summary>
    /// Abstract baseclass for creating decorators on native elements.
    /// </summary>
    abstract class BaseNativeElement : INativeElement
    {
        private INativeElement _inner;

        protected BaseNativeElement(INativeElement inner)
        {
            _inner = inner;
        }

        public virtual string GetAttributeValue(string attributeName)
        {
            return _inner.GetAttributeValue(attributeName);
        }

        public virtual void SetAttributeValue(string attributeName, string value)
        {
            _inner.SetAttributeValue(attributeName, value);
        }

        public virtual string GetStyleAttributeValue(string attributeName)
        {
            return _inner.GetStyleAttributeValue(attributeName);
        }

        public virtual void SetStyleAttributeValue(string attributeName, string value)
        {
            _inner.SetStyleAttributeValue(attributeName, value);
        }

        public virtual void SetFocus()
        {
            _inner.SetFocus();
        }

        public virtual void FireEvent(string eventName, NameValueCollection eventProperties)
        {
            _inner.FireEvent(eventName, eventProperties);
        }

        public virtual void FireEventNoWait(string eventName, NameValueCollection eventProperties)
        {
            _inner.FireEventNoWait(eventName, eventProperties);
        }

        public virtual bool IsElementReferenceStillValid()
        {
            return _inner.IsElementReferenceStillValid();
        }

        public virtual void Select()
        {
            _inner.Select();
        }

        public virtual void SubmitForm()
        {
            _inner.SubmitForm();
        }

        public virtual void SetFileUploadFile(DialogWatcher dialogWatcher, string fileName)
        {
            _inner.SetFileUploadFile(dialogWatcher, fileName);
        }

        public virtual void WaitUntilReady()
        {
            _inner.WaitUntilReady();
        }

        Rectangle INativeElement.GetElementBounds()
        {
            return GetElementBounds();
        }

        public virtual Rectangle GetElementBounds()
        {
            return _inner.GetElementBounds();
        }

        public virtual string GetJavaScriptElementReference()
        {
            return _inner.GetJavaScriptElementReference();
        }

        public virtual void Pin()
        {
            _inner.Pin();
        }

        public virtual INativeElementCollection Children
        {
            get { return _inner.Children; }
        }

        public virtual INativeElementCollection AllDescendants
        {
            get { return _inner.AllDescendants; }
        }

        public virtual INativeElementCollection TableRows
        {
            get { return _inner.TableRows; }
        }

        public virtual INativeElementCollection TableBodies
        {
            get { return _inner.TableBodies; }
        }

        public virtual INativeElementCollection TableCells
        {
            get { return _inner.TableCells; }
        }

        public virtual INativeElementCollection Options
        {
            get { return _inner.Options; }
        }

        public virtual string TextAfter
        {
            get { return _inner.TextAfter; }
        }

        public virtual string TextBefore
        {
            get { return _inner.TextBefore; }
        }

        public virtual INativeElement NextSibling
        {
            get { return _inner.NextSibling; }
        }

        public virtual INativeElement PreviousSibling
        {
            get { return _inner.PreviousSibling; }
        }

        public virtual INativeElement Parent
        {
            get { return _inner.Parent; }
        }

        public virtual string TagName
        {
            get { return _inner.TagName; }
        }
    }
}