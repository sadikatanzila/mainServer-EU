<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_libraryClearence.aspx.cs" Inherits="admin_libraryClearence"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Clearence&gt; Library</span></td>
        </tr>
    </table>
    
   <script language="javascript" type="text/javascript">
  function chech_valid()
  {  
      if (document.getElementById("ctl00$ContentPlaceHolder_definition$txt_year").value.toString()=="" )
      {
            alert("Please enter the year");
            return false;
      }
      else
            return true;
  }  
    </script>
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0">
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
                                <b><font  face="Arial" size="2">Accounce Library</font></b></p>
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
                        <td bgcolor="#ffffff" style="vertical-align: top; text-align: left" width="100%">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                            <br />
                                            <table>
                                                <tr>
                                                    <td style="width: 45px; height: auto">
                                                        Select</td>
                                                    <td style="width: auto; height: auto">
                                                        <asp:DropDownList ID="cmb_semester" runat="server">
                                                            <asp:ListItem Value="1">Spring</asp:ListItem>
                                                            <asp:ListItem Value="2">Summer</asp:ListItem>
                                                            <asp:ListItem Value="3">Fall</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    <td style="width: 35px; height: 22px">
                                                        Year</td>
                                                    <td style="width: 49px; height: 22px">
                                                        <asp:TextBox ID="txt_year" runat="server" MaxLength="4" Width="40px"></asp:TextBox></td>
                                                    <td style="width: 102px; height: 22px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 45px; height: auto">
                                                        Department</td>
                                                    <td colspan="4" style="height: auto">
                                                        <asp:DropDownList ID="cmb_department" runat="server">
                                                            <asp:ListItem Value="1">Spring</asp:ListItem>
                                                            <asp:ListItem Value="2">Summer</asp:ListItem>
                                                            <asp:ListItem Value="3">Fall</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 45px; height: auto">
                                                        Batch</td>
                                                    <td colspan="4" style="height: auto">
                                                        <asp:TextBox ID="txt_batch" runat="server" MaxLength="4" Width="40px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 45px; height: auto">
                                                    </td>
                                                    <td colspan="4" style="height: auto">
                                                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" /></td>
                                                </tr>
                                            </table>
                                        </span></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <hr />
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <asp:GridView ID="GridView_student" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%">
                                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_select" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="sid" HeaderText="ID">
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="sname" HeaderText="Name" />
                                                <asp:CheckBoxField DataField="libStatus" HeaderText="Is Cleared">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:CheckBoxField>
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="#330099" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#990000" Font-Bold="True" Font-Size="Small" ForeColor="#FFFFCC" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <asp:Button ID="btn_cleared" runat="server" OnClick="btn_cleared_Click" Text="Cleared" />
                                        <asp:Button ID="btn_noCleared" runat="server" OnClick="btn_noCleared_Click" Text="Not Cleared" /></td>
                                </tr>
                            </table>
                        </td>
                        <td bgcolor="white" style="height: 114px" width="14">
                            &nbsp;
                            <p>
                              </p>
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

