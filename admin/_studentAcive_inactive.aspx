<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_studentAcive_inactive.aspx.cs" Inherits="admin_studentAcive_inactive"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td class="header" style="font-weight: bold; height: 44px; background-color: #ffffff;
                text-align: center">
                <table border="0" style="text-align: left;width: 100%" >
                    <tr>
                        <td class="header" style=" height: 20px; color: #333333;text-align: center">
                <br />
                <br />
                Student List (active/inactive)
                <hr style="color: #333333" />

            </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td style="width: 45px; height: auto">
                            Select</td>
                        <td style="width: auto; height: auto; text-align: left;">
                            <asp:DropDownList ID="cmb_active_inactive" runat="server">
                                <asp:ListItem Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                            </asp:DropDownList></td>
                        <td colspan="3" style="height: 22px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 45px; height: auto; text-align: left">
                            Select</td>
                        <td style="width: auto; height: auto; text-align: left">
                            <asp:DropDownList ID="cmb_graduate" runat="server">
                                <asp:ListItem Value="1">Graduate</asp:ListItem>
                                <asp:ListItem Value="0">Non Graduate</asp:ListItem>
                            </asp:DropDownList></td>
                        <td colspan="3" style="height: 22px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 45px; height: auto">
                            Department</td>
                        <td colspan="4" style="height: auto; text-align: left">
                            <asp:DropDownList ID="cmb_department" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 45px; height: auto; text-align: left;">
                            Batch</td>
                        <td colspan="4" style="height: auto; text-align: left;">
                            <asp:TextBox ID="txt_batch" runat="server" MaxLength="4" Width="40px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 45px; height: auto">
                        </td>
                        <td colspan="4" style="height: auto; text-align: left;">
                            <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" /></td>
                    </tr>
                </table>
                <hr />
                <asp:Label ID="lbl_message" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="Red"
                    Text="Label"></asp:Label></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0" width="75%">
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
                                <b><font  face="Arial" size="2">Student List</font></b></p>
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
                                        <asp:GridView ID="GridView_studentList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="GridView_studentList_PageIndexChanging"
                                            PageSize="20" Width="80%">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="sid" HeaderText="ID" />
                                                <asp:BoundField DataField="sname" HeaderText="Name">
                                                    <ItemStyle Width="170px" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SPROGRAM" HeaderText="Department">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:CheckBoxField HeaderText="Active" DataField="active" /> 
                                            </Columns>
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Smaller" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                        <br />
                                        <asp:Button ID="btn_active" runat="server" OnClick="btn_active_Click" Text="Set Active"
                                            Width="115px" />
                                        <asp:Button ID="btn_inactive" runat="server" OnClick="btn_inactive_Click" Text="Set Inactive"
                                            Width="115px" /></td>
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

