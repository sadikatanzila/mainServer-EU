/**
 * File name: login.cs
 * Author: Anjan Kumar Paul
 * Date: July 07, 2010
 * 
 * Modification history:
 * Name                                 Date                              Desc
 * Anjan Kumar Paul					July 07, 2010				        Created
 * Version: 1.0
  */


using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EUPortalWeb.CommonClass;
using System.IO;
using System.Threading;
using System.Globalization;

using EUPortal.BAL;
using ARAS.Karnel.Utilities;



namespace EUPortalWeb
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            btnSignin.Attributes.Add("onclick", "return check();");

            if (!IsPostBack)
            {
                CheckCredentials();
            }
        }

        protected void btnSignin_Click(object sender, EventArgs e)
        {
            bool IsExist = false;
            try
            {

                if (ValidateInput())
                {
                    if (SystemUser.CheckCredential(txtUserName.Text.Trim(), txtPassword.Text.Trim()))
                    {
                        //string rememberLogin = string.Format("{0}|{1}", chkUserName.Checked ? SystemUser.CurrentUser.UserLogInID : string.Empty,
                        //    chkPassword.Checked ? SystemUser.CurrentUser.UserPassword : string.Empty);

                        //if (rememberLogin.Length > 0)
                        //{
                        //    FileManager.WriteToFile(string.Format(@"{0}\credential.crd", ThisSystem.DefaultDirectory), rememberLogin, false);
                        //}
                        IsExist = true;
                        Session["IsExist"] = IsExist;

                    }
                    else
                    {
                        MessageBox.Show("Enter a valid user name and or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Can not check user name and password due to :{0}{1}", Environment.NewLine, ex.Message));
            }

            if (IsExist)
            {
                Session["SYSTEMUSERID"] = SystemUser.CurrentUser.ID.ToString();
                Session["USERID"] = SystemUser.CurrentUser.UserLogInID.ToString();
                Session["USERNAME"] = SystemUser.CurrentUser.UserName.ToString();
                Session["PASSWD"] = SystemUser.CurrentUser.UserPassword.ToString();
                Session["USERTYPE"] = SystemUser.CurrentUser.UserType.ToString();
                Session["ISACTIVE"] = SystemUser.CurrentUser.IsActive.ToString();

                Response.Redirect(@"Pages/Home.aspx");
            }
        }

         #region CheckCredentials

        private void CheckCredentials()
        {
            try
            {
                string credentials = FileManager.ReadFromFile(string.Format(@"{0}\credential.crd", ThisSystem.DefaultDirectory));
                string[] values = credentials.Split(new string[] { "|" }, StringSplitOptions.None);

                txtUserName.Text = values[0];
                txtPassword.Text = values[1];
            }
            catch
            {
            }
            finally
            {
                btnSignin.Focus();

                if (txtPassword.Text.Length > 0)
                    chkPassword.Checked = true;
                else
                    txtPassword.Focus();


                if (txtUserName.Text.Length > 0)
                    chkUserName.Checked = true;
                else
                    txtUserName.Focus();
            }
        }

        #endregion

        #region ValidateInput

        private bool ValidateInput()
        {
            if (txtUserName.Text.Trim().Length <= 0)
            {
                MessageBox.Show(txtUserName.Text + ":" + "Please enter login id");
                return false;
            }

            return true;
        }

        #endregion

        
    
    }
}
