namespace Venturous.Tests.Controls
{
    class TextAndValuePage : WatPage
    {
        public WatElement Span1 { get { return Element.Find("span1"); } }
        public WatElement Text1 { get { return Element.Find("text1"); } }
        public WatElement Text2 { get { return Element.Find("text2"); } }
        public WatElement Check1 { get { return Element.Find("check1"); } }
        public WatElement Radio1 { get { return Element.Find("radio1"); } }
        public WatElement Button1 { get { return Element.Find("button1"); } }
        public WatElement Textarea1 { get { return Element.Find("textarea1"); } }
        public WatElement TextDisabled { get { return Element.Find("text_disabled"); } }
        public WatElement ButtonDisabled { get { return Element.Find("button_disabled"); } }
    }
}