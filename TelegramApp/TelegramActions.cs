using TelegramApp.Commands;
using TelegramApp.Dependency;

namespace TelegramApp
{
    public class TelegramActions
    {
        public ICommand GetCommandFromMessage(string message)
        {
            if (message != null)
            {
                if (message.ToUpper().Contains("@JONNWICKBOT"))
                {
                    switch ($@"{message.ToUpper()}")
                    {
                        case @"/SHOWEVENTS@JONNWICKBOT":
                            return new ShowAllEvents();

                        case @"/SHOWNEWEVENTS@JONNWICKBOT":
                            return new ShowNewEvents();
                    }
                }
                return new DefaultCommand();
            }
            return new EmptyCommand();
        }
    }
}
