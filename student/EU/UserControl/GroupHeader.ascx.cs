using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace EUPortalWeb.UserControl
{
    public partial class GroupHeader : System.Web.UI.UserControl
    {
        string _headerText;

        public string HeaderText
        {

            get { return this._headerText; }
            set { this._headerText = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblGroupHeader.Text = this._headerText;
        }
    }
}