<%@ Page Language="C#" MasterPageFile="~/student/academics/academics_masterPage.master" AutoEventWireup="true" CodeFile="_academicCalenderPrev.aspx.cs" Inherits="student_academics_academicCalenderPrev"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Academics&gt;Academic Calender</span></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table bgcolor="#ffffff" border="0" cellpadding="1" cellspacing="1" width="95%">
        <tr>
            <td style="height: 14px; text-align: left">
                <asp:Label ID="lbl_title" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Crimson"
                    Text="Label"></asp:Label></td>
        </tr>
        <tr style="font-size: 8pt; color: #ff0066; ">
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
            <td style="height: 14px; text-align: left;">
                <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td style="height: 14px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 14px; text-align: left;">
                *<em><span style="color: #ff0066">Subject to appearance of the moon</span></em></td>
        </tr>
        <tr>
            <td style="height: 14px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 14px; text-align: left;">
                <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

