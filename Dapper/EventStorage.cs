using DevBy;
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
    public class EventStorage
    {
        public void SaveEvents()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["TelegramApp"].ConnectionString))
            {
                string sqlQuery = "Insert into EventsDB values('11','22' )";
                db.Execute(sqlQuery);
                
            }

        }

    }
}
