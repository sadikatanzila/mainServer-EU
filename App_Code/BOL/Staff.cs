using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
    public class Staff
    {
        private String _STAFF_ID;
        private String _STAFF_NAME;
        private String _P_ADDRESS;
        private String _PER_ADDRESS;
        private String _PHONE_NUMBER;
        private String _MOBILE;
        private String _Gender;
        private String _E_MAIL;
        private String _DEPARTMENT;
        private String _JOB_TYPE;
        private String _JOB_CATEGORY;
        private String _JOB_DESIGNATION;
        private DateTime _JOIN_DATE;
        private String _LOGIN_NAME;
        private String _PASSWORD;
        private String _STAFF_CTRL;
        private String _STAFF_PICTURE;
        private String _IS_ADVISOR;
        private String _REGINED_REASON;
        private DateTime _REGINED_DATE;
        private DateTime _CREATED_DATE;
        private String _CREATED_BY;

        public String STAFF_ID
        {
            set
            {
                _STAFF_ID = value;
            }
            get
            {
                return _STAFF_ID;
            }
        }
        public String STAFF_NAME
        {
            set
            {
                _STAFF_NAME = value;
            }
            get
            {
                return _STAFF_NAME;
            }
        }
        public String P_ADDRESS
        {
            set
            {
                _P_ADDRESS = value;
            }
            get
            {
                return _P_ADDRESS;
            }
        }
        public String PER_ADDRESS
        {
            set
            {
                _PER_ADDRESS = value;
            }
            get
            {
                return _PER_ADDRESS;
            }
        }
        public String PHONE_NUMBER
        {
            set
            {
                _PHONE_NUMBER = value;
            }
            get
            {
                return _PHONE_NUMBER;
            }
        }
        public String MOBILE
        {
            set
            {
                _MOBILE = value;
            }
            get
            {
                return _MOBILE;
            }
        }
        public String Gender
        {
            set
            {
                _Gender = value;
            }
            get
            {
                return _Gender;
            }
        }
        public String E_MAIL
        {
            set
            {
                _E_MAIL = value;
            }
            get
            {
                return _E_MAIL;
            }
        }
        public String DEPARTMENT
        {
            set
            {
                _DEPARTMENT = value;
            }
            get
            {
                return _DEPARTMENT;
            }
        }
        public String JOB_TYPE
        {
            set
            {
                _JOB_TYPE = value;
            }
            get
            {
                return _JOB_TYPE;
            }
        }
        public String JOB_CATEGORY
        {
            set
            {
                _JOB_CATEGORY = value;
            }
            get
            {
                return _JOB_CATEGORY;
            }
        }
        public String JOB_DESIGNATION
        {
            set
            {
                _JOB_DESIGNATION = value;
            }
            get
            {
                return _JOB_DESIGNATION;
            }
        }
        public DateTime JOIN_DATE
        {
            set
            {
                _JOIN_DATE = value;
            }
            get
            {
                return _JOIN_DATE;
            }
        }
        public String LOGIN_NAME
        {
            set
            {
                _LOGIN_NAME = value;
            }
            get
            {
                return _LOGIN_NAME;
            }
        }
        public String PASSWORD
        {
            set
            {
                _PASSWORD = value;
            }
            get
            {
                return _PASSWORD;
            }
        }
        public String STAFF_CTRL
        {
            set
            {
                _STAFF_CTRL = value;
            }
            get
            {
                return _STAFF_CTRL;
            }
        }
        public String STAFF_PICTURE
        {
            set
            {
                _STAFF_PICTURE = value;
            }
            get
            {
                return _STAFF_PICTURE;
            }
        }
        public String IS_ADVISOR
        {
            set
            {
                _IS_ADVISOR = value;
            }
            get
            {
                return _IS_ADVISOR;
            }
        }       
        public String REGINED_REASON
        {
            set
            {
                _REGINED_REASON = value;
            }
            get
            {
                return _REGINED_REASON;
            }
        }
        public DateTime REGINED_DATE
        {
            set
            {
                _REGINED_DATE = value;
            }
            get
            {
                return _REGINED_DATE;
            }
        }
        public DateTime CREATED_DATE
        {
            set
            {
                _CREATED_DATE = value;
            }
            get
            {
                return _CREATED_DATE;
            }
        }
        public String CREATED_BY
        {
            set
            {
                _CREATED_BY = value;
            }
            get
            {
                return _CREATED_BY;
            }
        }
        
    }
