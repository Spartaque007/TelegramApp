namespace TelegramApp
{
    public class Update
    {
        public string ChatId { get; set; }
        public string LastUpdateID { get; set; }

        public Update(string chatId, string updateId)
        {
            ChatId = chatId;
            LastUpdateID = updateId;
        }

        public override string ToString()
        {
            return $"Chat ID: {ChatId} LastUpdateID: {LastUpdateID}";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.ToString().Equals(obj.ToString());
        }
    }
}
