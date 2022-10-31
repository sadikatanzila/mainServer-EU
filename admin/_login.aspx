<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_login.aspx.cs" Inherits="admin_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EU Portal: Admin Login</title>
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
<body>
    <form id="form1" runat="server" style="text-align:center; margin-left:0; margin-top:0; margin:0;">
    <div style="text-align:center">
        <table>
            <tr>
                <td style="background-image: url(images/login_ill.jpg); width:300px; height: 300px; text-align: center; border-right: inactivecaption 20px double; background-position: 60% 5%; border-top: inactivecaption 20px double; border-left: inactivecaption 20px double; border-bottom: inactivecaption 20px double; background-repeat: no-repeat; background-color: window;">
                    &nbsp;<table>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lbl_message" runat="server" ForeColor="Red" Text="Label" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="color: #ffffff" >
                                <strong><span style="color: #ff9966">Login Id</span></strong></td>
                            <td ><asp:TextBox ID="txt_id" runat="server" Width="100px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="color: #ffffff" >
                                <strong><span style="color: #ff9966">Password</span></strong></td>
                            <td ><asp:TextBox ID="txt_pass" runat="server" Width="100px" TextMode="Password"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td ></td>
                            <td style="text-align: left" >
                            <asp:Button ID="btn_submit" runat="server" Text="Login" OnClick="btn_submit_Click" BorderStyle="Dotted" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
      
    
    </div>
    </form>
</body>
</html>

