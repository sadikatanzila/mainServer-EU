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

public partial class student_uploadPicturet : System.Web.UI.Page
{

    string user = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
                user = Session["ctrl_admin_Id"].ToString();
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }



        //if (Session.Count == 0)
        //    Response.Redirect("../_login.aspx");
        //else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
        //{
        //    Response.Redirect("../_login.aspx");
        //}

        lbl_message.Text = "";
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        //student_images
        save_image();
    }


    private void save_image()
    {
        if ((txtID.Text != "") && (FileUpload_image.PostedFile != null) && (FileUpload_image.PostedFile.ContentLength > 0))
        {
            Session["ctrlId"] = txtID.Text;
            int fileLen;
            fileLen = FileUpload_image.PostedFile.ContentLength;
            byte[] input = new byte[fileLen];
            input = FileUpload_image.FileBytes;

            string fn = System.IO.Path.GetFileName(FileUpload_image.PostedFile.FileName);
            string[] fileExtension = fn.Split('.');
            string f_name = fn.Split('\\')[fn.Split('\\').Length - 1];

            try
            {
                string prePicName = new student_webService().get_student_picture();
                if (!string.IsNullOrEmpty(prePicName))
                {
                    try
                    {
                        string pn = Server.MapPath("~/student/profile/student_images") + "/" + Session["ctrlId"] + "." + prePicName.Split('.')[prePicName.Split('.').Length - 1];
                        File.Delete("" + pn);

                    }
                    catch (Exception e)
                    {
                        lbl_message.Text = "Previous file deletion failed.";//e.Message;
                    }
                }
                new student_webService().upload_student_picture(f_name);
                string SaveLocation = Server.MapPath("~/student/profile/student_images/") + Session["ctrlId"] + "." + fileExtension[fileExtension.Length - 1];
                FileUpload_image.PostedFile.SaveAs(SaveLocation);

                lbl_message.Text = "" + new cls_message().getMessage(8);
            }
            catch (Exception er)
            {
                lbl_message.Text = "Upload failed.";//er.Message;
            }
        }

    }

}
