using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper
{
    class Program
    {
        static void Main(string[] args)
        {
            EventStorage a = new EventStorage();
            a.SaveEvents();

        }
    }
}
