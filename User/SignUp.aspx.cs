using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RnsBaseWeb.DB;

namespace ChatSite.User
{
    public partial class SignUp : CBaseController
    {
        protected override void Run(object sender, EventArgs e)
        {
            string action = Request["action"];

            switch (action)
            {
                case "register":
                    DoRegister();
                    break;
                default:
                    break;
            }
        }

        private void DoRegister()
        {
            string account_name = Request["account_name"];
            string email = Request["email"];
            string pwd = Request["pwd"];

            CDBUser usr = dbAdapter.FindUserByAlias(account_name);

            if (usr != null)
                throw new Exception("Your alias is already used.");

            usr = dbAdapter.FindUserByEmail(email);

            if (usr != null)
                throw new Exception("Your email is already used.");
            
            dbAdapter.AddUser(account_name, email, pwd);

            ErrorMsg = ChatSite.Constants.S_OK;
        }
    }
}