<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_rptProbableGraduate.aspx.cs" Inherits="employee_rptProbableGraduate" %>

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
                                <b><font  face="Arial" size="2">Probable Graduate Students' List</font></b> <hr />

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
                                                        From</td>
                                                    <td style="width: auto; height: auto">
                                                        <asp:DropDownList ID="ddlFrmSemester" runat="server" Height="16px" Width="150px">
                                                            <asp:ListItem Value="1">Spring</asp:ListItem>
                                                            <asp:ListItem Value="2">Summer</asp:ListItem>
                                                            <asp:ListItem Value="3">Fall</asp:ListItem>
                                                        </asp:DropDownList>
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        Year&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFrmYear" runat="server" MaxLength="4" Width="100px"></asp:TextBox>
                                        </span></span>
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1">
                                                        To</td>
                                                    <td colspan="4" style="height: auto">
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                                        <asp:DropDownList ID="ddlToSemester" runat="server" Width="150px">
                                                            <asp:ListItem Value="1">Spring</asp:ListItem>
                                                            <asp:ListItem Value="2">Summer</asp:ListItem>
                                                            <asp:ListItem Value="3">Fall</asp:ListItem>
                                                        </asp:DropDownList>

                                             <span style="font-size: 10pt"><span style="color: #000000">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        Year&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtToYear" runat="server" MaxLength="4" Width="100px"></asp:TextBox>
                                        </span></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1">
                                                    </td>
                                                    <td colspan="4" style="height: auto">
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
                                            CellPadding="4" Width="100%" OnRowDataBound="GridView_student_RowDataBound">
                                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                            <Columns>

                              <asp:TemplateField HeaderText="Sl" ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>  
                               
                            <asp:TemplateField HeaderText="SID" ItemStyle-Width="5%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblsid" runat="server" Text='<%# Bind("sid") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="Name" ItemStyle-Width="7%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblSNAME" runat="server" Text='<%# Bind("SNAME") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Contact" ItemStyle-Width="5%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblContact" runat="server" Text='<%# Bind("PHONE") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Dept" ItemStyle-Width="5%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblDEPCODE" runat="server" Text='<%# Bind("DEPCODE") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField> 

                             <asp:TemplateField HeaderText="Last RegYear" ItemStyle-Width="5%" Visible="false"> 
                                <ItemTemplate>
                                                                           
                                </ItemTemplate>
                            </asp:TemplateField>                   
                                                                    
                              <asp:TemplateField HeaderText="LReg Year-Sem" ItemStyle-Width="5%"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblLRegYear" runat="server" Text='<%# Bind("LRY") %>'></asp:Label> <br />
                                   <asp:Label ID="lblLRegSem" runat="server" Text='<%# Bind("LSEMN") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField> 
                                                 
                            <asp:TemplateField HeaderText="TCGPA" ItemStyle-Width="5%" Visible="false"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblTCGPA" runat="server" Text='<%# Bind("TCGPA") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField> 
                                                
                                                                    
                            <asp:TemplateField HeaderText="CGPA" ItemStyle-Width="5%" Visible="false"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblCGPA" runat="server" Text='<%# Bind("CGPA") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>     
                               
                             <asp:TemplateField HeaderText="CGPA" ItemStyle-Width="5%" > 
                                <ItemTemplate>
                                   <asp:Label ID="lblFINAL_CGPA" runat="server" Text='<%# Bind("FINAL_CGPA") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>                     
                                                                               
                             <asp:TemplateField HeaderText="Req CH" ItemStyle-Width="5%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblREQCH" runat="server" Text='<%# Bind("REQCH") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Comp CH" ItemStyle-Width="5%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblCOMP_CHRS" runat="server" Text='<%# Bind("COMP_CHRS") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Wvd CH" ItemStyle-Width="5%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblWCHRS" runat="server" Text='<%# Bind("WCHRS") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Trns CH" ItemStyle-Width="5%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblTCHRS" runat="server" Text='<%# Bind("TCHRS") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>
                                                
                                                                                          
                              <asp:TemplateField HeaderText="TComp CH" ItemStyle-Width="5%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblTOTALCOMPLEDCH" runat="server" Text='<%# Bind("TOTALCOMPLEDCH") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>
                                                
                                                
                             <asp:TemplateField HeaderText="Payable" ItemStyle-Width="5%"  Visible="false"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblPAYABLE" runat="server" Text='<%# Bind("PAYABLE") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>
                                                
                                                                    
                             <asp:TemplateField HeaderText="Paid" ItemStyle-Width="5%"  Visible="false"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblPAID" runat="server" Text='<%# Bind("PAID") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Due" ItemStyle-Width="5%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblDue" runat="server" Text='<%# Bind("DUE") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Email" ItemStyle-Width="5%"  Visible="false"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("EMAIL") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField> 

                                                                   
                            <asp:TemplateField HeaderText="Address" ItemStyle-Width="10%" Visible="true"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("ADDRESS") %>'></asp:Label>                                         
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

