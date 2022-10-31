using System;

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

        }
        lblTotalDue.Text = SemDue ;