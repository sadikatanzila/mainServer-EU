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
/// Summary description for User
/// </summary>
public class User
{
	public User()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private String _NAME;
        private String _PASS;
    private String _TYPE;
    private String _USERNAME;
    

    public String NAME
        {
            set
            {
                _NAME = value;
            }
            get
            {
                return _NAME;
            }
        }
    public String PASS
    {
        set
        {
            _PASS = value;
        }
        get
        {
            return _PASS;
        }
    }
    public String TYPE
    {
        set
        {
            _TYPE = value;
        }
        get
        {
            return _TYPE;
        }
    }
    public String USERNAME
    {
        set
        {
            _USERNAME = value;
        }
        get
        {
            return _USERNAME;
        }
    }
    
}
