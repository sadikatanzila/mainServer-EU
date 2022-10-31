<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_StudentRoutine.aspx.cs" Inherits="admin_StudentRoutine" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
<table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Routine&gt;Individual Routine</span></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
<table border="0">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="100%">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                            </td>
                        <td  class="k" height="1" width="100%">
                            </td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            </td>
                    </tr>
                    <tr>
                        <td  class="h" height="22" width="100%">
                            <p align="center">
                                <b><font  face="Arial" size="2">Student Routine View</font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="100%">
                            </td>
                    </tr>
                    <tr>
                        <td  class="k" style="height: 114px" width="1">
                            </td>
                        <td bgcolor="white" style="height: 114px" width="18">
                           </td>
                        <td bgcolor="#ffffff" style="vertical-align: top" width="100%">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <table id="tbl_dates" runat="server">
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text=""></asp:Label></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #666666">
                                                <td>
                                                    Student ID</td>
                                                <td style="width: 7px">
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtStudentID" runat="server" Width="186px"></asp:TextBox></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #666666">
                                                <td>
                                                    Semester</td>
                                                <td style="width: 7px">
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="cmb_semester" runat="server" Width="157px">
                                                        <asp:ListItem Value="1">Spring</asp:ListItem>
                                                        <asp:ListItem Value="2">Summer</asp:ListItem>
                                                        <asp:ListItem Value="3">Fall</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #666666">
                                                <td>
                                                    Year</td>
                                                <td style="width: 7px">
                                                    :</td>
                                                <td colspan="2"><asp:TextBox ID="txt_year" runat="server" MaxLength="4" Width="166px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 22px">
                                                </td>
                                                <td style="width: 7px; height: 22px">
                                                </td>
                                                <td colspan="2" style="height: 22px">
                                                    <asp:Button ID="btn_save" runat="server" OnClick="btn_save_Click" Text="Submit" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <br />
                            
                             <asp:Label ID="lbl_title" runat="server" Font-Bold="True" Font-Size="Larger" Text="" Font-Underline="True"></asp:Label><br />
        <asp:Label ID="lbl_student" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Crimson"
            Text=""></asp:Label><br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1"
            GridLines="None">
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <Columns>
            <asp:TemplateField HeaderText="Course">                                        
            <ItemTemplate>
             <asp:Label ID="lblCourseCode" runat="server" Font-Bold="true"  Text='<%# Bind("coursecode") %>'></asp:Label>
            <br /><asp:Label ID="Label1" runat="server" Text='<%# Bind("cname") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
               
                
                <asp:BoundField DataField="ggroup" HeaderText="Group" >
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemStyle Width="15px" />
                </asp:BoundField>
                <asp:BoundField DataField="staff_name" HeaderText="Instructor" >
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="sch_cls_1" HeaderText="1st Class" >
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="sch_cls_2" HeaderText="2nd Class" >
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="sch_cls_3" HeaderText="3rd Class" >
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="tut_cls_2" HeaderText="4th Class"  Visible="false">
                    <ItemStyle Width="65px" />
                </asp:BoundField>
            </Columns>
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        </asp:GridView>
        &nbsp;
                            
                            &nbsp;</td>
                        <td bgcolor="white" style="height: 114px" width="14">
                            &nbsp;
                            <p>
                              </p>
                        </td>
                        <td  class="k" style="height: 114px" width="1">
                            </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                           </td>
                        <td bgcolor="white" height="1" width="100%">
                           </td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            </td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="100%">
                           </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

