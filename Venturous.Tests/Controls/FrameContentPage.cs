namespace Venturous.Tests.Controls
{
    class FrameContentPage : WatPage
    {
        public MessageDiv MessageDiv1
        {
            get { return Element.FindId<MessageDiv>("wrapper1"); }
        }

        public MessageDiv MessageDiv2
        {
            get { return Element.FindId<MessageDiv>("wrapper2"); }
        }
    }

    class MessageDiv : WatControl
    {
        public string MessageText
        {
            get { return Element.FindId("message").Text; }
        }
    }
}