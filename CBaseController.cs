using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RnsBaseWeb.DB;

namespace ChatSite
{
    public abstract class CBaseController : System.Web.UI.Page
    {
        protected CMySQLAdapter dbAdapter = DBMgr.GetConnection();
        private string _error = null;

        public string ErrorMsg
        {
            get
            {
                return this._error;
            }

            set
            {
                this._error = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Run(sender, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                this.ErrorMsg = ex.Message;
            }
        }

        protected abstract void Run(object sender, EventArgs e);
    }
}