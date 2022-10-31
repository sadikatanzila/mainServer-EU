<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_StaffactiveInactive.aspx.cs" Inherits="admin_StaffactiveInactive"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table style="width: 95%">
        <tr>
            <td class="header" style=" height: 20px; color: #333333;text-align: center">
                <br />
                <br />
                Staff List (active/inactive)
                <hr style="color: #333333" />

            </td>
        </tr>
        <tr>
            <td class="header" style="font-weight: bold; height: 44px; background-color: #ffffff;
                text-align: left">
                <br />
                <br />
                 
                <table>
                    <tr>
                        <td style="width: 91px">
                            Select</td>
                        <td>
                            <asp:DropDownList ID="cmb_activeInactive" runat="server">
                                <asp:ListItem Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 91px">
                            Department</td>
                        <td>
                              <asp:DropDownList ID="cmb_department" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 91px">
                            Type</td>
                        <td>
                            <asp:DropDownList ID="cmb_type" runat="server">
                                <asp:ListItem>Full</asp:ListItem>
                                <asp:ListItem>Part</asp:ListItem>
                                <asp:ListItem>All</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 91px">
                             </td>
                        <td>
                            <asp:Button ID="btn_show" runat="server" OnClick="btn_submit_Click" Text="Show" />
                            </td>
                    </tr>
                </table>
                <hr />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0"  width="75%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="100%">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                            </td>
                        <td  class="k" height="1" width="100%">
                           </td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            </td>
                    </tr>
                    <tr>
                        <td  class="h" height="22" width="100%">
                            <p align="center">
                                <b><font  face="Arial" size="2">Staff List</font></b></p>
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
                        <td bgcolor="#ffffff" style="vertical-align: top" width="100%">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <br />
                                        <asp:GridView ID="GridView_studentList" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" GridLines="None" Width="80%" AllowPaging="True" OnPageIndexChanging="GridView_studentList_PageIndexChanging" PageSize="20">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:HyperLinkField DataNavigateUrlFields="STAFF_ID" DataNavigateUrlFormatString="~/admin/_add_staff.aspx?ids={0}"
                                                    DataTextField="STAFF_ID" HeaderText="ID" NavigateUrl="~/admin/_add_staff.aspx" />
                                               
											   <asp:BoundField DataField="VALUE" HeaderText="Employee ID">
                                                    <ItemStyle Width="170px" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>

											  <asp:BoundField DataField="STAFF_NAME" HeaderText="Name">
                                                    <ItemStyle Width="170px" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="COLLEGENAME" HeaderText="Department">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField Visible="False">
                                                    
                                                    <ItemTemplate >
                                                        <asp:Label ID="STAFF_ID" runat="server" Text='<%# Bind("STAFF_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Smaller" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                        <br />
                                        <asp:Button ID="btn_active" runat="server" OnClick="btn_active_Click"
                                Text="Set Active" Width="115px" />
                                        <asp:Button ID="btn_inactive" runat="server" OnClick="btn_inactive_Click"
                                Text="Set Inactive" Width="115px" /></td>
                                </tr>
                            </table>
                            &nbsp;</td>
                        <td bgcolor="white" style="height: 114px" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td  class="k" style="height: 114px" width="1">
                           </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                           </td>
                        <td bgcolor="white" height="1" width="100%">
                            </td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            </td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="100%">
                           </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

