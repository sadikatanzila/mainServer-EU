using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
    public class LeaveType
    {
        private String _Type_Id;
        private String _Type_name;
        private String _Total_leav;
        private DateTime _Creted_date;
        private String _Created_by;

        public String Type_Id
        {
            set
            {
                _Type_Id = value;
            }
            get
            {
                return _Type_Id;
            }
        }

        public String Type_name
        {
            set
            {
                _Type_name = value;
            }
            get
            {
                return _Type_name;
            }
        }
        public String Total_leav
        {
            set
            {
                _Total_leav = value;
            }
            get
            {
                return _Total_leav;
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

