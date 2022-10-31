using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Globalization;
using System.Threading;
using System.Net;

public partial class employee_CommonStaffFacuty : System.Web.UI.MasterPage
{
    admin_webService obj_adminWeb = new admin_webService();

    private string userID = "";
    private string userRole = "";
    private string userFullName = "";
    private Int32 MenuHeadID = 0;
    private string SubMenuHeadID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Session["ctrl_admin_Id"].ToString()))
            {
                userID = Session["ctrl_admin_Id"].ToString();
                userRole = Session["ROLE_ID"].ToString();


              //  userFullName = Session["HR_UserName"].ToString();
            }
            else
                Response.Redirect("../employee/_login.aspx");
        }
        catch (Exception exp) { Response.Redirect("../employee/_login.aspx"); }

        
        if (!IsPostBack)
        {
            lblUser.Text = "Logged in as :  " + userID;
            CreateMenuSubMenuData();//PreCreateMenuSubMenuData();
        }

       
       CheckPage_asUser();

    }


    private void CheckPage_asUser()
    {
        DataTable UserData = new DataTable();

        userRole = Session["ROLE_ID"].ToString();

        string sPagePath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oFileInfo = new System.IO.FileInfo(sPagePath);
        string sPageName = oFileInfo.Name;

        string URL = sPageName;

        string folder = System.Web.HttpContext.Current.Request.ApplicationPath;
       
        UserData = obj_adminWeb.GetPage_asUser(userRole, URL);
        if (UserData.Rows.Count <= 0) //This submenu has no leave
        {
            // lblAdmin.Text = "Your User is not permitted for this page.....";
            Response.Redirect("../employee/_login.aspx");

        }
        else
        {
            //   lblAdmin.Text = " ";

        }


    }





  
    public string GetCurrentPageName()
    {
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string path = oInfo.FullName;
        string sRet = oInfo.Name;
        return sRet;
    }

   
    private void PreCreateMenuSubMenuData()
    {

        DataTable dtSubMenu = new DataTable();
        DataTable dtPermittedMenu = new DataTable();
        DataTable dtSub2Menu = null;
        DataSet ds1 = new DataSet();
        DataTable dtMenuHead = new DataTable();
        int tmpSubMenuID = 0, tmpPrevSubMenuID = 0;

       // userID = HRGlobalClass.userID;
        bool gotPermission = false;
        try
        {
            userRole = Session["ROLE_ID"].ToString();

            dtMenuHead = obj_adminWeb.GetDataTableMenuHead_byUserID(userRole);
            StringBuilder tableOutput = new StringBuilder();

            for (int i = 0; i < dtMenuHead.Rows.Count; i++)
            {
                //userID = dtMenuHead.Rows[i]["UserID"].ToString();
                MenuHeadID = Convert.ToInt32(dtMenuHead.Rows[i]["AD_MENUHEAD_ID"].ToString());

                gotPermission = GetPermissionByMenuHead(MenuHeadID);
                //if gotPermission is false then skip the following works else do all
                if (gotPermission == true)
                {
                    tableOutput.Append("<li><a runat='server' href='#'");
                    tableOutput.Append(">" + dtMenuHead.Rows[i]["HEADNAME"].ToString());
                    tableOutput.Append("</a>");
                    tempHtmlTable.Text = tableOutput.ToString();

                    dtSubMenu = obj_adminWeb.GetDataTableHR_MenuPageWeb(MenuHeadID);

                    tableOutput.Append("<ul> ");

                    for (int j = 0; j < dtSubMenu.Rows.Count; j++)
                    {
                        dtSub2Menu = new DataTable();
                        dtSub2Menu = obj_adminWeb.GetDataTableHR_MenuPage_Web(Convert.ToInt32(dtSubMenu.Rows[j]["AD_MENUPAGE_ID"]));

                        if (dtSub2Menu.Rows.Count <= 0) //This submenu has no leave
                        {
                            //This is submenu usder menu
                            //Check permission for this user
                            dtPermittedMenu = obj_adminWeb.GetDataTableMenuSubMenuPermission(Convert.ToInt32(userID),
                                Convert.ToInt32(dtSubMenu.Rows[j]["AD_MENUHEAD_ID"]), Convert.ToInt32(dtSubMenu.Rows[j]["AD_MENUPAGE_ID"]));
                            if (dtPermittedMenu.Rows.Count > 0)
                            {
                                //tableOutput.Append(" href=../" +webroot + dtSubMenu.Rows[j]["Url"].ToString());
                                tableOutput.Append("<li><a  runat='server' ID=" + dtSubMenu.Rows[j]["AD_MENUPAGE_ID"].ToString());
                                tableOutput.Append(" href=../" + dtSubMenu.Rows[j]["PathURL"].ToString());
                                tableOutput.Append(" >" + dtSubMenu.Rows[j]["PAGENAME"].ToString());
                                tableOutput.Append("</a></li>");
                            }
                            dtPermittedMenu = null;
                        }
                        else
                        {

                            //if (Convert.ToInt32(dtSubMenu.Rows[j]["AD_MENUPAGE_ID"]) == 2078)
                            //{
                            //    int ss = 0;
                            //    ss = 1;
                            //}

                            //Check Permission Here
                            gotPermission = false;
                            gotPermission = GetPermissionByMenuPage(Convert.ToInt32(dtSubMenu.Rows[j]["AD_MENUPAGE_ID"]));
                            if (gotPermission == true)
                            {
                                tableOutput.Append("<li><a runat='server' href='#'");
                                tableOutput.Append(">" + dtSubMenu.Rows[j]["PAGENAME"].ToString());
                                tableOutput.Append("</a>");
                                tempHtmlTable.Text = tableOutput.ToString();

                                tableOutput.Append("<ul> ");

                                for (int ssmrc = 0; ssmrc < dtSub2Menu.Rows.Count; ssmrc++)
                                {
                                    //Check permission for this user
                                    dtPermittedMenu = obj_adminWeb.GetDataTableMenuSubMenuPermission(Convert.ToInt32(userRole),
                                        Convert.ToInt32(dtSub2Menu.Rows[ssmrc]["AD_MENUHEAD_ID"]), Convert.ToInt32(dtSub2Menu.Rows[ssmrc]["AD_MENUPAGE_ID"]));

                                    if (dtPermittedMenu.Rows.Count > 0)
                                    {
                                        tableOutput.Append("<li><a  runat='server' ID=" + dtSub2Menu.Rows[ssmrc]["AD_MENUPAGE_ID"].ToString());
                                        tableOutput.Append(" href=../" + dtSub2Menu.Rows[ssmrc]["PathURL"].ToString());
                                        tableOutput.Append(" >" + dtSub2Menu.Rows[ssmrc]["PAGENAME"].ToString());
                                        tableOutput.Append("</a></li>");
                                    }
                                    dtPermittedMenu = null;

                                }//End of Inner For Loop
                                tableOutput.Append("</ul></li>");
                            }//End if Statement: Permission

                        }
                        tmpPrevSubMenuID = tmpSubMenuID;
                        tmpSubMenuID = 0;
                    }//End of For Loop
                    tableOutput.Append("</ul></li>");
                    dtSubMenu.Clear();

                }//End if statement: Permission

            }//End of Menu Head
            tempHtmlTable.Text = tableOutput.ToString();

        }
        catch (Exception ex)
        {
            Response.Redirect("_login.aspx");
        }
    }

    private void CreateMenuSubMenuData()
    {

        DataTable dtSubMenu = new DataTable();
        DataTable dtPermittedMenu = new DataTable();
        DataTable dtSub2Menu = null;
        DataSet ds1 = new DataSet();
        DataTable dtMenuHead = new DataTable();
        int tmpSubMenuID = 0, tmpPrevSubMenuID = 0;

        // userID = HRGlobalClass.userID;
        bool gotPermission = false;
        try
        {
            userRole = Session["ROLE_ID"].ToString();

            dtMenuHead = obj_adminWeb.GetDataTableMenuHead_byUserID(userRole);
            StringBuilder tableOutput = new StringBuilder();

            for (int i = 0; i < dtMenuHead.Rows.Count; i++)
            {
                //userID = dtMenuHead.Rows[i]["UserID"].ToString();
                MenuHeadID = Convert.ToInt32(dtMenuHead.Rows[i]["AD_MENUHEAD_ID"].ToString());

                gotPermission = GetPermissionByMenuHead(MenuHeadID);
                //if gotPermission is false then skip the following works else do all
                if (gotPermission == true)
                {
                    tableOutput.Append("<li><a runat='server' href='#'");
                    tableOutput.Append(">" + dtMenuHead.Rows[i]["HEADNAME"].ToString());
                    tableOutput.Append("</a>");
                    tempHtmlTable.Text = tableOutput.ToString();

                    dtSubMenu = obj_adminWeb.GetDataTableHR_MenuPageWeb(MenuHeadID);

                    tableOutput.Append("<ul> ");

                    for (int j = 0; j < dtSubMenu.Rows.Count; j++)
                    {
                        dtSub2Menu = new DataTable();
                        dtSub2Menu = obj_adminWeb.GetDataTableHR_MenuPage_Web(Convert.ToInt32(dtSubMenu.Rows[j]["AD_MENUPAGE_ID"]));

                        if (dtSub2Menu.Rows.Count <= 0) //This submenu has no leave
                        {
                            //This is submenu usder menu
                            //Check permission for this user
                            dtPermittedMenu = obj_adminWeb.GetDataTableMenuSubMenuPermission(Convert.ToInt32(userID),
                                Convert.ToInt32(dtSubMenu.Rows[j]["AD_MENUHEAD_ID"]), Convert.ToInt32(dtSubMenu.Rows[j]["AD_MENUPAGE_ID"]));
                            if (dtPermittedMenu.Rows.Count > 0)
                            {
                                //tableOutput.Append(" href=../" +webroot + dtSubMenu.Rows[j]["Url"].ToString());
                                tableOutput.Append("<li><a  runat='server' ID=" + dtSubMenu.Rows[j]["AD_MENUPAGE_ID"].ToString());
                                tableOutput.Append(" href=../" + dtSubMenu.Rows[j]["PathURL"].ToString());
                                tableOutput.Append(" >" + dtSubMenu.Rows[j]["PAGENAME"].ToString());
                                tableOutput.Append("</a></li>");
                            }
                            dtPermittedMenu = null;
                        }
                        else
                        {

                            //if (Convert.ToInt32(dtSubMenu.Rows[j]["AD_MENUPAGE_ID"]) == 2078)
                            //{
                            //    int ss = 0;
                            //    ss = 1;
                            //}

                            //Check Permission Here
                            gotPermission = false;
                            gotPermission = GetPermissionByMenuPage(Convert.ToInt32(dtSubMenu.Rows[j]["AD_MENUPAGE_ID"]));
                            if (gotPermission == true)
                            {
                                /*  tableOutput.Append("<li><a runat='server' href='#'");
                                  tableOutput.Append(">" + dtSubMenu.Rows[j]["PAGENAME"].ToString());
                                  tableOutput.Append("</a>");
                                  tempHtmlTable.Text = tableOutput.ToString();
                                 
                                tableOutput.Append("<ul> ");
                                 */
                                for (int ssmrc = 0; ssmrc < dtSub2Menu.Rows.Count; ssmrc++)
                                {
                                    //Check permission for this user
                                    dtPermittedMenu = obj_adminWeb.GetDataTableMenuSubMenuPermission(Convert.ToInt32(userRole),
                                        Convert.ToInt32(dtSub2Menu.Rows[ssmrc]["AD_MENUHEAD_ID"]), Convert.ToInt32(dtSub2Menu.Rows[ssmrc]["AD_MENUPAGE_ID"]));

                                    if (dtPermittedMenu.Rows.Count > 0)
                                    {
                                        tableOutput.Append("<li><a  runat='server' ID=" + dtSub2Menu.Rows[ssmrc]["AD_MENUPAGE_ID"].ToString());
                                        tableOutput.Append(" href=../" + dtSub2Menu.Rows[ssmrc]["PathURL"].ToString());
                                        tableOutput.Append(" >" + dtSub2Menu.Rows[ssmrc]["PAGENAME"].ToString());
                                        tableOutput.Append("</a></li>");
                                    }
                                    dtPermittedMenu = null;

                                }//End of Inner For Loop
                               // tableOutput.Append("</ul></li>");
                            }//End if Statement: Permission

                        }
                        tmpPrevSubMenuID = tmpSubMenuID;
                        tmpSubMenuID = 0;
                    }//End of For Loop
                    tableOutput.Append("</ul></li>");
                    dtSubMenu.Clear();

                }//End if statement: Permission

            }//End of Menu Head
            tempHtmlTable.Text = tableOutput.ToString();

        }
        catch (Exception ex)
        {
            Response.Redirect("_login.aspx");
        }
    }
    private bool GetPermissionByMenuHead(int pMenuHead)
    {
        DataTable dtsubMenuHead = new DataTable();
        DataTable dtPermission = null;
        bool permittedItem = false;
        string HeadUrl = "";

        try
        {
            //dtsubMenuHead = GetDataTableHR_MenuPageWeb("HR_MenuPageWeb_GetAllByMenuHeadID", MenuHeadID);
            dtsubMenuHead = obj_adminWeb.GetDataTableHR_MenuPageWeb(pMenuHead);
            if (dtsubMenuHead.Rows.Count <= 0)
            {
                dtsubMenuHead = null;
                return permittedItem;
            }

            for (int smri = 0; smri < dtsubMenuHead.Rows.Count; smri++)
            {
                HeadUrl = "";
                HeadUrl = Convert.ToString(dtsubMenuHead.Rows[smri]["URL"]);

                if (HeadUrl != "#")
                {
                    dtPermission = new DataTable();
                    //Have to send parameter userID also
                    dtPermission = obj_adminWeb.HR_MenuPermissionWeb_GetByPageId(Convert.ToInt32(dtsubMenuHead.Rows[smri]["AD_MENUPAGE_ID"]));
                    if (dtPermission.Rows.Count > 0)
                    {
                        permittedItem = true;
                        break;
                    }
                }
                else
                { 

                   permittedItem = GetPermissionBySubMenuHead(Convert.ToInt32(dtsubMenuHead.Rows[smri]["AD_MENUHEAD_ID"]), Convert.ToInt32(dtsubMenuHead.Rows[smri]["AD_MENUPAGE_ID"]));
                    if (permittedItem == true)
                    {
                        break;
                    }
                }

            }

            return permittedItem;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool GetPermissionByMenuPage(int pMenuPage)
    {
        DataTable dtsubMenuHead = new DataTable();
        DataTable dtPermission = null;
        bool permittedItem = false;
        string HeadUrl = "";

        try
        {
            //dtsubMenuHead = GetDataTableHR_MenuPageWeb("HR_MenuPageWeb_GetAllByMenuHeadID", MenuHeadID);
            dtsubMenuHead = obj_adminWeb.GetDataTableHR_MenuPage(pMenuPage);
            if (dtsubMenuHead.Rows.Count <= 0)
            {
                dtsubMenuHead = null;
                return permittedItem;
            }

            for (int smri = 0; smri < dtsubMenuHead.Rows.Count; smri++)
            {
                HeadUrl = "";
                HeadUrl = Convert.ToString(dtsubMenuHead.Rows[smri]["URL"]);

                if (HeadUrl != "#")
                {

                    dtPermission = new DataTable();
                    //Have to send parameter userID also
                    dtPermission = obj_adminWeb.HR_MenuPermissionWeb_GetByPageId(Convert.ToInt32(dtsubMenuHead.Rows[smri]["AD_MENUPAGE_ID"]));
                    if (dtPermission.Rows.Count > 0)
                    {
                        permittedItem = true;
                        break;
                    }
                }
                else
                { //For '#'

                    //int ui = Convert.ToInt32(dtsubMenuHead.Rows[smri]["AD_MENUPAGE_ID"]);
                    //if (Convert.ToInt32(dtsubMenuHead.Rows[smri]["AD_MENUPAGE_ID"]) == 2078)
                    //{
                    //    ui = ui;
                    //}

                    permittedItem = GetPermissionBySubMenuHead(Convert.ToInt32(dtsubMenuHead.Rows[smri]["AD_MENUHEAD_ID"]), Convert.ToInt32(dtsubMenuHead.Rows[smri]["AD_MENUPAGE_ID"]));
                    if (permittedItem == true)
                    {
                        break;
                    }
                }

            }

            return permittedItem;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool GetPermissionBySubMenuHead(int pMenuHeadID, int pPageId)
    {

        DataTable dtsubMenuHead = new DataTable();
        DataTable dtPermission = new DataTable();
        if (pPageId == 2078)
        {
            int si = 0;
        }
        bool permittedItem = false;
        try
        {
            //Search By SubMenu as Menu. Because It becomes menu now
            dtsubMenuHead = obj_adminWeb.GetDataTableHR_MenuPageWeb(pMenuHeadID);
            //dtsubMenuHead = obj_adminWeb.GetDataTableHR_MenuPageWeb(pSubMenuHeadID);
            if (dtsubMenuHead.Rows.Count <= 0)
            {
                //Check Permission By Root Page
                dtPermission = obj_adminWeb.HR_MenuPermissionWeb_GetByPageId(pPageId);
                if (dtPermission.Rows.Count > 0)
                {
                    permittedItem = true;
                }
            }
            else
            {
                for (int smri = 0; smri < dtsubMenuHead.Rows.Count; smri++)
                {
                    if (dtsubMenuHead.Rows[smri]["URL"] != "#")
                    {
                        //Search By PageId at the Permission Table
                        dtPermission = obj_adminWeb.HR_MenuPermissionWeb_GetByPageId(Convert.ToInt32(dtsubMenuHead.Rows[smri]["AD_MENUPAGE_ID"]));
                        if (dtPermission.Rows.Count > 0)
                        {
                            permittedItem = true;
                            break;
                        }
                    }//End of Loop
                }//End For Loop
            }//End If
            return permittedItem;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

  
}
