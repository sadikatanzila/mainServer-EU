<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_PageAuthorization.aspx.cs" Inherits="employee_PageAuthorization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

     <script  type="text/javascript">
         function SelectAllChkb(headerchk) {
             var grdmovie = document.getElementById('ContentPlaceHolder_tracker_gvPagesList');
             var i;
             if (headerchk.checked) {
                 for (i = 0; i < grdmovie.rows.length; i++) {
                     var inputs = grdmovie.rows[i].getElementsByTagName('input');
                     inputs[0].checked = true;
                 }
             }
             else {
                 for (i = 0; i < grdmovie.rows.length; i++) {
                     var inputs = grdmovie.rows[i].getElementsByTagName('input');
                     inputs[0].checked = false;
                 }
             }
         }
         function CheckChildchkb(gv, Chkitem) {
             var grdmovie = gv.parentNode.parentNode.parentNode;
             var selectAll = grdmovie.rows[0].cells[Chkitem].getElementsByTagName("input")[0];
             if (!gv.checked) {
                 selectAll.checked = false;
             }
             else {
                 var checked = true;
                 for (var i = 1; i < grdmovie.rows.length; i++) {
                     var chb = grdmovie.rows[i].cells[Chkitem].getElementsByTagName("input")[0];
                     if (!chb.checked) {
                         checked = false;
                         break;
                     }
                 }
                 selectAll.checked = checked;
             }
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">

 
     <div>
            <table cellpadding="2" border="0" width="100%">
                <tr>
                    <td align="center" style="font-weight: bold">
                        User Wise Authorization
                        <hr style="color: Green" />
                    </td>
                </tr>
            </table>
         <div>
                &nbsp;</div>
            <div align="center" style="width: 90%">
                <table style="padding-left: 50px;">
                    <tr>
                        <td>
                            <asp:Label ID="lblUserName" runat="server" Text="User Role Name"></asp:Label>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlHR_User" runat="server" CssClass="DropDownList" AutoPostBack="true"
                                 Height="20px" Width="150px" OnSelectedIndexChanged="ddlHR_User_SelectedIndexChanged">
                            </asp:DropDownList>
                           
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="width: 100%">
                &nbsp;<br />
                <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
            </div>
         
              
                   
                    <asp:GridView ID="gvPagesList" runat="server" AutoGenerateColumns="false" BackColor="White" style="margin-left:20%"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="AD_MENUPAGE_ID"
                        EmptyDataText="No Data Found" ShowHeaderWhenEmpty="true"  OnRowDataBound="gvPages_RowDataBound" Width="60%">

                        <Columns>
        <asp:TemplateField HeaderText="CheckAll">
                <HeaderTemplate>
                <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAllChkb(this);" />
                </HeaderTemplate>
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <ItemTemplate>
                    <asp:CheckBox ID="chkBoxPages" runat="server" onclick="CheckChildchkb(this,0)" />
                    </ItemTemplate>
        </asp:TemplateField>
                           

                            <asp:TemplateField  Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubMenuHeadID" runat="server" Text='<%#Bind("AD_MENUPAGE_ID") %>'></asp:Label>
                                </ItemTemplate>
                              
                            </asp:TemplateField>

                            <asp:TemplateField  Visible="False" >
                               
                                <ItemTemplate>
                                    <asp:Label ID="lblMenuHeadID" runat="server" Text='<%#Bind("AD_MENUHEAD_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MENU" >
                                <ItemTemplate>
                                    <asp:Label ID="lblHeads" runat="server" Text='<%#Bind("HEADNAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PAGE NAME"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblPages" runat="server" Text='<%#Bind("PAGENAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
               
          
            <div>
                &nbsp;</div>
            <div align="center" style="width: 100%">
                <asp:Button ID="btnPermission" runat="server" Text="Permission" ValidationGroup="UserPageAuthorizationForm"
                    CssClass="Button" OnClick="btnPermission_Click" />
            </div>
            <asp:ValidationSummary ID="vs" runat="server" CssClass="error" ValidationGroup="UserPageAuthorizationForm"
                ShowMessageBox="true" ForeColor="Red" ShowSummary="false" />
         </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

