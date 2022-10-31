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

public partial class admin_offeringDate : System.Web.UI.Page
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


       // btn_teacherOpening.Attributes.Add("onClick", "loadCalender_thOPening();");
      //  btn_teacherClosing.Attributes.Add("onClick", "loadCalender_thClosing();");
      //  btn_teacherReOpening.Attributes.Add("onClick", "loadCalender_thReOPening();");
      //  btn_teacherReClosing.Attributes.Add("onClick", "loadCalender_thReClosing();");
       // btn_studentOpening.Attributes.Add("onClick", "loadCalender_stOpening();");
      //  btn_studentClosing.Attributes.Add("onClick", "loadCalender_stClosing();");

      //  btn_teacher_evalOpening.Attributes.Add("onClick", "loadCalender_teacher_eval_open();");
      //  btn_teacher_evalClosing.Attributes.Add("onClick", "loadCalender_teacher_eval_close();");
      //  btn_resultPublish.Attributes.Add("onClick", "loadCalender_result();");



      //  btn_FinalAdmit_opening.Attributes.Add("onClick", "load_FinalAdmitCard_open();");
      //  btn_FinalAdmit_closing.Attributes.Add("onClick", "load_FinalAdmitCard_closing();");
      //  btn_MidAdmit_opening.Attributes.Add("onClick", "load_MidAdmitCard_open();");
      //  btn_MidAdmit_closing.Attributes.Add("onClick", "load_MidAdmitCard_closing();");


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
        txt_student_closing.Text = "";
        txt_student_ReOpening.Text = "";
        txt_student_Reclosing.Text = "";
        txt_teacher_re_opening.Text = "";
        txt_teacher_re_closing.Text = "";
        txt_student_LateFeeOpening.Text = "";
        txt_student_LateFeeClosing.Text = "";
        txt_teacher_opening.Text = "";
        txt_teacher_closing.Text = "";
        txt_teacher_eval_opening.Text = "";
        txt_teacher_eval_closing.Text = "";
        txt_resultPublish.Text = "";
        txt_FinalAdmit_opening.Text = "";
        txt_FinalAdmit_closing.Text = "";
        txt_MidAdmit_opening.Text = "";
        txt_MidAdmit_closing.Text = "";

        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_pre_offeringDate(Session["sem"].ToString(), Session["year"].ToString()));

        if (ds.Tables["WEB_PRE_OFFERING_DATE"].Rows.Count > 0)
        {
            btn_save.Text = "Update";
        }
        else
        {
            btn_save.Text = "Submit";
        }
        foreach (DataRow dr in ds.Tables["WEB_PRE_OFFERING_DATE"].Rows)
        {
           // DateTime localTime = DateTime.Now;
           // string timeString24Hour = localTime.ToString("HH:mm", CultureInfo.CurrentCulture);
            if (dr["OPENING_DATE"].ToString() != "")
            {
                DateTime OPENING_DATE = Convert.ToDateTime(dr["OPENING_DATE"].ToString());
                txt_student_opening.Text = OPENING_DATE.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture); // entity.FromDate.ToString("dd/MM/yyyy");
            }

            if (dr["CLOSING_DATE"].ToString() != "")
            {
                DateTime CLOSING_DATE = Convert.ToDateTime(dr["CLOSING_DATE"].ToString());
                txt_student_closing.Text = CLOSING_DATE.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
            }


            if (dr["LATEADD_OPENING_DATE"].ToString() != "")
            {
                DateTime LATE_OPENING_DATE = Convert.ToDateTime(dr["LATEADD_OPENING_DATE"].ToString());
                txt_student_LateFeeOpening.Text = LATE_OPENING_DATE.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture); // entity.FromDate.ToString("dd/MM/yyyy");
            }

            if (dr["LATEADD_CLOSING_DATE"].ToString() != "")
            {
                DateTime LATE_CLOSING_DATE = Convert.ToDateTime(dr["LATEADD_CLOSING_DATE"].ToString());
                txt_student_LateFeeClosing.Text = LATE_CLOSING_DATE.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
            }



            if (dr["READD_OPENING_DATE"].ToString() != "")
            {
                DateTime READD_OPENING_DATE = Convert.ToDateTime(dr["READD_OPENING_DATE"].ToString());
                txt_student_ReOpening.Text = READD_OPENING_DATE.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture); // entity.FromDate.ToString("dd/MM/yyyy");
            }

            if (dr["READD_CLOSING_DATE"].ToString() != "")
            {
                DateTime READD_CLOSING_DATE = Convert.ToDateTime(dr["READD_CLOSING_DATE"].ToString());
                txt_student_Reclosing.Text = READD_CLOSING_DATE.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
            }


            if (dr["TEACHER_OPENINGDATE"].ToString() != "")
            {
                DateTime TEACHER_OPENINGDATE = Convert.ToDateTime(dr["TEACHER_OPENINGDATE"].ToString());
                txt_teacher_opening.Text = TEACHER_OPENINGDATE.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
            }

            if (dr["TEACHER_CLOSINGDATE"].ToString() != "")
            {
                DateTime TEACHER_CLOSINGDATE = Convert.ToDateTime(dr["TEACHER_CLOSINGDATE"].ToString());
                txt_teacher_closing.Text = TEACHER_CLOSINGDATE.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
            }



            if (dr["TEACHER_RE_OPENINGDATE"].ToString() != "")
            {
                DateTime TEACHER_RE_OPENINGDATE = Convert.ToDateTime(dr["TEACHER_RE_OPENINGDATE"].ToString());
                txt_teacher_re_opening.Text = TEACHER_RE_OPENINGDATE.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
            }

            if (dr["TEACHER_RE_CLOSINGDATE"].ToString() != "")
            {
                DateTime TEACHER_RE_CLOSINGDATE = Convert.ToDateTime(dr["TEACHER_RE_CLOSINGDATE"].ToString());
                txt_teacher_re_closing.Text = TEACHER_RE_CLOSINGDATE.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
            }

            if (dr["EVAL_OPENING"].ToString() != "")
            {
                DateTime EVAL_OPENING = Convert.ToDateTime(dr["EVAL_OPENING"].ToString());
                txt_teacher_eval_opening.Text = EVAL_OPENING.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
            }

            if (dr["EVAL_CLOSING"].ToString() != "")
            {
                DateTime EVAL_CLOSING = Convert.ToDateTime(dr["EVAL_CLOSING"].ToString());
                txt_teacher_eval_closing.Text = EVAL_CLOSING.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
            }

            if (dr["SEM_RESULT_PUBLISH"].ToString() != "")
            {
                DateTime SEM_RESULT_PUBLISH = Convert.ToDateTime(dr["SEM_RESULT_PUBLISH"].ToString());
                txt_resultPublish.Text = SEM_RESULT_PUBLISH.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
            }

            if (dr["ADMIT_OPENING"].ToString() != "")
            {
                DateTime ADMIT_OPENING = Convert.ToDateTime(dr["ADMIT_OPENING"].ToString());
                txt_FinalAdmit_opening.Text = ADMIT_OPENING.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
            }
            

            if (dr["ADMIT_CLOSING"].ToString() != "")
            {
                DateTime ADMIT_CLOSING = Convert.ToDateTime(dr["ADMIT_CLOSING"].ToString());
                txt_FinalAdmit_closing.Text = ADMIT_CLOSING.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
            }

            if (dr["MIDAMIT_OPENING"].ToString() != "")
            {
                DateTime MIDAMIT_OPENING = Convert.ToDateTime(dr["MIDAMIT_OPENING"].ToString());
                txt_MidAdmit_opening.Text = MIDAMIT_OPENING.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
            }

            if (dr["MIDAMIT_CLOSING"].ToString() != "")
            {
                DateTime MIDAMIT_CLOSING = Convert.ToDateTime(dr["MIDAMIT_CLOSING"].ToString());
                txt_MidAdmit_closing.Text = MIDAMIT_CLOSING.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture);
            }
          
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
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("LATEADD_OPENING_DATE");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("LATEADD_CLOSING_DATE");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("READD_OPENING_DATE");
        ds.Tables["WEB_PRE_OFFERING_DATE"].Columns.Add("READD_CLOSING_DATE");
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

        dr["SEMESTER"] = "" + Session["sem"].ToString();
        dr["YEAR"] = "" + Session["year"].ToString();
        if (Convert.ToString(txt_student_opening.Text) != "")
        {
            dr["OPENING_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_student_opening.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));//Convert.ToDateTime(txt_student_opening.Text)); 
          //  new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_student_opening"].ToString()));
        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }

        if (Convert.ToString(txt_student_closing.Text) != "")
        {
            dr["CLOSING_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_student_closing.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));//Convert.ToDateTime("" + Convert.ToString(.Text)));
            //new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_student_closing"].ToString()));
        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }



        if (Convert.ToString(txt_student_LateFeeOpening.Text) != "")
        {
            dr["LATEADD_OPENING_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_student_LateFeeOpening.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));//Convert.ToDateTime(txt_student_opening.Text)); 
            //  new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_student_opening"].ToString()));
        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }

        if (Convert.ToString(txt_student_LateFeeClosing.Text) != "")
        {
            dr["LATEADD_CLOSING_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_student_LateFeeClosing.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));//Convert.ToDateTime("" + Convert.ToString(.Text)));
            //new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_student_closing"].ToString()));
        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }




        if (Convert.ToString(txt_student_ReOpening.Text) != "")
        {
            dr["READD_OPENING_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_student_ReOpening.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));
        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }

        if (Convert.ToString(txt_student_Reclosing.Text) != "")
        {
            dr["READD_CLOSING_DATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_student_Reclosing.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));
        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }







        dr["CTRL"] = "1";
        if (Convert.ToString(txt_teacher_opening.Text) != "")
        {
            dr["TEACHER_OPENINGDATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_teacher_opening.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)); 
            // new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_teacher_opening"].ToString()));
        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }

        if (Convert.ToString(txt_teacher_closing.Text) != "")
        {
            dr["TEACHER_CLOSINGDATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_teacher_closing.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));
            //+ new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_teacher_closing"].ToString()));
        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }

        if (Convert.ToString(txt_teacher_re_opening.Text) != "")
        {
            dr["TEACHER_RE_OPENINGDATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_teacher_re_opening.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));
            //new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_teacher_re_opening"].ToString()));
        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }

        if (Convert.ToString(txt_teacher_re_closing.Text) != "")
        {
            dr["TEACHER_RE_CLOSINGDATE"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_teacher_re_closing.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));
            //new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_teacher_re_closing"].ToString()));
        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }

        if (Convert.ToString(txt_teacher_eval_opening.Text) != "")
        {
            dr["EVAL_OPENING"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_teacher_eval_opening.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));
            //new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_teacher_eval_opening"].ToString()));

        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }

        if (Convert.ToString(txt_teacher_eval_closing.Text) != "")
        {
            dr["EVAL_CLOSING"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_teacher_eval_closing.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)); 
            //new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_teacher_eval_closing"].ToString()));

        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }

        if (Convert.ToString(txt_resultPublish.Text) != "")
        {
            dr["SEM_RESULT_PUBLISH"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_resultPublish.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)); 
            //new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_resultPublish"].ToString()));
        }
        else
        {
            lbl_message.Text = "Please enter valid  date";
        }


        if (Convert.ToString(txt_FinalAdmit_opening.Text) != "")
        {
            dr["ADMIT_OPENING"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_FinalAdmit_opening.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)); 
            //new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_FinalAdmit_opening"].ToString()));
        }
        if (Convert.ToString(txt_FinalAdmit_closing.Text) != "")
        {
            dr["ADMIT_CLOSING"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_FinalAdmit_closing.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)); 
            //new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_FinalAdmit_closing"].ToString()));
        }
        if (Convert.ToString(txt_MidAdmit_opening.Text) != "")
        {
            dr["MIDAMIT_OPENING"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_MidAdmit_opening.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture)); 
            //new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_MidAdmit_opening"].ToString()));
        }
        if (Convert.ToString(txt_MidAdmit_closing.Text) != "")
        {
            dr["MIDAMIT_CLOSING"] = "" + new cls_tools().get_database_formateDate(DateTime.ParseExact(txt_MidAdmit_closing.Text, "dd-MMM-yyyy", CultureInfo.CurrentCulture));
            //new cls_tools().get_database_formateDate(Convert.ToDateTime("" + Request["ctl00$ContentPlaceHolder_definition$txt_MidAdmit_closing"].ToString()));
        }
        AppSettings.CourseOfferingOpenedDepList = txtOpenedDep.Text;

        ds.Tables["WEB_PRE_OFFERING_DATE"].Rows.Add(dr);

        if (btn_save.Text == "Submit")
        {
            if (new admin_webService().save_pre_course_offeringDate(ds, Session["year"].ToString(), Session["sem"].ToString()) == "1")
            {
                lbl_message.Text = "" + new cls_message().getMessage(2);
                load_date_info();
            }
            else
                lbl_message.Text = "" + new cls_message().getMessage(14);
        }
        else
        {
            if (new admin_webService().Update_pre_course_offeringDate(ds, Session["year"].ToString(), Session["sem"].ToString()) == "1")
            {
                lbl_message.Text = "Date has been Updated (Except Result Publication Date)";
                load_date_info();
            }
            else
                lbl_message.Text = "" + new cls_message().getMessage(14);
        }
       


    }
}
