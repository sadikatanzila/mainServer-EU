<%@ Page Language="C#" MasterPageFile="~/staffs/courses/_courses_master.master" AutoEventWireup="true" CodeFile="_students.aspx.cs" Inherits="staffs_courses_students"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">  
    <table style="width: 95%">
        <tr>
            <td class="header" style="color: #ffa500; height: 20px; background-color: white;text-align: left">
                <table border="0" style="text-align: left">
                    <tr>
                        <td colspan="3" style="width: 445px; text-align: left">
                            <span style="color: #ffa500">Your location- Courses &gt; Course List &gt; Course Details
                                &gt; Student List </span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="header" style="height: 44px; background-color: #ffffff; text-align: left">
                <table style="width:auto">
                    <tr>
                        <td style="width: auto; text-align: left">
                            Select Course
                            <asp:DropDownList ID="cmb_course" runat="server" ></asp:DropDownList>
                            <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" /></td>
                    </tr>
                </table>
            </td>
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
                            <table border="0" style="text-align: left" id="TABLE1" onclick="return TABLE1_onclick()">
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
                                 <tr>
                                    <td>
                                        Program</td>
                                    <td>
                                        <asp:Label ID="lbl_Program" runat="server" Font-Bold="true"></asp:Label></td>
                                </tr>
                                 <tr>
                                    <td>
                                        Total Class Taken</td>
                                    <td>
                                        <asp:Label ID="lbl_TotalTaknCls" runat="server" Font-Bold="true"></asp:Label></td>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
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
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" height="114" width="18">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="18" /></td>
                        <td bgcolor="#ffffff" height="114" width="505" style="text-align:left; vertical-align:top">
                            <br />
                            <asp:GridView ID="GridView_students" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <Columns>
                                    <asp:BoundField HeaderText="Student ID" DataField="SID" >
                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Student Name" DataField="SNAME" >
                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Contact" DataField="phone" >
                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="CGPA" DataField="TCGPA" >
                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="cName" HeaderText="Program" Visible="false" />

                                    <asp:BoundField DataField="ATTEND_CLASS" HeaderText="Total Attendance">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Presentance" HeaderText="Presence %">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
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

