<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_course_teacherEvalListNew.aspx.cs" Inherits="admin_course_teacherEvalListNew"  %>

<script runat="server">

   
</script>

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
    <table border="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="100%">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                           </td>
                        <td  class="k" height="1"  width="100%">
                            </td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            </td>
                    </tr>
                    <tr>
                        <td  class="h" height="22"  width="100%">
                            <p align="center">
                                <b><font  face="Arial" size="2">Teacher Evaluation</font></b></p>
                            <hr style="color: #333333" />
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
          <asp:GridView ID="grdTeacherEve" runat="server" 
          AutoGenerateColumns="False" ShowHeader="true" onrowediting="grdTeacherEve_RowEditing"
          OnPageIndexChanging="grdTeacherEve_PageIndexChanging"
           CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" PageSize="250"
            onrowdatabound="grdTeacherEve_RowDataBound" >
           <Columns>
           
  <asp:TemplateField  visible="false">                         
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Bind("TEACHER_ID") %>' ID="lblTEACHER_ID"></asp:Label>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="left" />
    <HeaderStyle HorizontalAlign="Center" />
 </asp:TemplateField>     
 
     <asp:TemplateField HeaderText="Teacher" ItemStyle-Width="10%" HeaderStyle-Font-Size="Small" >                        
         <ItemTemplate>
         <asp:Label runat="server" Text='<%# Bind("Staff_Name") %>' ID="lblStaff_Name"></asp:Label>
         </ItemTemplate>
          <ItemStyle HorizontalAlign="left" />
          <HeaderStyle HorizontalAlign="Center" />
         </asp:TemplateField>                               
 
        
    <asp:TemplateField  HeaderText="Courses  | Evaluated  |Course Eval | Calculation"  ItemStyle-Width="80%" HeaderStyle-Font-Size="Smaller">
    <ItemTemplate>
    
     <asp:GridView ID="gdEvalutiondtl" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                 DataKeyNames="COURSE_TEACHER_ID" AllowPaging="True"  EmptyDataText="No rows returned"
                 BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                 CellPadding="3" Width="100%"  PageSize="30"  
                 OnRowEditing="gdEvalutiondtl_RowEditing">
                 <Columns>

  <asp:TemplateField  Visible="false">                        
         <ItemTemplate>
              <asp:Label runat="server" Text='<%# Bind("COURSE_TEACHER_ID") %>' ID="lblCOURSE_TEACHER_ID"></asp:Label>            
         </ItemTemplate>
          <ItemStyle HorizontalAlign="left" />
          <HeaderStyle HorizontalAlign="Center" />
     </asp:TemplateField>  


<asp:TemplateField HeaderText="Courses(grp)" ItemStyle-Width="27%">                   
    <ItemTemplate>
    <asp:LinkButton ID="lnkDetail" runat="server" CausesValidation="false" CommandName="Edit"
    Text='<%#Bind("COURSEGRP") %>' Font-Bold="true" ></asp:LinkButton>
    </ItemTemplate>
    <ItemStyle Width="100px" />
</asp:TemplateField>   
                                    
 <asp:TemplateField HeaderText="Courses(grp)" ItemStyle-Width="30%" Visible="false">                        
         <ItemTemplate>
              <asp:Label runat="server" Text='<%# Bind("COURSEGRP") %>' ID="lblCOURSE"></asp:Label>            
         </ItemTemplate>
          <ItemStyle HorizontalAlign="left" />
          <HeaderStyle HorizontalAlign="Center" />
     </asp:TemplateField>

<asp:TemplateField HeaderText="Evaluated" ItemStyle-Width="20%">                        
     <ItemTemplate>
          <asp:Label runat="server" Text='<%# Bind("EVALUTESTUDENT") %>' ID="lblEVALUTESTUDENT"></asp:Label>
          <asp:Label runat="server" Text=" of " ID="lblof"></asp:Label>
           <asp:Label runat="server" Text='<%# Bind("CAPACITY") %>' ID="lblCAPACITY"></asp:Label>
     </ItemTemplate>
      <ItemStyle HorizontalAlign="center" />
      <HeaderStyle HorizontalAlign="Center" />
 </asp:TemplateField>



<asp:TemplateField HeaderText="Course Eval" ItemStyle-Width="15%">                        
     <ItemTemplate>
          <asp:Label runat="server" Text='<%# Bind("AvgMark") %>' ID="lblAvgMark"></asp:Label>         
     </ItemTemplate>
      <ItemStyle HorizontalAlign="center" />
      <HeaderStyle HorizontalAlign="Center" />
 </asp:TemplateField>
 
 <asp:TemplateField HeaderText="calculation" ItemStyle-Width="38%">                        
     <ItemTemplate>
     <asp:Label runat="server" Text='<%# Bind("AvgMark") %>' ID="lblEve"></asp:Label>
     <asp:Label runat="server" Text="*" ID="Label4"></asp:Label>
          <asp:Label runat="server" Text='<%# Bind("EVALUTESTUDENT") %>' ID="lblstd"></asp:Label>
          <asp:Label runat="server" Text="=" ID="lbleuation"></asp:Label>
           <asp:Label runat="server" Text='<%# Bind("TOTALMARK1") %>' ID="lblTOTALMARK"></asp:Label>
     </ItemTemplate>
      <ItemStyle HorizontalAlign="left" />
      <HeaderStyle HorizontalAlign="Center" />
 </asp:TemplateField>

 </Columns>


                          

                 <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#696B8B" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#000066" />
                                <AlternatingRowStyle BackColor="AliceBlue" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />


             </asp:GridView>
       
    </ItemTemplate>
    <EditItemTemplate>
    
    </EditItemTemplate>
    <ItemStyle HorizontalAlign="left" />
   <HeaderStyle HorizontalAlign="center" />
    </asp:TemplateField>                                
         
         
                                    
  <asp:TemplateField HeaderText="Final Eval [sum(point) / sum(Response)]" ItemStyle-Width="10%" HeaderStyle-Font-Size="XX-Small">                        
         <ItemTemplate>
         <asp:Label runat="server" Text='<%# Bind("FinalEvaluation") %>' ID="lblFinalEve"></asp:Label>
         </ItemTemplate>
          <ItemStyle HorizontalAlign="Center" />
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
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

