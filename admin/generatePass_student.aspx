<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="generatePass_student.aspx.cs" Inherits="admin_generatePass_student" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table style="width: 95%">
        <tr>
            <td class="header" style="color: #ffa500; height: 20px; background-color: #eef5fa;
                text-align: left">
                Admin&gt;Password Generate for Student</td>
        </tr>
        <tr>
            <td class="header" style="font-weight: bold; height: 44px; background-color: #ffffff;
                text-align: left">
                <table>
                    <tr>
                        <td style="height: 22px; width: 224px;">
                            Batch
                            <asp:TextBox ID="txt_batch" runat="server" MaxLength="4" Width="40px"></asp:TextBox>
                            <asp:Button ID="btn_show" runat="server" OnClick="btn_submit_Click" Text="Show" /></td>
                        <td style="width: 60%; text-align: right;">
                         <asp:Button ID="btn_set_advisor" runat="server" Text="Generate Password"
                                Width="125px" OnClick="btn_set_advisor_Click1" />
                        </td>
                    </tr>
                    
                </table>
                <hr />
                <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="Red"></asp:Label></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table style="width: 95%">
        <tr>
            <td class="header" style="color: #ffa500; height: 20px; background-color: #eef5fa;
                text-align: left">
                <span style="font-size: 12px">Admin&gt;Password Generate for Student</span></td>
        </tr>
        <tr>
            <td class="header" style="font-weight: bold; height: 44px; background-color: #ffffff; text-align: left"> 
            <hr />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="95%">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="sid" >
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Name" DataField="sname" />
                        <asp:BoundField HeaderText="Password" DataField="SPASSWORD" >
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#E3EAEB" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
    &nbsp;
</asp:Content>

