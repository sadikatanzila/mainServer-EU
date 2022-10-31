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

public partial class student_classRoutine : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        generate_classRoutine();
    }

    private void generate_classRoutine()
    {
        DataSet ds = new DataSet();
        ds.Merge(new student_webService().get_al_routine_time("04140000712008"));
        ds.Tables.Add("routineTime");
        ds.Tables["routineTime"].Columns.Add("times");
        
        foreach (DataRow dr in ds.Tables["routineTime_sch"].Rows)
        {
            if (!String.IsNullOrEmpty(dr["SCH_CLS_1"].ToString()))
            {
                DataRow drn=ds.Tables["routineTime"].NewRow();
                drn["times"] = dr["SCH_CLS_1"].ToString().Split()[1] + " " + dr["SCH_CLS_1"].ToString().Split()[2];
                ds.Tables["routineTime"].Rows.Add(drn);
            }
              if (!String.IsNullOrEmpty(dr["SCH_CLS_2"].ToString()))
            {
                DataRow drn = ds.Tables["routineTime"].NewRow();
                drn["times"] = dr["SCH_CLS_2"].ToString().Split()[1] + " " + dr["SCH_CLS_2"].ToString().Split()[2];
                ds.Tables["routineTime"].Rows.Add(drn);            
            }
              if (!String.IsNullOrEmpty(dr["TUT_CLS_1"].ToString()))
            {
                DataRow drn = ds.Tables["routineTime"].NewRow();
                drn["times"] = dr["TUT_CLS_1"].ToString().Split()[1] + " " + dr["TUT_CLS_1"].ToString().Split()[2];
                ds.Tables["routineTime"].Rows.Add(drn);
            }
             if (!String.IsNullOrEmpty(dr["TUT_CLS_2"].ToString()))
            {
                DataRow drn = ds.Tables["routineTime"].NewRow();
                drn["times"] = dr["TUT_CLS_2"].ToString().Split()[1] + " " + dr["TUT_CLS_2"].ToString().Split()[2];
                ds.Tables["routineTime"].Rows.Add(drn);
            } 
        }

        for (int i = 0; i < ds.Tables["routineTime"].Rows.Count; i++)
            for (int j = i + 1; j < ds.Tables["routineTime"].Rows.Count; j++)
                if (ds.Tables["routineTime"].Rows[i]["times"].ToString() == ds.Tables["routineTime"].Rows[j]["times"].ToString())
                    ds.Tables["routineTime"].Rows.RemoveAt(j--);

        /* ---  Table  for generate --------------------*/
        
        PlaceHolder1.Controls.Clear();

        Table tbl = new Table();
        tbl.CellSpacing = 0;
        tbl.CellPadding = 0;
        PlaceHolder1.Controls.Add(tbl);

        TableRow trH = new TableRow();
        trH.BorderWidth = new Unit(1);
        trH.BorderColor = System.Drawing.Color.AntiqueWhite;
        tbl.Controls.Add(trH);

        TableCell tdD = new TableCell();
        tdD.Text = " &nbsp; Days  &nbsp; ";
        tdD.BorderWidth = new Unit(1);
        tdD.BorderColor = System.Drawing.Color.AntiqueWhite;
        trH.Controls.Add(tdD);

        foreach (DataRow dr in ds.Tables["routineTime"].Rows)
        {
            TableCell tdH = new TableCell();
            tdH.Text = " &nbsp; " + dr["times"].ToString().Split(' ')[0] + " &nbsp; ";
            tdH.BorderWidth = new Unit(1);
            tdH.BorderColor = System.Drawing.Color.AntiqueWhite;
            trH.Controls.Add(tdH);
        }

        ds.Merge(new student_webService().get_singlDay_routine_time("04140000712008", "Sat"));

        //foreach (DataRow dr in ds.Tables["routineTime"].Rows)
        //{
        //    foreach (DataRow drD in ds.Tables["routineTime_days"].Rows)
        //    { 
        //        if(!String.IsNullOrEmpty(dr["times"].ToString()) && !String.IsNullOrEmpty(dr["SCH_CLS_1"].ToString()) )
        //            if(dr["times"].ToString().Split(' ')[0]==drD["SCH_CLS_1"].ToString().Split(' ')[1])

        //    }
        //}

    }
}
