<%@ Page Language="C#" MasterPageFile="~/staffs/courses/_courses_master.master" AutoEventWireup="true" CodeFile="_singe_course.aspx.cs" Inherits="staffs_courses_singe_course" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {

}
function open_class_evaluation(ids)
     {
    
      window.open("teacherevaluation_result.aspx?code="+ids,'','titlebar=no,toolbar=no,scrollbars,resizable=false,height=650,top=50,left=100, width=850');
      return false; 
     }
</script>

    <table style="width: 95%">
        <tr>
            <td  style=" background-color: #ffffff; text-align: left; height: 10px; ">
                <table border="0" style="text-align: left">
                    <tr>
                        <td colspan="3" style="width: 355px;color: #ffa500; text-align: left">
                            Your location- Courses &gt; Course List &gt; Course Details&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
                <table style="width:533px" id="TABLE1" onclick="return TABLE1_onclick()">
                    <tr>
                        <td style="width:auto; text-align:left; height: 20px;">
                         Select Course
                         <asp:DropDownList ID="cmb_course" runat="server"></asp:DropDownList>
                         <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" />
                        </td>
                    </tr>
                </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
<table border="0">
    <tr>
        <td colspan="3">
        <hr/>
        </td>
        
    </tr>
    <tr>
        <td colspan="3">
            <table border="0" cellpadding="0" cellspacing="0" height="1" width="539">
                <tr>
                    <td colspan="2" height="24" rowspan="3" width="19">
                        <img border="0" height="24" src="../../staffs/images/lcurv.gif"
                            width="19" /></td>
                    <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                        <img border="0" height="1" src="../../staffs/images/spacer(1).gif"
                            width="100%" /></td>
                    <td align="right" colspan="2" height="24" rowspan="3" width="15">
                        <img border="0" height="24" src="../../staffs/images/rcurv.gif"
                            width="15" /></td>
                </tr>
                <tr>
                    <td bgcolor="#eef5fa" class="h" height="22" width="505">
                        <p align="center">
                            <b><font color="#ffa500" face="Arial" size="2">Course Information</font></b></p>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                        <img border="0" height="1" src="../../staffs/images/spacer(1).gif"
                            width="1" /></td>
                </tr>
                <tr>
                    <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                        <img border="0" height="1" src="../../staffs/images/spacer(1).gif"
                            width="1" /></td>
                    <td bgcolor="white" height="114" width="18">
                        <img border="0" height="1" src="../../staffs/images/spacer(1).gif"
                            width="18" /></td>
                    <td bgcolor="#ffffff" height="114" width="505">
                        <div style="text-align: left">
                            <br />
                            <table border="0">
                                <tr>
                                    <td style="background-color: aliceblue">Course Code</td>
                                    <td style="background-color: aliceblue"><asp:Label ID="lbl_course_code" Font-Bold="true" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Course Name</td>
                                    <td><asp:Label ID="lbl_course_name" Font-Bold="true" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="background-color: aliceblue">Credit hours</td>
                                    <td style="background-color: aliceblue"><asp:Label ID="lbl_credit_hours" Font-Bold="true" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Semester</td>
                                    <td><asp:Label ID="lbl_semester" Font-Bold="true" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="background-color: aliceblue">Section</td>
                                    <td style="background-color: aliceblue"><asp:Label ID="lbl_section" Font-Bold="true" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Total Student</td>
                                    <td><asp:Label ID="lbl_total_student" Font-Bold="true" runat="server"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                        </td>
                    <td bgcolor="white" height="114" width="14">
                        &nbsp;
                        <p>
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif"
                                width="14" /></p>
                    </td>
                    <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                        <img border="0" height="1" src="../../staffs/images/spacer(1).gif"
                            width="1" /></td>
                </tr>
                <tr>
                    <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                        <img border="0" height="15" src="../../staffs/images/blcurv.gif"
                            width="19" /></td>
                    <td bgcolor="white" height="1" width="505">
                        <img border="0" height="14" src="../../staffs/images/spacer(1).gif"
                            width="1" /></td>
                    <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                        <img border="0" height="15" src="../../staffs/images/brcurv.gif"
                            width="15" /></td>
                </tr>
                <tr>
                    <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                        <img border="0" height="1" src="../../staffs/images/spacer(1).gif"
                            width="150" /></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
    <table border="0">
        <tr>
            <td style="vertical-align:top">
                <table border="0" cellpadding="0" cellspacing="0" style="float:left" width="262">
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
                                <font color="#ffa500"><b>Course Outline &nbsp; &nbsp; &nbsp; &nbsp;<asp:HyperLink ID="hp_link_course_outline" runat="server">Add New</asp:HyperLink></b></font></p>
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
                            &gt;<asp:HyperLink ID="hp_link_courseOutline" runat="server" Font-Bold="True">Students</asp:HyperLink></td>
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
            <td style="width: 10px"></td>
            <td style="vertical-align:top" >
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
                                <font color="#ffa500"><b>Links</b></font></p>
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
                            &gt; <asp:HyperLink ID="hp_link_student" runat="server" Font-Bold="True">Students</asp:HyperLink><br />
                            &gt; <asp:HyperLink ID="hp_link_attendance" runat="server" Font-Bold="True">Attendance Entry</asp:HyperLink><br />
                            &gt; <asp:HyperLink ID="hp_link_final_gradding" runat="server" Font-Bold="True">Final Grading</asp:HyperLink><br />
                            &gt; <asp:HyperLink ID="hp_link_final_evaluation" runat="server" Font-Bold="True">Course Evaluation</asp:HyperLink><br />
                            &gt; <asp:HyperLink ID="hp_link_attendanceSheet" runat="server" Font-Bold="True">Class Attendance Sheet</asp:HyperLink><br />
                            &gt; <asp:HyperLink ID="hp_link_attendanceReport" runat="server" Font-Bold="True">Attendance Report</asp:HyperLink><br />


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
            <td >
            </td>
            <td >
            </td>
            <td>
            </td>
        </tr>
        <tr >
            <td style="vertical-align:top" >
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
                                <font color="#ffa500"><b> Class Lectures&nbsp; &nbsp; &nbsp;<asp:HyperLink ID="hp_link_class_lecture"
                                    runat="server">Add New</asp:HyperLink></b></font></p>
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
                            <br />
                            <asp:GridView ID="GridView_lecture_list" runat="server" AutoGenerateColumns="False"
                                ShowHeader="False">
                                <Columns>
                                    <asp:BoundField DataField="arrow" />
                                    <asp:HyperLinkField DataNavigateUrlFields="COURSE_MATERIALS_ID" DataNavigateUrlFormatString="~/staffs/courses/_lecture_write.aspx?code={0}"
                                        DataTextField="Title" NavigateUrl="~/staffs/courses/_lecture_write.aspx" />
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td bgcolor="white" height="63" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" height="63" style="width: 1px">
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
            <td ></td>
            <td style="vertical-align:top">
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
                                <font color="#ffa500"><b>Assignments&nbsp; &nbsp; &nbsp; &nbsp; 
                                <asp:HyperLink ID="hp_link_addnewAssignment" runat="server">Add New</asp:HyperLink></b></p>
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
                            <br />
                            <asp:GridView ID="GridView_assignment_list" runat="server" AutoGenerateColumns="False"
                                ShowHeader="False">
                                <Columns>
                                    <asp:BoundField DataField="arrow" />
                                    <asp:HyperLinkField DataNavigateUrlFields="COURSE_MATERIALS_ID" DataNavigateUrlFormatString="~/staffs/courses/_downloadAssignment.aspx?code={0}"
                                        DataTextField="Title" NavigateUrl="~/staffs/courses/_downloadAssignment.aspx" />
                                    <asp:BoundField DataField="due_dates" HeaderText="Due date" />
                                </Columns>
                            </asp:GridView>
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
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>

