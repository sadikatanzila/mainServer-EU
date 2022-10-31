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

public partial class staffs_courses_studentAttendanceP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // load_students();
        gvStdAttendance_DataBinding(sender, e);
    }

    private void load_students()
    {
        DataTable dt = new DataTable();
        dt.Merge(new staff_webService().get_Student_attendance());

        if (dt.Rows.Count > 0)
        {
            //Bind the First GridView with the original data from the DataTable
          //  GridView1.DataSource = dt;
          //  GridView1.DataBind();

            //Pivot the Original data from the DataTable by calling the
            //method PivotTable and pass the dt as the parameter

          //  DataTable pivotedTable = PivotTable(dt);
         //   GridView2.DataSource = pivotedTable;
         //   GridView2.DataBind();
        }

     



      
    }

   
    private DataTable PivotTable(DataTable origTable)
    {
        

        DataTable newTable = new DataTable();
        DataRow dr = null;


        //Add Columns to new Table
        newTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        newTable.Columns.Add(new DataColumn("Col1", typeof(string)));
        newTable.Columns.Add(new DataColumn("Col2", typeof(string)));
        newTable.Columns.Add(new DataColumn("Col3", typeof(string)));
        newTable.Columns.Add(new DataColumn("Col4", typeof(string)));
      //  newTable.Columns.Add(new DataColumn("Col5", typeof(string)));
        dr = newTable.NewRow();
        dr["RowNumber"] = 1;
        dr["Col1"] = string.Empty;
        dr["Col2"] = string.Empty;
        dr["Col3"] = string.Empty;
        dr["Col4"] = string.Empty;
       // dr["Col5"] = string.Empty;
        newTable.Rows.Add(dr);

        for (int i = 0; i <= origTable.Rows.Count; i++)
        {
            newTable.Columns.Add(new DataColumn(origTable.Columns[i].ColumnName, typeof(String)));
        }

        //Execute the Pivot Method
        for (int cols = 0; cols < origTable.Columns.Count; cols++)
        {
            dr = newTable.NewRow();
            for (int rows = 0; rows < origTable.Rows.Count; rows++)
            {
                if (rows < origTable.Columns.Count)
                {
                    dr[0] = origTable.Columns[cols].ColumnName; // Add the Column Name in the first Column
                    dr[rows + 1] = origTable.Rows[rows][cols];
                }
            }
            newTable.Rows.Add(dr); //add the DataRow to the new Table rows collection
        }
        return newTable;
    }
    protected void gvStdAttendance_DataBinding(object sender, EventArgs e)
    {

    
        DataSet dsStd = new DataSet();
        dsStd.Merge(new staff_webService().get_Student_List());

     /*   if (gvStdAttendance.SelectedRow.RowType == DataControlRowType.DataRow)
        {
            
        }*/
        
        



    }
   
}