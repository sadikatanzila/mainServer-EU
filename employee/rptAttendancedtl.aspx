<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="rptAttendancedtl.aspx.cs" Inherits="employee_rptAttendancedtl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">

    <table border="0" style="width:98%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="100%">
                    <tr>
                        <td colspan="2" height="24" rowspan="2" width="19">
                            </td>
                        <td  class="k" height="1" width="100%">
                            </td>
                        <td align="right" colspan="2" height="24" rowspan="2" width="15">
                            </td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="100%">

                            <table border="0" cellpadding="0" cellspacing="0" height="1" width="100%">
                    <tr>
                        <td colspan="2" height="24" rowspan="2" width="19">
                           </td>
                        <td  class="h" height="22"  width="100%">
                            <p align="center">
                                <b><font  face="Arial" size="2">Course wise Class Attendance Sheet</font></b></p>
                            <hr style="color: #333333" />
                        </td>
                        <td align="right" colspan="2" height="24" rowspan="2" width="15">
                            </td>
                    </tr>
                    <tr>
                        <td class="k" height="1"  width="100%">
                           </td>
                    </tr>

                    
                    <tr>
                        <td  class="k" style="height: 114px" width="1">
                            </td>
                        <td bgcolor="white" style="height: 114px" width="18">
                           </td>
                        <td bgcolor="#ffffff" style="vertical-align: top"  width="100%">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <table id="Table1" runat="server" width="100%">
                                            <tr>
                                                <td colspan="4">
                                                   <asp:Panel ID="pnlAdmin" runat="server">
                                                        <table id="Table2" runat="server" width="100%">

                                                            <tr style="font-size: 8pt; color: #000000">
                                                <td colspan="4">
                                                    Faculty: &nbsp;&nbsp;<asp:DropDownList ID="cmb_faculty" runat="server"  AutoPostBack="true"
                                                        onselectedindexchanged="cmb_faculty_SelectedIndexChanged">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td colspan="4">
                                                    Teacher: <asp:DropDownList ID="cmbTeacher" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td colspan="4">
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
                                                <td colspan="4">
                                                    <asp:Button ID="btn_show" runat="server" OnClick="btn_show_Click" Text="Show" />
													</td>
                                            </tr>
                                                            </table>

                                                   </asp:Panel>

                                                     <asp:Panel ID="pnlDept" runat="server">
<table id="Table3" runat="server" width="100%">

                                                            
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td colspan="4">
                                                    Teacher: <asp:DropDownList ID="ddlTeacher" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td colspan="4">
                                                    Semester
                                                    <asp:DropDownList ID="cmb_semester" runat="server">
                                                        <asp:ListItem Value="1">Spring</asp:ListItem>
                                                        <asp:ListItem Value="2">Summer</asp:ListItem>
                                                        <asp:ListItem Value="3">Fall</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp; Year
                                                    <asp:TextBox ID="txt_year" runat="server" MaxLength="4" Width="87px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td colspan="4">
                                                    <asp:Button ID="btnShow" runat="server" OnClick="btn_show_Click" Text="Show" />
													</td>
                                            </tr>
                                                            </table>


                                                     </asp:Panel>
                                                </td>
                                            </tr>
                                            
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td colspan="4">
													<asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" ></asp:Label></td>
                                            </tr>
                                        </table>
                                        <br />
                                        <asp:Label ID="lbl_message" runat="server"  ForeColor="Red" Text=""></asp:Label><hr />
          <asp:GridView ID="grdTeacherEve" runat="server"  AutoGenerateColumns="False" ShowHeader="true"
           CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" PageSize="250" BorderWidth="1px"
               OnRowEditing="grdTeacherEve_RowEditing" OnRowDataBound="grdTeacherEve_RowDataBound1"  >
           <Columns>
           
  <asp:TemplateField  visible="false">                         
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Bind("TEACHER_ID") %>' ID="lblTEACHER_ID"></asp:Label>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="left" />
    <HeaderStyle HorizontalAlign="Center" />
 </asp:TemplateField>     
    <asp:TemplateField HeaderText="Sl" ItemStyle-Width="5%" >
                        <ItemTemplate>
                            <asp:Label ID="lblSerial" runat="server" ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
        </asp:TemplateField>  
     <asp:TemplateField HeaderText="Teacher" ItemStyle-Width="10%"  >                        
         <ItemTemplate>
         <asp:Label runat="server" Text='<%# Bind("Staff_Name") %>' ID="lblStaff_Name"></asp:Label>
         </ItemTemplate>
          <ItemStyle HorizontalAlign="left" />
          <HeaderStyle HorizontalAlign="Center" />
         </asp:TemplateField>                               
   <asp:TemplateField HeaderText="ID" ItemStyle-Width="8%"  >                        
         <ItemTemplate>
         <asp:Label runat="server" Text='<%# Bind("VALUE") %>' ID="lblVALUE"></asp:Label>
         </ItemTemplate>
          <ItemStyle HorizontalAlign="Center" />
          <HeaderStyle HorizontalAlign="Center" />
         </asp:TemplateField>  
        <asp:TemplateField  Visible="false">                        
         <ItemTemplate>
              <asp:Label runat="server" Text='<%# Bind("COURSE_TEACHER_ID") %>' ID="lblCOURSE_TEACHER_ID"></asp:Label>            
         </ItemTemplate>
          <ItemStyle HorizontalAlign="left" />
          <HeaderStyle HorizontalAlign="Center" />
     </asp:TemplateField>  

  
