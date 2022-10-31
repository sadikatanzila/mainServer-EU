using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EUPortalWeb.UserControl;
using EUPortal.BAL;
using ARAS.Karnel.Utilities;

namespace EUPortalWeb
{
    public partial class EUPortalMasterPage : System.Web.UI.MasterPage
    {
        protected int _intRow = 0;

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USERNAME"] != null)
                {
                    this.lblUser.Text = Session["USERNAME"].ToString();
                }
                else
                {
                    Page.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["strLoginPage"].ToString());
                }
            }
        }

        #endregion

        
    }
}
