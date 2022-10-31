<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_uploadPicturet.aspx.cs" Inherits="student_uploadPicturet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Profile&gt;Upload Image</span></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0">
        <tr>
            <td colspan="3" style="text-align: left">
                <asp:Label ID="lbl_message" runat="server" Font-Bold="True" ForeColor="Red" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="3">
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="100%">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                            <img border="0" height="24" src="../student/images/lcurv.gif" width="19" /></td>
                        <td  class="k" height="1" width="100%">
                            <img border="0" height="1" src="../student/images/spacer(1).gif" width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../student/images/rcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td  class="h" height="22" width="100%">
                            <p align="center">
                                <b><font  face="Arial" size="2">Upload image</font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="100%">
                            <img border="0" height="1" src="../student/images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td  class="k" height="114" width="1">
                            <img border="0" height="1" src="../student/images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" height="114" width="18">
                            <img border="0" height="1" src="../student/images/spacer(1).gif" width="18" /></td>
                        <td bgcolor="#ffffff" height="114" width="100%">
                            <div style="text-align: left">
                                <br />
                                <table border="0">
                                    <tr>
                                        <td>
                                            Student ID</td>
                                        <td style="width: 5px">
                                            :</td>
                                        <td>
                                            <asp:TextBox ID="txtID" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image</td>
                                        <td style="width: 5px">
                                            :</td>
                                        <td>
                                            &nbsp;<asp:FileUpload ID="FileUpload_image" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="width: 5px">
                                        </td>
                                        <td>
                                            <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td bgcolor="white" height="114" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../student/images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td  class="k" height="114" width="1">
                            <img border="0" height="1" src="../student/images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                            <img border="0" height="15" src="../student/images/blcurv.gif" width="19" /></td>
                        <td bgcolor="white" height="1" width="100%">
                            <img border="0" height="14" src="../student/images/spacer(1).gif" width="1" /></td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            <img border="0" height="15" src="../student/images/brcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="100%">
                            <img border="0" height="1" src="../student/images/spacer(1).gif" width="150" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
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
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

