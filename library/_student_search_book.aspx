<%@ Page Language="C#" MasterPageFile="~/student/library/library_masterPage.master" AutoEventWireup="true" CodeFile="_student_search_book.aspx.cs" Inherits="student_library_search__book"  %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder_tracker">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Library&gt;Book Search</span></td>
        </tr>
    </table>
    &nbsp;</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder_definition">
    <br />
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
                                <b><font color="#ffa500" face="Arial" size="2">Book Search</font></b></p>
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
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 100px; height: auto">
                                                    Book Title</td>
                                                <td style="width: 323px; height: auto">
                                                    <asp:TextBox ID="txt_title" runat="server" Width="266px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; height: auto">
                                                    Author</td>
                                                <td style="width: 323px; height: auto">
                                                    <asp:TextBox ID="txt_author" runat="server" Width="130px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 45px; height: auto">
                                                    Publisher</td>
                                                <td style="width: 323px; height: auto">
                                                    <asp:DropDownList ID="cmb_publisher" runat="server">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 45px; height: auto">
                                                    Department</td>
                                                <td style="width: 323px; height: auto">
                                                    <asp:DropDownList ID="cmb_department" runat="server">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 45px; height: auto">
                                                </td>
                                                <td style="width: 323px; height: auto">
                                                    <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" /></td>
                                            </tr>
                                        </table>
                                        <hr />
                                        <asp:GridView ID="GridView_bookList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            ForeColor="#333333" GridLines="None" Visible="False" Width="100%">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Smaller" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="serial" HeaderText="Sl No">
                                                    <ItemStyle Width="40px" />
                                                </asp:BoundField>
                                                <asp:HyperLinkField DataNavigateUrlFields="BOOK_ID" DataNavigateUrlFormatString="~/student/library/_bookDetails.aspx?code={0}"
                                                    DataTextField="TITLE" HeaderText="Title" NavigateUrl="~/student/library/_bookDetails.aspx" />
                                                <asp:BoundField DataField="AUTHORS" HeaderText="Author">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PUBLISHER_NAME" HeaderText="Publisher">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="available" HeaderText="Available">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
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
    <br />
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ContentPlaceHolder_content">
    &nbsp;</asp:Content>
