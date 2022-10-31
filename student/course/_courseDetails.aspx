<%@ Page Language="C#" MasterPageFile="~/student/course/course_masterPage.master" AutoEventWireup="true" CodeFile="_courseDetails.aspx.cs" Inherits="student_course_courseDetails"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table style="width: 95%">
        <tr>
            <td style="height: 10px; background-color: #ffffff; text-align: left">
                <table border="0" style="text-align: left">
                    <tr>
                        <td colspan="3" style="width: 355px; color: #ffa500; text-align: left">
                            Your location- Courses &gt; Course List &gt; Course Details&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="TABLE1"  style="width: 533px">
        <tr>
            <td style="width: auto; height: 20px; text-align: left">
                Select Course
                <asp:DropDownList ID="cmb_course" runat="server">
                </asp:DropDownList>
                <asp:Button ID="btn_submit" runat="server"  Text="Submit" OnClick="btn_submit_Click" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0">
        <tr>
            <td colspan="3">
                <hr />
            </td>
        </tr>
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
                        <td bgcolor="#eef5fa" class="h" height="22" width="505">
                            <p align="center">
                                <b><font color="#ffa500" face="Arial" size="2">Course Information</font></b></p>
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
                        <td bgcolor="#ffffff" height="114" width="505">
                            <div style="text-align: left">
                                <br />
                                <table border="0" width="100%">
                                    <tr>
                                        <td style="background-color: aliceblue; ">
                                            Course Code</td>
                                        <td style="background-color: aliceblue; width: 8px;">
                                            :</td>
                                        <td style="background-color: aliceblue">
                                            <asp:Label ID="lbl_course_code" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 95px">
                                            Course Name</td>
                                        <td style="width: 8px">
                                            :</td>
                                        <td>
                                            <asp:Label ID="lbl_course_name" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: aliceblue; width: 95px;">
                                            Credit hours</td>
                                        <td style="background-color: aliceblue; width: 8px;">
                                            :</td>
                                        <td style="background-color: aliceblue">
                                            <asp:Label ID="lbl_credit_hours" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 95px">
                                            Semester</td>
                                        <td style="width: 8px">
                                            :</td>
                                        <td>
                                            <asp:Label ID="lbl_semester" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: aliceblue; width: 95px;">
                                            Section</td>
                                        <td style="background-color: aliceblue; width: 8px;">
                                            :</td>
                                        <td style="background-color: aliceblue">
                                            <asp:Label ID="lbl_section" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 95px">
                                        </td>
                                        <td style="width: 8px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 95px">
                                            Course outline</td>
                                        <td style="width: 8px">
                                            :</td>
                                        <td>
                                            <asp:HyperLink ID="hp_link_courseOutline" runat="server" Font-Bold="True">Students</asp:HyperLink></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 95px">
                                            &nbsp;</td>
                                        <td style="width: 8px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                                            <asp:Label ID="lbl_eval_message" runat="server" ForeColor="ActiveCaption" Text="Teacher evaluation process is running. To evaluate"></asp:Label>
                                            <asp:HyperLink ID="hplink_eval_message" runat="server">click here</asp:HyperLink></td>
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
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
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
                                <font color="#ffa500"><b>Class Lectures</b></font></p>
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
            <td>
            </td>
            <td style="vertical-align: top">
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
                                <b><span style="color: orange">Assignments</span></b></p>
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
                            &nbsp;<asp:GridView ID="GridView_assignment_list" runat="server" AutoGenerateColumns="False"
                                ShowHeader="False">
                                <Columns>
                                    <asp:BoundField DataField="arrow" />
                                    <asp:HyperLinkField DataNavigateUrlFields="COURSE_MATERIALS_ID" DataNavigateUrlFormatString="~/student/course/_submitAssignment.aspx?code={0}"
                                        DataTextField="Title" NavigateUrl="~/student/course/_submitAssignment.aspx" />
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
    <br />
    <br />
</asp:Content>

