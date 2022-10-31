<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="EUPortalWeb.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EU Portal - Login</title>
    
    <script type="text/javascript" language="Javascript"> 
    function check() 
    {
        try
        {
            if (document.getElementById('txtUserName').value =='')
            {
                alert ('User name cannot be null')
                return false
            }
            else if (document.getElementById('txtPassword').value =='')
            {
                alert ('Password cannot be null')
                return false
            }
        }
        catch(e)
        {
        
        }
        return true
          
    }
    
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<br />
        <br />
        <br />
        <br />
        <div style="text-align: center">
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Panel ID="Panel1" runat="server" Height="325px"
                            Width="589px">
                            <br />
                            <div style="text-align: center">
                                <table style="width: 100%;background:#3A4A5A;">
                                    <tr>
                                        <td style="width: 139px">
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="X-Large" ForeColor="LightGray"
                                                Text="EU Web Portal" Font-Bold="True" Width="500px"></asp:Label>
                                        </td>
                                        <td style="width: 100px">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <br />
                            <table style="width: 589px; border:1px solid; background:#FFFFF0;">
                                <tr>
                                    <td align="center">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Medium"
                                                        ForeColor="Gold" Text="User Name" Width="124px"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtUserName" TabIndex="1" Width="120px" runat="server" ToolTip="User Id of the user"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Medium"
                                                        ForeColor="Gold" Text="Password"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtPassword" Width="120px" TabIndex="2" runat="server" TextMode="Password"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px">
                                                    <asp:CheckBox ID="chkUserName" runat="server" Text="Remember Login ID" Width="150px" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkPassword" runat="server" Text="Remember Password" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: right">
                                                    
                                                    <asp:Button ID="btnSignin" runat="server" BackColor="Gold" TabIndex="3" Font-Names="Verdana"
                                                        OnClick="btnSignin_Click" Text="Sign in" BorderStyle="None" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Crimson" Font-Size="Small"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
<%--
    <script type="text/javascript">
        window.onload = function() {
            document.getElementById("txtUserName").focus();
        }
    </script>--%>
    </form>
</body>
</html>
