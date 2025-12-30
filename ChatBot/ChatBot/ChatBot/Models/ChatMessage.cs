namespace ChatBot.Models
{
    /// <summary>
    /// Chat ekranında gösterilecek mesaj modeli
    /// </summary>
    public class ChatMessage
    {
        /// <summary>
        /// Mesaj metni
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Mesaj sahibi (User veya Bot)
        /// </summary>
        public MessageSender Sender { get; set; }

        /// <summary>
        /// Mesaj zamanı
        /// </summary>
        public System.DateTime Timestamp { get; set; }

        public ChatMessage()
        {
            Timestamp = System.DateTime.Now;
        }
    }

    /// <summary>
    /// Mesaj gönderen tipi
    /// </summary>
    public enum MessageSender
    {
        User,
        Bot
    }
}


