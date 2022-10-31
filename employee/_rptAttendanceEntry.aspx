<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_rptAttendanceEntry.aspx.cs" Inherits="employee_rptAttendanceEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
   
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0" style="width:95%">
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
                                <b><font  face="Arial" size="2">Taken Class Wise Attendance Report</font></b> <hr />

</p>
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
                        <td bgcolor="#ffffff" style="vertical-align: top; text-align: left" width="100%">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                            <br />
                                            <table style="width:75%">
                                                <tr>
                                                    <td class="auto-style1">
                                                     Select  Semester</td>
                                                    <td style="width: auto; height: auto">
                                                        <asp:DropDownList ID="ddlSemester" runat="server" Height="16px" Width="150px">
                                                            <asp:ListItem Value="1">Spring</asp:ListItem>
                                                            <asp:ListItem Value="2">Summer</asp:ListItem>
                                                            <asp:ListItem Value="3">Fall</asp:ListItem>
                                                        </asp:DropDownList>
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        Year&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtYear" runat="server" MaxLength="4" Width="100px"></asp:TextBox>
                                        </span></span>
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1">
                                                        Faculty</td>
                                                    <td style="width: auto; height: auto">
                                                        <asp:DropDownList ID="ddlFaculty" runat="server" Height="16px" Width="150px">
                                                            
                                                        </asp:DropDownList>
                                                        <!--<asp:ListItem Value="0">All</asp:ListItem>
                                                            <asp:ListItem Value="01">Arts</asp:ListItem>
                                                            <asp:ListItem Value="02">Business Administration</asp:ListItem>
                                                            <asp:ListItem Value="03">Engineering & Technology</asp:ListItem>
                                                            <asp:ListItem Value="04">Law</asp:ListItem> -->
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1">
                                                    </td>
                                                    <td style="height: auto">
                                                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" /></td>
                                                </tr>
                                            </table>
                                        </span></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <hr />
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text=""></asp:Label></td>
                                </tr>

                                 <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: center">
                                      
                                        <asp:Label ID="lblHeading" runat="server" Font-Size="Large" ForeColor="#000099" Text=""></asp:Label>
                                        <br /><br />


                                    </td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <asp:GridView ID="GridView_student" runat="server" AutoGenerateColumns="False" 
                                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="4" Width="95%" OnRowDataBound="GridView_student_RowDataBound">
                                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                            <Columns>

                              <asp:TemplateField HeaderText="Sl" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>  


                            <asp:TemplateField HeaderText="Course" ItemStyle-Width="7%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblCOURSE_KEY" runat="server" Text='<%# Bind("COURSE_KEY") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>     
                                                
                                                 
                            <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="15%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblCNAME" runat="server" Text='<%# Bind("CNAME") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="Teacher ID" ItemStyle-Width="7%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblTEACHER_ID" runat="server" Text='<%# Bind("VALUE") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="Teacher Name" ItemStyle-Width="15%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblSTAFF_NAME" runat="server" Text='<%# Bind("STAFF_NAME") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>                   
                             
                                                
                                                                                       
                              <asp:TemplateField HeaderText="Department" ItemStyle-Width="8%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblCOLLEGENAME" runat="server" Text='<%# Bind("COLLEGENAME") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>                   
                                                                      
                              <asp:TemplateField HeaderText="SECTION" ItemStyle-Width="5%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblSECTION" runat="server" Text='<%# Bind("SECTION") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>
                                                
                                                
                             <asp:TemplateField HeaderText="Total Student" ItemStyle-Width="8%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lbltotalstd" runat="server" Text='<%# Bind("totalstd") %>'></asp:Label>                                         
                                </ItemTemplate>

                    
                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Attendance Entry" ItemStyle-Width="8%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lbltaken" runat="server" Text='<%# Bind("TAKENCLASS") %>'></asp:Label>                                         
                                </ItemTemplate>

                    
                            </asp:TemplateField>               
                                                                    
                             
                                                
                                                                                         
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="#330099" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#6699FF" Font-Bold="True" Font-Size="Small" ForeColor="#FFFFCC" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                      </td>
                                </tr>
                            </table>
                        </td>
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
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

