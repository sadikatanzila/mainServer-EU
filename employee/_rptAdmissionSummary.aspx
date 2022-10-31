<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_rptAdmissionSummary.aspx.cs" Inherits="employee_rptAdmissionSummary" %>

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
                                <b><font  face="Arial" size="2">Admitted Student List </font></b> <hr />

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
                                                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" Width="65px" />
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                                        <asp:Button ID="btn_Clear" runat="server" OnClick="btn_clear_Click" Text="Clear" Style="margin-left:75px" />
                                        </span></span>
                                                    </td>
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
                                        <asp:GridView ID="GridView_student" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="4" Width="80%" OnRowDataBound="GridView_student_RowDataBound">
                                            <FooterStyle BackColor="#D9E6FF" ForeColor="#330099" />
                                            <Columns>

                              <asp:TemplateField HeaderText="Sl" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>  


                            <asp:TemplateField HeaderText="Program" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblProgram" runat="server" Text='<%# Bind("Program") %>'></asp:Label>                                         
                                </ItemTemplate>

                            </asp:TemplateField>     
                                                
                                                 
                            <asp:TemplateField HeaderText="Regular" ItemStyle-Width="20%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblRegular" runat="server" Text='<%# Bind("Regular") %>'></asp:Label>                                         
                                </ItemTemplate>

                   <FooterTemplate>                        
                        <asp:Label ID="lblRegularTotal" runat="server" />
                    </FooterTemplate> 
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="Transfer" ItemStyle-Width="20%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblTransfer" runat="server" Text='<%# Bind("Transfer") %>'></asp:Label>                                         
                                </ItemTemplate>

                     <FooterTemplate>                        
                        <asp:Label ID="lblTransferTotal" runat="server" />
                    </FooterTemplate> 
                            </asp:TemplateField>
                                                               
                              <asp:TemplateField HeaderText="Total" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total") %>'></asp:Label>                                         
                                </ItemTemplate>

                                  <FooterTemplate>                        
                        <asp:Label ID="lblGrandTotal" runat="server" />
                    </FooterTemplate> 
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

