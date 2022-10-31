<%@ Page Title="" Language="C#" MasterPageFile="~/student/StudentForms/Forms_masterPage.master" AutoEventWireup="true" CodeFile="_TransportRoute.aspx.cs" Inherits="student_StudentForms_TransportRoute" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0">
        <tr>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="620">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                            <img border="0" height="24" src="../images/lcurv.gif" width="19" /></td>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../images/rcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" height="22" width="615">
                            <p align="center">
                                <b><font color="#000066" face="Arial" size="3">Transportation Service to Permanent Campus</font></b>
                                <br />
                                <b><font color="#000066" face="Arial" >(Permanent Campus: Road 6, Block B, Ashulia Model Town, Ashulia, Savar, Dhaka)</font></b>
                            </p>
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
                        <td bgcolor="#ffffff" height="114" valign="top" width="505">
                            <div style="text-align: left">
                                <br />
                                <b><font  face="Arial" size="2" >Please fill up your Convinent Place to Pick up</font></b>
                          <br />

                                <asp:Label ID="lblTransportID" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="#0000CC"></asp:Label>

                                        <br /> <br />
                              <asp:Label ID="lbl_message" runat="server" ></asp:Label> 
                                <br />
                                <br />
                                <table style="width:95%; ">
                                    <tr>
                                        <td colspan="2" style="font-size: 14px;">

                                            Name&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblName" runat="server" Text=""></asp:Label>

                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Contact:&nbsp;&nbsp; <asp:Label ID="lblContact" runat="server" Text=""></asp:Label>

                                        </td>

                                    </tr>

                                    <tr>
                                        <td style="font-size: 14px;">

                                            ID&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblID" runat="server" Text=""></asp:Label>

                                        </td>

                                        <td style="font-size: 14px;">

                                            Program&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblProgram" runat="server" Text=""></asp:Label>

                                            <asp:Label ID="lblProgramID" runat="server" Text="" Visible="false"></asp:Label>

                                        </td>

                                    </tr>

                                    <tr>
                                        <td colspan="2" style="font-size: 14px;">

                                            Contact

                                            &nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtContact" runat="server"></asp:TextBox>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td colspan="2" style="font-size: 14px;">
                                             Route&nbsp; :&nbsp;&nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlRoute" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged">
    </asp:DropDownList> </td>

                                    </tr>

                                    <tr>
                                        <td colspan="2" style="font-size: 14px;">
                                             Pick up Point&nbsp; : &nbsp;&nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlPicupPoint" runat="server" Width="200px" >
    </asp:DropDownList> </td>

                                    </tr>

                                    <tr>
                                        <td colspan="2" style="font-size: 14px;">
                                             Place of Pick & Drop
                                            &nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                            <asp:TextBox ID="txtDrop" runat="server" Width="200px"></asp:TextBox></td>

                                    </tr>

                                    <tr>
                                        <td colspan="2" style=" vertical-align: top; font-size: 14px;">
                                           Precent Address
                                            &nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            &nbsp;
                                            <asp:TextBox ID="txtPreAddress" runat="server" TextMode="MultiLine" Width="200px" Height="50px"></asp:TextBox>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td colspan="2" style=" vertical-align: top; text-align: center;">
                                            &nbsp;</td>

                                    </tr>

                                    <tr>
                                        <td colspan="2" style=" vertical-align: top; text-align: center;">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="66px" Height="26px" />
                                        </td>

                                    </tr>

                                </table>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

