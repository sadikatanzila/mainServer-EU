<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_add_teacher.aspx.cs" Inherits="admin_add_staff" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left"><span style="color: #ffa500">Your location- Staffs&gt;New Teacher</span></td>
        </tr>
  </table>
    

      <script type="text/javascript">
          function ddlChanged(ddl) {
            var txt = document.getElementById('<%=txt_ID.ClientID %>');
              if (!(ddl.value =='Part')) {
                 txt.style.display = '';
                 //alert("Please select valid Reference.");
             }
             else {
                  txt.style.display = 'none';
             }
         }

         ddlChanged(document.getElementById('<%=cmb_job_type.ClientID %>'));
	</script>
    
  <script language="javascript" type="text/javascript">
  
       function loadCalender_stOpening()
        {
          window.open('html/CalenderST_opening.htm','','titlebar=no,toolbar=no,scrollbars=false,resizable=false,height=170,width=300');
          return false;
        } 
        
        function check_Data()
        {
            if(document.getElementById("ctl00$ContentPlaceHolder_definition$txt_name").value=="")
              {
                alert("Please enter the name.");
                return false;
              }
              else if(document.getElementById("ctl00$ContentPlaceHolder_definition$txt_password").value=="")
              {
                alert("Confirm password doesn't matching");
                return false;
              }
              else if(document.getElementById("ctl00$ContentPlaceHolder_definition$txt_student_opening").value=="")
              {
                alert("Please enter the date.");
                return false;
              }
              
              else if(document.getElementById("ctl00$ContentPlaceHolder_definition$txt_passwordConfirm").value!= document.getElementById("ctl00$ContentPlaceHolder_definition$txt_password").value)
              {
                alert("Confirm password doesn't matching");
                return false;
              }
              else
                return true;
        }
        
        function set_depart()
        {
            
            if(document.getElementById("ctl00$ContentPlaceHolder_definition$cmb_jb_category").value=="Staff")
            {            
               alert(""+document.getElementById("ctl00$ContentPlaceHolder_definition$cmb_jb_category").value);
                document.getElementById("ctl00$ContentPlaceHolder_definition$Select1sdfs").invisible=true;
                 document.getElementById("ctl00$ContentPlaceHolder_definition$cmb_teacher_designation").disabled=true;
                  document.getElementById("ctl00$ContentPlaceHolder_definition$cmb_teacher_designation").invisible=true;
                
                //visible=true
            }
        }
    
  </script>
    
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
                                <b><font  face="Arial" size="2">Teacher</font></b><b><font 
                                    face="Arial" size="2"> Information</font></b></p>
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
                                                    Job type</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="cmb_job_type" runat="server" onchange="ddlChanged(this)">
                                                        <asp:ListItem Selected="True" Value="Full">Full-time</asp:ListItem>
                                                        <asp:ListItem Value="Part">Part-time</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #666666">
                                                <td>
                                                     Teacher ID</td>
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
                                                    <asp:TextBox ID="txt_presendAddress" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Permanent address</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_permanentAddress" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Phone</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_phone" runat="server" MaxLength="80"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Mobile</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_mobile" runat="server" MaxLength="80"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    E-mail</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_email" runat="server" MaxLength="80"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Password</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_password" runat="server" TextMode="Password" MaxLength="80"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Confirm password</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txt_passwordConfirm" runat="server" TextMode="Password" MaxLength="80"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td colspan="2">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    College</td>
                                                <td>
                                                </td>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="cmb_teacher_department" runat="server">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Designation</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="cmb_teacher_designation" runat="server">
                                                        <asp:ListItem>T.A.</asp:ListItem>
                                                        <asp:ListItem>Lecturer</asp:ListItem>
                                                        <asp:ListItem>Sr.Lecturer</asp:ListItem>
                                                        <asp:ListItem>Assistant Professor</asp:ListItem>
                                                        <asp:ListItem>Professor</asp:ListItem>
                                                        <asp:ListItem>Associate Professor</asp:ListItem>
                                                        <asp:ListItem>Coordinator</asp:ListItem>
                                                        <asp:ListItem>Dean</asp:ListItem>
                                                        <asp:ListItem>Chairman</asp:ListItem>
                                                        <asp:ListItem>Visiting Faculty</asp:ListItem>
                                                        <asp:ListItem>Advisor</asp:ListItem>
														
                                                    </asp:DropDownList>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Join date</td>
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
                                                    Confiramtion date</td>
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
                                                <td style="height: 21px">
                                                </td>
                                                <td style="height: 21px">
                                                </td>
                                                <td colspan="2" style="height: 21px">
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

