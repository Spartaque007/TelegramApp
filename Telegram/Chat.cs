namespace Telegram

{
    public class Chat
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public override string ToString() => $"Chat ID: {Id} Chat Title: {Title} Chat Type {Type}";

    }
}
