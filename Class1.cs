﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auto_salon
{
    internal class Class1
    {
        public static MySqlConnection GetSqlConnection(string host, int port, string database, string user, string password)
        {
            String connString = "Server=" + host + ";database=" + database + ";port=" + port.ToString() + ";user=" + user + ";password=" + password + ";";
            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
    }
}
