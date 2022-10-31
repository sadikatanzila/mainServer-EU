<%@ Page Language="C#" MasterPageFile="~/staffs/Evaluation/MasterPage_Evaluation.master" AutoEventWireup="true" CodeFile="_show_Evaluation.aspx.cs" Inherits="staffs_Evaluation_show_Evaluation" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
<script language="javascript" type="text/javascript">
  
       function goto_eval_details(courseT)
        {
          window.open('_course_teacherEvalDetails.aspx?ids='+courseT,'','titlebar=yes,toolbar=no,scrollbars,resizable=true,height=600,width=700');
          return false;
        } 
</script>

<table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Teacher Evaluation</span></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
<table border="0">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="539">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                            <img border="0" height="24" src="../images/lcurv.gif" width="19" /></td>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../images/rcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" height="22" width="505">
                            <p align="center">
                                <b><font color="#ffa500" face="Arial" size="2">Teacher Evaluation</font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" style="height: 114px" width="1">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" style="height: 114px" width="18">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="18" /></td>
                        <td bgcolor="#ffffff" style="vertical-align: top" width="505">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <asp:Panel ID="pnlDean" runat="server">
                                             <table id="Table1" runat="server" width="100%">
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td>
                                                    Teacher: <asp:DropDownList ID="cmbTeacher" runat="server">
                                                    </asp:DropDownList>
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
                                                    <asp:TextBox ID="txt_s_year" runat="server" MaxLength="4" Width="38px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td>
                                                    <asp:Button ID="btn_show" runat="server" OnClick="btn_show_Click" Text="Show" />
													<asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" ></asp:Label></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>


                                        </asp:Panel>

                                        <asp:Panel ID="pnlTeacher" runat="server">
                                              <table id="Table2" runat="server" width="100%">
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td>
                                                    Semester
                                                    <asp:DropDownList ID="cmb_semester" runat="server">
                                                        <asp:ListItem Value="1">Spring</asp:ListItem>
                                                        <asp:ListItem Value="2">Summer</asp:ListItem>
                                                        <asp:ListItem Value="3">Fall</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp; Year
                                                    <asp:TextBox ID="txt_year" runat="server" MaxLength="4" Width="38px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td>
                                                    <asp:Button ID="btnShow" runat="server" OnClick="btn_show_Click" Text="Show" />
													<asp:Label ID="lblError_msg" runat="server" Text="" ForeColor="Red" ></asp:Label></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #000000">
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>

                                        </asp:Panel>

                                       
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
         <RowStyle ForeColor="#000066" />
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
                                <img border="0" height="1" src="../images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" style="height: 114px" width="1">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                            <img border="0" height="15" src="../images/blcurv.gif" width="19" /></td>
                        <td bgcolor="white" height="1" width="505">
                            <img border="0" height="14" src="../images/spacer(1).gif" width="1" /></td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            <img border="0" height="15" src="../images/brcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="150" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

