using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;

public partial class employee_PageAuthorization : System.Web.UI.Page
{
    #region Fields

    admin_webService obj_adminWeb = new admin_webService();
    private string HR_UserID = "";
    private string HR_UserPassword = "";
    DataTable dtMenuPermission = null;
    string code = "";
    #endregion

    #region Methods

    private void Save()
    {
        string TRANID = "";

        DataTable dtHR_User = new DataTable();
        DataTable dtGotPermission = new DataTable();
        int BranchId = 0;

       


       
            for (int head = 0; head < gvPagesList.Rows.Count; head++)
            {
                int pageID = Convert.ToInt32(gvPagesList.DataKeys[head].Value);
                int menuHeadID = Convert.ToInt32(((Label)gvPagesList.Rows[head].FindControl("lblMenuHeadID")).Text);
                int submenuHeadID = Convert.ToInt32(((Label)gvPagesList.Rows[head].FindControl("lblSubMenuHeadID")).Text);
                string user = Convert.ToString(ddlHR_User.SelectedValue);


                if (((CheckBox)gvPagesList.Rows[head].FindControl("chkBoxPages")).Checked)
                {
                    dtGotPermission = obj_adminWeb.GetPermission_Controls_DataTable_Update(Convert.ToInt32(gvPagesList.DataKeys[head].Value), Convert.ToString(ddlHR_User.SelectedValue));
                    if (dtGotPermission.Rows.Count <= 0)
                    {
                        //Add new record
                        DataSet Pageds = new DataSet();
                        Pageds.Tables.Add("AD_MENUPERMISSION");
                        Pageds.Tables["AD_MENUPERMISSION"].Columns.Add("AD_ROLE_ID");
                        Pageds.Tables["AD_MENUPERMISSION"].Columns.Add("AD_MENUPAGE_ID");
                        Pageds.Tables["AD_MENUPERMISSION"].Columns.Add("ISACTIVE");
                        

                        GridViewRow row = gvPagesList.Rows[head];
                        DataRow drn = Pageds.Tables["AD_MENUPERMISSION"].NewRow();
                        drn["AD_ROLE_ID"] = "" + Convert.ToString(ddlHR_User.SelectedValue);
                        drn["AD_MENUPAGE_ID"] = "" + Convert.ToString(((Label)row.FindControl("lblSubMenuHeadID")).Text);
                        drn["ISACTIVE"] = "Y";
                        Pageds.Tables["AD_MENUPERMISSION"].Rows.Add(drn);
                       // TRANID = new admin_webService().insert_PageAuthentication(Pageds);
                        TRANID = new admin_webService().MenuPermission_Add(Pageds);
                    }
                    else
                    {
                        //Update on existing record
                        dtGotPermission = obj_adminWeb.GetPermission_Controls_DataTable_Update(Convert.ToInt32(gvPagesList.DataKeys[head].Value), Convert.ToString(ddlHR_User.SelectedValue));
                        if (dtGotPermission.Rows.Count > 0)
                        {
                            DataSet Pageds = new DataSet();
                            Pageds.Tables.Add("AD_MENUPERMISSION");
                            Pageds.Tables["AD_MENUPERMISSION"].Columns.Add("AD_ROLE_ID");
                            Pageds.Tables["AD_MENUPERMISSION"].Columns.Add("AD_MENUPAGE_ID");
                            Pageds.Tables["AD_MENUPERMISSION"].Columns.Add("ISACTIVE");

                            GridViewRow row = gvPagesList.Rows[head];
                            DataRow drn = Pageds.Tables["AD_MENUPERMISSION"].NewRow();
                            drn["AD_ROLE_ID"] = "" + Convert.ToString(ddlHR_User.SelectedValue);
                            drn["AD_MENUPAGE_ID"] = "" + Convert.ToString(((Label)row.FindControl("lblSubMenuHeadID")).Text);
                            drn["ISACTIVE"] = "Y";
                            Pageds.Tables["AD_MENUPERMISSION"].Rows.Add(drn);
                            TRANID = new admin_webService().MenuPermission_Update(Pageds);
                        }
                    }
                  
                }
                else
                {
                    dtGotPermission = obj_adminWeb.GetPermission_Controls_DataTable_Update(Convert.ToInt32(gvPagesList.DataKeys[head].Value), Convert.ToString(ddlHR_User.SelectedValue));
                    if (dtGotPermission.Rows.Count > 0)
                    {
                        DataSet Pageds = new DataSet();
                        Pageds.Tables.Add("AD_MENUPERMISSION");
                        Pageds.Tables["AD_MENUPERMISSION"].Columns.Add("AD_ROLE_ID");
                        Pageds.Tables["AD_MENUPERMISSION"].Columns.Add("AD_MENUPAGE_ID");
                        Pageds.Tables["AD_MENUPERMISSION"].Columns.Add("ISACTIVE");

                        GridViewRow row = gvPagesList.Rows[head];
                        DataRow drn = Pageds.Tables["AD_MENUPERMISSION"].NewRow();
                        drn["AD_ROLE_ID"] = "" + Convert.ToString(ddlHR_User.SelectedValue);
                        drn["AD_MENUPAGE_ID"] = "" + Convert.ToString(((Label)row.FindControl("lblSubMenuHeadID")).Text);
                        drn["ISACTIVE"] = "N";
                        Pageds.Tables["AD_MENUPERMISSION"].Rows.Add(drn);
                        TRANID = new admin_webService().MenuPermission_Update(Pageds);
                    }

                }
                

            }

            if (TRANID == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Menu permission has not been Updated.');", true);
               
               // lblMessage.Text = "Menu permission has not been Updated";
            }
            else
            if (Convert.ToInt32(TRANID) > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Menu permission has been given.');", true);
                code = "";
                BindRole();
                BindMenuHead();
               
            } 

      
    }
    
