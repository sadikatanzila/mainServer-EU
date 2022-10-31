<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_academic_calender.aspx.cs" Inherits="staffs_academic_calender" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Eastern University | Academic Calender </title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table bgcolor="#ffffff" border="0" cellpadding="1" cellspacing="1" width="95%">
            <tr>
                <td style="height: 14px; text-align: center">
                    <img src="../images1/EUMainlogo.jpg" /></td>
            </tr>
            <tr>
                <td style="height: 14px; text-align: left">
                    <asp:Label ID="lbl_title" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Crimson"
                        Text="Label"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr >
                <td style="height: 14px; text-align: justify">
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
            <tr>
                <td style="height: 14px">
                </td>
            </tr>
            <tr>
                <td style="height: 14px; text-align: left">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Crimson"
                        Text="List of Holidays"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 14px; text-align: left">
                    <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
            <tr>
                <td style="height: 14px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="height: 14px; text-align: left">
                    *<em><span style="color: #ff0066">Subject to appearance of the moon</span></em></td>
            </tr>
            <tr>
                <td style="height: 14px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="height: 14px; text-align: left">
                    <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
