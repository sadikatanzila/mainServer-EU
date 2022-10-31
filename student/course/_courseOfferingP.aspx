<%@ Page Language="C#" MasterPageFile="~/student/course/course_masterPage.master" AutoEventWireup="true" CodeFile="_courseOfferingP.aspx.cs" Inherits="student_course_courseOffering" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Course&gt;Course Offering</span></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0" id="TABLE1" runat="server" width="540">
        <tr>
            <td colspan="3" style="text-align: left">
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
                <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="3">
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="539" id="tbl_offered_courses" runat="server">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19" id="TD1" runat="server">
                            <img border="0" height="24" src="../images/lcurv.gif" width="19" /></td>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../images/rcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" height="22" width="505">
                            <p align="center">
                                <b><font color="#ffa500" face="Arial" size="2">Course Offering</font></b></p>
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
                        <td bgcolor="#ffffff" height="114" width="505" style="text-align: left">
                            <div style="text-align: left">
                                &nbsp;</div>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
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
                                    </td>
                                    <td>
                                    </td>
                                    <td style="color: #0000ff">
                                        <asp:GridView ID="GridView_taken_list" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%">
                                            <Columns>
                                                <asp:TemplateField>                                                    
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                 <asp:TemplateField HeaderText="Course" >
                         <ItemTemplate>
                             <asp:Label runat="server" Text='<%# Bind("cName") %>' ID="lblCourse"></asp:Label>
                          <br />   <asp:Label runat="server" Text='<%# Bind("course") %>' ID="lblCourseID"></asp:Label>
                         </ItemTemplate>
                          <ItemStyle HorizontalAlign="left" />
                          <HeaderStyle HorizontalAlign="left" />
                     </asp:TemplateField>
                                                
                                                
                                                
                                                <asp:BoundField DataField="STAFF_NAME" HeaderText="Teacher" Visible="False" />
                                                <asp:BoundField HeaderText="Credit" DataField="CHOURS" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GGROUP" HeaderText="Group" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SCH_CLS_1" HeaderText="1st Class " >
                                                    <ItemStyle Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SCH_CLS_2" HeaderText="2nd Class" >
                                                    <ItemStyle Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SCH_CLS_3" HeaderText="3rd Class" />
                                                <asp:TemplateField Visible="False">                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="COURSEKEY" runat="server" Text='<%# Bind("COURSEKEY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="False">                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="REGKEY" runat="server" Text='<%# Bind("REGKEY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <RowStyle ForeColor="#000066" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="AntiqueWhite" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lbl_total_credit" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_deleteCourse" runat="server" Text="Delete Course" OnClick="btn_deleteCourse_Click" /></td>
                                </tr>
                            </table>
                            &nbsp;
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server"><table border="0">
    <tr>
        <td colspan="3">
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <table border="0" cellpadding="0" cellspacing="0" height="1" width="539" id="tbl_newOffering" runat="server">
                <tr>
                    <td colspan="2" height="24" rowspan="3" width="19">
                        <img border="0" height="24" src="../images/lcurv.gif" width="19" /></td>
                    <td bgcolor="#6fb1d9" class="k" height="1" style="width: 505px">
                        <img border="0" height="1" src="../images/spacer(1).gif" width="100%" /></td>
                    <td align="right" colspan="2" height="24" rowspan="3" width="15">
                        <img border="0" height="24" src="../images/rcurv.gif" width="15" /></td>
                </tr>
                <tr>
                    <td bgcolor="#eef5fa" class="h" height="22" style="width: 505px">
                        <p align="center">
                            <b><font color="#ffa500" face="Arial" size="2" id="FONT1">Offer New Course</font></b></p>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#6fb1d9" class="k" height="1" style="width: 505px">
                        <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                </tr>
                <tr>
                    <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                        <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                    <td bgcolor="white" height="114" width="18">
                        <img border="0" height="1" src="../images/spacer(1).gif" width="18" /></td>
                    <td bgcolor="#ffffff" height="114" style="width: 505px">
                        <div style="text-align: left">
                            <br />
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        <strong><span>&nbsp;</span></strong></td>
                                </tr>
                                <tr>
                                    <td>
                                        Select</td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmb_course" runat="server">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_show" runat="server" Text="Show" OnClick="btn_show_Click" />&nbsp;<asp:Button ID="btn_prerequisit"
                                            runat="server" Text="Prerequisite" /></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_course" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="Red"
                                            Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="color: #0000ff">
                                        <asp:GridView ID="GridView_availableCourses" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%">
                                            <Columns>
                                                <asp:TemplateField> 
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_select" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="STAFF_NAME" HeaderText="Teacher" Visible="False" />
                                                <asp:BoundField HeaderText="Credit" DataField="CHOURS" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Group"> 
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_group" runat="server" Text='<%# Bind("SECTION") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SCH_CLS_1" HeaderText="1st Class " >
                                                    <ItemStyle Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SCH_CLS_2" HeaderText="2nd Class" >
                                                    <ItemStyle Width="65px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SCH_CLS_3" HeaderText="3rd Class" />
                                                <asp:TemplateField Visible="False">                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_course_key" runat="server" Text='<%# Bind("COURSE_KEY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="available_seat" HeaderText="Enrolled" />
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <RowStyle ForeColor="#000066" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="AntiqueWhite" />
                                        </asp:GridView>
                                        <asp:Label ID="lbl_advice" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_add" runat="server" Text="Add to Offering" OnClick="btn_add_Click" /></td>
                                </tr>
                            </table>
                        </div>
                        &nbsp;
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
                    <td bgcolor="white" height="1" style="width: 505px">
                        <img border="0" height="14" src="../images/spacer(1).gif" width="1" /></td>
                    <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                        <img border="0" height="15" src="../images/brcurv.gif" width="15" /></td>
                </tr>
                <tr>
                    <td bgcolor="#6fb1d9" class="k" height="1" style="width: 505px">
                        <img border="0" height="1" src="../images/spacer(1).gif" width="150" /></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</asp:Content>

