using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RnsBaseWeb.Session;

namespace ChatSite
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool bLogin = CSessionMgr.IsLogined();

            if (!bLogin)
                Response.Redirect("User/SignIn.aspx");
        }
    }
}