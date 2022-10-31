<%@ Page Language="C#" MasterPageFile="~/student/library/library_masterPage.master" AutoEventWireup="true" CodeFile="_student_book_requisition.aspx.cs" Inherits="student_library_requestForBook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Library&gt;Book Request</span></td>
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
                            <img border="0" height="24" src="../images/lcurv.gif"
                                width="19" /></td>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif"
                                width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../images/rcurv.gif"
                                width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" height="22" width="505">
                            <p align="center">
                                <b><font color="#ffa500" face="Arial" size="2">Book List (Requisition)</font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif"
                                width="1" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" style="height: 114px" width="1">
                            <img border="0" height="1" src="../images/spacer(1).gif"
                                width="1" /></td>
                        <td bgcolor="white" style="height: 114px" width="18">
                            <img border="0" height="1" src="../images/spacer(1).gif"
                                width="18" /></td>
                        <td bgcolor="#ffffff" style="vertical-align: top" width="505">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <table>
                                            <tr>
                                                <td style="width: 45px; height: auto">
                                                    Select</td>
                                                <td style="width: auto; height: auto">
                                                    <asp:DropDownList ID="DropDownList2" runat="server">
                                                        <asp:ListItem Value="1">Spring</asp:ListItem>
                                                        <asp:ListItem Value="2">Summer</asp:ListItem>
                                                        <asp:ListItem Value="3">Fall</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td style="width: 35px; height: 22px">
                                                    Year</td>
                                                <td style="width: 49px; height: 22px">
                                                    <asp:TextBox ID="TextBox2" runat="server" MaxLength="4" Width="40px"></asp:TextBox></td>
                                                <td style="width: 102px; height: 22px">
                                                    <asp:Button ID="btn_submit" runat="server" Text="Submit" /></td>
                                            </tr>
                                        </table>
                                        <hr />
                                        <asp:GridView ID="GridView_courseList" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" GridLines="None" Visible="False">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Smaller" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:HyperLinkField DataTextField="c_code" HeaderText="Code" NavigateUrl="~/staffs/courses/_singe_course.aspx" />
                                                <asp:BoundField DataField="c_name" HeaderText="Name">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="c_credit_hrs" HeaderText="Credit hours">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="c_student" HeaderText="Number of Student">
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
                                <img border="0" height="1" src="../images/spacer(1).gif"
                                    width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" style="height: 114px" width="1">
                            <img border="0" height="1" src="../images/spacer(1).gif"
                                width="1" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                            <img border="0" height="15" src="../images/blcurv.gif"
                                width="19" /></td>
                        <td bgcolor="white" height="1" width="505">
                            <img border="0" height="14" src="../images/spacer(1).gif"
                                width="1" /></td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            <img border="0" height="15" src="../images/brcurv.gif"
                                width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif"
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
            <td colspan="3" style="width: 542px">
                <table border="0" cellpadding="0" cellspacing="0" height="1" style="float: left"width="262">
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
                            <p align="center"><font color="#ffa500"><b>Add Books</b></font></p>
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
                            <table>
                                <tr>
                                    <td>Name</td>
                                    <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Author</td>
                                    <td><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Edition</td>
                                    <td><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Copy</td>
                                    <td><asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Comments</td>
                                    <td><textarea id="TextArea1" rows="2" style="width: 146px"></textarea></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td><asp:Button ID="btn_fupload_submit" runat="server" Text="Submit" /></td>
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


