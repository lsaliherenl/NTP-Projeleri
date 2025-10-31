namespace ChatClientWPF
{
    public sealed class MessageItem
    {
        public string Text { get; }
        public bool IsSelf { get; }

        public MessageItem(string text, bool isSelf)
        {
            Text = text;
            IsSelf = isSelf;
        }
    }
}

