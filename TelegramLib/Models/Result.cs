namespace Telegram
{
    public class Result
    {
        public int Update_id { get; set; }
        public Message message = new Message();
        public override string ToString() => $"Update ID: {Update_id} \nMessage text:\n{message}";
    }
}
