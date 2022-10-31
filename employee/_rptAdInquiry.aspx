﻿<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_rptAdInquiry.aspx.cs" Inherits="employee_rptAdInquiry" %>

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
                                <b><font  face="Arial" size="2">Report on Prospective Students</font></b> <hr />

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
                                            CellPadding="4" Width="99%" OnRowDataBound="GridView_student_RowDataBound" >
                                            <FooterStyle BackColor="#D9E6FF" ForeColor="#330099" />
                                            <Columns>

                              <asp:TemplateField HeaderText="SL." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>  


                            <asp:TemplateField  Visible="false"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblC_INQUIRY_ID" runat="server" Text='<%# Bind("C_INQUIRY_ID") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>     
                                                
                                                 
                            <asp:TemplateField HeaderText="Name" ItemStyle-Width="18%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblSNAME" runat="server" Text='<%# Bind("STUDENT_NAME") %>'></asp:Label>                                         
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                        <asp:HyperLinkField DataNavigateUrlFields="CONTACT" DataNavigateUrlFormatString="~/admin/_Inquiry.aspx?nid={0}"
                                        HeaderText="Contact" NavigateUrl="~/admin/_Inquiry.aspx" DataTextField="CONTACT" ItemStyle-Width="10%" />

                         

                           <asp:TemplateField HeaderText="Program" ItemStyle-Width="10%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblProg" runat="server" Text='<%# Bind("Prog_Name") %>'></asp:Label>                                         
                                </ItemTemplate>
                               <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="Semester" ItemStyle-Width="7%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblSem" runat="server" Text='<%# Bind("SEMESTER_Name") %>'></asp:Label>                              
                                </ItemTemplate>  
                               <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                           <asp:TemplateField HeaderText="Year" ItemStyle-Width="5%"> 
                                <ItemTemplate>
                                  
                                     <asp:Label ID="lblYear" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>                                    
                                </ItemTemplate>  
                               <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="District" ItemStyle-Width="7%"> 
                                <ItemTemplate>
                                      <asp:Label ID="lblDistrict" runat="server" Text='<%# Bind("DISTRICT") %>'></asp:Label>                                         
                                </ItemTemplate> 
                                <HeaderStyle HorizontalAlign="Center" />   
                            </asp:TemplateField>    

                             <asp:TemplateField HeaderText="Institution" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                      <asp:Label ID="lblInstituteName" runat="server" Text='<%# Bind("InstituteName") %>'></asp:Label>                                         
                                </ItemTemplate> 
                                 <HeaderStyle HorizontalAlign="Center" />   
                            </asp:TemplateField>                   
                             
                            <asp:TemplateField HeaderText="Reference" ItemStyle-Width="7%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblRef" runat="server" Text='<%# Bind("REFFERENCE") %>'></asp:Label>                              
                                </ItemTemplate> 
                                <HeaderStyle HorizontalAlign="Center" /> 
                            </asp:TemplateField>

<asp:TemplateField HeaderText="Coming Date" ItemStyle-Width="15%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblCOMINGDATE" runat="server" Text='<%# Eval("COMINGDATE", "{0:dd-MMM-yyyy}") %>'></asp:Label>                                     
                                </ItemTemplate>    
                            </asp:TemplateField> 
                                                                               
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="#330099" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#6699FF" Font-Bold="True" Font-Size="Small" ForeColor="#FFFFCC" />
                                        </asp:GridView>



                                        
                            <!--   <asp:TemplateField HeaderText="Contact" ItemStyle-Width="10%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblCONTACT" runat="server" Text='<%# Bind("CONTACT") %>'></asp:Label>                                         
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                                -->   
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

