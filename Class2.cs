using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auto_salon
{
    internal class Class2
    {
        public static MySqlConnection GetSqlConnection()
        {
            string host = "188.93.210.124";
            int port = 3306;
            string database = "smirnovBD";
            string user = "test123";
            string password = "test1234";

            return Class1.GetSqlConnection(host, port, database, user, password);
        }
    }
}
