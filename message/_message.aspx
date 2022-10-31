<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_message.aspx.cs" Inherits="student_message_message" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EU Portal</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width:80%; height:auto">
            <tr>
                <td>
                    &nbsp;<img src="../../student/images/banner.jpg" /></td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:749px">
                        <tr>
                            <td style=" vertical-align:top"><strong>Message</strong></td>
                            <td style=""></td>
                            <td style=""><asp:Label ID="lbl_title" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="vertical-align:top; width:100px"><strong>Publish date</strong></td>
                            <td style=""></td>
                            <td style=""><asp:Label ID="lbl_pub_date" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="vertical-align:top"><strong>Message Details</strong></td>
                            <td style=""></td>
                            <td style=""><asp:Label ID="lbl_description" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
