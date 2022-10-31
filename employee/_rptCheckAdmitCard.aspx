<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_rptCheckAdmitCard.aspx.cs" Inherits="employee_rptAdmitCardClearance" %>

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
                                <b><font  face="Arial" size="2">Report for Eligible Admid Card for Exam</font></b> <hr />

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
                                                     
                                        &nbsp;&nbsp;&nbsp;&nbsp;</span></span></td>
                                                    
                                                </tr>
                                                <tr>
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                                    <td class="auto-style1" __designer:mapid="159">
                                                        Program</td>
                                                    <td style="width: auto; height: auto" __designer:mapid="15a">
                                        <span style="font-size: 10pt" __designer:mapid="15b"><span style="color: #000000" __designer:mapid="15c">
                                                        <asp:DropDownList ID="ddlProgram" runat="server" Height="16px" Width="150px">
                                                        </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                             Exam Type 
                                                        <asp:DropDownList ID="ddlExamType" runat="server" Height="16px" Width="150px">
                                                            <asp:ListItem Value="M">Mid</asp:ListItem>
                                                            <asp:ListItem Value="F">Final</asp:ListItem>
                                                        </asp:DropDownList>      
                                        </span></span>
                                                    </td>
                                                    
                                        </span></span>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="auto-style1">
                                                        &nbsp;</td>
                                                    <td style="width: auto; height: auto">
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                                        <asp:Button ID="btnsubmitYearSem" runat="server"  Text="Submit" OnClick="btnsubmitYearSem_Click" />
                                                     
                                        </span></span>
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1" colspan="2">
                                                        <asp:Panel ID="pnlCourse" runat="server" Visible="false">
                                                            <table style="width:95%">
                                                                <tr>
                                                                    <td class="auto-style1">Course</td>
                                                                    <td style="width: auto; height: auto"><span style="font-size: 10pt"><span style="color: #000000">
                                                                        <asp:DropDownList ID="ddlCourse" runat="server" Height="16px" Width="150px">
                                                                        </asp:DropDownList>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Section<asp:TextBox ID="txtSection" runat="server" MaxLength="4" Width="100px"></asp:TextBox>
                                                                        </span></span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style1">&nbsp;</td>
                                                                    <td style="width: auto; height: auto"><span style="font-size: 10pt"><span style="color: #000000">
                                                                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Student List" Width="116px" />
                                                                        <asp:Button ID="btn_Clear" runat="server" OnClick="btn_Clear_Click" Text="Clear" />
                                                                        </span></span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style1">&nbsp;</td>
                                                                    <td style="width: auto; height: auto"> </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    
                                                </tr>
                                                
                                                <tr>
                                                    <td class="auto-style1">
                                                    </td>
                                                    <td style="height: auto">
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
                                        <asp:GridView ID="GridView_student" runat="server" AutoGenerateColumns="False" 
                                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="4" Width="85%" OnRowDataBound="GridView_student_RowDataBound">
                                            <FooterStyle BackColor="#D9E6FF" ForeColor="#330099" />
                                            <Columns>

                              <asp:TemplateField HeaderText="Sl" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>  


                            <asp:TemplateField HeaderText="SID" ItemStyle-Width="20%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblSID" runat="server" Text='<%# Bind("SID") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>     
                                                
                                                 
                            <asp:TemplateField HeaderText="NAME" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblSNAME" runat="server" Text='<%# Bind("SNAME") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PROGRAM" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblPROGRAM" runat="server" Text='<%# Bind("PROGRAM") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="Student Signature" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblSignature" runat="server" Text='<%# Bind("SIGN") %>'></asp:Label>                                         
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

