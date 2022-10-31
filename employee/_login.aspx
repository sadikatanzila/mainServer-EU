<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_login.aspx.cs" Inherits="staffs_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EU Portal: Staff Login</title>
    <link href="../App_themes/jind.css" rel="stylesheet" type="text/css" />
    
     <script language="javascript" type="text/javascript">
  
  function check_valid()
  {
 
  
      if (document.getElementById("txt_id").value.toString()=="" )
      {
            alert("Please enter the user id");
            return false;
      }
      else if (document.getElementById("txt_pass").value.toString()=="" )
      {
            alert("Please enter the password");
            return false;
      }
      else
            return true;
            
   }  
  
  </script> 
    
</head>
<body style="padding-top:100px; margin-left: 200px;text-align:center;  background-color:#B3CBDD";>
    <form id="form1" runat="server" style="text-align:center; margin-left:0; margin-top:0; margin:0;">
        <table border="0" cellpadding="0" cellspacing="0" align="center">
            <tr>
                <td align="middle" valign="center" >
                    <table border="0" cellpadding="1" cellspacing="1" width="400" style="border: medium groove #0000FF; background-image: url(../images/MasterBG_HomePage.jpg);">
                        <tr>
                            <td align="middle" align="top">
                                <img src="../images/BannerX.jpg" /></td>
                        </tr>
                        <tr>
                            <td align="middle"  valign="top">
                                <br />
                                <table border="0" cellpadding="0" cellspacing="0" width="170" align="center">
                                    <tr>
                                        <td align="left" valign="center">
                                            <table border="0" cellpadding="0" cellspacing="1" width="100%">
                                            <div style="font-size:medium;font-weight:bold; color:#0000FF;">Employee Login</div>
                                                <tr>
                                                    <td align="left" height="15" valign="bottom">
                                                        <asp:Label ID="lbl_message" runat="server" Font-Bold="True" ForeColor="Red" Text="Label"></asp:Label></td>
                                                </tr>
                                                <tr style="font-size: 7pt; color: #0000FF;">
                                                    <td align="left" height="15" valign="bottom">
                                                        <font color="black" face="verdana" size="1">
                                                            <strong>Employee ID</strong></font></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" height="20" valign="center">
                                                        <input id="txt_id" runat="server" class="inputlogin" name="login_username" size="23"
                                                            tabindex="1" />
                                                    </td>
                                                </tr>
                                                </table>

                                            <asp:Panel ID="pnlPassword" runat="server">
                                             <table border="0" cellpadding="0" cellspacing="1" width="100%">

                                                <tr>
                                                    <td align="left" height="15" valign="bottom">
                                                        <font color="black" face="verdana" size="1"><strong>
                                                            Password</strong> </font></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" height="20" valign="center">
                                                        <input id="txt_pass" runat="server" class="inputlogin" name="secretkey" size="23"
                                                            tabindex='\"2\"' type="password" />
                    &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" height="25" valign="center">
                                                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" /></td>
                                                </tr>
                                                 </table>
                                                     </asp:Panel>
                                                 <table border="0" cellpadding="0" cellspacing="1" width="100%">
                                                <tr>
                                                    <td>
                                                        
                                                     <asp:Label ID="Label1" runat="server" Visible="false" Text="Choose Role:"></asp:Label>     <asp:DropDownList ID="cmb_employee" runat="server" Height="16px" Width="120px" Visible="false">
                                                    </asp:DropDownList>
                                                        <br />
                                                        <asp:Button ID="btn_submitN" runat="server" OnClick="btn_submitN_Click" Text="OK" Visible="false" Height="29px" Width="56px" />
                                                    </td>

                                                </tr>
                                            </table>
                                       

                                        </td>
                                    </tr>
                                </table>
                                <p>
                                    <br />
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td align="middle" bgcolor="#4276E3" valign="top">
                                <a  style="color:White;font-weight: bold;" href="http://www.easternuni.edu.bd">Home</a></td>
                        </tr>
                        <tr>
                            <td align="middle" bgcolor="#4276E3" valign="top">
                                <%--<img src="" style="width: 100%" width="0" />--%></td><%--../images1/login-Bott.gif--%>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
