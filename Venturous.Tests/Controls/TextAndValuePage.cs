namespace Venturous.Tests.Controls
{
    class TextAndValuePage : WatPage
    {
        public WatElement Span1 = Auto();
        public WatElement Text1 = Auto();
        public WatElement Text2 = Auto();
        public WatElement Check1 = Auto();
        public WatElement Radio1 = Auto();
        public WatElement Button1 = Auto();
        public WatElement Textarea1 = Auto();
        public WatElement TextDisabled = Auto("text_disabled");
        public WatElement ButtonDisabled = Auto("button_disabled");
    }
}