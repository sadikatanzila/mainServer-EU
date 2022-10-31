<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_course_teacherEvalList.aspx.cs" Inherits="admin_course_teacherEvalList"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">

<script language="javascript" type="text/javascript">
  
       function goto_eval_details(courseT)
        {
          window.open('_course_teacherEvalDetails.aspx?ids='+courseT,'','titlebar=no,toolbar=no,,resizable=false,height=600,width=700');
          return false;
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
                                <b><font  face="Arial" size="2">Teacher Evaluation</font></b></p>
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
                                                    Semester
                                                    <asp:DropDownList ID="cmb_s_semester" runat="server">
                                                        <asp:ListItem Value="1">Spring</asp:ListItem>
                                                        <asp:ListItem Value="2">Summer</asp:ListItem>
                                                        <asp:ListItem Value="3">Fall</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp; Year
                                                    <asp:TextBox ID="txt_s_year" runat="server" MaxLength="4" Width="38px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td colspan="4">
                                                    <asp:Button ID="btn_show" runat="server" OnClick="btn_show_Click" Text="Show" />
													<asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td colspan="4">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
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

