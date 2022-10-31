<%@ Page Language="C#" MasterPageFile="~/student/course/course_masterPage.master" AutoEventWireup="true" CodeFile="_course_advisor.aspx.cs" Inherits="student_course_advisor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Course&gt;Course Advisor</span></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0">
        <tr>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="539">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                            <img border="0" height="24" src="../images/lcurv.gif" width="19" /></td>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../images/rcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" height="22" width="505">
                            <p align="center">
                                <b><font color="#ffa500" face="Arial" size="2">Course Advisor</font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" height="114" width="18">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="18" /></td>
                        <td bgcolor="#ffffff" height="114" width="505">
                            <div style="text-align: left">
                                &nbsp;</div>
                            <div style="float: left; width: 390px; text-align: left">
                                <table id="tbl_dates" runat="server">
                                    <tr style="font-size: 8pt; color: #666666">
                                        <td>
                                            Name</td>
                                        <td>
                                            :</td>
                                        <td colspan="2">
                                            <asp:Label ID="lbl_name" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Phone</td>
                                        <td>
                                            :</td>
                                        <td colspan="2">
                                            <asp:Label ID="lbl_phone" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Mobile</td>
                                        <td>
                                            :</td>
                                        <td colspan="2">
                                            <asp:Label ID="lbl_mobile" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            E-mail</td>
                                        <td>
                                            :</td>
                                        <td colspan="2">
                                            <asp:Label ID="lbl_email" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td colspan="2">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            College</td>
                                        <td>
                                            :</td>
                                        <td colspan="2">
                                            <asp:Label ID="lbl_college" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Designation</td>
                                        <td>
                                            :</td>
                                        <td colspan="2">
                                            &nbsp;<asp:Label ID="lbl_designation" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                </table>
                            </div>                            
                            <div style="float: right; text-align: right">
                                <asp:Image ID="img_myProfile" runat="server" Height="105px" Width="104px" />
                            </div>
                        </td>
                        <td bgcolor="white" height="114" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                            <img border="0" height="15" src="../images/blcurv.gif" width="19" /></td>
                        <td bgcolor="white" height="1" width="505">
                            <img border="0" height="14" src="../images/spacer(1).gif" width="1" /></td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            <img border="0" height="15" src="../images/brcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="150" /></td>
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
    <br />
    <br />
    <br />
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

