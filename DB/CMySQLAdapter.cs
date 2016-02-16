using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.Common;
using MySql.Data.MySqlClient;

namespace RnsBaseWeb.DB
{
    public class CMySQLAdapter : IDisposable
    {
        public MySqlConnection conn;

        public CMySQLAdapter( string connStr )
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Dispose()
        {
            conn.Close();
            conn = null;
        }
        /// <summary>
        /// get user of id value
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user info</returns>
        public CDBUser GetUser(Int64 id)
        {
            string query = 
                "SELECT `id`, `alias`, `email`, `pwd` FROM tbl_user WHERE `id` = @id";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            
            CDBUser user = new CDBUser();

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    user.id = id;
                    user.alias = reader.GetString("alias");
                    user.email = reader.GetString("email");
                    user.pwd = reader.GetString("pwd");
                }
            }

            return user;
        }

        /// <summary>
        /// find a user by email
        /// </summary>
        /// <param name="email">email address</param>
        /// <returns>user info</returns>
        public CDBUser FindUserByEmail(string email)
        {
            string query =
                "SELECT `id`, `alias`, `email`, `pwd` FROM tbl_user WHERE `email` = @email";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@email", email);

            CDBUser user = null;

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    user = new CDBUser();
                    user.id = reader.GetInt64("id");
                    user.alias = reader.GetString("alias");
                    user.email = reader.GetString("email");
                    user.pwd = reader.GetString("pwd");
                }
            }

            return user;
        }

        /// <summary>
        /// find a user by alias
        /// </summary>
        /// <param name="alias">user alias</param>
        /// <returns>user info</returns>
        public CDBUser FindUserByAlias(string alias)
        {
            string query =
                "SELECT `id`, `alias`, `email`, `pwd` FROM tbl_user WHERE `alias` = @alias";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@alias", alias);

            CDBUser user = null;

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    user = new CDBUser();

                    user.id = reader.GetInt64("id");
                    user.alias = reader.GetString("alias");
                    user.email = reader.GetString("email");
                    user.pwd = reader.GetString("pwd");
                }
            }

            return user;
        }



        /// <summary>
        /// query user by alias
        /// </summary>
        /// <param name="phrase">string to be checked</param>
        /// <returns></returns>
        public List<CDBUser> QueryUser(string phrase, Int64 uid)
        {
            string query = "SELECT id, alias FROM tbl_user WHERE alias LIKE '%" + phrase + "%' AND id <> " + uid.ToString();

            MySqlCommand cmd = new MySqlCommand(query, conn);
            //cmd.Parameters.AddWithValue("@alias", phrase);

            List<CDBUser> result = new List<CDBUser>();

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    CDBUser user = new CDBUser();

                    user.id = reader.GetInt64(reader.GetOrdinal("id"));
                    user.alias = reader["alias"].ToString();

                    result.Add(user);
                }
            }

            return result;
        }
        /// <summary>
        /// add a new user
        /// </summary>
        /// <param name="alias">user alias</param>
        /// <param name="email">user email</param>
        /// <param name="pwd">user password</param>
        public void AddUser(string alias, string email, string pwd)
        {
            string query = "INSERT INTO tbl_user(alias, email, pwd) VALUES(@alias,@email,@pwd);";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@alias", alias);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@pwd", pwd);

            cmd.ExecuteNonQuery();
        }
    }
}