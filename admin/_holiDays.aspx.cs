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

public partial class admin_holiDays : System.Web.UI.Page
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

     //   btn_studentOpening.Attributes.Add("onClick", "loadCalender_stOpening();");
      //  btn_studentClosing.Attributes.Add("onClick", "loadCalender_stClosing();");

    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("WEB_ACADEMIC_HOLIDAYS");

        ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Columns.Add("id");
        ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Columns.Add("YEAR");
        ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Columns.Add("SEMESTER");
        ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Columns.Add("day_title");
        ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Columns.Add("FROM_DATE");
        ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Columns.Add("TO_DATE");
        ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Columns.Add("COMMENTS");
        ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Columns.Add("CTRL");

        DataRow dr = ds.Tables["WEB_ACADEMIC_HOLIDAYS"].NewRow();

        dr["id"] = "test";

        foreach (GridViewRow gr in GridView_list.Rows)
        {
            if (((CheckBox)(gr.FindControl("chk_select"))).Checked == true)
            {
                dr["id"] = ((Label)(gr.FindControl("lbl_id"))).Text;
            }
        }
        dr["YEAR"] = "" + txt_year.Text;
        dr["SEMESTER"] = "" + cmb_semester.SelectedValue.ToString();
        dr["day_title"] = "" + txt_program.Text;

        if (Convert.ToString(txt_student_opening.Text) != "")
        {
            dr["FROM_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_student_opening.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)); ;

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
        
     //   dr["FROM_DATE"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_student_opening"].ToString()));
     //   dr["TO_DATE"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_student_closing"].ToString()));
       
        
        dr["COMMENTS"] = "" + txt_comments.Text;

        if (chk_active.Checked == true)
            dr["CTRL"] = "1";
        else
            dr["CTRL"] = "0";
        ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Rows.Add(dr);

        string status = "";

        if (dr["id"].ToString() == "test")
            status = obj_admin.add_academic_holidays(ds);
        else
            status = obj_admin.update_academic_holidays(ds);

        if (status == "1")
        {
            txt_comments.Text = "";
            txt_program.Text = "";
            txt_s_year.Text = txt_year.Text;
            txt_student_opening.Text = "";
            txt_student_closing.Text = "";
            cmb_s_semester.SelectedValue = cmb_semester.SelectedValue.ToString();
            lbl_message.Text = new cls_message().getMessage(2);
            load_holiday();

        }
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
                DataSet ds = new DataSet();
                ds.Merge(obj_admin.get_a_academic_holiday_details(ids));
                foreach (DataRow dr in ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Rows)
                {
                    txt_comments.Text = dr["COMMENTS"].ToString();
                    txt_program.Text = dr["DAY_TITLE"].ToString();
                    txt_year.Text = dr["YEAR"].ToString();
                    cmb_semester.SelectedValue = dr["SEMESTER"].ToString();

                   // txt_student_closing.Text = new cls_tools().get_user_formateDate(dr["TO_DATE"].ToString());
                  //  txt_student_opening.Text = new cls_tools().get_user_formateDate(dr["FROM_DATE"].ToString());
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
                if (obj_admin.delete_a_academic_holiday(ids) == "1")
                {
                    lbl_list_message.Text = "" + new cls_message().getMessage(21);
                    load_holiday();
                }
            }
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

        load_holiday();
    }

    private void load_holiday()
    {
        DataSet ds = new DataSet();
        cls_tools obj_tools = new cls_tools();

        ds.Merge(obj_admin.get_all_academic_holidays_forA_semester(cmb_s_semester.SelectedValue.ToString(), txt_s_year.Text));

        ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Columns.Add("fromDate");
        ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Columns.Add("toDate");
        ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Columns.Add("acCtrl");

        foreach (DataRow dr in ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Rows)
        {
            dr["fromDate"] = "" + obj_tools.get_user_short_formateDate(dr["FROM_DATE"].ToString());
            dr["toDate"] = "" + obj_tools.get_user_short_formateDate(dr["TO_DATE"].ToString());

            if (dr["CTRL"].ToString() == "1")
                dr["acCtrl"] = "true";
            else
                dr["acCtrl"] = "false";
        }

        if (ds.Tables["WEB_ACADEMIC_HOLIDAYS"].Rows.Count > 0)
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
        GridView_list.DataMember = "WEB_ACADEMIC_HOLIDAYS";
        GridView_list.DataBind();
    }
}
