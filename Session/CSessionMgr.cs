using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RnsBaseWeb.Session
{

    public class CSessionMgr
    {
        public static readonly string _sessUid = "sess_uid";
        public static readonly string _sessAccount = "sess_account";

        static HttpSessionState Session
        {
            get
            {
                if (HttpContext.Current == null)
                    throw new ApplicationException("No Http Context, No Session to Get!");

                return HttpContext.Current.Session;
            }
        }

        public static T Get<T>(string key)
        {
            if (Session[key] == null)
                return default(T);
            else
                return (T)Session[key];
        }

        public static void Set<T>(string key, T value)
        {
            Session[key] = value;
        }

        public static Int64 GetUid()
        {
            return Get<Int64>(_sessUid);
        }
        public static string GetAccountName()
        {
            return Get<string>(_sessAccount);
        }

        public static bool IsLogined()
        {
            if (string.IsNullOrEmpty(GetAccountName()))
                return false;

            return true;
        }

        public static void Login(Int64 uid, string account)
        {
            Set<Int64>(_sessUid, uid);
            Set<string>(_sessAccount, account);
        }

        public static void Logout()
        {
            Session.Remove(CSessionMgr._sessUid);
            Session.Remove(CSessionMgr._sessAccount);
        }
    }
}