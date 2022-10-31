using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class student_AdmitCard_Default2 : System.Web.UI.Page
{
    string sid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
            Response.Redirect("../_login.aspx");
        else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
        {
            sid = Session["ctrlId"].ToString();
            Response.Redirect("../_login.aspx");
        }
        else
        {
            sid = Session["ctrlId"].ToString();
        }      

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string DUE = "", SemDue = "", graceAmt="";
        DataSet InsDate_ds = new DataSet();
        InsDate_ds.Merge(new student_webService().get_FN_GET_PER_SEM_DUE(Convert.ToString(txtID.Text), Convert.ToString(txtYar.Text), Convert.ToString(txtSem.Text)));
        if (InsDate_ds.Tables["SEM_DUE"].Rows.Count > 0)
        {
            foreach (DataRow InsDate_dr in InsDate_ds.Tables["SEM_DUE"].Rows)
            {
                DUE = Convert.ToString(InsDate_dr["DUE"]);

                string[] code = DUE.Split('|'); //Request.QueryString["DUE"].ToString().Split('|');
                if (code.Length > 0)
                {
                    SemDue = code[0];
                    graceAmt = code[1];


                    



                    //if (code[0] == null || code[0] == "")
                    //{
                    //    SemDue = "null";
                    //}
                    //else
                    //{
                    //    SemDue = code[0];
                    //}
                    //if (code[1] == null || code[1] == "")
                    //{
                    //    graceAmt = "null";
                    //}
                    //else
                    //{
                    //    graceAmt = code[1];
                    //}
                   
                }

                lblDueT.Text = Convert.ToString(SemDue);
                lblGrs.Text = Convert.ToString(graceAmt);
                if (SemDue == "0" || graceAmt == "0")
                    {
                        lblmsg.Text = "No due found";
                    }
            }

            lblDue.Text = DUE;
        }
    }

//    private void func_call()
//    {
//        OracleConnection conn = new OracleConnection("Data Source=oracledb; User Id=UserID;Password=Password;");

//// create the command for the function
//OracleCommand cmd = new OracleCommand();
//cmd.Connection = conn;
//cmd.CommandText = "GET_EMPLOYEE_EMAIL";
//cmd.CommandType = CommandType.StoredProcedure;

//// add the parameters, including the return parameter to retrieve
//// the return value
//cmd.Parameters.Add("p_employee_id", OracleType.Number).Value = 101;
//cmd.Parameters.Add("p_email", OracleType.VarChar, 25).Direction = ParameterDirection.ReturnValue;

//// execute the function
//conn.Open();
//cmd.ExecuteNonQuery();
//conn.Close();
//    }
}
