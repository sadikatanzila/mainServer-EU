using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for UserAction
/// </summary>
public class UserAction
{
	public UserAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public User get(string userName,string password)
    {
        DatabaseHelper objhelper = new DatabaseHelper();
        string query = "select NAME, PASS, TYPE, USERNAME  from USERLOGON where NAME  = ? and PASS  = ?";
        objhelper.AddParameter("@NAME ", userName);
        objhelper.AddParameter("@PASS ", password);
        DataSet ds = new DataSet();
        ds.Merge(objhelper.ExecuteDataSet(query));
        User user = new User();

        if (ds.Tables[0].Rows.Count > 0)
        {
            user.NAME = ds.Tables[0].Rows[0]["NAME"].ToString();
            user.PASS = ds.Tables[0].Rows[0]["PASS"].ToString();
            user.TYPE = ds.Tables[0].Rows[0]["TYPE"].ToString();
            user.USERNAME = ds.Tables[0].Rows[0]["USERNAME"].ToString();          
            
        }
        return user;
    }
}
