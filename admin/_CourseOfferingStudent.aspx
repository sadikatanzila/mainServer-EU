<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_CourseOfferingStudent.aspx.cs" Inherits="admin_CourseOfferingStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">

    <script type="text/javascript">
        function SelectedTextValue(ele) {
            if (ele.selectedIndex > 0) {
                var selectedText = ele.options[ele.selectedIndex].innerHTML;
                var selectedValue = ele.value;
                document.getElementById("txtContent").value = selectedValue;
            }
            else {
                document.getElementById("txtContent").value = "";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    

    <table   style="width:85%; text-align: left;">
        <tr>
            <td>
<p style="font-size:large; text-align:center; font-weight:bold">Student Course Offering</p>
                <hr />
                </td>
            <td>


            </td>
        </tr>
        <tr>
            <td>

                <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text=""></asp:Label>
                <br />
                <asp:Label ID="lbl_Error" runat="server" Font-Bold="True" ForeColor="Red" Text=""></asp:Label>
                <br />
                <asp:Label ID="lbl_Approval" runat="server" Font-Bold="True" ForeColor="Red" Text=""></asp:Label>
            </td>
          
        </tr>
        <tr>
            <td>

                Student ID:&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtSID" runat="server" AutoPostBack="true" OnTextChanged="txtSID_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblStudent" runat="server" Text=""></asp:Label>
            </td>
          
        </tr>
        </table>
    <asp:Panel ID="pnlViewStudentInfo" runat="server" Visible="false">
    
         <table   style="width:85%; text-align: left;">
        <tr>
            <td>

                Select Semester:

                <asp:DropDownList ID="cmb_semester" runat="server" Height="16px" Width="150px">
                    <asp:ListItem Value="1">Spring</asp:ListItem>
                    <asp:ListItem Value="2">Summer</asp:ListItem>
                    <asp:ListItem Value="3">Fall</asp:ListItem>
                </asp:DropDownList>
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Year:&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtYear" runat="server" ></asp:TextBox>
               
                
            </td>
            <td>


                &nbsp;</td>
        </tr>
        <tr>
            <td>

                &nbsp;</td>
            <td>


                &nbsp;</td>
        </tr>
        <tr>
            <td>

                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="View Courses" />
                 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                 <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear All" />
            </td>
            <td>


                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
<!---
                 <asp:GridView ID="GridView_CourseOffering" runat="server" AutoGenerateColumns="False"   
            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"   
            CellPadding="3" DataKeyNames="id" GridLines="Vertical"   
           >  
            <AlternatingRowStyle BackColor="#DCDCDC" />  
            <Columns>  
                
                <asp:TemplateField HeaderText="Course(s)">  
                    <EditItemTemplate>  
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>  
                    </EditItemTemplate>  
                    <ItemTemplate>  
                        <asp:DropDownList ID="DropDownList1" runat="server">  
                        </asp:DropDownList>  
                    </ItemTemplate>  
                </asp:TemplateField>  

                <asp:TemplateField HeaderText="Credit Hour">  
                    <EditItemTemplate>  
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("id") %>'></asp:TextBox>  
                    </EditItemTemplate>  
                    <ItemTemplate>  
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("id") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Section">  
                    <EditItemTemplate>  
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("name") %>'></asp:TextBox>  
                    </EditItemTemplate>  
                    <ItemTemplate>  
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("name") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="1st class"> 
                   
                    <ItemTemplate>  
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("city") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField> 
                
                 <asp:TemplateField HeaderText="2nd class">  
                   
                    <ItemTemplate>  
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("city") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  

                <asp:TemplateField HeaderText="CourseKey">  
                   
                    <ItemTemplate>  
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("city") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField> 

                <asp:TemplateField HeaderText="Enrolled">  
                   
                    <ItemTemplate>  
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("city") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField> 
            </Columns>  
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />  
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />  
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />  
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />  
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />  
            <SortedAscendingCellStyle BackColor="#F1F1F1" />  
            <SortedAscendingHeaderStyle BackColor="#0000A9" />  
            <SortedDescendingCellStyle BackColor="#CAC9C9" />  
            <SortedDescendingHeaderStyle BackColor="#000065" />  
        </asp:GridView> 
    -->
           
            </td>
        </tr>
       
        </table>

    <asp:Panel ID="pnlCourseTaken" runat="server" Visible="false">
          <table   style="width:85%; text-align: left;">
         <tr>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="lblTaken" runat="server" Text="Taken Courses" Font-Bold="True" Font-Size="Large" ForeColor="#666666"></asp:Label>
                 <hr />
                </td>
        </tr>
        <tr>
            <td colspan="2">
                

               <asp:GridView ID="GridView_taken_list" runat="server" AutoGenerateColumns="False" margin-left="50px"
                                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="1" Width="85%">
                                            <Columns>
                                              
                                          <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>        
                  <asp:TemplateField HeaderText="Course" >
                         <ItemTemplate>
                             <asp:Label runat="server" Text='<%# Bind("cName") %>' ID="lblCourse"></asp:Label>
                          <br />   <asp:Label runat="server" Text='<%# Bind("course") %>' ID="lblCourseID"></asp:Label>
                         </ItemTemplate>
                          <ItemStyle HorizontalAlign="left" />
                          <HeaderStyle HorizontalAlign="left" />
                       <ItemStyle Width="25%" />
                     </asp:TemplateField>
                                                
                                             
                                              
                                                <asp:BoundField DataField="CHOURS" HeaderText="Credit">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                     <ItemStyle Width="7%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GGROUP" HeaderText="Group">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                     <ItemStyle Width="10%" />
                                                </asp:BoundField>
                                                
                                                     
                                                
                                                <asp:BoundField DataField="SCH_CLS_1" HeaderText="1st Class ">
                                                    <ItemStyle Width="20%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SCH_CLS_2" HeaderText="2nd Class">
                                                    <ItemStyle Width="20%" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="SCH_CLS_3" HeaderText="3rd Class">
                                                    <ItemStyle Width="7%" />
                                                </asp:BoundField>
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="COURSEKEY" runat="server" Text='<%# Bind("COURSEKEY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="REGKEY" runat="server" Text='<%# Bind("REGKEY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enrolled">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTOTAL_STUDENT" runat="server" Text='<%# Bind("TOTAL_STUDENT") %>'></asp:Label>/
                                                         <asp:Label ID="lblTOTAL_CAPACITY" runat="server" Text='<%# Bind("TOTAL_CAPACITY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle Width="15%" />
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="available_seat" HeaderText="Enrolled"  Visible="false"/>
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <RowStyle ForeColor="#000066" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="AntiqueWhite" />
                                        </asp:GridView>
               

                <p style="margin-left:75%"><asp:Label ID="lbl_total_credit" runat="server" Font-Bold="True"></asp:Label></p>

                 
               





            </td>
        </tr>
        <tr>
            <td style="text-align: left;">

              
               
               <p style=" margin-left:20%">
                    Enrolled = Taken/Capacity<br />
                    <asp:Button ID="btn_deleteCourse" runat="server" OnClick="btn_deleteCourse_Click" Visible="false"
                                            Text="Delete Course" />

                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnModifyCourse" runat="server" OnClick="btnModifyCourse_Click"
                                            Text="Modify Course" />
               </p>

                 

            </td>
            <td style="text-align: right">
               
            </td>
        </tr>

        </table>
       </asp:Panel>
        <asp:Panel ID="pnlCourseOffer" runat="server" Visible="false">
               

                 <table  runat="server" border="0" cellpadding="0" cellspacing="0"
                    height="1" width="85%">
                    <tr>
                       <td>


                        </td>
                    </tr>
                    <tr>
                        <td>


                        </td>
                    </tr>
                   
                    <tr>
                         <td>


                        
                                <br />
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td colspan="3" style="text-align: center">
                                            <strong><span>&nbsp;</span></strong></td>
                                    </tr>
                                    <tr  style="text-align:center">
                                        <td colspan="3">
                                            <p style="font-size:large" >
                                <b>Add New Course</b></p>
 <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Select</td>
                                        <td>
                                        </td>
                                        <td  style="text-align:left">
                                            <asp:DropDownList ID="cmb_course" runat="server"  Width="250px">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr  style="text-align:left">
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btn_show" runat="server" OnClick="btn_show_Click" Text="Show" />&nbsp;<asp:Button
                                                ID="btn_prerequisit" runat="server" OnClick="btn_prerequisit_Click" Text="Prerequisite" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_course" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="Red"
                                                Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="color: #0000ff">
                                        
                                        
                                       
                                        
                                            <asp:GridView ID="GridView_availableCourses" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_select" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="STAFF_NAME" HeaderText="Teacher" />
                                                    <asp:BoundField DataField="CHOURS" HeaderText="Credit">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Group">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_group" runat="server" Text='<%# Bind("SECTION") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="SCH_CLS_1" HeaderText="1st Class ">
                                                        <ItemStyle Width="60px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SCH_CLS_2" HeaderText="2nd Class">
                                                        <ItemStyle Width="60px" />
                                                    </asp:BoundField>
                                                    
                                                   
                                                   <asp:BoundField DataField="SCH_CLS_3" HeaderText="3rd Class">
                                                        <ItemStyle Width="60px" />
                                                    </asp:BoundField>
                                                   
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_course_key" runat="server" Text='<%# Bind("COURSE_KEY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    
                                                    
                                                    <asp:BoundField DataField="available_seat" HeaderText="Enrolled" />
                                                    
                                                    
                                                    
                                                </Columns>
                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <RowStyle ForeColor="#000066" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="AntiqueWhite" />
                                            </asp:GridView>
                                            <asp:Label ID="lbl_advice" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr style="text-align:left">
                                        <td style="height: 19px">
                                            &nbsp;</td>
                                        <td style="height: 19px">
                                            &nbsp;</td>
                                        <td style="height: 19px">
                                            Enrolled = Offered/Capacity</td>
                                    </tr>
                                    <tr  style="text-align:left">
                                        <td style="height: 19px">
                                        </td>
                                        <td style="height: 19px">
                                        </td>
                                        <td style="height: 19px">
                                            <asp:Button ID="btn_add" runat="server" OnClick="btn_add_Click" Text="Add for Final Approval" Height="30px" /></td>
                                    </tr>
                                </table>
                            </div>
                            &nbsp;
                        </td>
                       
                    </tr>
                    <tr>
                       <td>

                       </td>
                    </tr>
                    <tr>
                       <td>

                       </td>
                    </tr>
                </table>
          

    </asp:Panel>
           </asp:Panel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

