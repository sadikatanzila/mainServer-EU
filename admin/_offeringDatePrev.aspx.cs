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

public partial class admin_offeringDatePrev : System.Web.UI.Page
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


        btn_submit.Attributes.Add("onClick", " return chech_valid();");
        btn_save.Attributes.Add("onClick", " return chech_valid_data();");
        lbl_message.Text = "";
        lbl_semHeader.Text = "";


        btn_teacherOpening.Attributes.Add("onClick", "loadCalender_thOPening();");
        btn_teacherClosing.Attributes.Add("onClick", "loadCalender_thClosing();");
        btn_teacherReOpening.Attributes.Add("onClick", "loadCalender_thReOPening();");
        btn_teacherReClosing.Attributes.Add("onClick", "loadCalender_thReClosing();");
        btn_studentOpening.Attributes.Add("onClick", "loadCalender_stOpening();");
        btn_studentClosing.Attributes.Add("onClick", "loadCalender_stClosing();");

        btn_teacher_evalOpening.Attributes.Add("onClick", "loadCalender_teacher_eval_open();");
        btn_teacher_evalClosing.Attributes.Add("onClick", "loadCalender_teacher_eval_close();");
        btn_resultPublish.Attributes.Add("onClick", "loadCalender_result();");



        btn_FinalAdmit_opening.Attributes.Add("onClick", "load_FinalAdmitCard_open();");
        btn_FinalAdmit_closing.Attributes.Add("onClick", "load_FinalAdmitCard_closing();");
        btn_MidAdmit_opening.Attributes.Add("onClick", "load_MidAdmitCard_open();");
        btn_MidAdmit_closing.Attributes.Add("onClick", "load_MidAdmitCard_closing();");


        if (IsPostBack)
            tbl_dates.Visible = true;
        else
        {
            tbl_dates.Visible = false;
            Session["sem"] = "";
            Session["year"] = "";
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        Session["sem"] = "" + cmb_semester.SelectedValue.ToString();
        Session["year"] = ""+txt_year.Text.Trim();
        lbl_semHeader.Text = "Semester: " + new cls_tools().get_word_semester(cmb_semester.SelectedValue.ToString()) + " " + txt_year.Text;
        load_date_info();
        txtOpenedDep.Text = AppSettings.CourseOfferingOpenedDepList;
    }

    private void load_date_info()
    {
        txt_student_opening.Text = "" ;
        txt_student_closing.Text = "";
        txt_teacher_opening.Text = "";
        txt_teacher_closing.Text = "";
        txt_teacher_re_opening.Text = "";
        txt_teacher_re_closing.Text = "";
        txt_teacher_eval_opening.Text = "";
        txt_teacher_eval_closing.Text = "";
        txt_resultPublish.Text = "";

        txt_FinalAdmit_opening.Text = "";
        txt_FinalAdmit_closing.Text = "";
        txt_MidAdmit_opening.Text = "";
        txt_MidAdmit_closing.Text = "";

        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_pre_offeringDate(Session["sem"].ToString(), Session["year"].ToString()));
        foreach (DataRow dr in ds.Tables["WEB_PRE_OFFERING_DATE"].Rows)
        {
            txt_student_opening.Text = "" + new cls_tools().get_user_formateDate(dr["OPENING_DATE"].ToString());
            txt_student_closing.Text = "" + new cls_tools().get_user_formateDate(dr["CLOSING_DATE"].ToString());
            txt_teacher_opening.Text = "" + new cls_tools().get_user_formateDate(dr["TEACHER_OPENINGDATE"].ToString());
            txt_teacher_closing.Text = "" + new cls_tools().get_user_formateDate(dr["TEACHER_CLOSINGDATE"].ToString());
            txt_teacher_re_opening.Text = "" + new cls_tools().get_user_formateDate(dr["TEACHER_RE_OPENINGDATE"].ToString());
            txt_teacher_re_closing.Text = "" + new cls_tools().get_user_formateDate(dr["TEACHER_RE_CLOSINGDATE"].ToString());
            txt_teacher_eval_opening.Text = "" + new cls_tools().get_user_formateDate(dr["EVAL_OPENING"].ToString());
            txt_teacher_eval_closing.Text = "" + new cls_tools().get_user_formateDate(dr["EVAL_CLOSING"].ToString());
            txt_resultPublish.Text = "" + new cls_tools().get_user_formateDate(dr["SEM_RESULT_PUBLISH"].ToString());

            txt_FinalAdmit_opening.Text = "" + new cls_tools().get_user_formateDate(dr["ADMIT_OPENING"].ToString());
            txt_FinalAdmit_closing.Text = "" + new cls_tools().get_user_formateDate(dr["ADMIT_CLOSING"].ToString());
            txt_MidAdmit_opening.Text = "" + new cls_tools().get_user_formateDate(dr["MIDAMIT_OPENING"].ToString());
            txt_MidAdmit_closing.Text = "" + new cls_tools().get_user_formateDate(dr["MIDAMIT_CLOSING"].ToString());
        }    
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        /*if (Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_definition$txt_student_closing"].ToString()) > Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_definition$txt_teacher_opening"].ToString()) || Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_definition$txt_student_closing"].ToString()) > Convert.ToDateTime(Request["ctl00$ContentPlaceHolder_definition$txt_teacher_closing"].ToString()))
        {
            lbl_message.Text = "Please enter valid sequence date for student and teacher" ;
            return;
        }*/

        DataSet ds = new DataSet();
        
        ds.Tables.Add("WEB_PRE_OFFERING_DATE");

        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("SEMESTER");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("YEAR");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("OPENING_DATE");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("CLOSING_DATE");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("CTRL");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("TEACHER_OPENINGDATE");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("TEACHER_CLOSINGDATE");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("TEACHER_RE_OPENINGDATE");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("TEACHER_RE_CLOSINGDATE");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("EVAL_OPENING");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("EVAL_CLOSING");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("SEM_RESULT_PUBLISH");


        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("ADMIT_OPENING");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("ADMIT_CLOSING");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("MIDAMIT_OPENING");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("MIDAMIT_CLOSING");
        

        DataRow dr = ds.Tables["WEB_PRE_OFFERING_DATE"].NewRow();

        dr["SEMESTER"] = ""+Session["sem"].ToString();
        dr["YEAR"] = "" + Session["year"].ToString();
        dr["OPENING_DATE"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_student_opening"].ToString()));
        dr["CLOSING_DATE"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_student_closing"].ToString()));
        dr["CTRL"] = "1";
        dr["TEACHER_OPENINGDATE"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_teacher_opening"].ToString()));
        dr["TEACHER_CLOSINGDATE"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_teacher_closing"].ToString()));
        
		if (Request["ctl00$ContentPlaceHolder_definition$txt_teacher_re_opening"].ToString() != "")
        {
            dr["TEACHER_RE_OPENINGDATE"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_teacher_re_opening"].ToString()));
        }
        if (Request["ctl00$ContentPlaceHolder_definition$txt_teacher_re_closing"].ToString() != "")
        {
            dr["TEACHER_RE_CLOSINGDATE"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_teacher_re_closing"].ToString()));
        }

        dr["EVAL_OPENING"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_teacher_eval_opening"].ToString()));
        dr["EVAL_CLOSING"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_teacher_eval_closing"].ToString()));
        dr["SEM_RESULT_PUBLISH"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_resultPublish"].ToString()));

        if (Request["ctl00$ContentPlaceHolder_definition$txt_FinalAdmit_opening"].ToString() != "")
        {
            dr["ADMIT_OPENING"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_FinalAdmit_opening"].ToString()));
        }
        if (Request["ctl00$ContentPlaceHolder_definition$txt_FinalAdmit_closing"].ToString() != "")
        {
            dr["ADMIT_CLOSING"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_FinalAdmit_closing"].ToString()));
        }
        if (Request["ctl00$ContentPlaceHolder_definition$txt_MidAdmit_opening"].ToString() != "")
        {
            dr["MIDAMIT_OPENING"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_MidAdmit_opening"].ToString()));
        }
        if (Request["ctl00$ContentPlaceHolder_definition$txt_MidAdmit_closing"].ToString() != "")
        {
            dr["MIDAMIT_CLOSING"] = "" + new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_MidAdmit_closing"].ToString()));
        }
		AppSettings.CourseOfferingOpenedDepList = txtOpenedDep.Text;

        ds.Tables["WEB_PRE_OFFERING_DATE"].Rows.Add(dr);

        if (new admin_webService().save_pre_course_offeringDate(ds, Session["year"].ToString(), Session["sem"].ToString()) == "1")
        {
            lbl_message.Text = "" + new cls_message().getMessage(2);
            load_date_info();
        }
        else
            lbl_message.Text = "" + new cls_message().getMessage(14);


    }
}
