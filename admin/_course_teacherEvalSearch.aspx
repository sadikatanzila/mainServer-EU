<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_course_teacherEvalSearch.aspx.cs" Inherits="admin_course_teacherEvalSearch" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">

<script language="javascript" type="text/javascript">
  
       function goto_eval_details(courseT)
        {
          window.open('_course_teacherEvalDetails.aspx?ids='+courseT,'','titlebar=no,toolbar=no,,resizable=false,height=600,width=700');
          return false;
        } 
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0" style="width: 100%">
        <tr>
            <td style="width: 100%">
                <table border="0" cellpadding="0" cellspacing="0" height="1" style="width: 100%">
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
                                <b><font  face="Arial" size="2">Teacher Evaluation Summery Search</font></b></p>
                            <hr style="color: #333333" />
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

                                        <asp:Panel ID="pnlOffice" runat="server">
                                            <table id="Table2" runat="server" width="100%">
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                                 <tr style="font-size: 8pt; color: #000000">
                                                <td>
Semester
                                                    <asp:DropDownList ID="cmb_s_semester" runat="server">
                                                        <asp:ListItem Value="1">Spring</asp:ListItem>
                                                        <asp:ListItem Value="2">Summer</asp:ListItem>
                                                        <asp:ListItem Value="3">Fall</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp; Year
                                                    <asp:TextBox ID="txt_s_year" runat="server" MaxLength="4" Width="82px"></asp:TextBox>
                                                    </td>
                                                     </tr>

                                                <tr style="font-size: 8pt; color: #000000">
                                                <td>
                                                    Evaluation Mark <asp:TextBox ID="txtMark" runat="server" MaxLength="4" Width="82px"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlMark" runat="server">
                                                        <asp:ListItem Value="1"><=</asp:ListItem>
                                                        <asp:ListItem Value="2">>=</asp:ListItem>
                                                        <asp:ListItem Value="3"><</asp:ListItem>
                                                        <asp:ListItem Value="4">></asp:ListItem>
                                                        
                                                    </asp:DropDownList>
                                                   
                                                     </td>
                                                     </tr>
                                                <tr style="font-size: 8pt; color: #000000">
                                                <td>
                                                     <asp:Button ID="btn_show" runat="server" OnClick="btn_show_Click" Text="Show" />
													<asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" ></asp:Label>
                                                   </td>
                                                    </tr>

                                                <tr style="font-size: 8pt; color: #000000">
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblHeading" runat="server" Font-Size="Large" ForeColor="#000099" Text=""></asp:Label>
                                                    </td>
                                                </tr>

                                                </table>

                                        </asp:Panel>

                                         
                                        
                                        <br />
           <asp:GridView ID="grdTeacherEve" runat="server" AutoGenerateColumns="False" 
                                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="4" Width="99%" OnRowDataBound="grdTeacherEve_RowDataBound">
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
                                   <asp:Label ID="lblTEACHER_ID" runat="server" Text='<%# Bind("TEACHER_ID") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField>     
                                                
                                                 
                            <asp:TemplateField HeaderText="Name" ItemStyle-Width="20%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblSNAME" runat="server" Text='<%# Bind("STAFF_NAME") %>'></asp:Label>                                         
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

 <asp:TemplateField  HeaderText="Teacher ID" ItemStyle-Width="10%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblVALUE" runat="server" Text='<%# Bind("VALUE") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField> 

<asp:TemplateField  HeaderText="Type" ItemStyle-Width="15%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblTYPE" runat="server" Text='<%# Bind("JOB_TYPE") %>'></asp:Label>                                         
                                </ItemTemplate>
                            </asp:TemplateField> 
                        
                           <asp:TemplateField HeaderText="Department" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblDEPARTMENT" runat="server" Text='<%# Bind("DEPARTMENTNAME") %>'></asp:Label>                                         
                                </ItemTemplate>
                               <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="Evaluation" ItemStyle-Width="15%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblEvaluation" runat="server" Text='<%# Bind("FINALEVALUATION") %>'></asp:Label>                              
                                </ItemTemplate>  
                               <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                           <asp:TemplateField HeaderText="Avg. Evaluation" ItemStyle-Width="15%"> 
                                <ItemTemplate>
                                  
                                     <asp:Label ID="lblAvgEvaluation" runat="server" Text='<%# Bind("AVGEVE") %>'></asp:Label>                                    
                                </ItemTemplate>  
                               <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                                                           
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="#330099" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#6699FF" Font-Bold="True" Font-Size="Small" ForeColor="#FFFFCC" />
                                        </asp:GridView>                           
                                        
                                    </td>
                                </tr>
                            </table>
                            &nbsp;</td>
                        <td bgcolor="white" style="height: 114px; width: 249px;">
                            &nbsp;
                            <p>
                              </p>
                        </td>
                        <td  class="k" style="height: 114px; width: 67px;">
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

