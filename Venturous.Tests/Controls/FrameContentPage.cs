namespace Venturous.Tests.Controls
{
    class FrameContentPage : WatPage
    {
        public MessageDiv MessageDiv1 = Auto<MessageDiv>("wrapper1");
        public MessageDiv MessageDiv2 = Auto<MessageDiv>("wrapper2");
    }

    class MessageDiv : WatControl
    {
        private WatElement _message = Auto();

        public string MessageText
        {
            get { return _message.Text; }
        }
    }
}