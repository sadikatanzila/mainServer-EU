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
using System.Text.RegularExpressions;


public partial class student_StudentForms_TransportRoute : System.Web.UI.Page
{
    string faculty_id = "", point_id = "";
    string sid = "",SNAME="", PROGRAM_ID = "",PROGRAM="",Contact="", COLLEGECODE = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
                Response.Redirect("~/student/_login.aspx");
            else
                if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
                {
                    Response.Redirect("~/student/_login.aspx");
                }
                else
                {
                    sid = Session["ctrlId"].ToString();
                    if (!IsPostBack)
                    {

                        DataSet S_ds = new DataSet();
                        S_ds.Merge(new student_webService().get_PROGRAM_ID(Session["ctrlId"].ToString()));

                        if (S_ds.Tables["Student"].Rows.Count > 0)
                        {
                            foreach (DataRow R_dr in S_ds.Tables["Student"].Rows)
                            {
                                sid = R_dr["SID"].ToString();
                                PROGRAM = R_dr["PROGRAM"].ToString();
                                COLLEGECODE = R_dr["COLLEGECODE"].ToString();
                                SNAME = R_dr["SNAME"].ToString();
                                PROGRAM_ID = R_dr["PROGRAM_ID"].ToString();
                                Contact = R_dr["PHONE"].ToString();


                                lblID.Text = sid;
                                lblName.Text = SNAME;
                                lblContact.Text = Contact;
                                lblProgram.Text = PROGRAM;
                                lblProgramID.Text = PROGRAM_ID;




                            }
                        }



                        DataSet T_ds = new DataSet();
                        T_ds.Merge(new student_webService().get_TransportInfo(Session["ctrlId"].ToString()));
                        if (T_ds.Tables["Student"].Rows.Count > 0)
                        {
                            foreach (DataRow T_dr in T_ds.Tables["Student"].Rows)
                            {
                                lblTransportID.Text = "Your Transport information already have saved as " + T_dr["TRANSPORT_ROUTE_ID"].ToString() + " ID, To Change your Router related Information Please Contact with Registrar Office";
                                txtContact.Text = T_dr["CONTACT"].ToString();
                                txtDrop.Text = T_dr["PICK_PLACE"].ToString();
                                txtPreAddress.Text = replaceOposite(T_dr["PRESENT_ADDRESS"].ToString());

                                txtContact.ReadOnly = true;
                                txtDrop.ReadOnly = true;
                                txtPreAddress.ReadOnly = true;

                                loadRoute();
                                if (T_dr["ROUTE_ID"].ToString() != "")
                                    ddlRoute.SelectedValue = T_dr["ROUTE_ID"].ToString();
                                else
                                    ddlRoute.SelectedValue = "Select";



                                if (T_dr["POINT_ID"].ToString() != "")
                                {
                                    ddlRoute_SelectedIndexChanged(sender, e);
                                    ddlPicupPoint.SelectedValue = T_dr["POINT_ID"].ToString();
                                }
                                //   else
                                //      ddlPicupPoint.SelectedValue = "Select";
                                //   btnSave.Visible = false;
                                btnSave.Text = "Update";

                            }
                        }
                        else
                        {
                            lblTransportID.Text = "";

                            txtContact.ReadOnly = false;
                            txtDrop.ReadOnly = false;
                            txtPreAddress.ReadOnly = false;

                            btnSave.Visible = true;

                            loadRoute();

                        }
                    }
                    else
                    {

                    }


                    

                }
        }
        catch (Exception exp) { Response.Redirect("~/student/_login.aspx"); }
    }

    private void loadRoute()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_Route());

        DataRow dr = ds.Tables["ROUTELIST"].NewRow();
        dr["ROUTE_NAME"] = "Select";
        dr["ROUTE_ID"] = "Select";
        ds.Tables["ROUTELIST"].Rows.Add(dr);

        ddlRoute.DataSource = ds.Tables["ROUTELIST"];
        ddlRoute.DataTextField = "ROUTE_NAME";
        ddlRoute.DataValueField = "ROUTE_ID";
        ddlRoute.DataBind();


        if (faculty_id == "")
        {
            ddlRoute.SelectedValue = "Select";
        }
        else
            ddlRoute.SelectedValue = faculty_id;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtContact.Text != "" && txtDrop.Text != "" && txtPreAddress.Text != "")
        {
            save_LeaveInfo();
        }
        else
        {
            lbl_message.Text = "Please Fill up the necessary Fields";
        }
    }

    protected string replace_(string st)
    {

        //Regex rx = new Regex(" ");
        //string s1 = rx.Replace(st, "&nbsp;");
        Regex ry = new Regex("\r\n|\n|\r");
        string s2 = ry.Replace(st, "<br/>");
        return s2;


    }

    protected string replaceOposite(string st)
    {

        //Regex rx = new Regex("&nbsp;");
        //string s1 = rx.Replace(st, " ");
        Regex ry = new Regex("<br/>");
        string s2 = ry.Replace(st, "\r\n");
        return s2;


    }

    private void save_LeaveInfo()
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("C_TRANSPORT_ROUTE");

        ds.Tables["C_TRANSPORT_ROUTE"].Columns.Add("SID");
        ds.Tables["C_TRANSPORT_ROUTE"].Columns.Add("PREV_CONTACT");
        ds.Tables["C_TRANSPORT_ROUTE"].Columns.Add("PROGRAM_ID");
        ds.Tables["C_TRANSPORT_ROUTE"].Columns.Add("CONTACT");
        ds.Tables["C_TRANSPORT_ROUTE"].Columns.Add("PICK_PLACE");
        ds.Tables["C_TRANSPORT_ROUTE"].Columns.Add("PRESENT_ADDRESS");
        ds.Tables["C_TRANSPORT_ROUTE"].Columns.Add("INSERTION");



        DataRow dr = ds.Tables["C_TRANSPORT_ROUTE"].NewRow();

        dr["SID"] = "" + lblID.Text;
        dr["PREV_CONTACT"] = "" + lblContact.Text;
        dr["PROGRAM_ID"] = "" + lblProgramID.Text;

        dr["CONTACT"] = "" + txtContact.Text;
        dr["PICK_PLACE"] = "" + txtDrop.Text;
        dr["PRESENT_ADDRESS"] = "" + replace_(txtPreAddress.Text);
        dr["INSERTION"] = "" + Convert.ToString(Convert.ToDateTime(DateTime.Now)); 

        ds.Tables["C_TRANSPORT_ROUTE"].Rows.Add(dr);

        string status = "";

        if (btnSave.Text == "Save")
        {
            dr["INSERTION"] = "" + Convert.ToString(Convert.ToDateTime(DateTime.Now)); 
            status = new student_webService().save_Student_Route_info(ds);

            if (status != "")
            {
                Clear();
                lbl_message.Text = "" + new cls_message().getMessage(2);
                lblTransportID.Text = "Your Transport information is saved as " + status + " ID, Please Save this ID";
            }
            else
                lblTransportID.Text = "" + new cls_message().getMessage(14);
        }
        else
        {
            dr["UPDATE_TIME"] = "" + Convert.ToString(Convert.ToDateTime(DateTime.Now));
            status = new student_webService().Update_Student_Route_info(ds, lblTransportID.Text);

            if (status == "1")
            {
                Clear();
                lbl_message.Text = "Information has been updated. " ;
                lblTransportID.Text = "Your Transport information is saved as " + lblTransportID.Text + " ID, Please Save this ID";
            }
            else
                lblTransportID.Text = "" + new cls_message().getMessage(14);
        }
        //   status = new admin_webService().save_Staff_info(ds);


        

    }

    private void Clear()
    {

        txtContact.Text = "";
        txtDrop.Text = "";
        txtPreAddress.Text = "";
        btnSave.Visible = false;
    }
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds_point = new DataSet();
        ds_point.Merge(new admin_webService().get_Route_Point(ddlRoute.SelectedValue.ToString()));

        DataRow dr = ds_point.Tables["PointList"].NewRow();
        dr["POINT_NAME"] = "Select";
        dr["POINT_ID"] = "Select";
        ds_point.Tables["PointList"].Rows.Add(dr);


        ddlPicupPoint.DataSource = ds_point.Tables["PointList"];
        ddlPicupPoint.DataTextField = "POINT_NAME";
        ddlPicupPoint.DataValueField = "POINT_ID";
        ddlPicupPoint.DataBind();

        if (point_id == "")
        {
            ddlPicupPoint.SelectedValue = "Select";
        }
        else
            ddlPicupPoint.SelectedValue = point_id;

    }
}