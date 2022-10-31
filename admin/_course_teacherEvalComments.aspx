<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_course_teacherEvalComments.aspx.cs" Inherits="admin_course_teacherEvalComments" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">

    <script language="javascript" type="text/javascript">

        function goto_eval_details(courseT) {
            window.open('_course_teacherEvalDetails.aspx?ids=' + courseT, '', 'titlebar=no,toolbar=no,,resizable=false,height=600,width=700');
            return false;
        }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0" style="width: 95%">
        <tr>
            <td style="width: 105%">
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
                                <b><font  face="Arial" size="2">Teacher Evaluation Comments Summery</font></b></p>
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
                                        <table id="Table1" runat="server" width="100%">
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td>
                                                    Faculty: &nbsp;&nbsp;<asp:DropDownList ID="cmb_faculty" runat="server"  AutoPostBack="true"
                                                        onselectedindexchanged="cmb_faculty_SelectedIndexChanged">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td>
                                                    Teacher: <asp:DropDownList ID="cmbTeacher" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td> Year:
                                                    <asp:TextBox ID="txt_year" runat="server" MaxLength="4" Width="100px"></asp:TextBox>
                                                   &nbsp;&nbsp;&nbsp;&nbsp; Semester:
                                                    <asp:DropDownList ID="cmb_s_semester" runat="server">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Spring</asp:ListItem>
                                                        <asp:ListItem Value="2">Summer</asp:ListItem>
                                                        <asp:ListItem Value="3">Fall</asp:ListItem>
                                                    </asp:DropDownList>
                                                    </td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td>
                                                    <asp:Button ID="btn_show" runat="server" OnClick="btn_show_Click" Text="Show" Width="100px" />
													<asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" ></asp:Label></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                        <br />
          <asp:GridView ID="grdTeacherEve" runat="server" 
          AutoGenerateColumns="False" ShowHeader="true" onrowediting="grdTeacherEve_RowEditing"
          OnPageIndexChanging="grdTeacherEve_PageIndexChanging" BorderWidth="1px" 
           CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" PageSize="250"
               onrowdatabound="grdTeacherEve_RowDataBound" >
           <Columns>
           
 
 
                     
 
        
    <asp:TemplateField  HeaderText="Students' Evaluation Comments"  ItemStyle-Width="85%" >
    <ItemTemplate>
     Name: <asp:Label runat="server" Text='<%# Bind("TEACHER_NAME") %>' ID="lblStaff_Name" ></asp:Label>
    <br />ID:  <asp:Label runat="server" Text='<%# Bind("TEACHER_ID") %>' ID="lblTEACHER_ID"></asp:Label>
     <br /><asp:Label runat="server" Text='<%# Bind("job_designation") %>' ID="lblJobDesg"></asp:Label>
      <br />Dept: <asp:Label runat="server" Text='<%# Bind("DEPARTMENT_NAME") %>' ID="lblDepartment"></asp:Label>
     
     <asp:GridView ID="gdEvalutionCmnts" runat="server" AutoGenerateColumns="False" ShowHeader="true"
                 DataKeyNames="TEACHER_ID" AllowPaging="True"  EmptyDataText="No rows returned"
                 BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                 CellPadding="3" Width="100%"  PageSize="150" onrowdatabound="gdEvalutionCmnts_RowDataBound">
                 <Columns>

 
<asp:TemplateField  Visible="false">                   
    <ItemTemplate>
     <asp:Label runat="server" Text='<%# Bind("TEACHER_ID") %>' ID="lblTeacher"></asp:Label>            
     </ItemTemplate>
    <ItemStyle Width="100px" />
</asp:TemplateField>  

   <asp:TemplateField  Visible="false">                   
    <ItemTemplate>
     <asp:Label runat="server" Text='<%# Bind("COURSE_TEACHER_ID") %>' ID="lblCOURSE_TEACHER_ID"></asp:Label>            
     </ItemTemplate>
    <ItemStyle Width="100px" />
</asp:TemplateField> 

<asp:TemplateField HeaderText="Year" ItemStyle-Width="7%">                   
    <ItemTemplate>
     <asp:Label runat="server" Text='<%# Bind("YEAR") %>' ID="lblYEAR"></asp:Label>            
     </ItemTemplate>
    <ItemStyle Width="100px" />
</asp:TemplateField>   
  <asp:TemplateField  Visible="false">                        
         <ItemTemplate>
              <asp:Label runat="server" Text='<%# Bind("SEMESTER_ID") %>' ID="lblSemisterID"></asp:Label>            
         </ItemTemplate>
          <ItemStyle HorizontalAlign="left" />
          <HeaderStyle HorizontalAlign="Center" />
     </asp:TemplateField>                                  
 <asp:TemplateField HeaderText="Semester" ItemStyle-Width="10%" >                        
         <ItemTemplate>
              <asp:Label runat="server" Text='<%# Bind("SEM") %>' ID="lblSemister"></asp:Label>            
         </ItemTemplate>
          <ItemStyle HorizontalAlign="left" />
          <HeaderStyle HorizontalAlign="Center" />
     </asp:TemplateField>

<asp:TemplateField HeaderText="Course" ItemStyle-Width="12%" >                        
         <ItemTemplate>
              <asp:Label runat="server" Text='<%# Bind("COURSENAME") %>' ID="lblCourse"></asp:Label> 
              (<asp:Label runat="server" Text='<%# Bind("SECTION") %>' ID="lblSECTION"></asp:Label>)           
         </ItemTemplate>
          <ItemStyle HorizontalAlign="left" />
          <HeaderStyle HorizontalAlign="Center" />
     </asp:TemplateField>



<asp:TemplateField HeaderText="Student Comments" ItemStyle-Width="72%">                        
     <ItemTemplate>
         <asp:GridView ID="gdEvalutionCmntsdtl" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                 DataKeyNames="COURSE_TEACHER_ID" AllowPaging="True"  EmptyDataText="No rows returned" 
                 BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                 CellPadding="3" Width="100%"  PageSize="200" >
                 <Columns>
 <asp:TemplateField Visible="false"  >                        
         <ItemTemplate>
               <asp:Label runat="server" Text='<%# Bind("SID") %>' ID="lblSID"></asp:Label>        
         </ItemTemplate>
          <ItemStyle HorizontalAlign="left" />
          <HeaderStyle HorizontalAlign="Center" />
     </asp:TemplateField>  

  <asp:TemplateField  ItemStyle-Width="95%">                        
         <ItemTemplate>
               <asp:Label runat="server" Text='<%# Bind("ST_COMMENTS") %>' ID="lblFINALEVALUATION"></asp:Label>        
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
      <ItemStyle HorizontalAlign="left" />
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
       
    </ItemTemplate>
    <EditItemTemplate>
    
    </EditItemTemplate>
    <ItemStyle HorizontalAlign="left" />
   <HeaderStyle HorizontalAlign="center" />
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

