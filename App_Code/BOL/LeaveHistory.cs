using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
    public class LeaveHistory
    {
        private String _LEAVE_HIS_ID;
        private String _Staff_id;
        private String _Leave_type_id;
        private String _Leave_type_name;
        private DateTime _From_date;
        private DateTime _To_date;        
        private DateTime _application_date;
        private DateTime _Approv_date;
        private String _Reasons;
        private DateTime _Creted_date;
        private String _Created_by;

        public String Leave_type_name
        {
            set
            {
                _Leave_type_name = value;
            }
            get
            {
                return _Leave_type_name;
            }
        }


        public String LEAVE_HIS_ID
        {
            set
            {
                _LEAVE_HIS_ID = value;
            }
            get
            {
                return _LEAVE_HIS_ID;
            }
        }

        public String Staff_id
        {
            set
            {
                _Staff_id = value;
            }
            get
            {
                return _Staff_id;
            }
        }
        public String Leave_type
        {
            set
            {
                _Leave_type_id = value;
            }
            get
            {
                return _Leave_type_id;
            }
        }
        public DateTime From_date
        {
            set
            {
                _From_date = value;
            }
            get
            {
                return _From_date;
            }
        }
        public DateTime To_date
        {
            set
            {
                _To_date = value;
            }
            get
            {
                return _To_date;
            }
        }
      
        public DateTime application_date
        {
            set
            {
                _application_date = value;
            }
            get
            {
                return _application_date;
            }
        }
        public DateTime Approv_date
        {
            set
            {
                _Approv_date = value;
            }
            get
            {
                return _Approv_date;
            }
        }
        public String Reasons
        {
            set
            {
                _Reasons = value;
            }
            get
            {
                return _Reasons;
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

