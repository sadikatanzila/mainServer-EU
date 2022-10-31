using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Services;

public partial class employee_ddlJquery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindCountries();
    }

    public void BindCountries()
    {
        DataTable ds = new DataTable();
        if (Session["DEPTCODE"].ToString() != "")
        {
            ds.Merge(new student_webService().get_ProgramListDept(Session["DEPTCODE"].ToString(), "ProgramList"));
        }
        else
        {
            ds.Merge(new student_webService().get_DeparmentList("ProgramList"));
        }



        DataRow dr = ds.NewRow();
        dr["PROGRAM"] = "Select";
        dr["C_PROGINDEPT_ID"] = "0";
        ds.Rows.Add(dr);
        ddlcountries.DataSource = ds;
        ddlcountries.DataTextField = "COLLEGENAME";
        ddlcountries.DataValueField = "COLLEGECODE";
        ddlcountries.DataBind();
    }

    /*
     public void BindCountries()
{
String strQuery = "select CountryID,CountryName from Country";
using (SqlConnection con = new SqlConnection(strcon))
{
using (SqlCommand cmd = new SqlCommand())
{
cmd.CommandType = CommandType.Text;
cmd.CommandText = strQuery;
cmd.Connection = con;
con.Open();
ddlcountries.DataSource = cmd.ExecuteReader();
ddlcountries.DataTextField = "CountryName";
ddlcountries.DataValueField = "CountryID";
ddlcountries.DataBind();
ddlcountries.Items.Insert(0, new ListItem("Select Country", "0"));
con.Close();
}
}
}
    



    [WebMethod]
    public static string BindStates(string country)
    {
        StringWriter builder = new StringWriter();
        String strQuery = "select StateID,StateName from State where CountryID=@CountryID";
        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.Parameters.AddWithValue("@countryid", country);
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
            }
        }
        DataTable dt = ds.Tables[0];
        builder.WriteLine("[");
        if (dt.Rows.Count > 0)
        {
            builder.WriteLine("{\"optionDisplay\":\"Select State\",");
            builder.WriteLine("\"optionValue\":\"0\"},");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                builder.WriteLine("{\"optionDisplay\":\"" + dt.Rows[i]["StateName"] + "\",");
                builder.WriteLine("\"optionValue\":\"" + dt.Rows[i]["StateID"] + "\"},");
            }
        }
        else
        {
            builder.WriteLine("{\"optionDisplay\":\"Select State\",");
            builder.WriteLine("\"optionValue\":\"0\"},");
        }
        string returnjson = builder.ToString().Substring(0, builder.ToString().Length - 3);
        returnjson = returnjson + "]";
        return returnjson.Replace("\r", "").Replace("\n", "");
    }

    [WebMethod]
    public static string BindRegion(string state)
    {
        StringWriter builder = new StringWriter();
        String strQuery = "select RegionID, RegionName from Region where StateID=@StateID";
        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.Parameters.AddWithValue("@StateID", state);
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
            }
        }
        DataTable dt = ds.Tables[0];
        builder.WriteLine("[");
        if (dt.Rows.Count > 0)
        {
            builder.WriteLine("{\"optionDisplay\":\"Select Region\",");
            builder.WriteLine("\"optionValue\":\"0\"},");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                builder.WriteLine("{\"optionDisplay\":\"" + dt.Rows[i]["RegionName"] + "\",");
                builder.WriteLine("\"optionValue\":\"" + dt.Rows[i]["RegionID"] + "\"},");
            }
        }
        else
        {
            builder.WriteLine("{\"optionDisplay\":\"Select Region\",");
            builder.WriteLine("\"optionValue\":\"0\"},");
        }
        string returnjson = builder.ToString().Substring(0, builder.ToString().Length - 3);
        returnjson = returnjson + "]";
        return returnjson.Replace("\r", "").Replace("\n", "");
    }
     */
}