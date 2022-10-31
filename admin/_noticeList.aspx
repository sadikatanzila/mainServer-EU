<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_noticeList.aspx.cs" Inherits="admin_noticeList"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Notice&gt; Notice List</span></td>
        </tr>
    </table>
    
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
      if (document.getElementById("ctl00_ContentPlaceHolder_definition_txt_student_opening").value.toString()=="" )
      {
            alert("Please enter from date.");
            return false;
      }
      else  if (document.getElementById("ctl00_ContentPlaceHolder_definition_txt_student_closing").value.toString()=="" )
      {
            alert("Please enter to date.");
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
                                <b><font  face="Arial" size="2">Notice List</font></b></p>
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
                        <td bgcolor="#ffffff"  style="vertical-align: top; text-align:left"   width="100%">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                        From</span> </span>
                                        <asp:TextBox ID="txt_student_opening" runat="server"  Width="150px"></asp:TextBox>
                                          <asp:CalendarExtender ID="CalendarExtender_student_opening" runat="server"
                                                         Format="dd-MMM-yyyy" TargetControlID="txt_student_opening"></asp:CalendarExtender>

                                                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

                                        &nbsp;<span style="font-size: 10pt; color: #000000"> to&nbsp;
                                            <asp:TextBox ID="txt_student_closing" runat="server"  Width="150px"></asp:TextBox>  
                                            <asp:CalendarExtender ID="CalendarExtender_student_closing" runat="server"
                                                         Format="dd-MMM-yyyy" TargetControlID="txt_student_closing"></asp:CalendarExtender>
                                            </td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <span style="font-size: 10pt; color: #000000">Notice Type</span>
                                        <asp:DropDownList ID="cmb_messageType" runat="server">
                                            <asp:ListItem Value="1">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Inactive</asp:ListItem>
                                        </asp:DropDownList>&nbsp;<asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click"
                                            Text="Submit" /></td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                    <hr />
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align:top; background-color: #ffffff; text-align: left">
                            <asp:GridView ID="GridView_noticeList" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                <Columns>
                                    <asp:TemplateField> 
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_select" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:HyperLinkField DataNavigateUrlFields="NOTICE_ID" DataNavigateUrlFormatString="~/admin/_newNotice.aspx?nid={0}"
                                        HeaderText="Title" NavigateUrl="~/admin/_newNotice.aspx" DataTextField="TITLE" />
                                    <asp:BoundField DataField="pub_date" HeaderText="Pub-date">
                                        <ItemStyle Width="70px" />
                                    </asp:BoundField>
                                    <asp:CheckBoxField DataField="student" HeaderText="Student">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CheckBoxField>
                                    <asp:CheckBoxField DataField="teacher" HeaderText="Teacher">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CheckBoxField>
                                    <asp:CheckBoxField DataField="general" HeaderText="General">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CheckBoxField>
                                    <asp:TemplateField Visible="False"> 
                                        <ItemTemplate>
                                            <asp:Label ID="NOTICE_ID" runat="server" Text='<%# Bind("NOTICE_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                                        <asp:Button ID="btn_active" runat="server" Text="Make Active" OnClick="btn_active_Click" />
                                        <asp:Button ID="btn_inactive" runat="server" Text="Make Inactive" OnClick="btn_inactive_Click" /></td>
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

