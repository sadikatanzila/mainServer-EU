<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_add_staff.aspx.cs" Inherits="admin_add_staff"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Staffs&gt;New General Staff</span></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0">
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
                                <b><font  face="Arial" size="2">Staff Information</font></b></p>
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
                                        <table id="tbl_dates" runat="server">
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text="Label"></asp:Label></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #666666">
                                                <td>
                                                    Staff ID</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_ID" runat="server" Width="186px"></asp:TextBox></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #666666">
                                                <td>
                                                    Name</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_name" runat="server" Width="186px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Present address</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_presendAddress" runat="server" Width="300px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Permanent address</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_permanentAddress" runat="server"  Width="300px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Phone</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_phone" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Mobile</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_mobile" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    E-mail</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_email" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Password</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_password" runat="server" TextMode="Password"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Confirm password</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_passwordConfirm" runat="server" TextMode="Password"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Job type</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="cmb_job_type" runat="server">
                                                        <asp:ListItem Selected="True" Value="Full">Full-time</asp:ListItem>
                                                        <asp:ListItem Value="Part">Part-time</asp:ListItem>
                                                    </asp:DropDownList><select id="Select1sdfs" runat="server" visible="false" name="D1">
                                                        <option selected="selected"></option>
                                                    </select></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Job category</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="cmb_jb_category" runat="server">
                                                        <asp:ListItem>Teacher</asp:ListItem>
                                                        <asp:ListItem>Staff</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                         <tr>
                                                <td>
                                                    Department</td>
                                                <td>
                                                </td>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="cmb_teacher_department" runat="server">
                                                        <asp:ListItem value="0">Select</asp:ListItem>
                                                        <asp:ListItem value="01">Engineering & Technology</asp:ListItem>
                                                        <asp:ListItem value="02">Business Administration</asp:ListItem>
                                                        <asp:ListItem value="03">Arts</asp:ListItem>
                                                        <asp:ListItem value="04">Law</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Designation</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                     <asp:DropDownList ID="cmb_staff_designation" runat="server">
                                                            <asp:ListItem>Vice Chancellor</asp:ListItem>
                                                            <asp:ListItem>Pro-Vice Chancellor</asp:ListItem>
                                                            <asp:ListItem>Treasurer</asp:ListItem>
                                                            <asp:ListItem>Registrar</asp:ListItem>
                                                            <asp:ListItem>Deputy Registrar</asp:ListItem>
                                                            <asp:ListItem>Assistant Registrar</asp:ListItem>

                                                            <asp:ListItem>Accountant</asp:ListItem>
                                                            <asp:ListItem>Advisor</asp:ListItem>
                                                            <asp:ListItem>Assistant Controller</asp:ListItem>
                                                            <asp:ListItem>Assistant Director</asp:ListItem> 
                                                            <asp:ListItem>Assistant Librarian</asp:ListItem> 
                                                            <asp:ListItem>Cameraman</asp:ListItem>
                                                            <asp:ListItem>Campus Supervisor</asp:ListItem>                                                         
                                                            <asp:ListItem>Coordinator</asp:ListItem>
                                                            <asp:ListItem>Controller</asp:ListItem>
                                                            <asp:ListItem>Chairman</asp:ListItem>
                                                            <asp:ListItem>Counsellor</asp:ListItem>
                                                            <asp:ListItem>Cleaner</asp:ListItem>
                                                            <asp:ListItem>Dean</asp:ListItem>
                                                            <asp:ListItem>Deputy Director</asp:ListItem>
                                                            <asp:ListItem>Director</asp:ListItem>
                                                            <asp:ListItem>Driver</asp:ListItem>
                                                            <asp:ListItem>Electrician</asp:ListItem>
                                                            <asp:ListItem>Executive</asp:ListItem>
                                                            <asp:ListItem>Female Nurse</asp:ListItem>
                                                            <asp:ListItem>Front Desk Officer</asp:ListItem>                                                                                                                
                                                            <asp:ListItem>Head Accountant</asp:ListItem>
                                                            <asp:ListItem>Junior Executive</asp:ListItem>
                                                            <asp:ListItem>Lab Assistant (CSE Lab)</asp:ListItem>
                                                            <asp:ListItem>Lab Manager</asp:ListItem>
                                                            <asp:ListItem>Librarian</asp:ListItem>  
                                                            <asp:ListItem>Liftman</asp:ListItem>  
                                                                                                                
                                                            <asp:ListItem>Medical Officer</asp:ListItem> 
                                                            <asp:ListItem>Messenger</asp:ListItem>    
                                                            <asp:ListItem>Messenger cum Caretaker</asp:ListItem>                                                     
                                                            <asp:ListItem>Project Director</asp:ListItem>
                                                            <asp:ListItem>Senior Counsellor</asp:ListItem>
                                                            <asp:ListItem>Senior Executive</asp:ListItem>
                                                            <asp:ListItem>Site Engineer</asp:ListItem>
                                                            <asp:ListItem>T.A.</asp:ListItem>
                                                            <asp:ListItem>Visiting Faculty</asp:ListItem>
                                                        
                                                       
                                                                                                          
                                                        
                                                       
                                                        
                                                        
                                                        
                                                        
                                                        
                                                        
                                                       
                                                         
                                                        
                                                          

                                                        
                                                         
                                                       
                                                         
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="cmb_teacher_designation" runat="server" Visible="false">
                                                        <asp:ListItem>Lecturer</asp:ListItem>
                                                        <asp:ListItem>Assistant Professor</asp:ListItem>
                                                        <asp:ListItem>Professor</asp:ListItem>
                                                        <asp:ListItem>Associate Professor</asp:ListItem>
                                                        <asp:ListItem>Coordinator</asp:ListItem>
                                                        <asp:ListItem>Dean</asp:ListItem>
                                                        <asp:ListItem>Chairman</asp:ListItem>
                                                        <asp:ListItem>Director</asp:ListItem>
                                                        <asp:ListItem>Advisor</asp:ListItem>
                                                    </asp:DropDownList>
                                                   </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Join Date</td>
                                                <td>
                                                    :</td>
                                                <td>
                                                     <asp:TextBox ID="txt_student_opening" runat="server" Width="150px"></asp:TextBox>
                                                    
                                                     <asp:CalendarExtender ID="CalendarExtender_student_opening" runat="server"
                                                         Format="dd-MMM-yyyy" TargetControlID="txt_student_opening"></asp:CalendarExtender>

                                                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>


                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Confirmation Date</td>
                                                <td>
                                                    :</td>
                                                <td>
                                                     <asp:TextBox ID="txt_student_Confirmation" runat="server" Width="150px"></asp:TextBox>
                                                    
                                                     <asp:CalendarExtender ID="txt_student_Confirmation_CalendarExtender" runat="server"
                                                         Format="dd-MMM-yyyy" TargetControlID="txt_student_Confirmation"></asp:CalendarExtender>


                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Button ID="btn_save" runat="server" OnClick="btn_save_Click" Text="Submit" /></td>
                                            </tr>
                                        </table>
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

