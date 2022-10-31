using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;

public partial class employee_msgSending : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        load_student();
    }
   
    protected void btnsend_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtmsg.Text != String.Empty)
            {
                SendMessage();
              
              //  txtmono.Text = String.Empty;
                txtmsg.Text = String.Empty;
                lblmsg.Visible = true;
                lblmsg.Text = "Message Sented";
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Not Sented";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }


    private void SendMessage()
    {
        int count = 0;
        string CONTACT = ""; string createdURL = "";
        //we creating the necessary URL string:
        string ozSURL = "http://bulksms.teletalk.com.bd/link_sms_send.php?"; //where Ozeki NG SMS Gateway is running
    
        string ozUser = HttpUtility.UrlEncode("eastern"); //username for successful login
        string ozPassw = HttpUtility.UrlEncode("eastern"); //user's password
        string message = "";

        ////http://bulksms.teletalk.com.bd/link_sms_send.php?op=SMS&user=&pass=&mobile=8801XXXXXXXXX&charset=UTF-8&sms=বাংলা

        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_MessagingStudent());

        foreach (DataRow drEmp in ds.Tables["C_MESSAGE_STUDENT"].Rows)
        {
            count = (drEmp["CONTACT"].ToString()).Length; //txtmono.Text.Length;
            if (count == 11 && count < 12)
            {
                CONTACT = "88" + drEmp["CONTACT"].ToString();
            }
            else
                if (count == 13)
                {
                    CONTACT = drEmp["CONTACT"].ToString();
                }

            message = "You have dues of " + drEmp["DUE"].ToString() + " tk, Please pay that to EU account";
            createdURL = ozSURL + "op=SMS&user=" + ozUser + "&pass=" + ozPassw + "&mobile=" + CONTACT + "&charset=UTF-8&sms=" + message;// txtmsg.Text;

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(createdURL);

            //Get response from Ozeki NG SMS Gateway Server and read the answer
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
        }


        //string createdURL = ozSURL + "op=SMS&user=" + ozUser + "&pass=" + ozPassw + "&mobile=" + txtmono.Text + "&charset=UTF-8&sms=" + txtmsg.Text;
      
       

    }
    protected void btnsAdd_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("C_MESSAGE_STUDENT");

     //   ds.Tables["C_MESSAGE_STUDENT"].Columns.Add("C_MESSAGE_STUDENT_ID");
        ds.Tables["C_MESSAGE_STUDENT"].Columns.Add("SID");
        ds.Tables["C_MESSAGE_STUDENT"].Columns.Add("CONTACT");
        DataRow dr = ds.Tables["C_MESSAGE_STUDENT"].NewRow();


        dr["SID"] = "" + txtSID.Text;
        dr["CONTACT"] = "" + txtContact.Text;

        ds.Tables["C_MESSAGE_STUDENT"].Rows.Add(dr);

        string status = "";

        status = new admin_webService().save_MessagingStudent(ds);
        load_student();
    }

    private void load_student()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_MessagingStudent());
        GridView_studentList.DataSource = ds;
        GridView_studentList.DataMember = "C_MESSAGE_STUDENT";
        GridView_studentList.DataBind();
    }

  

}