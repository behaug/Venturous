namespace Venturous.Tests.Controls
{
    class TextAndValuePage : WatPage
    {
        public WatElement Span1 { get { return Element.FindId("span1"); } }
        public WatElement Text1 { get { return Element.FindId("text1"); } }
        public WatElement Text2 { get { return Element.FindId("text2"); } }
        public WatElement Check1 { get { return Element.FindId("check1"); } }
        public WatElement Radio1 { get { return Element.FindId("radio1"); } }
        public WatElement Button1 { get { return Element.FindId("button1"); } }
        public WatElement Textarea1 { get { return Element.FindId("textarea1"); } }
        public WatElement TextDisabled { get { return Element.FindId("text_disabled"); } }
        public WatElement ButtonDisabled { get { return Element.FindId("button_disabled"); } }
    }
}