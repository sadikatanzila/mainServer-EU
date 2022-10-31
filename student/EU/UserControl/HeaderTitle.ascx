<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderTitle.ascx.cs"
    Inherits="EUPortalWeb.UserControl.HeaderTitle1" %>
<table style="width: 100%;" cellpadding="0" cellspacing="0" border="0" class="header-thickLine">
    <tr >
        <td style="width: 70%;" valign="bottom">
        
            <asp:Label ID="lblProjectName" CssClass="ProjectName" runat="server" 
                Text="" EnableTheming="False"></asp:Label>
                
                <%--<asp:Label ID="Label1" CssClass="ProjectName" runat="server" 
                Text="" EnableTheming="False"></asp:Label>--%>
                
            <%--<asp:Label ID="lblProjectVersion"           CssClass="ProjectVersion" 
                runat="server" EnableTheming="False"></asp:Label>--%>
        </td>
        <td style="width: 30%; text-align: right;" >
            <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/TopLogo.png" />--%>
            <%--<asp:Image ID="Image1" runat="server" ImageUrl="#" />--%>
            </td>
    </tr>
<%--    <tr><td colspan="2" >
        
    </td></tr>
--%>    
</table>    
