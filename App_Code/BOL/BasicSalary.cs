using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
    public class BasicSalary
    {
        private String _DESIG_ID;
        private String _Basic_salary;
        private DateTime _Active_date;
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
        public String Basic_salary
        {
            set
            {
                _Basic_salary = value;
            }
            get
            {
                return _Basic_salary;
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

