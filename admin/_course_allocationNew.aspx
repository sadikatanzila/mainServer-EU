<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_course_allocationNew.aspx.cs" Inherits="admin_course_allocationNew" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">

  
   <script runat="server">

    void Button1_Click(object sender, EventArgs e)
    {
        // Do some other processing...

        StringBuilder sb = new StringBuilder();
        sb.Append("<script>");
        sb.Append("window.open('../admin/_login.aspx', '', '');");
        sb.Append("</scri");
        sb.Append("pt>");

        Page.RegisterStartupScript("test", sb.ToString());
    }

   
</script>




<table border="0" width="100%">
        <tr>
            <td colspan="3" style="text-align: left">
                <asp:Label ID="lbl_message" 
                runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="Red" Text="Label"></asp:Label>

                <br />
                 <asp:Label ID="lbl_msg" 
                runat="server" Font-Bold="True"  ForeColor="Red" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="100%">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                            </td>
                        <td class="k" height="1" width="100%">
                            </td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                           </td>
                    </tr>
                    <tr>
                        <td  class="h" height="22" width="100%">
                            <p align="center">
                                <b><font  face="Arial" size="2">Course Information</font></b></p>
                              <hr style="color: #333333" />
                        </td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="100%">
                            </td>
                    </tr>
                    <tr>
                        <td  class="k" height="114" width="1">
                            </td>
                        <td bgcolor="white" height="114" width="18">
                            </td>
                        <td  height="114" width="100%">
                            <div style="text-align: left">
                                <br />
                                <table border="0" style="width:80%">
                                    <tr>
                                        <td style="background-color: aliceblue">
                Course Code</td>
                                        <td style="background-color: aliceblue">
                                            <asp:Label ID="lbl_course_code" runat="server" Font-Bold="true"></asp:Label>
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                Course Name</td>
                                        <td>
                                            <asp:Label ID="lbl_course_name" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: aliceblue">
                Credit hours</td>
                                        <td style="background-color: aliceblue">
                                            <asp:Label ID="lbl_credit_hours" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                Semester</td>
                                        <td>
                                            <asp:Label ID="lbl_semester" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td bgcolor="white" height="114" width="14">
                            &nbsp;
                            <p>
                                </p>
                        </td>
                        <td class="k" height="114" width="1">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">

