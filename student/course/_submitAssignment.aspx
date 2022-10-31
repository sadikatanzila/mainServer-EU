<%@ Page Language="C#" MasterPageFile="~/student/course/course_masterPage.master" AutoEventWireup="true" CodeFile="_submitAssignment.aspx.cs" Inherits="student_course_submitAssignment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table style="width: 95%">
        <tr>
            <td style="height: 10px; background-color: #ffffff; text-align: left">
                <table border="0" style="text-align: left">
                    <tr>
                        <td colspan="3" style="width:auto; color: #ffa500; text-align: left">
                            Your location- Courses &gt; Course List &gt; Course Details &gt; Assignment &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0">
        <tr>
            <td colspan="3" style="text-align: left">
                <hr />
                <asp:Label ID="lbl_message" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="Red"
                    Text="Label"></asp:Label></td>
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
                                <b><font color="#ffa500" face="Arial" size="2">Assignment Information</font></b></p>
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
                                <table border="0">
                                    <tr>
                                        <td style="background-color: aliceblue">
                                            Title</td>
                                        <td style="background-color: aliceblue">
                                            :</td>
                                        <td style="background-color: aliceblue">
                                            <asp:Label ID="lbl_title" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Description</td>
                                        <td>
                                            :</td>
                                        <td>
                                            <asp:Label ID="lbl_description" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: aliceblue">
                                            Due date</td>
                                        <td style="background-color: aliceblue">
                                            :</td>
                                        <td style="background-color: aliceblue">
                                            <asp:Label ID="lbl_dueDate" runat="server" Font-Bold="True" ForeColor="IndianRed"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Assignment</td>
                                        <td>
                                            :</td>
                                        <td>
                                            <asp:HyperLink ID="hp_link_aaaisnment" runat="server">HyperLink</asp:HyperLink></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Submit</td>
                                        <td>
                                            :</td>
                                        <td>
                                            <asp:HyperLink ID="hp_link_s_assignment" runat="server" Font-Bold="True">Students</asp:HyperLink>
                                            <asp:FileUpload ID="fu_attachment" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" /></td>
                                    </tr>
                                </table>
                            </div>
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
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

