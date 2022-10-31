<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_ConvoAddConvoDouble.aspx.cs"
 Inherits="admin_ConvoAddConvoDouble" Title="Extra Added Student in Convocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
<div style="text-align:left">
<table >
    <tr>
        <td colspan="2">
            <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="#0000CC"></asp:Label></td>
    </tr>
    <tr>
        <td>
        </td>
        <td style="width: 275px">
        </td>
    </tr>
    <tr>
        <td>
            Year :</td>
        <td style="width: 275px">
  <asp:TextBox ID="txtYear" runat="server">2019</asp:TextBox>
        </td>
    </tr>
<tr>

<td>
    <asp:Label ID="lblStdID" runat="server" Text="Student ID :"></asp:Label>

</td>

<td style="width: 275px">
  <asp:TextBox ID="txtStudentID" runat="server" Width="250px"></asp:TextBox>
</td>
</tr>
    <tr>
        <td>
        </td>
        <td style="width: 275px">
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 22px">
            <asp:Button ID="btnAdd" runat="server" Text="Add New Student" OnClick="btnAdd_Click" /> &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Button ID="btnClear" runat="server" Text="View All" OnClick="btnClear_Click" /></td>
    </tr>

</table>
<div style="width:80%; text-align: left; margin-left:25px">
    <asp:Label ID="Label1" runat="server" Text="Newly Added Student List in Convocation"></asp:Label>
<br />

<asp:GridView ID="GridView_student" runat="server" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="4" Width="60%" OnRowDataBound="GridView_student_RowDataBound">
                                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                            <Columns>
                                                 <asp:TemplateField HeaderText="Sl" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblSerial" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" />
        </asp:TemplateField>  
                                                <asp:BoundField DataField="SID" HeaderText="Student ID" />
                                                <asp:BoundField DataField="SNAME" HeaderText="Name" />
                                                  <asp:BoundField DataField="CONVOCATION_YEAR" HeaderText="Convo Year" />
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="#330099" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#666666" Font-Bold="True" Font-Size="Small" ForeColor="#FFFFCC" />
                                        </asp:GridView>
</div>

</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

