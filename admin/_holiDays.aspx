<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_holiDays.aspx.cs" Inherits="admin_holiDays"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">

<script language="javascript" type="text/javascript">

function loadCalender_stOpening()
{
    window.open('html/CalenderST_opening.htm','','titlebar=no,toolbar=no,scrollbars=false,resizable=false,height=170,width=300');
    return false;
}

function loadCalender_stClosing()
{
    window.open('html/CalenderST_closing.htm','','titlebar=no,toolbar=no,scrollbars=false,resizable=false,height=170,width=300');
    return false;
}
 
  
  
   function chech_valid_data()
  {  
    if (document.getElementById("ctl00_ContentPlaceHolder_definition_txt_year").value.toString()=="" )
      {
            alert("Please enter the year");
            return false;
      }
      else  if (isNaN(document.getElementById("ctl00_ContentPlaceHolder_definition_txt_year").value.toString()))
      {
            alert("Please enter a valid year");
            return false;
      }
       else if (document.getElementById("ctl00_ContentPlaceHolder_definition_txt_program").value.toString()=="" )
      {
            alert("Please enter student opening date.");
            return false;
      }      
      else if (document.getElementById("ctl00_ContentPlaceHolder_definition_txt_student_opening").value.toString()=="" )
      {
            alert("Please enter student opening date.");
            return false;
      }
      else  if (document.getElementById("ctl00_ContentPlaceHolder_definition_txt_student_closing").value.toString()=="" )
      {
            alert("Please enter student closing date.");
            return false;
      } 
      else
            return true;
  }

  </script> 


    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="height: 15px; text-align: left">
                <span style="color: #ffa500">Your location- News/Notice&gt;Holidays </span>
            </td>
        </tr>
    </table>
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
                                <b><font  face="Arial" size="2">Holiday List</font></b></p>
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
                                        <table id="Table1" runat="server" width="100%">
                                            <tr>
                                                <td colspan="4">
                                                </td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td colspan="4">
                                                    Semester
                                                    <asp:DropDownList ID="cmb_s_semester" runat="server">
                                                        <asp:ListItem Value="1">Spring</asp:ListItem>
                                                        <asp:ListItem Value="2">Summer</asp:ListItem>
                                                        <asp:ListItem Value="3">Fall</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp; Year
                                                    <asp:TextBox ID="txt_s_year" runat="server" MaxLength="4" Width="38px"></asp:TextBox>
                                                    <asp:Button ID="btn_show" runat="server" OnClick="btn_show_Click" Text="Show" /></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td colspan="4">
                                                    <hr />
                                                    <asp:Label ID="lbl_list_message" runat="server" Font-Size="X-Small" ForeColor="Red"
                                                        Text="Label"></asp:Label></td>
                                            </tr>
                                        </table>
                                        <asp:GridView ID="GridView_list" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            ForeColor="#333333" GridLines="None" Width="100%">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_select" runat="server" Enabled="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:HyperLinkField DataTextField="DAY_TITLE" HeaderText="Holiday Title">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:HyperLinkField>
                                                <asp:BoundField DataField="FROMDATE" HeaderText="From">
                                                    <ItemStyle Width="70px" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TODATE" HeaderText="To">
                                                    <ItemStyle Width="70px" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="COMMENTS">
                                                    <ItemStyle Width="80px" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:CheckBoxField DataField="acCtrl" HeaderText="Active">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:CheckBoxField>
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_id" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <asp:Button ID="btn_modify" runat="server" OnClick="btn_modify_Click" Text="Modify" />
                                        <asp:Button ID="btn_delete" runat="server" OnClick="btn_delete_Click" Text="Delete" /></td>
                                </tr>
                            </table>
                            &nbsp;</td>
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
                                <b><font  face="Arial" size="2">Add New Holiday</font></b></p>
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
                                        <table id="tbl_dates" runat="server" width="100%">
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text="Label"></asp:Label></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td style="width: 93px">
                                                    Semester</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="cmb_semester" runat="server">
                                                        <asp:ListItem Value="1">Spring</asp:ListItem>
                                                        <asp:ListItem Value="2">Summer</asp:ListItem>
                                                        <asp:ListItem Value="3">Fall</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td style="width: 93px">
                                                    Year</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_year" runat="server" MaxLength="4" Width="38px"></asp:TextBox></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td style="width: 93px">
                                                    Holiday Title</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_program" runat="server" Width="198px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 93px">
                                                    From Date</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_student_opening" runat="server" Width="150px"></asp:TextBox>
                                                    
                                                    <asp:CalendarExtender ID="CalendarExtender_student_opening" runat="server"
                                                         Format="dd-MMM-yyyy" TargetControlID="txt_student_opening"></asp:CalendarExtender>

                                                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 93px">
                                                    To Date</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_student_closing" runat="server" Width="150px"></asp:TextBox>
                                                     <asp:CalendarExtender ID="CalendarExtender_student_closing" runat="server"
                                                         Format="dd-MMM-yyyy" TargetControlID="txt_student_closing"></asp:CalendarExtender>


                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 93px">
                                                    Comments</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_comments" runat="server" Width="110px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 93px">
                                                    Active</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:CheckBox ID="chk_active" runat="server" Text="Active" /></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 93px">
                                                </td>
                                                <td>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Button ID="btn_save" runat="server" OnClick="btn_save_Click" Text="Submit" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            &nbsp;</td>
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

