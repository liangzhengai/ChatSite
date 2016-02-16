using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RnsBaseWeb.DB;
using RnsBaseWeb.Session;
using Rns.Utility;

namespace ChatSite.User
{
    public partial class SignIn : CBaseController
    {
        protected override void Run(object sender, EventArgs e)
        {
            string action = Request["action"];
            string account_name = Request["account_name"];
            string pwd = Request["pwd"];

            if( action == "signin" )
            {
                DoSignin(account_name, pwd);
                Response.Redirect(ResolveUrl("~/Default.aspx"));
            }
            else if (action == "signout")
            {
                CSessionMgr.Logout();
            }
                
        }

        private void DoSignin(string account, string pwd)
        {
            if (String.IsNullOrEmpty(account))
                throw new Exception("Input the account name or email");

            if (String.IsNullOrEmpty(pwd))
                throw new Exception("Input the password.");

            CDBUser usr = null;
            
            if( RegexUtility.IsValidEmail(account) )
                usr = dbAdapter.FindUserByEmail(account);
            else
                usr = dbAdapter.FindUserByAlias(account);

            if (usr == null)
                throw new Exception("Not registered account.");

            if (pwd != usr.pwd)
                throw new Exception("Password invalid");

            CSessionMgr.Login(usr.id, usr.alias);
        }
    }
}