namespace Venturous.Infrastructure
{
    internal class AutoWatElement : WatElement
    {
        internal AutoWatElement()
            : base(By.Body, null, false)
        {
            Finder = By.Auto;
        }

        internal AutoWatElement(By finder)
            : base(By.Body, null, false)
        {
            Finder = finder;
        }

        public By Finder { get; private set; }
    }
}