namespace Venturous.Tests.Controls
{
    class SlowBootPage : WatPage
    {
        private WatElement _greeting = Auto();

        public string Greeting
        {
            get { return _greeting.Text; }
        }
    }
}