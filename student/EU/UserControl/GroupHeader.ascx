<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupHeader.ascx.cs" Inherits="EUPortalWeb.UserControl.GroupHeader" %>
<link href="../App_Themes/Default/Style.css" rel="stylesheet" type="text/css" />
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="width:15px;">
            <asp:Image ID="imgGroupHeader" runat="server" ImageUrl="~/Images/GroupPoint.png" /></td>
        <td class="groupHeaderText" >
            <asp:Label ID="lblGroupHeader" runat="server" Text="Group Header"></asp:Label></td>
    </tr>
</table>