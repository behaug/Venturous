namespace Venturous
{
    /// <summary>
    /// Represents a user control in a web page
    /// </summary>
    public abstract class WatControl
    {
        protected WatElement Element;

        internal void InitializeControl(WatElement element)
        {
            Element = element;
        }
    }
}