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

public partial class student_course_teacherEvaluation : System.Web.UI.Page
{
    RadioButton[][] rBtn;
    string sid = "";
    string course_teacher = "";
    int total_argment = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_message.Text = "";

        try
        {
            if (Session.Count == 0)
                Response.Redirect("../_login.aspx");
            else if (String.IsNullOrEmpty(Session["ctrlId"].ToString()))
            {
                Response.Redirect("../_login.aspx");
            }
            else
            {
                sid = Session["ctrlId"].ToString();

                if (!string.IsNullOrEmpty(Session["evl"].ToString()))
                    course_teacher = Session["evl"].ToString();
            }
        }
        catch (Exception er) { Response.Redirect("../_login.aspx"); }
         
        load_question();
        load_courses();
    }

    private void load_courses()
    {
        DataSet ds = new DataSet();
        ds.Merge(new staff_webService().get_course_teacher_Info(course_teacher ));
  
        foreach (DataRow dr in ds.Tables["course_teacher_Info"].Rows)
        {
            lbl_teacher.Text = dr["staff_name"].ToString();
            lbl_course_name.Text = dr["COURSECODE"].ToString() + ":" + dr["cname"].ToString();
           // lbl_credit_hours.Text = dr["CHOURS"].ToString();
            lbl_section.Text = dr["section"].ToString();
            lbl_semester.Text = new cls_tools().get_word_semester(Session["sem"].ToString()) + " " + Session["year"].ToString();
                break;
        }
    }

    private void load_question()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_all_evaluation_statement());
         rBtn = new RadioButton[ds.Tables["WEB_TEACHER_EVAL_ARGUMENT"].Rows.Count][];
       
        total_argment = ds.Tables["WEB_TEACHER_EVAL_ARGUMENT"].Rows.Count;

        PlaceHolder1.Controls.Clear();

        Table tbl = new Table();
        tbl.Width = new Unit("100%");
        tbl.BorderWidth = new Unit(1);
        tbl.BorderColor = System.Drawing.Color.FromName("#C00000");
        tbl.CellSpacing= 0;

        PlaceHolder1.Controls.Add(tbl);

        //================ Header ----------------------------
        TableRow trh = new TableRow();
        trh.BackColor = System.Drawing.Color.CornflowerBlue;
        trh.Height = new Unit(30);
        tbl.Controls.Add(trh);

        TableCell tdh = new TableCell();
        trh.Controls.Add(tdh);
        tdh.ForeColor = System.Drawing.Color.White;
        tdh.Font.Bold = true;
        tdh.Text = "The Course Teacher";

        TableCell td_optionh = new TableCell();
        td_optionh.Text = "&nbsp;5 &nbsp;&nbsp; 4 &nbsp; 3 &nbsp; 2 &nbsp; 1";
        td_optionh.ForeColor = System.Drawing.Color.White;
        td_optionh.Width = new Unit("23%");
        td_optionh.Font.Bold = true;
        trh.Controls.Add(td_optionh);

        // -----------------  End header -----------------------------
         

        int i = 0;
        foreach (DataRow dr in ds.Tables["WEB_TEACHER_EVAL_ARGUMENT"].Rows)
        {
            TableRow tr = new TableRow();
            if (i % 2 != 0)
            {
                tr.BackColor = System.Drawing.Color.FromName("#ffebf4");
            }
            tbl.Controls.Add(tr);

            TableCell td = new TableCell();
            tr.Controls.Add(td);
            td.Text = ""+(i+1)+". " + dr["ARGUMENT"].ToString();

            TableCell td_option = new TableCell();
            tr.Controls.Add(td_option);

            rBtn[i] = new RadioButton[5];
            for (int count = 0; count < 5; count++)
            {
                rBtn[i][count] = new RadioButton();
                rBtn[i][count].GroupName = "" + i;
                rBtn[i][count].ID = "" + i + "_" + count;
                td_option.Controls.Add(rBtn[i][count]); 
            }
            i++;
        }
    }
     
    protected void btn_submit_Click(object sender, EventArgs e)
    {  int count=0;
        for (int i = 0; i < rBtn.Length; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (rBtn[i][j].Checked == true)
                    count++;
            }
        }

        if (total_argment == count)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add("WEB_TEACHER_EVAL_VALUE");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("COURSE_TEACHER");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("REGKEY");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("DATES");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_1");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_2");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_3");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_4");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_5");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_6");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_7");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_8");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_9");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_10");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_11");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_12");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_13");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_14");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_15");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_16");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_17");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_18");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_19");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_20");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_21");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_22");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_23");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_24");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_25");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_26");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_27");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_28");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_29");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("S_30");
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Columns.Add("ST_COMMENTS");

            DataRow dr = ds.Tables["WEB_TEACHER_EVAL_VALUE"].NewRow();

            dr["COURSE_TEACHER"] = ""+course_teacher;
            dr["REGKEY"] = "" + sid + Session["sem"].ToString() + Session["year"].ToString();
            dr["DATES"] = "" + new cls_tools().get_database_formateDate(DateTime.Today);

            for (int i = 0; i < rBtn.Length; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (rBtn[i][j].Checked == true)
                    {
                        if (!String.IsNullOrEmpty(rBtn[i][j].ID))
                        {
                           dr["S_" + (i + 1)] = (5- Convert.ToInt32("0" + rBtn[i][j].ID.Split('_')[1]) );
                           break;
                        }
                    }
                }
            }

            dr["ST_COMMENTS"] = "" + txt_comments.Text;
            ds.Tables["WEB_TEACHER_EVAL_VALUE"].Rows.Add(dr);

            string status=new student_webService().insert_teacher_evaluation(ds);
            if (status == "1")
            {
                lbl_message.Text = "" + new cls_message().getMessage(2)+", click on Back to return"; 
            }
            else
                lbl_message.Text = "" + new cls_message().getMessage(5); 

        }
        else
        {
            lbl_message.Text = ""+new cls_message().getMessage(22); 
        }
    }
}
