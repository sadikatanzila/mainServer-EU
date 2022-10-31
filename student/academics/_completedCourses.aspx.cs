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

public partial class student_academics_completedCourses : System.Web.UI.Page
{
    String sid = "";
    protected void Page_Load(object sender, EventArgs e)
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
        }

        load_courseInfo();
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
        trHeader.BackColor = System.Drawing.Color.LightBlue;
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

        string str = "";
        string str_t = "";
        int count = 0;
        cls_tools obj_tools = new cls_tools();

        for (int i = 0; i < ds.Tables["WEB_DEPARTMENT_COURSES"].Rows.Count; i++)
        {
            DataRow dr = ds.Tables["WEB_DEPARTMENT_COURSES"].Rows[i];

            if (str != dr["PROPOSEDSEMESTER"].ToString())
            {
                str = dr["PROPOSEDSEMESTER"].ToString();
                str_t = str;

                TableRow tr = new TableRow();
                tr.BorderWidth = new Unit(1);
                tbl.Controls.Add(tr);

                if (dr["isCompleted"].ToString() == "1")
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
                td_semester.RowSpan = count;
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
