<%@ Page Language="C#" MasterPageFile="~/staffs/advisor/MasterPage_advisor.master" AutoEventWireup="true" CodeFile="_courseAdvisingDetails.aspx.cs"
     Inherits="staffs_advisor_courseAdvisingDetails"   %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    
    <script language="javascript" type="text/javascript">
   
  
 
 function open_current_academic_status( sid )
 {
  window.open('_academicStatus.aspx?ids='+sid,'','titlebar=no,toolbar=no,scrollbars,resizable=false,height=650,width=620');
  return false; 
 } 
   
  
   
 </script>
    
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Advisoship &gt; Course Advising Details</span></td>
        </tr>
    </table><table border="0" cellpadding="2" cellspacing="0" width="90%">
        <tr>            
            <td style="width: 20%">
                &nbsp;</td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 15%; text-align: left">Student ID</td>
            <td style="width: 2%; text-align: left">:</td>
            <td style="text-align: left">
                <asp:Label ID="lbl_sid" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 20%; text-align: left">Name</td>
            <td style="width: 2%; text-align: left">:</td>
            <td style="text-align: left">
                <asp:Label ID="lbl_sName" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 20%; text-align: left">Program</td>
            <td style="width: 2%; text-align: left">:</td>
            <td style="text-align: left">
                <asp:Label ID="lbl_program" runat="server" Text="Label"></asp:Label></td>
        </tr> 
        <tr>
            <td style="width: 20%; text-align: left">
            </td>
            <td style="width: 2%; text-align: left">
            </td>
            <td style="text-align: left">
                <asp:HyperLink ID="hplnk_academicStat" runat="server" Font-Bold="True">Academic Status</asp:HyperLink></td>
        </tr>
        
    </table>
    &nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table id="TABLE1" runat="server" border="0" width="540">
        <tr>
            <td colspan="3" style="text-align: left">
                <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text="Label"></asp:Label>
                <br />
                <asp:Label ID="lbl_Error" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table id="tbl_offered_courses" runat="server" border="0" cellpadding="0" cellspacing="0"
                    height="1" width="539">
                    <tr>
                        <td id="TD1" runat="server" colspan="2" height="24" rowspan="3" width="19">
                            <img border="0" height="24" src="../images/lcurv.gif" width="19" /></td>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../images/rcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" height="22" width="505">
                            <p align="center">
                                <b><font color="#ffa500" face="Arial" size="2">Courses Choosen</font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" height="114" width="18">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="18" /></td>
                        <td bgcolor="#ffffff" height="114" style="text-align: left" width="505">
                            <div style="text-align: left">
                                &nbsp;</div>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="color: #0000ff">
                                        <asp:GridView ID="GridView_taken_list" runat="server" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="3" Width="100%">
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
                     </asp:TemplateField>
                                                
                                             
                                                <asp:BoundField DataField="STAFF_NAME" HeaderText="Teacher" />
                                                <asp:BoundField DataField="CHOURS" HeaderText="Credit">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GGROUP" HeaderText="Group">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                
                     <%--<asp:TemplateField HeaderText="1st Class" >
                         <ItemTemplate>
                              <asp:Label runat="server" Text='<%# Bind("SCH_CLS_1") %>' ID="lblroom1"></asp:Label>
                         </ItemTemplate>
                         <ItemStyle Width="60px" />
                          <ItemStyle HorizontalAlign="left" />
                          <HeaderStyle HorizontalAlign="left" />
                     </asp:TemplateField>    
                                                
                    <asp:TemplateField HeaderText="2nd Class" >
                         <ItemTemplate>
                           <asp:Label runat="server" Text='<%# Bind("SCH_CLS_2") %>' ID="lblroom2"></asp:Label>
                        
                         </ItemTemplate>
                         <ItemStyle Width="60px" />
                          <ItemStyle HorizontalAlign="left" />
                          <HeaderStyle HorizontalAlign="left" />
                     </asp:TemplateField> 
                     
                     
                       <asp:TemplateField HeaderText="3rd Class" >
                         <ItemTemplate>
                           <asp:Label runat="server" Text='<%# Bind("SCH_CLS_3") %>' ID="lblroom3"></asp:Label>
                        
                         </ItemTemplate>
                         <ItemStyle Width="60px" />
                          <ItemStyle HorizontalAlign="left" />
                          <HeaderStyle HorizontalAlign="left" />
                     </asp:TemplateField> --%>                                   
                                                
                                                <asp:BoundField DataField="SCH_CLS_1" HeaderText="1st Class ">
                                                    <ItemStyle Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SCH_CLS_2" HeaderText="2nd Class">
                                                    <ItemStyle Width="60px" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="SCH_CLS_3" HeaderText="3rd Class">
                                                    <ItemStyle Width="60px" />
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
                                                </asp:TemplateField>
                                               
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <RowStyle ForeColor="#000066" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="AntiqueWhite" />
                                        </asp:GridView>

                                         <!--  <asp:BoundField DataField="available_seat" HeaderText="Enrolled" />-->
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lbl_total_credit" runat="server" Font-Bold="True"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        Enrolled = Taken/Capacity</td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_deleteCourse" runat="server" OnClick="btn_deleteCourse_Click"
                                            Text="Delete Course" /></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" 
                                            onclick="btnPrint_Click" Visible="false" />
                                        <asp:Button ID="btn_final_offering" runat="server" OnClick="btn_final_offering_Click"
                                            Text="Finally Approved (& closing)" /></td>
                                </tr>
                            </table>
                            &nbsp;
                        </td>
                        <td bgcolor="white" height="114" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
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
    <table border="0">
        <tr>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table id="tbl_newOffering" runat="server" border="0" cellpadding="0" cellspacing="0"
                    height="1" width="539">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                            <img border="0" height="24" src="../images/lcurv.gif" width="19" /></td>
                        <td bgcolor="#6fb1d9" class="k" height="1" style="width: 505px">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../images/rcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" height="22" style="width: 505px">
                            <p align="center">
                                <b><font id="FONT1" color="#ffa500" face="Arial" size="2">Offer New Course</font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" style="width: 505px">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" height="114" width="18">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="18" /></td>
                        <td bgcolor="#ffffff" height="114" style="width: 505px">
                            <div style="text-align: left">
                                <br />
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td colspan="3" style="text-align: center">
                                            <strong><span>&nbsp;</span></strong></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Select</td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmb_course" runat="server">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
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
                                                   
                                                    <asp:TemplateField Visible="False">
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
                                     <tr>
                                        <td style="height: 19px">
                                            &nbsp;</td>
                                        <td style="height: 19px">
                                            &nbsp;</td>
                                        <td style="height: 19px">
                                            Enrolled = Offered/Capacity</td>
                                    </tr>
                                    <tr>
                                        <td style="height: 19px">
                                        </td>
                                        <td style="height: 19px">
                                        </td>
                                        <td style="height: 19px">
                                            <asp:Button ID="btn_add" runat="server" OnClick="btn_add_Click" Text="Add to Offering" /></td>
                                    </tr>
                                </table>
                            </div>
                            &nbsp;
                        </td>
                        <td bgcolor="white" height="114" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                            <img border="0" height="15" src="../images/blcurv.gif" width="19" /></td>
                        <td bgcolor="white" height="1" style="width: 505px">
                            <img border="0" height="14" src="../images/spacer(1).gif" width="1" /></td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            <img border="0" height="15" src="../images/brcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" style="width: 505px">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="150" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

