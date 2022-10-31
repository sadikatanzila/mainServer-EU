<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true"
     CodeFile="_teacherAttendance.aspx.cs" Inherits="admin_teacherAttendance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script runat="server">

   
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0"  width="100%">
        <tr>
            <td>
             
                    <table border="0" cellpadding="0" cellspacing="0" height="1" width="100%">
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
                                <b><font  face="Arial" size="2">Teacher wise Attendance</font></b></p>
                              <hr style="color: #333333" />
                        </td>
                    </tr>
                    <tr>
                        <td class="k" height="1" width="100%">
                           </td>
                    </tr>
                    <tr>
                        <td  class="k" style="height: 114px" width="1"></td>
                        <td bgcolor="white" style="height: 114px" width="18"></td>
                        <td  style="vertical-align: top" width="80%">
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
                                                    Faculty: &nbsp;&nbsp;<asp:DropDownList ID="cmb_faculty" runat="server"  AutoPostBack="true"
                                                        onselectedindexchanged="cmb_faculty_SelectedIndexChanged">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td colspan="4">
                                                    Teacher: <asp:DropDownList ID="cmbTeacher" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td colspan="4">
                                                   From Date</span> </span>
                                        <asp:TextBox ID="txt_student_opening" runat="server"  Width="150px"></asp:TextBox> 
                                        <asp:CalendarExtender ID="CalendarExtender_student_opening" runat="server"
                                                         Format="dd-MMM-yyyy" TargetControlID="txt_student_opening"></asp:CalendarExtender>

                                                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

                                        &nbsp;<span style="font-size: 10pt; color: #000000"> To Date&nbsp;
                                            <asp:TextBox ID="txt_student_closing" runat="server"  Width="150px"></asp:TextBox>
                                         <asp:CalendarExtender ID="CalendarExtender_student_closing" runat="server"
                                                         Format="dd-MMM-yyyy" TargetControlID="txt_student_closing"></asp:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td colspan="4">
                                                    <asp:Button ID="btn_show" runat="server" OnClick="btn_show_Click" Text="Show" />
													<asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" ></asp:Label></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td colspan="4">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                        <br />
          
                                        
                                        
                                        
                                    </td>
                                </tr>
                            </table>
                            &nbsp;</td>
                        <td bgcolor="white" style="height: 114px" width="14">
                            &nbsp;
                          
                        </td>
                        <td class="k" style="height: 114px" width="1">
                            </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                           </td>
                        <td bgcolor="white" height="1" width="80%">
                            </td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            </td>
                    </tr>
                    <tr>
                        <td " class="k" height="1" width="80%">
                           </td>
                    </tr>
                </table>
              
                
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

