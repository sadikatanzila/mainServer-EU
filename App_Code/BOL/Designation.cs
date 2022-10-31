using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
    public class Designation
    {
        private String _DESIG_ID;
        private String _DESIG_NAME;
        private String _DEP_TYPE;
        private int _DESIG_CTRL;
        private DateTime _Creted_date;
        private String _Created_by;

        public String DESIG_ID
        {
            set
            {
                _DESIG_ID = value;
            }
            get
            {
                return _DESIG_ID;
            }
        }
        public String DESIG_NAME
        {
            set
            {
                _DESIG_NAME = value;
            }
            get
            {
                return _DESIG_NAME;
            }
        }
        public String DEP_TYPE
        {
            set
            {
                _DEP_TYPE = value;
            }
            get
            {
                return _DEP_TYPE;
            }
        }
        public int DESIG_CTRL
        {
            set
            {
                _DESIG_CTRL = value;
            }
            get
            {
                return _DESIG_CTRL;
            }
        }

        public bool IsActive
        {
            
            get
            {
                if (_DESIG_CTRL == 1)
                    return true;
                else return false;
            }
        }
        public DateTime Creted_date
        {
            set
            {
                _Creted_date = value;
            }
            get
            {
                return _Creted_date;
            }
        }
        public String Created_by
        {
            set
            {
                _Created_by = value;
            }
            get
            {
                return _Created_by;
            }
        }
        
        
    }

