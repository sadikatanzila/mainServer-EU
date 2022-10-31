<%@ Page Language="C#" MasterPageFile="~/student/library/library_masterPage.master" AutoEventWireup="true" CodeFile="_bookDetails.aspx.cs" Inherits="student_library_bookDetails"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Library &gt; Book Search &gt; Book details</span></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0">
        <tr>
            <td>
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
                                <b><font color="#ffa500" face="Arial" size="2">Book Details</font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" style="height: 114px" width="1">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" style="height: 114px" width="18">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="18" /></td>
                        <td bgcolor="#ffffff" style="vertical-align: top" width="505">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="font-weight: bold; height: 10px; background-color: #ffffff;
                                        text-align: left">
                                        <br />
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 73px; height: auto">
                                                    <span style="font-size: 8pt">Book Title</span></td>
                                                <td style="width: 9px; height: auto">
                                                    :</td>
                                                <td style="width: 323px; height: auto">
                                                    <asp:Label ID="lbl_title" runat="server" ForeColor="Desktop" Text="Label"></asp:Label></td>
                                            </tr>
                                            <tr style="color: #000000">
                                                <td style="width: 73px; height: auto">
                                                    Author</td>
                                                <td style="width: 9px; height: auto">
                                                    :</td>
                                                <td style="width: 323px; height: auto">
                                                    <asp:Label ID="lbl_author" runat="server" ForeColor="Desktop" Text="Label"></asp:Label></td>
                                            </tr>
                                            <tr style="color: #000000">
                                                <td style="width: 73px; height: auto">
                                                    Publisher</td>
                                                <td style="width: 9px; height: auto">
                                                    :</td>
                                                <td style="width: 323px; height: auto">
                                                    <asp:Label ID="lbl_publisher" runat="server" ForeColor="Desktop" Text="Label"></asp:Label></td>
                                            </tr>
                                            <tr style="color: #000000">
                                                <td style="width: 73px; height: auto">
                                                    Department</td>
                                                <td >
                                                    :</td>
                                                <td style="font-size: 8pt; width: 323px; height: auto">
                                                    <asp:Label ID="lbl_department" runat="server" ForeColor="Desktop" Text="Label"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 73px; height: auto">
                                                    Available</td>
                                                <td style="width: 9px; height: auto">
                                                    :</td>
                                                <td style="width: 323px; height: auto">
                                                    <asp:Label ID="lbl_availableCopies" runat="server" ForeColor="Desktop" Text="Label"></asp:Label></td>
                                            </tr>
                                        </table>
                                        <br />
                                        <span style="font-size: 8pt"><span style="color: #006699">Available Copies:</span> </span>
                                        <hr />
                                        <asp:GridView ID="GridView_bookList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            ForeColor="#333333" GridLines="None" Width="100%">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="serial" HeaderText="Sl No">
                                                    <ItemStyle Width="40px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ACCESSION_NO" HeaderText="ACCESSION">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BOOK_LOCATION" HeaderText="LOCATION" />
                                                <asp:BoundField DataField="EDITION" HeaderText="EDITION" />
                                                <asp:BoundField DataField="YR_OF_PUB" HeaderText="PUB. YEAR" />
                                                <asp:BoundField DataField="BOOK_NOTE" HeaderText="NOTE" />
                                            </Columns>
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Smaller" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                    </td>
                                </tr>
                            </table>
                            &nbsp;</td>
                        <td bgcolor="white" style="height: 114px" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" style="height: 114px" width="1">
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

