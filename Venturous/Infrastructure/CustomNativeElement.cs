using System;
using System.Collections.Specialized;
using WatiN.Core;
using WatiN.Core.Native;
using WatiN.Core.UtilityClasses;

namespace Venturous.Infrastructure
{
    /// <summary>
    /// Custom subclass of NativeElement to add support for waiting for UpdatePanel to finish loading.
    /// </summary>
    class CustomNativeElement : BaseNativeElement
    {
        private readonly Document _doc;

        public CustomNativeElement(INativeElement inner, Document doc)
            : base(inner)
        {
            _doc = doc;
        }

        public override void FireEvent(string eventName, NameValueCollection eventProperties)
        {
            base.FireEvent(eventName, eventProperties);

            try
            {
                WaitForAsyncPostback();
                Wait(200); // Let it complete the UI updates after response comes back
            }
            catch
            {
                // Ignore any errors
            }
        }

        private void WaitForAsyncPostback()
        {
            bool inAsyncPostBack = IsInAsyncPostBack();
            if (!inAsyncPostBack)
                return;

            SetAttributeValue("waittoken", "waittoken");
            _doc.Element(Find.By("waittoken", "waittoken")).WaitUntilRemoved(Settings.WaitForCompleteTimeOut);
        }

        private void Wait(int milliseconds)
        {
            TryFuncUntilTimeOut.Try(TimeSpan.FromMilliseconds(milliseconds), () => _doc.Eval("false") == "wait");
        }

        private bool IsInAsyncPostBack()
        {
            const string isInAsyncPostBackScript =
                "(function() { " +
                "   var win = window;" +

                "   if (typeof(win.Sys) !== 'undefined' && win.Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack()) " +
                "       return true;" +

                "   for (var f = 0; f < window.frames.length; f++) { " +
                "       win = window.frames[f]; " +
                "       if (typeof(win.Sys) !== 'undefined' && win.Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack()) " +
                "           return true;" +
                "   }" +

                "   return false;" +
                "})();";

            var res = _doc.Eval(isInAsyncPostBackScript);
            return res.ToLower() == "true";
        }
    }
}