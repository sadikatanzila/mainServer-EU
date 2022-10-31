<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_semCourseList_Summary.aspx.cs" Inherits="admin_semCourseList_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <script language="javascript" type="text/javascript">

        function chech_valid() {
            if (document.getElementById("ctl00_ContentPlaceHolder_definition_txt_year").value.toString() == "") {
                alert("Please enter the year");
                return false;
            }
            else
                return true;
        }

  </script> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">

    <table border="0"  width="100%">
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
                                <b><font  face="Arial" size="2">Course List Summary</font></b></p>
                              <hr style="color: #333333" />
                        </td>
                    </tr>
                    <tr>
                        <td class="k" height="1" width="100%">
                          </td>
                    </tr>
                    <tr>
                        <td class="k" style="height: 114px" width="1">
                            </td>
                        <td bgcolor="white" style="height: 114px" width="18">
                          </td>
                        <td bgcolor="#ffffff" style="vertical-align: top" width="100%">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <table>
                                            <tr>
                                                <td style="width: 45px; height: auto">
                                                    Select</td>
                                                <td style="width: auto; height: auto">
                                                    <asp:DropDownList ID="cmb_semester" runat="server">
                                                        <asp:ListItem Value="1">Spring</asp:ListItem>
                                                        <asp:ListItem Value="2">Summer</asp:ListItem>
                                                        <asp:ListItem Value="3">Fall</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td style="width: 35px; height: 22px">
                                                    Year</td>
                                                <td style="width: 49px; height: 22px">
                                                    <asp:TextBox ID="txt_year" runat="server" MaxLength="4" Width="40px"></asp:TextBox></td>
                                                <td style="width: 102px; height: 22px">
                                                    <asp:Button ID="btn_submit" runat="server"  Text="Submit" OnClick="btn_submit_Click" /></td>
                                            </tr>
                                        </table>
                                        <hr />
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text=""></asp:Label><br />
                                       
                                       
                               
                                    <table style="width:100%">
                                        <tr>
                                            <td style="text-align: center">
                                                 <asp:Label ID="lblmsg" runat="server" Font-Size="Large" ForeColor="Blue" Text="" ></asp:Label>

                                            </td>
                                        </tr>

                                    </table>
                                        <asp:GridView ID="grd_CourseList" runat="server" AutoGenerateColumns="False" Width="98%"
                                            CellPadding="4" ForeColor="#333333" GridLines="None"  
                                            OnRowEditing="GridView_courseList_RowEditing" onrowdatabound="GridView_courseList_RowDataBound">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Smaller" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                 <asp:TemplateField  HeaderText="Sl" ItemStyle-Width="3%">                        
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Bind("serial") %>' ID="lblSerial"></asp:Label>            
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField> 


                                                 <asp:TemplateField HeaderText="Code" ItemStyle-Width="7%" >                        
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Bind("COURSECODE") %>' ID="lblCOURSECODE"></asp:Label>            
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField> 

                                                 <asp:TemplateField  Visible="false">                        
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Bind("COURSEKEY") %>' ID="lblCOURSEKEY"></asp:Label>            
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="Code"  Visible="false"  >                   
                                                        <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDetail" runat="server" CausesValidation="false" CommandName="Edit"
                                                        Text='<%#Bind("COURSECODE") %>' Font-Bold="true" ></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" />
                                              </asp:TemplateField>  

                                              
                                                <asp:TemplateField  HeaderText="Name" ItemStyle-Width="20%">                        
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Bind("CNAME") %>' ID="lblCNAME"></asp:Label>            
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField> 

 <asp:TemplateField  HeaderText="Teacher      |Section           |Class 1       |Class 2        |Taken/Capacity    "  ItemStyle-Width="70%" >
    <ItemTemplate>


        <asp:GridView ID="gdCourseOfferingdtl" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                 DataKeyNames="COURSEKEY" AllowPaging="True"  EmptyDataText="No rows returned" 
                 BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                 CellPadding="3" Width="100%"  PageSize="150">
                 <Columns>
<asp:TemplateField Visible="false" >                        
        <ItemTemplate>
            <asp:Label runat="server" Text='<%# Bind("COURSECODE") %>' ID="lbl_COURSECODE"></asp:Label>            
        </ItemTemplate>
        <ItemStyle HorizontalAlign="left" />
        <HeaderStyle HorizontalAlign="Center" />
    </asp:TemplateField> 
  <asp:TemplateField  Visible="false">                        
         <ItemTemplate>
              <asp:Label runat="server" Text='<%# Bind("COURSEKEY") %>' ID="lblCOURSEKEY"></asp:Label>            
         </ItemTemplate>
          <ItemStyle HorizontalAlign="left" />
          <HeaderStyle HorizontalAlign="Center" />
     </asp:TemplateField>  

<asp:TemplateField HeaderText="Teacher" ItemStyle-Width="30%">                   
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Bind("STAFF_NAME") %>' ID="lblSTAFF_NAME"></asp:Label>    

    </ItemTemplate>
    <ItemStyle Width="100px" />
</asp:TemplateField>   
 
                                    
 <asp:TemplateField HeaderText="Section" ItemStyle-Width="10%" >                        
         <ItemTemplate>
              <asp:Label runat="server" Text='<%# Bind("SECTION") %>' ID="lblSECTION"></asp:Label>            
         </ItemTemplate>
          <ItemStyle HorizontalAlign="left" />
          <HeaderStyle HorizontalAlign="Center" />
     </asp:TemplateField>

<asp:TemplateField HeaderText="Class 1" ItemStyle-Width="20%">                        
     <ItemTemplate>
          <asp:Label runat="server" Text='<%# Bind("SCH_CLS_1") %>' ID="lblSCH_CLS_1"></asp:Label>
         
     </ItemTemplate>
      <ItemStyle HorizontalAlign="center" />
      <HeaderStyle HorizontalAlign="Center" />
 </asp:TemplateField>


<asp:TemplateField HeaderText="Class 2" ItemStyle-Width="20%">                        
     <ItemTemplate>
          <asp:Label runat="server" Text='<%# Bind("SCH_CLS_2") %>' ID="lblSCH_CLS_2"></asp:Label>
         
     </ItemTemplate>
      <ItemStyle HorizontalAlign="center" />
      <HeaderStyle HorizontalAlign="Center" />
 </asp:TemplateField>



 <asp:TemplateField HeaderText="Capacity" ItemStyle-Width="10%">                        
     <ItemTemplate>
          <asp:Label runat="server" Text='<%# Bind("TakenStd") %>' ID="lblTakenStd"></asp:Label>/
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
          </ItemTemplate>
    <EditItemTemplate>
  </EditItemTemplate>
    <ItemStyle HorizontalAlign="left" />
   <HeaderStyle HorizontalAlign="center" />
    </asp:TemplateField>   


                                            </Columns>
                                            <EditRowStyle BackColor="White" />
                                        </asp:GridView>
                                   
                                   
                                   
                                    </td>
                                </tr>
                            </table>
                            &nbsp;</td>
                        <td bgcolor="white" style="height: 114px" width="14">
                           
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

