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
using System.IO;
using System.Net;
using System.Text; 

public partial class student_finance_updateLedger : System.Web.UI.Page
{
    string sid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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


                if (!IsPostBack)
                {
                    CheckPayment(sid);
                }
            }
        }
        catch (Exception exp) { Response.Redirect("../_login.aspx"); }
    }

    private void CheckPayment(string SID)
    {
        if (new student_webService().get_Payment_dateRange(SID))
        {
            if (new student_webService().Chk_StdControl(SID))
            {
                btn_submit.Enabled = false;
                lblMsg.Text += "Ladger will not be Updated twice in a day";
            }
            else
            {
                btn_submit.Enabled = true;
            }
        }
        else
        {
            btn_submit.Enabled = false;
            lblMsg.Text += "There is No Payment Issued in this Current Date";
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string s = sid;
        byte[] StringAscII = System.Text.Encoding.ASCII.GetBytes(s);
        string a = "", token = "";
        /*int key = 2;
        for (int i = 0; i < StringAscII.Length; i++)
        {
            //if (StringAscII[i] != 124)
            //{
            //    StringAscII[i] = Convert.ToInt32(StringAscII[i]) + 2;
            //}
            if (StringAscII[i] == 124)
                a += Convert.ToString(StringAscII[i]) + "-";  //if i=0 a= 65, if i=1 a=66 and so on
            else
                a += Convert.ToString(StringAscII[i] + key) + "-";  //if i=0 a= 65, if i=1 a=66 and so on

            // a += Convert.ToString(StringAscII[i])+"-";//if i=0 a= 65, if i=1 a=66 and so on

        }*/
        token = "eastern";

        // Create a request for the URL.
        // Create a request using a URL that can receive a post.  

        String hostURL = "http://webportal.easternuni.edu.bd:8080"; //Please lookup this value from a properties file to make it dynamic;
        WebRequest request = WebRequest.Create(hostURL + "/icampusnew/rest/uni/ledger"); 
       // WebRequest request = WebRequest.Create("http://www.contoso.com/default.html");
        // Set the Method property of the request to POST.  
        request.Method = "POST";
        // Create POST data and convert it to a byte array.  
        string postData = "sid=" + "" + sid + "&token=" + token; //"This is a test that posts this string to a Web server.";
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        // Set the ContentType property of the WebRequest.  
        request.ContentType = "application/x-www-form-urlencoded";
        // Set the ContentLength property of the WebRequest.  
        request.ContentLength = byteArray.Length;
        // Get the request stream.  
        Stream dataStream = request.GetRequestStream();
        // Write the data to the request stream.  
        dataStream.Write(byteArray, 0, byteArray.Length);
        // Close the Stream object.  
        dataStream.Close();
        // Get the response.  
        WebResponse response = request.GetResponse();
        // Display the status.  
        Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        // Get the stream containing content returned by the server.  
        dataStream = response.GetResponseStream();
        // Open the stream using a StreamReader for easy access.  
        StreamReader reader = new StreamReader(dataStream);
        // Read the content.  
        string responseFromServer = reader.ReadToEnd();
        // Display the content.  
        Console.WriteLine(responseFromServer);
        // Clean up the streams.  
        reader.Close();
        dataStream.Close();
        response.Close();


     //   Response.Redirect(string.Format("http://webportal.easternuni.edu.bd:9090/icampusnew/admitCardShow.action?token={0}", token));
    
    }
}
