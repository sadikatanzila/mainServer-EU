using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
    public class Department
    {
        private String _Id;
        private String _Name;
        private String _Description;
        private DateTime _Creted_date;
        private String _Created_by;

        public String Id
        {
            set
            {
                _Id = value;
            }
            get
            {
                return _Id;
            }
        }

        public String Name
        {
            set
            {
                _Name = value;
            }
            get
            {
                return _Name;
            }
        }
        public String Description
        {
            set
            {
                _Description = value;
            }
            get
            {
                return _Description;
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


