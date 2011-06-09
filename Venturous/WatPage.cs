using WatiN.Core;

namespace Venturous
{
    /// <summary>
    /// Represents a web page.
    /// </summary>
    public abstract class WatPage : WatControl
    {
        internal void InitializePage(Document document)
        {
            var element = new WatElement();
            element.InitializeElement(document);
            InitializeControl(element);
        }
    }
}
