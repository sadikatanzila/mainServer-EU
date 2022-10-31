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

public partial class admin_course_teacherEvalDetails : System.Web.UI.Page
{
    string courseTeacherID = "";
    string user = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if((!String.IsNullOrEmpty(Session["ChkEv_deptid"].ToString())))
            { 
                   user = Session["ChkEv_deptid"].ToString();
            }
            else

                Response.Redirect("_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("_login.aspx"); }

        if (string.IsNullOrEmpty(Convert.ToString(Session["courseTeacherID"])))
            Response.Redirect("../_login.aspx");
        else
            courseTeacherID = Convert.ToString(Session["courseTeacherID"]);
        //if (!String.IsNullOrEmpty(Request.QueryString["ids"]))
        //{
        //    courseTeacherID = Request.QueryString["ids"].ToString();
        //}

        //courseTeacherID = "C_T_0000000011";
        get_evaluation_details();
    }

    private void get_evaluation_details()
    {
        DataSet ds = new DataSet();
        ds.Merge(new admin_webService().get_course_teacher_Eval_Details(courseTeacherID));
         
        if(ds.Tables["course_teacher_eval_details"].Rows.Count==0)
            return;

        int column_count=0;

        for (int i = 3; i < 33; i++)
        {
            if (ds.Tables["course_teacher_eval_details"].Rows[0][i].ToString() != "0")
                column_count++;
        }
       
        Table tbl = new Table();
        PlaceHolder1.Controls.Clear();
        PlaceHolder1.Controls.Add(tbl);

        /*------------- Course settings-----------------------------*/ 
        TableRow trCour = new TableRow(); 
        tbl.Controls.Add(trCour);

        TableCell tdCourse = new TableCell();
        tdCourse.ColumnSpan = column_count + 2;
        tdCourse.Text = "Course    : " + ds.Tables["course_teacher_eval_details"].Rows[0]["coursecode"].ToString() + ":" + ds.Tables["course_teacher_eval_details"].Rows[0]["cName"].ToString();
        tdCourse.Font.Bold = true;
        trCour.Controls.Add(tdCourse);

        TableRow trteacher = new TableRow();
        tbl.Controls.Add(trteacher);

        TableCell tdteacher = new TableCell();
        tdteacher.ColumnSpan = column_count + 2;
        tdteacher.Text = "Teacher: " + ds.Tables["course_teacher_eval_details"].Rows[0]["staff_name"].ToString() + " (" + ds.Tables["course_teacher_eval_details"].Rows[0]["teacher_id"].ToString()+")";
        tdteacher.Font.Bold = true;
        trteacher.Controls.Add(tdteacher);

        TableRow trSec = new TableRow();
        tbl.Controls.Add(trSec);

        TableCell tdSec = new TableCell();
        tdSec.ColumnSpan = column_count + 2;
        tdSec.Text = "Section : " + ds.Tables["course_teacher_eval_details"].Rows[0]["section"].ToString();
        tdSec.Font.Bold = true;
        trSec.Controls.Add(tdSec);

        TableRow trbl1= new TableRow();
        trbl1.Height = new Unit(30);
        tbl.Controls.Add(trbl1);

        /*------------- Value Setting-----------------------------*/ 
        TableRow trH = new TableRow();
        trH.BackColor = System.Drawing.Color.CornflowerBlue;
        tbl.Controls.Add(trH);

        TableCell tdH = new TableCell();
        tdH.Text = "Sl no.";
        tdH.ForeColor = System.Drawing.Color.White;
        tdH.Font.Bold = true;
        trH.Controls.Add(tdH);

        for (int i = 0; i < column_count; i++)
        {
            TableCell td = new TableCell();
            td.Font.Bold = true;
            td.ForeColor = System.Drawing.Color.White;
            td.Text = "Q-" + (i + 1);
            trH.Controls.Add(td); 
        }

        TableCell tdt = new TableCell();
        tdt.ForeColor = System.Drawing.Color.White;
        tdt.Font.Bold = true;
        tdt.Text = "Total";
        trH.Controls.Add(tdt); 

        int c=0;
        int valSum = 0;
        foreach(DataRow dr in ds.Tables["course_teacher_eval_details"].Rows)
        {
            valSum = 0;

            TableRow tr = new TableRow();
            tbl.Controls.Add(tr);

            TableCell tdC = new TableCell();
            tdC.BorderColor = System.Drawing.Color.CornflowerBlue;
            tdC.HorizontalAlign = HorizontalAlign.Center;
            tdC.BorderWidth = new Unit(1);
            tdC.BackColor = System.Drawing.Color.AntiqueWhite;
            tdC.Text = ""+(c+1);
            tr.Controls.Add(tdC);

            for (int i = 0; i < column_count; i++)
            {
                TableCell tdv = new TableCell();
                tdv.BorderColor = System.Drawing.Color.CornflowerBlue;
                tdv.BorderWidth = new Unit(1);
                tdv.HorizontalAlign = HorizontalAlign.Center;
                tdv.Text = dr["S_" + (i + 1)].ToString();
                tr.Controls.Add(tdv);
                valSum += Convert.ToInt32("0" + dr["S_" + (i + 1)].ToString());
            }

            TableCell tdvt = new TableCell();
            tdvt.BorderColor = System.Drawing.Color.CornflowerBlue;
            tdvt.BackColor= System.Drawing.Color.Azure;
            tdvt.Font.Bold = true;
            tdvt.BorderWidth = new Unit(1);
            tdvt.HorizontalAlign = HorizontalAlign.Center;
            tdvt.Text = "" + valSum; 
            tr.Controls.Add(tdvt);

            c++;
        }

        /*------------- Commenets Setting-----------------------------*/ 
        TableRow trBl = new TableRow();
        trBl.Height = new Unit(30);
        tbl.Controls.Add(trBl);

        TableRow trComment_h = new TableRow();
        tbl.Controls.Add(trComment_h);

        TableCell tdCommnet_h = new TableCell();
        tdCommnet_h.Text = "Student commnets";
        tdCommnet_h.Font.Bold = true;
        tdCommnet_h.ColumnSpan = column_count + 2;
        trComment_h.Controls.Add(tdCommnet_h);

        foreach (DataRow dr in ds.Tables["course_teacher_eval_details"].Rows)
        {
            if (dr["ST_COMMENTS"].ToString() != "")
            {
                TableRow trComment= new TableRow();
                tbl.Controls.Add(trComment);

                TableCell tdCommnet = new TableCell();
                tdCommnet.Text = dr["ST_COMMENTS"].ToString();
                tdCommnet.BorderWidth = new Unit(1);
                tdCommnet.BorderColor = System.Drawing.Color.AntiqueWhite;
                tdCommnet.ColumnSpan = column_count + 2;
                trComment.Controls.Add(tdCommnet);
            }
        }


    }

}
