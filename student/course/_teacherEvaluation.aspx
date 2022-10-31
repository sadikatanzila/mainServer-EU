<%@ Page Language="C#" MasterPageFile="~/student/course/course_masterPage.master" AutoEventWireup="true" CodeFile="_teacherEvaluation.aspx.cs" Inherits="student_course_teacherEvaluation"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="width: 355px; color: #ffa500; text-align: left">
                Your location- Courses &gt; Course List &gt; Course Details</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0">
        <tr>
            <td colspan="3" style="text-align: left">
                <asp:Label ID="lbl_message" runat="server" Font-Bold="True" Font-Italic="False" Font-Size="X-Small"
                    ForeColor="Red" Text="Label"></asp:Label></td>
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
                            <b><font color="#ffa500" face="Arial" size="2"><a href="_courseDetails.aspx">Back</a> &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; Course Teacher Evaluation Process &nbsp;&nbsp; &nbsp;</font></b></p>
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
                                <table border="0" width="100%" cellspacing="0" >
                                    <tr>
                                        <td style="background-color: aliceblue; width:10%">
                                            Teacher</td>
                                        <td style="width: 8px; background-color: aliceblue">:</td>
                                        <td style="background-color: aliceblue">
                                            <asp:Label ID="lbl_teacher" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Course</td>
                                        <td style="width: 8px">:</td>
                                        <td><asp:Label ID="lbl_course_name" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: aliceblue">Semester</td>
                                        <td style="width: 8px; background-color: aliceblue">:</td>
                                        <td style="background-color: aliceblue">
                                            <asp:Label ID="lbl_semester" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td >Section</td>
                                        <td style="width: 8px">:</td>
                                        <td>
                                            <asp:Label ID="lbl_section" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td >
                                            &nbsp;</td>
                                        <td style="width: 8px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="color: activecaption; text-align: justify">
                                            Please indicate your degree of argument or disagreement with the following statements.</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td style="width: 8px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <strong>Note that:</strong></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table style="border-right: #c00000 1px solid; border-top: #c00000 1px solid; border-left: #c00000 1px solid; border-bottom: #c00000 1px solid;  background-color:#ffebf4">
                                                    <tr>
                                                        <td style="width: 80%">
                                                            Strongly Agree</td>
                                                        <td >=</td>
                                                        <td >5</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 80%">
                                                            Agree</td>
                                                        <td>
                                                            =</td>
                                                        <td>
                                                            4</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 80%">
                                                            Neither Agree nor disagree</td>
                                                        <td>
                                                            =</td>
                                                        <td>
                                                            3</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 80%">
                                                            Disagree</td>
                                                        <td>
                                                            =</td>
                                                        <td>
                                                            2</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 80%">
                                                            Strongly Disagree</td>
                                                        <td>
                                                            =</td>
                                                        <td>
                                                            1</td>
                                                    </tr>
                                                </table>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                            &nbsp;&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <strong>
                                                Comments(if any)</strong><br />
                                            <asp:TextBox ID="txt_comments" runat="server" Height="115px" TextMode="MultiLine"
                                                    Width="219px"></asp:TextBox>
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" /></td>
                                        <td style="width: 8px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        &nbsp;</td>
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
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

