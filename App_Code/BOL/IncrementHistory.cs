using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
    public class IncrementHistory
    {

        private String _Increment_his_ID;
        private String _Staff_ID;
        private String _Increment_amount;
        private DateTime _Active_date;
        private DateTime _Next_increment_date;
        private String _comments;
        private String _Increment_ctrl;
        private DateTime _Creted_date;
        private String _Created_by;

        public String Increment_his_ID
        {
            set
            {
                _Increment_his_ID = value;
            }
            get
            {
                return _Increment_his_ID;
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
        public String Increment_amount
        {
            set
            {
                _Increment_amount = value;
            }
            get
            {
                return _Increment_amount;
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
        public DateTime Next_increment_date
        {
            set
            {
                _Next_increment_date = value;
            }
            get
            {
                return _Next_increment_date;
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
        public String Increment_ctrl
        {
            set
            {
                _Increment_ctrl = value;
            }
            get
            {
                return _Increment_ctrl;
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