<table border="0" style="width:100%">
    
    
    <tr>
        <td colspan="3">
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
                            <b><font  face="Arial" size="2">Add Course Teacher and Class Schedule</font></b></p><hr style="color: #333333" />
                    </td>
                </tr>
                <tr>
                    <td  class="k" height="1" width="100%">
                        </td>
                </tr>
                <tr>
                    <td  class="k" height="114" width="1">
                        </td>
                    <td bgcolor="white" height="114" width="18">
                        </td>
                    <td bgcolor="#ffffff" height="114" width="100%">
                        <div style="text-align: left">
                            <br />
                            <table border="0" width="75%">
                                <tr>
                                    <td>
                                        <strong>Faculty</strong></td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="cmb_Faculty" runat="server" 
                                             AutoPostBack="true" Width="125px" 
                                             onselectedindexchanged="cmb_Faculty_SelectedIndexChanged"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Teacher</strong></td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="cmb_teacher" runat="server" Width="125px" AutoPostBack="true"
                                            onselectedindexchanged="cmb_teacher_SelectedIndexChanged"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>
                                    <asp:label ID="lblHRT" runat="server" Text="HRT" Font-Bold="True" Visible="False"></asp:label>
                                        </td>
                                    <td colspan="3">
                                        <asp:Label ID="lblFacultyID" runat="server" Text="" Font-Bold="True" Visible="False"></asp:Label>
                                                                          </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Group</strong></td>
                                    <td>
                                        <asp:DropDownList ID="cmb_group" runat="server" Width="125px"></asp:DropDownList></td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Capacity</strong></td>
                                    <td>
                                        <asp:TextBox ID="txt_Student" runat="server" ></asp:TextBox>
                                       students<asp:Button id="Button1" onclick="Button1_Click" runat="server" Text="V" Visible=false></asp:Button>

                                      
                                    
                                   <asp:Button id="Button2" onclick="Test_Click" runat="server" 
                                            Text="View Room Distributions" Visible="false"></asp:Button>

                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="height: 4px"></td>
                                    <td style="height: 4px">
                                        </td>
                                    <td style="height: 4px">
                                    
                                        </td>
                                    <td style="height: 4px"></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; border-left-color: cornflowerblue; border-bottom-color: cornflowerblue; color: white; border-top-style: solid; border-top-color: cornflowerblue; border-right-style: solid; border-left-style: solid; background-color: cornflowerblue; border-right-color: cornflowerblue; border-bottom-style: solid;"><strong>Class</strong></td>
                                    <td style="text-align: left; border-left-color: cornflowerblue; border-bottom-color: cornflowerblue; color: white; border-top-style: solid; border-top-color: cornflowerblue; border-right-style: solid; border-left-style: solid; background-color: cornflowerblue; border-right-color: cornflowerblue; border-bottom-style: solid;" 
                                        colspan="3"><strong>Day &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Room</strong>
                                    
                                    <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Time    </strong>
                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td>First class</td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="cmb_date1" runat="server" 
                                        AutoPostBack="true"   onselectedindexchanged="cmb_Room1_SelectedIndexChanged"></asp:DropDownList>&nbsp;
                                        <asp:DropDownList ID="cmb_Room1" runat="server" Width="100px" 
                                         AutoPostBack="true"   onselectedindexchanged="cmb_Room1_SelectedIndexChanged" ></asp:DropDownList>&nbsp;
                                        <asp:DropDownList ID="cmb_Slot1" runat="server" Width="100px"  ></asp:DropDownList>
                                       
                                        
                                        
                                        
                                        
                                                                          </td>
                                </tr>
                                <tr>
                                    <td>Second class</td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="cmb_date2" runat="server"
                                        AutoPostBack="true"      onselectedindexchanged="cmb_Room2_SelectedIndexChanged">
                                    </asp:DropDownList>&nbsp;
                                    <asp:DropDownList ID="cmb_Room2" runat="server" Width="100px" 
                                      AutoPostBack="true"      onselectedindexchanged="cmb_Room2_SelectedIndexChanged" ></asp:DropDownList>&nbsp;
                                       
                                    <asp:DropDownList ID="cmb_Slot2" runat="server" Width="100px" ></asp:DropDownList>
                                        
                                       
                                        </td>
                                </tr>
                                <tr>
                                    <td>Third class</td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="cmb_date3" runat="server" 
                                        AutoPostBack="true"   onselectedindexchanged="cmb_Room3_SelectedIndexChanged">
                                    </asp:DropDownList>&nbsp;
                                     <asp:DropDownList ID="cmb_Room3" runat="server" Width="100px" 
                                          AutoPostBack="true"   onselectedindexchanged="cmb_Room3_SelectedIndexChanged"></asp:DropDownList>&nbsp;
                                        
                                    <asp:DropDownList ID="cmb_Slot3" runat="server"  Width="100px"></asp:DropDownList>
                                        
                                       
                                       
                                        </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        &nbsp;
                                    &nbsp;
                                        
                                        <asp:DropDownList ID="cmb_date4" runat="server" Visible="false">
                                    </asp:DropDownList>&nbsp;
                                    <asp:DropDownList ID="cmb_Slot4" runat="server" Visible="false"></asp:DropDownList>&nbsp;
                                        
                                        
                                        
                                        <asp:DropDownList ID="cmb_Room4" runat="server" Visible="false"></asp:DropDownList>
                                        
                                        </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" />
                                         <asp:Button ID="btn_Cancel" runat="server" OnClick="btn_Cancel_Click" Text="Clear" />
                                        </td>
                                   
                                    <td>
                                        &nbsp;</td>
                                   
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td bgcolor="white" height="114" width="14">
                        &nbsp;
                        <p>
                            </p>
                    </td>
                    <td  class="k" height="114" width="1">
                        </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                      </td>
                    <td bgcolor="white" height="1" width="100%">
                        &nbsp;</td>
                    <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                       </td>
                </tr>
                <tr>
                    
                       </td>
                </tr>
            </table>
        </td>
    </tr>
    
    <tr>
        <td colspan="3">
            <table border="0" cellpadding="0" cellspacing="0" height="1" width="100%">
                <tr>
                    <td colspan="2" height="24" rowspan="3" width="19">
                        </td>
                    
                        </td>
                    <td style="text-align:center" height="24" rowspan="3" >
                            <b><font  face="Arial" size="2">Schedule</font></b>
                          <hr style="color: #333333" />
                    </td>
                </tr>
                <tr>
                    <td class="h" height="22" width="100%">
                       <p align="center">
                            &nbsp;</p> 
                    </td>
                   
                </tr>
                <tr>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td  class="k" width="1" style="height: 114px">
                        </td>
                    <td bgcolor="white" width="18" style="height: 114px">
                        </td>
                    <td bgcolor="#ffffff" width="100%" style="height: 114px">
                        <div style="text-align: left">
                            <asp:Label ID="lbl_teacher_message" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="Red"
                                Text="Label"></asp:Label><br />
                            <asp:GridView ID="GridView_courseList" runat="server" AutoGenerateColumns="False"
                                CellPadding="4" ForeColor="#333333" GridLines="None" Width="75%">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField>                                       
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_select" runat="server" Checked="false" Enabled="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="STAFF_NAME" HeaderText="Name">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="SECTION" HeaderText="Group" >
                                        <ItemStyle HorizontalAlign="Center" />
                                         <ItemStyle Width="25px" />
                                    </asp:BoundField>
                                    
                                    <asp:TemplateField HeaderText="Capacity"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCapacity" runat="server" Text='<%# Bind("TOTAL_CAPACITY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                         <ItemStyle Width="25px" />
                                    </asp:TemplateField>
                                    
                                  
                                    
                                 <%--   <asp:BoundField DataField="" HeaderText="">
                                        <HeaderStyle HorizontalAlign="left" />
                                        <ItemStyle Width="250px" />
                                    </asp:BoundField>--%>
                                    
                                    
                                    <asp:TemplateField HeaderText="1st Class" >
                         
                         <ItemTemplate>
                          <asp:Label runat="server" Text='<%# Bind("day1") %>' ID="lbldayID1"  ></asp:Label>
                             
                             <asp:Label runat="server" Text='<%# Bind("SCH_CLS_1") %>' ID="lblClsdtl1" Visible="false" ></asp:Label>
                             <asp:Label runat="server" Text='<%# Bind("TUT_CLS_1") %>' ID="lblday1" Visible="false"></asp:Label>
                            
                         </ItemTemplate>
                         <ItemStyle Width="250px" />
                          <ItemStyle HorizontalAlign="Center" />
                          <HeaderStyle HorizontalAlign="Center" />
                     </asp:TemplateField>
                     
                     <asp:BoundField DataField="room1" HeaderText="Room1" >
                                        <ItemStyle HorizontalAlign="Center" />
                                         <ItemStyle Width="65px" />
                                    </asp:BoundField>
                     
                     
                 
                         <asp:TemplateField HeaderText="2nd Class" >
                         
                         <ItemTemplate>
                         <asp:Label runat="server" Text='<%# Bind("day2") %>' ID="lbldayID2"  ></asp:Label>
                             <asp:Label runat="server" Text='<%# Bind("SCH_CLS_2") %>' ID="lblClsdtl2" Visible="false"></asp:Label>
                             <asp:Label runat="server" Text='<%# Bind("TUT_CLS_2") %>' ID="lblday2" Visible="false"></asp:Label>
                           
                         </ItemTemplate>
                         <ItemStyle Width="250px" />
                          <ItemStyle HorizontalAlign="Center" />
                          <HeaderStyle HorizontalAlign="Center" />
                     </asp:TemplateField>
                     
                                 <asp:BoundField DataField="room2" HeaderText="Room2" >
                                        <ItemStyle HorizontalAlign="Center" />
                                         <ItemStyle Width="65px" />
                                    </asp:BoundField>   
                                    
                                    
                                    
                                    
                                    <asp:TemplateField HeaderText="3rd Class" >
                         
                         <ItemTemplate>
                         <asp:Label runat="server" Text='<%# Bind("day3") %>' ID="lbldayID3"  ></asp:Label>
                             <asp:Label runat="server" Text='<%# Bind("SCH_CLS_3") %>' ID="lblClsdtl3" Visible="false"></asp:Label>
                             <asp:Label runat="server" Text='<%# Bind("TUT_CLS_3") %>' ID="lblday3" Visible="false"></asp:Label>
                           
                         </ItemTemplate>
                         <ItemStyle Width="250px" />
                          <ItemStyle HorizontalAlign="Center" />
                          <HeaderStyle HorizontalAlign="Center" />
                     </asp:TemplateField>
                     
                                 <asp:BoundField DataField="room3" HeaderText="Room3" >
                                        <ItemStyle HorizontalAlign="Center" />
                                         <ItemStyle Width="65px" />
                                    </asp:BoundField>  
                       <%-- <br />   <asp:Label runat="server" Text='<%# Bind("time2") %>' ID="lbltime2"></asp:Label>
                          <br />   <asp:Label runat="server" Text='<%# Bind("room2") %>' ID="lblroom2"></asp:Label>--%>            
                                    
                                     
                                 
                                    
                                    
                                   
                                    <asp:TemplateField Visible="False"> 
                                        <ItemTemplate>
                                            <asp:Label ID="Label_link" runat="server" Text='<%# Bind("COURSE_TEACHER_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="lbl_teacherId">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_teacherId" runat="server" Text='<%# Bind("TEACHER_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_CourseTeacherID" runat="server" Text='<%# Bind("COURSE_TEACHER_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                               
                                    

                               <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_facultyId" runat="server" Text='<%# Bind("DEPARTMENT_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                   
                                   
                                   <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Id" runat="server" Text='<%# Bind("COURSE_TEACHER_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Smaller" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            <br />
                            &nbsp;&nbsp;<asp:Button ID="btn_bodify" runat="server" Text="Modify" OnClick="btn_bodify_Click" />
                            <asp:Button ID="btn_delete" runat="server" OnClick="btn_delete_Click" Text="Delete" />
                            </div>
                 
                 
                    </td>
                    <td bgcolor="white" width="14" style="height: 114px">
                        &nbsp;
                        <p>
                            </p>
                    </td>
                    <td  class="k" width="1" style="height: 114px">
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
                    <td  class="k" width="100%" style="height: 1px">
                       </td>
                </tr>
            </table>
        </td>
    </tr>
</table>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

