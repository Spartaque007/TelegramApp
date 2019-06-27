using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramApp.Dependency
{
    public interface ILoger
    {
       void WriteLog(string logInformation);
    }
}
