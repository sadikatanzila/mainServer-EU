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
using System.Globalization;
using System.Threading;


public partial class admin_academicCalender : System.Web.UI.Page
{
    string user = "";
    admin_webService obj_admin = new admin_webService();

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

       // btn_submit.Attributes.Add("onClick", " return chech_valid();");
        btn_save.Attributes.Add("onClick", " return chech_valid_data();");

        lbl_list_message.Text = "";
        lbl_message.Text = "";

        if (!IsPostBack)
        {
            btn_modify.Enabled = false;
            btn_delete.Enabled = false;
        }

       // btn_studentOpening.Attributes.Add("onClick", "loadCalender_stOpening();");
       // btn_studentClosing.Attributes.Add("onClick", "loadCalender_stClosing();");
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("AC_calender");

        ds.Tables["AC_calender"].Columns.Add("id");
        ds.Tables["AC_calender"].Columns.Add("YEAR");
        ds.Tables["AC_calender"].Columns.Add("SEMESTER");
        ds.Tables["AC_calender"].Columns.Add("EVENT");
        ds.Tables["AC_calender"].Columns.Add("FROM_DATE");
        ds.Tables["AC_calender"].Columns.Add("TO_DATE");
        ds.Tables["AC_calender"].Columns.Add("COMMENTS");
        ds.Tables["AC_calender"].Columns.Add("CTRL");

        DataRow dr = ds.Tables["AC_calender"].NewRow();

        dr["id"] = "test";

        foreach (GridViewRow gr in GridView_list.Rows)
        {
            if (((CheckBox)(gr.FindControl("chk_select"))).Checked == true)
            {
                dr["id"] = ((Label)(gr.FindControl("lbl_id"))).Text;
            }
        }  
        dr["YEAR"] = ""+txt_year.Text;
        dr["SEMESTER"] = "" + cmb_semester.SelectedValue.ToString();
        dr["EVENT"] = ""+txt_program.Text;
       
        if (Convert.ToString(txt_student_opening.Text) != "")
        {
            dr["FROM_DATE"] =  "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_student_opening.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)); ;

        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }


        if (Convert.ToString(txt_student_closing.Text) != "")
        {
            dr["TO_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_student_closing.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));

        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }
        
        
        dr["COMMENTS"] = "" + txt_comments.Text;

        if (chk_active.Checked==true)
            dr["CTRL"] = "1";
        else
            dr["CTRL"] = "0";
        ds.Tables["AC_calender"].Rows.Add(dr); 

        string status = "";

        if (dr["id"].ToString()=="test")
            status = new admin_webService().add_academic_calender(ds);
        else
            status = new admin_webService().update_academic_calender(ds);

        if ( status== "1")
        {
            txt_comments.Text = "";
            txt_program.Text = "";
            txt_s_year.Text = txt_year.Text;
            txt_student_opening.Text = "";
            txt_student_closing.Text = "";
            cmb_s_semester.SelectedValue = cmb_semester.SelectedValue.ToString();
            lbl_message.Text = new cls_message().getMessage(2);
            load_ac();
            
        }

    }
    protected void btn_show_Click(object sender, EventArgs e)
    {

        btn_delete.Enabled = true;
        btn_modify.Enabled = true;

        txt_comments.Text = "";
        txt_program.Text = "";
        txt_year.Text = "";
        txt_student_closing.Text = "";
        txt_student_opening.Text = "";
        chk_active.Checked = true;

        load_ac();
    }

    private void load_ac()
    {
        DataSet ds = new DataSet();
        cls_tools obj_tools = new cls_tools();

        ds.Merge(obj_admin.get_all_academic_calender_forA_semester(cmb_s_semester.SelectedValue.ToString(), txt_s_year.Text));

        ds.Tables["WEB_ACADEMIC_CALENDER"].Columns.Add("fromDate");
        ds.Tables["WEB_ACADEMIC_CALENDER"].Columns.Add("toDate");
        ds.Tables["WEB_ACADEMIC_CALENDER"].Columns.Add("acCtrl");

        foreach (DataRow dr in ds.Tables["WEB_ACADEMIC_CALENDER"].Rows)
        {
            dr["fromDate"] = "" + obj_tools.get_user_short_formateDate(dr["FROM_DATE"].ToString());
            dr["toDate"] = "" + obj_tools.get_user_short_formateDate(dr["TO_DATE"].ToString());

            if (dr["CTRL"].ToString() == "1")
                dr["acCtrl"] = "true";
            else
                dr["acCtrl"] = "false";
        }

        if (ds.Tables["WEB_ACADEMIC_CALENDER"].Rows.Count > 0)
        {
            btn_modify.Enabled = true;
            btn_delete.Enabled = true;
        }
        else
        {
            btn_modify.Enabled = false;
            btn_delete.Enabled = false;
        }

        GridView_list.DataSource = ds;
        GridView_list.DataMember = "WEB_ACADEMIC_CALENDER";
        GridView_list.DataBind();    
    }
    protected void btn_modify_Click(object sender, EventArgs e)
    {
        int count = 0;
        string ids = "";
        foreach (GridViewRow gr in GridView_list.Rows)
        {
            if (((CheckBox)(gr.FindControl("chk_select"))).Checked == true)
            {
                ids = ((Label)(gr.FindControl("lbl_id"))).Text;
                DataSet ds=new DataSet();
                ds.Merge(obj_admin.get_a_academicCalender_details(ids));
                foreach (DataRow dr in ds.Tables["WEB_ACADEMIC_CALENDER"].Rows)
                {
                    txt_comments.Text = dr["COMMENTS"].ToString();
                    txt_program.Text = dr["EVENT"].ToString();
                    txt_year.Text = dr["YEAR"].ToString();
                    cmb_semester.SelectedValue = dr["SEMESTER"].ToString();

                    if (dr["FROM_DATE"].ToString() != "")
                    {
                        DateTime OPENING_DATE = Convert.ToDateTime(dr["FROM_DATE"].ToString());
                        txt_student_opening.Text = OPENING_DATE.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
                    }

                    if (dr["TO_DATE"].ToString() != "")
                    {
                        DateTime CLOSING_DATE = Convert.ToDateTime(dr["TO_DATE"].ToString());
                        txt_student_closing.Text = CLOSING_DATE.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
                    }

                  //  txt_student_closing.Text = new cls_tools().get_user_formateDate(dr["TO_DATE"].ToString());
                  //  txt_student_opening.Text = new cls_tools().get_user_formateDate(dr["FROM_DATE"].ToString());


                    if (dr["CTRL"].ToString() == "1")
                        chk_active.Checked = true;
                    else
                        chk_active.Checked = false;

                    break;
                }
                gr.Enabled = false;
                btn_delete.Enabled = false;
                btn_modify.Enabled = false;
            }
        }
       // load_teacher();
    }

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        int count = 0;
        string ids = "";
        foreach (GridViewRow gr in GridView_list.Rows)
        {
            if (((CheckBox)(gr.FindControl("chk_select"))).Checked == true)
            {
                ids = ((Label)(gr.FindControl("lbl_id"))).Text;
                if (obj_admin.delete_a_academicCalender(ids)=="1")
                {
                    lbl_list_message.Text = "" + new cls_message().getMessage(21);
                    load_ac();
                }
                
            }
        }
    }
}
