namespace Venturous.Tests.Controls
{
    class SlowBootPage : WatPage
    {
        public string Greeting
        {
            get { return Element.Find("greeting").Text; }
        }
    }
}