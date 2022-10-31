<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_print_studentGrade.aspx.cs" Inherits="staffs_courses_print_studentGrade" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Grade Sheet Summay</title>
    <link href="../../App_themes/jind.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table border="0">
            <tr>
                <td colspan="3" style="text-align: right">
                    <asp:Label ID="lbl_printDate" runat="server" Font-Bold="True" Text="Label"></asp:Label></td>
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
                                    <b><font color="#333399" face="Arial" size="2"><span style="color: #ffa500">Course Information</span></font></b></p>
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
                            <td align="left" bgcolor="#ffffff" height="114" width="505">
                                <br />
                                <table id="TABLE1" border="0" onclick="return TABLE1_onclick()" style="text-align: left">
                                    <tr>
                                        <td style="background-color: aliceblue">
                                            Course Code</td>
                                        <td style="background-color: aliceblue">
                                            <asp:Label ID="lbl_course_code" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Course Name</td>
                                        <td>
                                            <asp:Label ID="lbl_course_name" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: aliceblue">
                                            Credit hours</td>
                                        <td style="background-color: aliceblue">
                                            <asp:Label ID="lbl_credit_hours" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Semester</td>
                                        <td>
                                            <asp:Label ID="lbl_semester" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: aliceblue">
                                            Section</td>
                                        <td style="background-color: aliceblue">
                                            <asp:Label ID="lbl_section" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Total Student</td>
                                        <td>
                                            <asp:Label ID="lbl_total_student" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                </table>
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
        <table border="0">
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
                                    <b><font color="#333399" face="Arial" size="2"><span style="color: #ffa500">Student
                                        List</span></font></b></p>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        </tr>
                        <tr>
                            <td bgcolor="#6fb1d9" class="k" style="height: 109px" width="1">
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                            <td bgcolor="white" style="height: 109px" width="18">
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="18" /></td>
                            <td bgcolor="#ffffff" style="vertical-align: top; height: 109px; text-align: left"
                                width="505">
                                <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label><br />
                                <asp:GridView ID="GridView_students" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None" Width="100%">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="SID" HeaderText="Student ID">
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SNAME" HeaderText="Student Name">
                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SPROGRAM" HeaderText="Program" />
                                        <asp:BoundField DataField="GGRADE2" HeaderText="Grade" >
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Fail Status">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("FAIL_status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    </Columns>
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                &nbsp;
                                <br />
                            </td>
                            <td bgcolor="white" style="height: 109px" width="14">
                                &nbsp;
                                <p>
                                    <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="14" /></p>
                            </td>
                            <td bgcolor="#6fb1d9" class="k" style="height: 109px" width="1">
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
    
    </div>
    </form>
</body>
</html>
