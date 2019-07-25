using System;
using TelegramApp;
using Xunit;
using TelegramApp.Commands;

namespace UnitTest.TelegramApp.Tests
{
    public class TelegramActionsTest
    {
        [Theory]
        [InlineData(null, typeof(EmptyCommand))]
        [InlineData("", typeof(DefaultCommand))]
        [InlineData(@"/SHOWEVENTS@JONNWICKBOT", typeof(ShowAllEvents))]
        [InlineData(@"/SHOWNEWEVENTS@JONNWICKBOT", typeof(ShowNewEvents))]
        public void GetCommandFromMessage_AllMessage(string message, Type type)
        {
            TelegramActions operation = new TelegramActions();
            var command = operation.GetCommandFromMessage(message);
            Assert.IsType(type, command);

        }
             
    }
}
