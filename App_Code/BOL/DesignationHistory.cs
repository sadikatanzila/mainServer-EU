using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
    public class DesignationHistory
    {
        private String _Design_his_ID;
        private String _Staff_ID;
        private String _DESIG_ID;
        private String _DESIG_name;
        private DateTime _Active_date;
        private DateTime _Next_desig_date;
        private String _comments;
        private String _Desig_ctrl;
        private DateTime _Creted_date;
        private String _Created_by;
        public String DESIG_name
        {
            set
            {
                _DESIG_name = value;
            }
            get
            {
                return _DESIG_name;
            }
        }
        public String Design_his_ID
        {
            set
            {
                _Design_his_ID = value;
            }
            get
            {
                return _Design_his_ID;
            }
        }
        public String Staff_ID
        {
            set
            {
                _Staff_ID = value;
            }
            get
            {
                return _Staff_ID;
            }
        }
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
        public DateTime Active_date
        {
            set
            {
                _Active_date = value;
            }
            get
            {
                return _Active_date;
            }
        }
        public DateTime Next_desig_date
        {
            set
            {
                _Next_desig_date = value;
            }
            get
            {
                return _Next_desig_date;
            }
        }
        public String comments
        {
            set
            {
                _comments = value;
            }
            get
            {
                return _comments;
            }
        }
        public String Desig_ctrl
        {
            set
            {
                _Desig_ctrl = value;
            }
            get
            {
                return _Desig_ctrl;
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
