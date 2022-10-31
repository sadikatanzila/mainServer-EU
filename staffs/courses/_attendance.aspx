<%@ Page Language="C#" MasterPageFile="~/staffs/courses/_courses_master.master" AutoEventWireup="true" CodeFile="_attendance.aspx.cs" Inherits="staffs_courses_attendance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
   
    <script type="text/javascript" language="javascript">
function loadCalender()
{
  window.open('../html/CalenderaTT.htm','','titlebar=no,toolbar=no,scrollbars=false,resizable=false,height=170,width=300');
  return false;
}  

function save_check()
{ 
    if(document.getElementById('ctl00_ContentPlaceHolder_content_txt_date').value=="")
    {
        alert("Please enter date.");
        return false;
    }
    
    else return true;
    
}


</script>

       <script type="text/javascript" language="javascript">
           functionCheckall(Checkbox)
           { var GridView1 = document.getElementById("<%=GridView_students.ClientID %>"); 
               for (i = 1; i < GridView1.rows.length; i++) 
               {
                   GridView1.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
               } 
           }

       </script> 

   
   
    <table style="width: 95%">
        <tr>
            <td class="header" style="color: #ffa500; height: 20px; background-color: white;
                text-align: left">
                <table border="0" style="text-align: left">
                    <tr>
                        <td colspan="3" style="width: 496px; text-align: left">
                            <span style="color: #ffa500">Your location- Courses &gt; Course List &gt; Course Details
                                &gt; Student Attendance </span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="header" style="height: 44px; background-color: #ffffff; text-align: left">
                <table style="width: auto">
                    <tr>
                        <td style="width: auto; text-align: left">
                            Select Course
                            <asp:DropDownList ID="cmb_course" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmb_course_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" Visible="false"/></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0">
        <tr>
            <td colspan="3" style="text-align:left" id="lbl_message">
                <hr />
                <asp:Label ID="lbl_message" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="Red"
                    Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="3">
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="539">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                            <img border="0" height="24" src="../../staffs/images/lcurv.gif" width="19" /></td>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../../staffs/images/rcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" height="22" width="505">
                            <p align="center">
                                <b><font color="#333399" face="Arial" size="2"><span style="color: #ffa500">Course Information</span></font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" height="114" width="18">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="18" /></td>
                        <td align="left" bgcolor="#ffffff" height="114" width="505">
                            <br />
                            <table id="TABLE1" border="0" onclick="return TABLE1_onclick()" style="text-align: left">
                                <tr>
                                    <td style="background-color: aliceblue">
                                        Course Code</td>
                                    <td style="background-color: aliceblue">
                                        <asp:Label ID="lbl_course_code" runat="server" Font-Bold="true"></asp:Label>

                                         <asp:Label ID="lbl_course_key" runat="server" Font-Bold="true" Visible="false"></asp:Label>

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
                                <tr>
                                    <td style="background-color: aliceblue">
                                        Section</td>
                                    <td style="background-color: aliceblue">
                                        <asp:Label ID="lbl_section" runat="server" Font-Bold="true"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        Total Student</td>
                                    <td>
                                        <asp:Label ID="lbl_total_student" runat="server" Font-Bold="true"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                        <td bgcolor="white" height="114" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                            <img border="0" height="15" src="../../staffs/images/blcurv.gif" width="19" /></td>
                        <td bgcolor="white" height="1" width="505">
                            <img border="0" height="14" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            <img border="0" height="15" src="../../staffs/images/brcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="150" /></td>
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
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="539">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                            <img border="0" height="24" src="../../staffs/images/lcurv.gif" width="19" /></td>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../../staffs/images/rcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" height="22" width="505">
                            <p align="center">
                                <b><font color="#333399" face="Arial" size="2"><span style="color: #ffa500">Student
                                    List</span></font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" height="114" width="18">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="18" /></td>
                        <td bgcolor="#ffffff" height="114" style="vertical-align: top; text-align: left"
                            width="505"><table style="width: auto">
                                <tr>
                                    <td style="width: auto; text-align: left">
                                        Date &nbsp;
                                    </td>
                                    <td style="width: auto;">
                                        <asp:TextBox ID="txt_date" runat="server" Width="150px" Height="15px"></asp:TextBox>
                                      

                                        <asp:CalendarExtender ID="CalendarExtender_txt_date" runat="server"
                                                         Format="dd-MMM-yyyy" TargetControlID="txt_date"></asp:CalendarExtender>

                                                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: auto; text-align: left">
                                        Time Slot</td>
                                    <td style="width: auto">
                                        <asp:DropDownList ID="ddlSlot" runat="server" Width="200px">
                            </asp:DropDownList>
                                    
                                    </td>
                                </tr>
                                  <tr>
                                    <td style="width: auto; text-align: left">
                                        Class Type</td>
                                    <td style="width: auto">
                                       <asp:RadioButton ID="rbtnRegular" runat="server" GroupName="clstype" Text="Regular" Checked="true" />

<asp:RadioButton ID="rtbnMakeup" runat="server" GroupName="clstype" Text="Makeup" />

                                        <asp:RadioButton ID="rtbnExtra" runat="server" GroupName="clstype" Text="Extra" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: auto; text-align: left">
                                    </td>
                                    <td style="width: auto">
                                        <asp:Button ID="btn_show" runat="server" OnClick="btn_show_Click" Text="Show" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btn_set_attendance" runat="server" OnClick="btn_set_attendance_Click" Text="Set Attendance" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btn_Drop_attendance" runat="server" Text="Drop Attendance" OnClick="btn_Drop_attendance_Click"  /></td>
                                </tr>
                            </table>
                            <br />

                            <asp:Panel ID="pnlAttendance" runat="server" Visible="false">
<asp:GridView ID="GridView_students" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="None">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <Columns>

        
       <asp:TemplateField >      
             <HeaderTemplate>  
                     <asp:CheckBox ID="CheckBox1" AutoPostBack="true" OnCheckedChanged="chckchanged" runat="server" />

             </HeaderTemplate>                                 
              <ItemTemplate>
                                   <asp:CheckBox ID="chkSelected" runat="server"   />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                  

                                    <asp:BoundField DataField="SID" HeaderText="Student ID">
                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                   <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttendID" runat="server" Text='<%# Bind("ATTEND_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:BoundField DataField="SNAME" HeaderText="Student Name">
                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SPROGRAM" HeaderText="Program" />
                                    <asp:BoundField DataField="ATTEND" Visible="False" />
                                </Columns>
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>

                            </asp:Panel>
      
                        </td>
                        <td bgcolor="white" height="114" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                            <img border="0" height="15" src="../../staffs/images/blcurv.gif" width="19" /></td>
                        <td bgcolor="white" height="1" width="505">
                            <img border="0" height="14" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            <img border="0" height="15" src="../../staffs/images/brcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="150" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>

