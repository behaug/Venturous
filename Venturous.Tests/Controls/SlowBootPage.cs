namespace Venturous.Tests.Controls
{
    class SlowBootPage : WatPage
    {
        public string Greeting
        {
            get { return Element.FindId("greeting").Text; }
        }
    }
}