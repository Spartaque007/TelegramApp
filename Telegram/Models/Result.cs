namespace Telegram
{
   // [JsonObject("result")]
    public class Result
    {
        public int update_id { get; set; }
        public Message message = new Message();
        public override string ToString() => $"Update ID: {update_id} \nMessage text:\n{message}";
    }
}
