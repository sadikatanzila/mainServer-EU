<%@ Page Language="C#" MasterPageFile="~/student/course/course_masterPage.master" AutoEventWireup="true" CodeFile="_takenCourses.aspx.cs" Inherits="student_course_takenCourses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Course&gt;Taken Courses</span></td>
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
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="700">
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
                                <b><font color="#ffa500" face="Arial" size="2"> Taken Courses</font></b></p>
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
                            <br />
                            <table>
                                <tr>
                                    <td style="width: 45px; height: auto">
                                        Select</td>
                                    <td style="width: auto; height: auto">
                                        <asp:DropDownList ID="cmb_semester" runat="server">
                                            <asp:ListItem Value="1">Spring</asp:ListItem>
                                            <asp:ListItem Value="2">Summer</asp:ListItem>
                                            <asp:ListItem Value="3">Fall</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="width: 35px; height: 22px">
                                        Year</td>
                                    <td style="width: 49px; height: 22px">
                                        <asp:TextBox ID="txt_year" runat="server" MaxLength="4" Width="40px"></asp:TextBox></td>
                                    <td style="width: 102px; height: 22px">
                                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" /></td>
                                </tr>
                            </table>
                             
                            <hr />
                            <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text="Label"></asp:Label>
                            <asp:Label ID="lbl_semester" runat="server" Font-Bold="True" Text="Label"></asp:Label>&nbsp;<br />
                                <asp:GridView ID="GridView_courseList" runat="server" AutoGenerateColumns="False"
                                    CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:HyperLinkField DataTextField="COURSECODE" HeaderText="Code" NavigateUrl="~/student/course/_courseDetails.aspx" DataNavigateUrlFields="course_teacher_id" DataNavigateUrlFormatString="~/student/course/_courseDetails.aspx?code={0}">
                                            <ItemStyle Width="60px" />
                                        </asp:HyperLinkField>
                                        <asp:BoundField DataField="cName" HeaderText="Name" ItemStyle-Width="100px"/>
                                        <asp:BoundField DataField="chours" HeaderText="Credit">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="teacher" HeaderText="Teacher" />
                                        <asp:BoundField DataField="GGROUP" HeaderText="Section">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>

                                         <asp:BoundField DataField="SCH_CLS_1" HeaderText="1st Class" />

                                         <asp:BoundField DataField="SCH_CLS_2" HeaderText="2nd Class" />
<asp:TemplateField  HeaderText="Evaluation Status">
        <ItemTemplate>
            <asp:Label ID="lblEvaluation_status" runat="server" Text='<%#Bind("Evaluation_status") %>'></asp:Label>
        </ItemTemplate>
       <ItemStyle ForeColor="Red" />
    </asp:TemplateField>
  <%--<asp:BoundField DataField="Evaluation_status" HeaderText="Evaluation Status"/>--%>
                                    </Columns>
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                                <br />
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

