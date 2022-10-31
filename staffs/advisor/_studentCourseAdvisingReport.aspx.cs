using System;using System.Data;public partial class staffs_advisor_studentCourseAdvisingReport : System.Web.UI.Page{    protected void Page_Load(object sender, EventArgs e)    {        try        {            if (Session.Count == 0)                Response.Redirect("_login.aspx");            else if (String.IsNullOrEmpty(Session["user"].ToString()))            {                Response.Redirect("_login.aspx");            }        }        catch (Exception erp) { Response.Redirect("_login.aspx"); }		lblTitle.Text = Session["P_Title"].ToString();        lblStudentId.Text = Session["P_StudentId"].ToString();        lblStudentName.Text = Session["P_StudentName"].ToString();        lblDepartment.Text = Session["P_StudentDepartment"].ToString();        lblSemester.Text = Session["P_Semester"].ToString();        lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");        lblAdvisorId.Text = Session["P_AdvisorId"].ToString();        lblAdvisorName.Text = Session["P_AdvisorName"].ToString();        double totalCredit = 0.0;        foreach (DataRow dr in (Session["StudentTakenCourses"] as DataSet).Tables[0].Rows)        {            totalCredit += Convert.ToDouble(dr["CHOURS"]);        }        lblTotalCredit.Text = totalCredit.ToString();        gvStudentCourse.DataSource = Session["StudentTakenCourses"];        gvStudentCourse.DataBind();				string accSem = "", accYear = "";        accYear = lblSemester.Text.Split(',')[1].Trim();        accSem = lblSemester.Text.Split(',')[0].Trim();        accSem = (accSem == "Spring" ? "1" : (accSem == "Summer" ? "2" : "3"));		//lblSemesterText.Text = lblSemester.Text;

        string P_Year = "", prevSem = "", R_Sem = "", R_Year = "";
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_ADMINREGISTRATIONRATE_LYS());

        if (ds.Tables["Studentlist"].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables["Studentlist"].Rows)
            {
                prevSem = dr["semester"].ToString();
                P_Year = dr["year"].ToString();
            }
        }


        string DUE = "", SemDue = "", graceAmt = "";
        DataSet InsDate_ds = new DataSet();
        InsDate_ds.Merge(new student_webService().get_FN_GET_PER_SEM_DUE(Session["P_StudentId"].ToString(), P_Year, prevSem));
        if (InsDate_ds.Tables["SEM_DUE"].Rows.Count > 0)
        {
            foreach (DataRow InsDate_dr in InsDate_ds.Tables["SEM_DUE"].Rows)
            {
                DUE = Convert.ToString(InsDate_dr["DUE"]);

                string[] code = DUE.Split('|'); //Request.QueryString["DUE"].ToString().Split('|');
                if (code.Length > 0)
                {
                    SemDue = code[0];
                    graceAmt = code[1];

                }

            }

        }        //lblSemesterDue.Text = new student_webService().GetTakenCourseAccountBalance(lblStudentId.Text, accSem, accYear).ToString();
        lblTotalDue.Text = SemDue ;    }}