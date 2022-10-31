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

public partial class admin_course_teacherEvalList : System.Web.UI.Page
{

    admin_webService obj_admin = new admin_webService();
    string user = "";
    string dep = "";

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

        if (IsPostBack)
        {
            dep = cmb_faculty.SelectedValue.ToString();
        }

        if (!IsPostBack) load_faculty();
    }

    private void load_faculty()
    {
        DataSet ds = new DataSet();
        ds.Merge(obj_admin.get_allCollege());

        cmb_faculty.DataSource=ds.Tables["COLLEGE"];
        cmb_faculty.DataTextField = "COLLEGENAME";
        cmb_faculty.DataValueField = "COLLEGECODE";
        cmb_faculty.DataBind();

        if (!string.IsNullOrEmpty(dep))
            cmb_faculty.SelectedValue = dep;
		
		cmb_faculty_SelectedIndexChanged(null, null);
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Visible = false;
            load_evaluation();
        }
        catch (Exception ex)
        {
            lblError.Visible = true;
            lblError.Text = ex.ToString();
        }
    }

    private void load_evaluation()
    {
        DataSet ds = new DataSet();
        if (cmbTeacher.SelectedIndex > 0)
        {
            ds.Merge(obj_admin.get_course_teacher_list(cmb_faculty.SelectedValue.ToString(), cmbTeacher.SelectedValue.ToString(), cmb_s_semester.SelectedValue.ToString(), txt_s_year.Text.Trim()));
        }
        else
        {
            ds.Merge(obj_admin.get_course_teacher_list(cmb_faculty.SelectedValue.ToString(), cmb_s_semester.SelectedValue.ToString(), txt_s_year.Text.Trim()));
        }

        Table tbl = new Table();
        tbl.Width=new Unit("100%");
        PlaceHolder1.Controls.Clear();
        PlaceHolder1.Controls.Add(tbl);

        TableRow trH = new TableRow();
        trH.BackColor = System.Drawing.Color.CornflowerBlue;
        tbl.Controls.Add(trH);

        TableCell tdT = new TableCell();
        tdT.Text = "Teacher";
        tdT.HorizontalAlign = HorizontalAlign.Center;
        tdT.Width = new Unit("30%");
        trH.Controls.Add(tdT);

        TableCell tdC = new TableCell();
        tdC.Text = "Courses";
        tdC.HorizontalAlign = HorizontalAlign.Center;
        trH.Width = new Unit("25%");
        trH.Controls.Add(tdC); 

        TableCell tdQ = new TableCell();
        tdQ.Text = "Evaluated";
        tdQ.HorizontalAlign = HorizontalAlign.Center;
        trH.Controls.Add(tdQ);

        TableCell tdCV = new TableCell();
        tdCV.Text = "Course Eval";
        tdCV.HorizontalAlign = HorizontalAlign.Center;
        trH.Controls.Add(tdCV);


        TableCell tdCVG = new TableCell();
        tdCVG.Text = "Final Eval";
        trH.Controls.Add(tdCVG);
        string staff = "";
        int count = 0;
        int i = 0;
        int j = 0;
        bool flag = false;
        Label[] txt_final = new Label[ds.Tables["course_teacher"].Rows.Count];
        double val = 0;
        double val_count = 0;

        double total_arg = new admin_webService().get_all_evaluation_statement().Rows.Count;


        foreach (DataRow dr in ds.Tables["course_teacher"].Rows)
        {
            TableRow tr = new TableRow();
            if (i % 2 == 0)            
                tr.BackColor = System.Drawing.Color.AntiqueWhite;
                   
            tbl.Controls.Add(tr);

            flag = false;
            if (staff != dr["teacher_id"].ToString())
            {
                staff = dr["teacher_id"].ToString();
                flag = true;
                count=0;
                foreach(DataRow drc in ds.Tables["course_teacher"].Rows)
                    if(dr["teacher_id"].ToString()==drc["teacher_id"].ToString())
                        count++;

                if (i != 0)
                {
                    if (val_count == 0)
                        txt_final[j++].Text = "0";
                    else 
                        txt_final[j++].Text=""+ Math.Round(Convert.ToDouble(val / val_count), 2);
                    val = 0;
                    val_count = 0;
                }

                TableCell td = new TableCell();
                td.RowSpan = count;
                td.Text = dr["staff_name"].ToString();
                td.BackColor = System.Drawing.Color.AntiqueWhite;
                tr.Controls.Add(td);
            }

            TableCell tdCname = new TableCell();
            tdCname.Text = dr["courseCode"].ToString() + " (" + dr["section"] + ")";
            tdCname.ForeColor = System.Drawing.Color.Red;
            tdCname.Attributes.Add("onClick", "goto_eval_details('" + dr["COURSE_TEACHER_ID"].ToString() + "');");
            tr.Controls.Add(tdCname);

            ds.Merge(obj_admin.get_course_teacher_Eval_list(cmb_s_semester.SelectedValue.ToString(), txt_s_year.Text.Trim()));
           
            TableCell tdCQT = new TableCell();
            tdCQT.HorizontalAlign = HorizontalAlign.Center;
            tdCQT.Text = "0 of " + dr["total_student"];

            TableCell tdCEvl = new TableCell();
            tdCEvl.HorizontalAlign = HorizontalAlign.Center;
            tdCEvl.Text = "0";

            foreach (DataRow drq in ds.Tables["course_teacher_eval"].Rows)
            {
                if (drq["course_teacher"].ToString() == dr["course_teacher_id"].ToString())
                {
                    tdCQT.Text = drq["eval_qty"].ToString()+" of "+ dr["total_student"];
                    if (drq["eval_qty"].ToString() != "")
                    {
                        val += Convert.ToDouble((Convert.ToDouble("0" + drq["eval_total"].ToString()) / total_arg) / Convert.ToDouble("0" + drq["eval_qty"].ToString()));
                        tdCEvl.Text = "" + Math.Round(Convert.ToDouble((Convert.ToDouble("0" + drq["eval_total"].ToString()) / total_arg) / Convert.ToDouble("0" + drq["eval_qty"].ToString())), 2);
                       
                        val_count++;
                    }
                }
            }            
            tr.Controls.Add(tdCQT);
            tr.Controls.Add(tdCEvl);

            if (flag ==true)
            {
                TableCell tdFinalEvl = new TableCell();
                tdFinalEvl.RowSpan = count;
                tdFinalEvl.BackColor = System.Drawing.Color.AntiqueWhite;
                tdFinalEvl.HorizontalAlign = HorizontalAlign.Center;
                tr.Controls.Add(tdFinalEvl);

                txt_final[j] = new Label();
                tdFinalEvl.Controls.Add(txt_final[j]);
            }

            i++;     
        }

        
            if (txt_final.Length > 0)
        {
            if (val_count == 0)
                txt_final[j].Text = "0";
            else
                txt_final[j].Text = "" + Math.Round(Convert.ToDouble("0" + (val / val_count)), 2);
        }
            val = 0;
            val_count = 0;
         
    }
	
	protected void cmb_faculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_faculty.SelectedIndex > -1)
        {
            DataSet ds = new DataSet();

            ds.Merge(obj_admin.get_allAdvisor(cmb_faculty.SelectedValue));

            cmbTeacher.DataSource = ds.Tables["advisorList"];
            cmbTeacher.DataTextField = "STAFF_NAME";
            cmbTeacher.DataValueField = "STAFF_ID";
            cmbTeacher.DataBind();

            cmbTeacher.Items.Insert(0, " ");
        }
    }

}
