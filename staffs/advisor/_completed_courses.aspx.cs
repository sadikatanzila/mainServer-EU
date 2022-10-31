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

public partial class staffs_advisor_completed_courses : System.Web.UI.Page
{
    DataSet ds_data = new DataSet();
    string sid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
      if (Session.Count == 0)
            Response.Redirect("../_login.aspx");
        else if (String.IsNullOrEmpty(Session["user"].ToString()))
        {
            Response.Redirect("../_login.aspx");
        }

        if (Request.QueryString["ids"] != null)
            sid = Request.QueryString["ids"].ToString();
        flash_info();
        load_student_information();
        load_courseInfo();
        load_courseInfo();
    }

    private void flash_info()
    {
        lbl_address.Text = "";
        lbl_admission.Text = "";
        lbl_admissionAs.Text = "";
        lbl_advisor.Text = "";
        lbl_cgpa.Text = "";
        lbl_comments.Text = "";
        lbl_completd_hrs.Text = "";
        lbl_dob.Text = "";
        lbl_email.Text = "";
        lbl_gender.Text = "";
        lbl_id.Text = "";
        lbl_major.Text = "";
        lbl_name.Text = "";
        lbl_phone.Text = "";
        lbl_program.Text = "";
    }

    private void load_student_information()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_a_student_information(sid));
       
        
        foreach (DataRow dr in ds.Tables["student"].Rows)
        {
            if (!String.IsNullOrEmpty(dr["ADDRESS"].ToString()))
                lbl_address.Text = dr["ADDRESS"].ToString();
            else
                lbl_address.Text = "N/A";

            lbl_admission.Text = new cls_tools().get_word_semester(dr["ADMINSEMETER"].ToString()) + ", " + dr["ADMINYEAR"].ToString(); ;

            lbl_admissionAs.Text = dr["STATUS"].ToString();

            if (!String.IsNullOrEmpty(dr["ADVISOR_ID"].ToString()))
                lbl_advisor.Text = dr["STAFF_NAME"].ToString();
            else
                lbl_advisor.Text = "N/A";

            lbl_cgpa.Text = "N/A";

            if (!String.IsNullOrEmpty(dr["COMMENTS"].ToString()))
                lbl_comments.Text = dr["COMMENTS"].ToString();
            else
                lbl_comments.Text = "N/A";

            lbl_completd_hrs.Text = "N/A";

            if (!String.IsNullOrEmpty(dr["DOB"].ToString()))
                lbl_dob.Text = new cls_tools().get_user_formateDate(dr["DOB"].ToString());
            else
                lbl_dob.Text = "N/A";

            if (!String.IsNullOrEmpty(dr["EMAIL"].ToString()))
                lbl_email.Text = dr["EMAIL"].ToString();
            else
                lbl_email.Text = "N/A";

            if (dr["GENDER"].ToString() == "F")
                lbl_gender.Text = "Female";
            else if (dr["GENDER"].ToString() == "M")
                lbl_gender.Text = "Male";

            lbl_id.Text = dr["SID"].ToString();

            if (!String.IsNullOrEmpty(dr["MAJOR"].ToString()))
                lbl_major.Text = dr["MAJOR"].ToString();
            else
                lbl_major.Text = "N/A";

            lbl_name.Text = dr["SNAME"].ToString();

            if (!String.IsNullOrEmpty(dr["PHONE"].ToString()))
                lbl_phone.Text = dr["PHONE"].ToString();
            else
                lbl_phone.Text = "N/A";

            lbl_program.Text = dr["SPROGRAM"].ToString();

            break;
        }
    }

    private void create_dataSet()
    {
        ds_data.Tables.Add("table1");
        ds_data.Tables["table1"].Columns.Add("semester");
        ds_data.Tables["table1"].Columns.Add("code");
        ds_data.Tables["table1"].Columns.Add("name");
        ds_data.Tables["table1"].Columns.Add("is_completed");
    }


    private void load_courseInfo()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_all_course_ofa_Department(sid));
        ds.Merge(new staff_webService().get_completed_course_information(sid));//courseList
        ds.Tables["WEB_DEPARTMENT_COURSES"].Columns.Add("isCompleted");

        foreach (DataRow dr in ds.Tables["WEB_DEPARTMENT_COURSES"].Rows)
        {
            foreach (DataRow drs in ds.Tables["courseList"].Rows)
            {
                dr["isCompleted"] = "0";
                if (dr["COURSECODE"].ToString() == drs["COURSECODE"].ToString())
                {
                    dr["isCompleted"] = "1";
                    break;
                }
            }
        }

        PlaceHolder_001.Controls.Clear();

        Table tbl = new Table();
        //tbl.BorderColor = System.Drawing.Color.Honeydew;
        PlaceHolder_001.Controls.Add(tbl);
        
        TableRow trHeader = new TableRow();
        trHeader.BackColor= System.Drawing.Color.LightBlue;
        tbl.Controls.Add(trHeader);

        TableCell td_semester_header = new TableCell();
        td_semester_header.Text = "Semester";
        td_semester_header.Font.Bold = true;
        trHeader.Controls.Add(td_semester_header);

        TableCell td_code_header = new TableCell();
        td_code_header.Text = "Code";
        td_code_header.Font.Bold = true;
        td_code_header.Width = new Unit(60);
        td_code_header.HorizontalAlign = HorizontalAlign.Center;
        trHeader.Controls.Add(td_code_header);

        TableCell td_name_header = new TableCell();
        td_name_header.Font.Bold = true;
        td_name_header.Text = "Name";
        td_name_header.HorizontalAlign = HorizontalAlign.Center;
        trHeader.Controls.Add(td_name_header);

        TableCell td_completed_header = new TableCell();
        td_completed_header.Font.Bold = true;
        td_completed_header.Text = "Completed";
        trHeader.Controls.Add(td_completed_header);
        
        string str="";
        string str_t = "";
        int count = 0;
        cls_tools obj_tools = new cls_tools();

        for (int i = 0; i < ds.Tables["WEB_DEPARTMENT_COURSES"].Rows.Count;i++ )
        {
            DataRow dr = ds.Tables["WEB_DEPARTMENT_COURSES"].Rows[i];           

            if (str != dr["PROPOSEDSEMESTER"].ToString())
            { 
                str = dr["PROPOSEDSEMESTER"].ToString();
                str_t = str;

                TableRow tr = new TableRow();
                tr.BorderWidth = new Unit(1);
                tbl.Controls.Add(tr);

                if (dr["isCompleted"].ToString() =="1")
                    tr.BackColor = System.Drawing.Color.LightSalmon;
                else if (i % 2 == 0)
                    tr.BackColor = System.Drawing.Color.Honeydew;

                count = 0;
                TableCell td_semester = new TableCell();

                for (int j = i; j < ds.Tables["WEB_DEPARTMENT_COURSES"].Rows.Count && str_t == ds.Tables["WEB_DEPARTMENT_COURSES"].Rows[j]["PROPOSEDSEMESTER"].ToString(); j++)
                {
                    count++;
                }

                td_semester.Text = obj_tools.get_sem_text(dr["PROPOSEDSEMESTER"].ToString());
                td_semester.RowSpan= count;
                td_semester.HorizontalAlign = HorizontalAlign.Center;
                td_semester.BackColor = System.Drawing.Color.LightBlue;
                tr.Controls.Add(td_semester);


                TableCell td_code = new TableCell();
                td_code.Text = dr["COURSECODE"].ToString();

                TableCell td_name = new TableCell();
                td_name.Text = dr["CNAME"].ToString();

                TableCell td_completed = new TableCell();                
                CheckBox chk_completed = new CheckBox();
                chk_completed.Enabled = false;
                if (dr["isCompleted"].ToString() == "1")
                    chk_completed.Checked = true;
                else
                    chk_completed.Checked = false;
                td_completed.HorizontalAlign = HorizontalAlign.Center;
                td_completed.Controls.Add(chk_completed);                

                tr.Controls.Add(td_code);
                tr.Controls.Add(td_name);
                tr.Controls.Add(td_completed);

               // count = 1;
                i++;
                for (; i < ds.Tables["WEB_DEPARTMENT_COURSES"].Rows.Count && str == ds.Tables["WEB_DEPARTMENT_COURSES"].Rows[i]["PROPOSEDSEMESTER"].ToString(); i++)
                {
                    dr = ds.Tables["WEB_DEPARTMENT_COURSES"].Rows[i];
                    count++;
                    TableRow tr1 = new TableRow();
                    tbl.Controls.Add(tr1);

                    if (dr["isCompleted"].ToString() == "1")
                        tr1.BackColor = System.Drawing.Color.LightSalmon;
                    else if (i % 2 == 0)
                        tr1.BackColor = System.Drawing.Color.Honeydew;
                    //else
                     //   tr1.BackColor = System.Drawing.Color.LightBlue;
 

                    TableCell td_code1 = new TableCell();
                    td_code1.Text = dr["COURSECODE"].ToString();

                    TableCell td_name1 = new TableCell();
                    td_name1.Text = dr["CNAME"].ToString();

                    TableCell td_completed1 = new TableCell();
                    CheckBox chk_completed1 = new CheckBox();
                    chk_completed1.Enabled = false;
                    if (dr["isCompleted"].ToString() == "1")
                        chk_completed1.Checked = true;
                    else
                        chk_completed1.Checked = false;
                    td_completed1.HorizontalAlign = HorizontalAlign.Center;
                    td_completed1.Controls.Add(chk_completed1);

                    tr1.Controls.Add(td_code1);
                    tr1.Controls.Add(td_name1);
                    tr1.Controls.Add(td_completed1);
                }
                i--;
               
            }
            
        }
    }

   


}
