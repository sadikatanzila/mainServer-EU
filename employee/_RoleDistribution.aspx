<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_RoleDistribution.aspx.cs" Inherits="employee_RoleDistribution" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <table border="0"  width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="80%">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                           </td>
                        <td  class="k" height="1" width="80%">
                           </td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                           </td>
                    </tr>
                    <tr>
                        <td  class="h" height="22" width="80%">
                            <p align="center">
                                <b><font  face="Arial" size="2">Role Distribution</font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="100%">
                            </td>
                    </tr>
                    <tr>
                        <td  class="k" style="height: 114px" width="1">
                            </td>
                        <td bgcolor="white" style="height: 114px" width="18">
                           </td>
                        <td bgcolor="#ffffff" style="vertical-align: top" width="80%">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <table id="Table1" runat="server" width="100%">
                                            <tr>
                                                <td>
                                                       <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text=""></asp:Label></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td>
                                                    Employee ID: &nbsp;&nbsp;<asp:DropDownList ID="cmb_EmpID" runat="server"  AutoPostBack="true"
                                                        onselectedindexchanged="cmb_EmpID_SelectedIndexChanged">
                                                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                                    &nbsp;&nbsp;<asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td>
                                                   &nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="cmbRole" runat="server" Visible="false">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td>
													<asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" ></asp:Label></td>
                                            </tr>
                                            
                                            <tr style="font-size: 8pt; color: #000000; text-align: center;">
                                                <td>
                                                    <asp:Button ID="btn_Submit" runat="server" Text="Submit" Width="150px" OnClick="btn_Submit_Click" Visible="false" />
													</td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td>
                                                   
<asp:GridView ID="gvPagesList" runat="server" AutoGenerateColumns="false" BackColor="White" style="margin-left:20%"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="ID"
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
                           

                            <asp:TemplateField Visible="false" >
                                <ItemTemplate>
                                    <asp:Label ID="lblRoleID" runat="server" Text='<%#Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                              
                            </asp:TemplateField>

                           

                            <asp:TemplateField HeaderText="ROLE" >
                                <ItemTemplate>
                                    <asp:Label ID="lblRole" runat="server" Text='<%#Bind("ROLE_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblDescrip" runat="server" Text='<%#Bind("DESCRIPTION") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>

<br /><br />
 <div align="center" style="width: 100%">
                <asp:Button ID="btnPermission" runat="server" Text="Permission" ValidationGroup="UserPageAuthorizationForm"
                    CssClass="Button" OnClick="btnPermission_Click" />
            </div>




                                                </td>
                                            </tr>
                                        </table>
                                        <br />
          
                                        
                                        
                                        
                                    </td>
                                </tr>
                            </table>
                            &nbsp;</td>
                        <td bgcolor="white" style="height: 114px" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../admin/images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td  class="k" style="height: 114px" width="1">
                            <img border="0" height="1" src="../admin/images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                            <img border="0" height="15" src="../admin/images/blcurv.gif" width="19" /></td>
                        <td bgcolor="white" height="1" width="80%">
                            <img border="0" height="14" src="../admin/images/spacer(1).gif" width="1" /></td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            <img border="0" height="15" src="../admin/images/brcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="80%">
                            <img border="0" height="1" src="../admin/images/spacer(1).gif" width="150" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