    private void BindMenuHead()
    {
        try
        {

            dtMenuPermission = obj_adminWeb.CreatePagelist();
            gvPagesList.DataSource = dtMenuPermission;
            gvPagesList.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindRole()
    {
        try
        {
            DataSet ds = new DataSet();
            ds.Merge(new admin_webService().BindRole());
            ddlHR_User.DataSource = ds.Tables["AllRoleList"];
            ddlHR_User.DataTextField = "ROLE_NAME";
            ddlHR_User.DataValueField = "ID";
            ddlHR_User.DataBind();
            if (code == "")
            {
              
            }
            else
                ddlHR_User.SelectedValue = code;
            //ddlHR_User.SelectedValue = code;

           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindMenuHeadHR_User(string UserID)
    {

        //List<MenuHead> listHeader = objMenuHeadBLL.MenuHead_GetAll_HR_User(HR_UserId);
        //gvPagesList.DataSource = listHeader;
        //gvPagesList.DataBind();

    }

    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
       /* try
        {
            HR_UserID = Session["HR_UserID"].ToString();
            HR_UserPassword = Session["Password"].ToString();
        }
        catch
        {
            Response.Redirect("~/HR_Login.aspx");
        }*/

        if (!Page.IsPostBack)
        {
            BindMenuHead();
            BindRole();
            ddlHR_User_SelectedIndexChanged(sender, e);
            //CommonBinder.LoadDDLHR_User(ddlHR_User);

        }
        else
        {
            code = ddlHR_User.SelectedValue.ToString();
        }
    }

  

    protected void gvPagesList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            // when mouse is over the row, save original color to new attribute, and change it to highlight color
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#98F5FF'");

            // when mouse leaves the row, change the bg color to its original value  
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");


        }
    }

    protected void gvPagesList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        BindMenuHead();
    }

    protected void gvPages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            //when mouse is over the row, save original color to new attribute, and change it to highlight color
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#0AD4CD'");

            // when mouse leaves the row, change the bg color to its original value  
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

        }
    }

    protected void btnPermission_Click(object sender, EventArgs e)
    {
        int isChecked = 0;

        if (ddlHR_User.SelectedIndex < 0)
        {
            //lblMessage.Text = "Sorry! please select HR_User.";
            //lblMessage.ForeColor = Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", " alert('Sorry! Please select User Role.');", true);

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please select HR_User.');", true);

            return;
        }

        if (gvPagesList.Rows.Count > 0)
        {
            Save();
        }
    }

    protected void ddlHR_User_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(ddlHR_User.SelectedValue) != "-- Please Select --")
        {
            try
            {
                DataTable dtGotPermission = null;
                DataTable dtHR_User = null;
                int BranchId = 0;

                if (ddlHR_User.SelectedIndex < 0)
                {
                    BindMenuHead();
                }
                else
                {

                    if (gvPagesList.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvPagesList.Rows.Count; i++)
                        {
                           
                            dtGotPermission = new DataTable();
                            dtGotPermission = obj_adminWeb.GetPermission_Controls_DataTable(Convert.ToInt32(gvPagesList.DataKeys[i].Value), Convert.ToString(ddlHR_User.SelectedValue));

                            if (dtGotPermission.Rows.Count > 0)
                            {
                                ((CheckBox)gvPagesList.Rows[i].FindControl("chkBoxPages")).Checked = true;
                            }
                            else
                            {
                                ((CheckBox)gvPagesList.Rows[i].FindControl("chkBoxPages")).Checked = false;
                            }
                            dtGotPermission = null;
                            dtHR_User = null;
                        }

                     //  btnPermission.Text = "Permission Update";
                    }//End of if statement
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        else
        {
            lblMessage.Text = "Please Select the fixed User.";
        }








    }
    #endregion
  
}