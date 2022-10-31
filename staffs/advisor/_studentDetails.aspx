<%@ Page Language="C#" MasterPageFile="~/staffs/advisor/MasterPage_advisor.master" AutoEventWireup="true" CodeFile="_studentDetails.aspx.cs" Inherits="staffs_advisor_studentDetails"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Advisoship &gt; Student Information</span></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
   
 <script language="javascript" type="text/javascript">
   
 function open_academic_backgrounf( sid )
 {
  window.open('_academic_background.aspx?ids='+sid,'','titlebar=no,toolbar=no,scrollbars=true,resizable=false,height=550,width=560');
  return false; 
 }  
 
 function open_current_academic_status( sid )
 {
  window.open('_academicStatus.aspx?ids='+sid,'','titlebar=no,toolbar=no,scrollbars,resizable=false,height=650,width=620');
  return false; 
 } 
   
 function open_completed_course( sid )
 {
  window.open('_completed_courses.aspx?ids='+sid,'','titlebar=no,toolbar=no,scrollbars,resizable=true,height=650,width=580');
  return false; 
 }  
   
 </script>
   
      
    <table border="0">
        <tr>
            <td colspan="3">
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="539">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                            <img border="0" height="24" src="../../staffs/images/lcurv.gif" width="19" /></td>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../../staffs/images/rcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" width="505" style="height: 22px">
                            <p align="center">
                                <b><font color="#333399" face="Arial" size="2"><span style="color: #ffa500">Student
                                    Information</span></font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" height="114" width="18">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="18" /></td>
                        <td bgcolor="#ffffff" height="114" width="505" align="left">
                            <br />
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        <strong><span style="text-decoration: underline">Admission Information</span></strong></td>
                                </tr>
                                <tr>
                                    <td >ID</td>
                                    <td>:&nbsp;
                                    </td>
                                    <td ><asp:Label ID="lbl_id" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                               <tr>
                                    <td >Name</td>
                                   <td>:</td>
                                    <td><asp:Label ID="lbl_name" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Program</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lbl_program" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Major</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lbl_major" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Admission
                                    </td>
                                    <td>:</td>
                                    <td><asp:Label ID="lbl_admission" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Admission as</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lbl_admissionAs" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        Completed Credit&nbsp;
                                    </td>
                                    <td>
                                        :</td>
                                    <td>
                                        <asp:Label ID="lbl_completd_hrs" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        CGPA</td>
                                    <td>
                                        :</td>
                                    <td>
                                        <asp:Label ID="lbl_cgpa" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        &nbsp; <strong><span style="text-decoration: underline">Personal Information</span></strong></td>
                                </tr>
                                <tr>
                                    <td>Gender</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lbl_gender" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Date of barth</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lbl_dob" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Address</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lbl_address" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Phone</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lbl_phone" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>E-mail</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lbl_email" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Advisor</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lbl_advisor" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Comments</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lbl_comments" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td bgcolor="white" height="114" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                            <img border="0" height="15" src="../../staffs/images/blcurv.gif" width="19" /></td>
                        <td bgcolor="white" height="1" width="505">
                            <img border="0" height="14" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            <img border="0" height="15" src="../../staffs/images/brcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="150" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
    <table border="0">
        <tr>
            <td colspan="3" style="width: 542px">
                <table border="0" cellpadding="0" cellspacing="0" height="1" style="float: left"
                    width="262">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                            <img border="0" height="24" src="../../staffs/images/lcurv.gif" width="19" /></td>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="228">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../../staffs/images/rcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" height="22" width="228">
                            <p align="center">
                                <font color="#ffa500"><b>Student Links</b></font></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="228">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="63" width="1">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" height="63" width="18">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="18" /></td>
                        <td align="left" bgcolor="#ffffff" height="auto" valign="top" width="228">
                            &nbsp;<br />
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hp_link_completed_course" runat="server">Completed Courses</asp:HyperLink></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td> <a href="#"></a> 
                                        <asp:HyperLink ID="hpLink_academic_status" runat="server">Current Academic Status</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hp_link_academic_background" runat="server">Academic Background</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td bgcolor="white" height="63" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" height="63" width="1">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                            <img border="0" height="15" src="../../staffs/images/blcurv.gif" width="19" /></td>
                        <td bgcolor="white" height="1" width="228">
                            <img border="0" height="14" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            <img border="0" height="15" src="../../staffs/images/brcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="228">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="150" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="width: 542px">
            </td>
        </tr>
    </table>
</asp:Content>