<asp:TemplateField HeaderText="Courses Code" ItemStyle-Width="15%">                   
    <ItemTemplate>
    <asp:LinkButton ID="lnkDetail" runat="server" CausesValidation="false" CommandName="Edit"
    Text='<%#Bind("COURSECODE") %>' Font-Bold="true" ></asp:LinkButton>
        (<asp:Label runat="server" Text='<%# Bind("SECTION") %>' ID="lblSECTION"></asp:Label>)
    </ItemTemplate>
    <ItemStyle Width="100px" />
</asp:TemplateField>   
                                    



   <asp:TemplateField HeaderText="Name" ItemStyle-Width="20%" >                        
         <ItemTemplate>
              <asp:Label runat="server" Text='<%# Bind("CNAME") %>' ID="lblCNAME"></asp:Label>            
         </ItemTemplate>
          <ItemStyle HorizontalAlign="left" />
          <HeaderStyle HorizontalAlign="Center" />
     </asp:TemplateField>

  <asp:TemplateField HeaderText="CH" ItemStyle-Width="5%" >                        
         <ItemTemplate>
              <asp:Label runat="server" Text='<%# Bind("CHOURS") %>' ID="lblCHOURS"></asp:Label>            
         </ItemTemplate>
          <ItemStyle HorizontalAlign="Center" />
          <HeaderStyle HorizontalAlign="Center" />
     </asp:TemplateField>

<asp:TemplateField HeaderText="Class 1" ItemStyle-Width="15%">                        
     <ItemTemplate>
          <asp:Label runat="server" Text='<%# Bind("SCH_CLS_1") %>' ID="lblcls1"></asp:Label>
     </ItemTemplate>
      <ItemStyle HorizontalAlign="center" />
      <HeaderStyle HorizontalAlign="Center" />
 </asp:TemplateField>



<asp:TemplateField HeaderText="Class 2" ItemStyle-Width="15%">                        
     <ItemTemplate>
          <asp:Label runat="server" Text='<%# Bind("SCH_CLS_2") %>' ID="lblcls2"></asp:Label>         
     </ItemTemplate>
      <ItemStyle HorizontalAlign="center" />
      <HeaderStyle HorizontalAlign="Center" />
 </asp:TemplateField>
                                    
         
         
     <asp:TemplateField HeaderText="Student" ItemStyle-Width="10%">                        
     <ItemTemplate>
          <asp:Label runat="server" Text='<%# Bind("TOTAL_STUDENT") %>' ID="lblTOTAL_STUDENT"></asp:Label>  /
          <asp:Label runat="server" Text='<%# Bind("TOTAL_CAPACITY") %>' ID="lblTOTAL_CAPACITY"></asp:Label>         
     </ItemTemplate>
      <ItemStyle HorizontalAlign="center" />
      <HeaderStyle HorizontalAlign="Center" />
 </asp:TemplateField>                               
                        
                                    
   
                                    
                                    
                                    
                                           </Columns>
        <AlternatingRowStyle BackColor="White" BorderColor="Silver" BorderStyle="Inset"
            BorderWidth="1px" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#E3EAEB" />
        <EditRowStyle BackColor="#7C6F57" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#6699FF" Font-Bold="True" ForeColor="White" />
   </asp:GridView>
                                        
                                        
                                        
                                    </td>
                                </tr>
                            </table>
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
                        <td bgcolor="white" height="1"  width="100%">
                          </td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                           </td>
                    </tr>
                    <tr>
                        <td class="k" height="1"  width="100%">
                          </td>
                    </tr>
                </table>


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
                                        
                                        


                                         <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" Visible="true"
        BestFitPage="False" ToolPanelView="None" />    
                                    </td>
                                </tr>

                                 <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: center">
                                      
                                        <br /><br />


                                    </td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        &nbsp;</td>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

