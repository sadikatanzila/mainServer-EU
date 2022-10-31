<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_rptCourseOfferingClearance.aspx.cs" Inherits="employee_rptCourseOfferingClearance" %>

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
                                <b><font  face="Arial" size="2">Report on Course Offering Clearance</font></b> <hr />

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
                                            CellPadding="4" Width="90%" OnRowDataBound="GridView_student_RowDataBound">
                                            <FooterStyle BackColor="#D9E6FF" ForeColor="#330099" />
                                            <Columns>

                              <asp:TemplateField HeaderText="Sl" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>  


                            <asp:TemplateField HeaderText="SID" ItemStyle-Width="10%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblSID" runat="server" Text='<%# Bind("SID") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>     
                                                
                                                 
                            <asp:TemplateField HeaderText="NAME" ItemStyle-Width="17%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblSNAME" runat="server" Text='<%# Bind("SNAME") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Department" ItemStyle-Width="10%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblDept" runat="server" Text='<%# Bind("SPROGRAM") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="Sem Year" ItemStyle-Width="12%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblSem" runat="server" Text='<%# Bind("SEMESTER_Name") %>'></asp:Label>   
                                      <asp:Label ID="lblYear" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>                                         
                                </ItemTemplate>  
                            </asp:TemplateField>
                            
                                           
                             

                          <asp:TemplateField HeaderText="Cleared by" ItemStyle-Width="15%"> 
                                <ItemTemplate>                               
                                     <asp:Label ID="lblSTAFF_NAME" runat="server" Text='<%# Bind("STAFF_NAME") %>'></asp:Label>  
                                    <br />
                                    (<asp:Label ID="lblINSERTIONID" runat="server" Text='<%# Bind("CLEAREDBY") %>'></asp:Label>)                                     
                                </ItemTemplate>  
                              <ItemStyle VerticalAlign="NotSet" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cleared Time" ItemStyle-Width="15%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblCLEAREDDATE" runat="server" Text='<%# Eval("CLEAREDDATE", "{0:dd-MMM-yyyy}") %>'></asp:Label><br />   
                                      <asp:Label ID="lblCLEARED_TIME" runat="server" Text='<%# Bind("CLEARED_TIME") %>'></asp:Label>                                         
                                </ItemTemplate>  
                            </asp:TemplateField>                    
                                            
                               <asp:TemplateField HeaderText="Approval Dues" ItemStyle-Width="10%"> 
                                <ItemTemplate>
                                     
                                    <asp:Label ID="lblDUES" runat="server" Text='<%# Bind("DUES") %>'></asp:Label>                                          
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

