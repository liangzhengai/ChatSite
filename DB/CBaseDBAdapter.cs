using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RnsBaseWeb.DB
{
    public class CDBUser
    {
        public Int64 id;
        public string alias;
        public string email;
        public string pwd;
    }

    public class DBMgr
    {
        public static CMySQLAdapter GetConnection()
        {
            ConnectionStringSettings oConnectionSettings = ConfigurationManager.ConnectionStrings["ConnectionString2"];
            string connStr = oConnectionSettings.ConnectionString;
            string provider = oConnectionSettings.ProviderName;

            return new CMySQLAdapter(connStr);
        }
    }
}
