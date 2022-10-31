<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_newNewsEvent.aspx.cs" Inherits="admin_newNewsEvent"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- News/Events&gt; New News/Event</span></td>
        </tr>
    </table>
    
    <script language="javascript" type="text/javascript">
  
  function loadCalender_stOpening()
    {
      window.open('html/CalenderST_opening.htm','','titlebar=no,toolbar=no,scrollbars=false,resizable=false,height=170,width=300');
      return false;
    } 

    function loadCalender_stClosing()
    {
      window.open('html/CalenderST_closing.htm','','titlebar=no,toolbar=no,scrollbars=false,resizable=false,height=170,width=300');
      return false;
    } 
    
    
      function chech_valid_data()
  {  
    if (document.getElementById("ctl00_ContentPlaceHolder_definition_txt_title").value.toString()=="" )
      {
            alert("Please enter the title");
            return false;
      }
     else if (document.getElementById("ctl00_ContentPlaceHolder_definition_txt_student_opening").value.toString()=="" )
      {
            alert("Please enter from date.");
            return false;
      }
      else  if (document.getElementById("ctl00_ContentPlaceHolder_definition_txt_student_closing").value.toString()=="" )
      {
            alert("Please enter to date.");
            return false;
      }
      else  if (document.getElementById("ctl00_ContentPlaceHolder_definition_cmb_type").value.toString()=="1" )
      {
          if (document.getElementById("ctl00_ContentPlaceHolder_definition_FileUpload_image").value.toString()=="" )
            {
                 alert("Please select a image for news.");
                 return false;
            }
            else return true;
      }
     
      else
            return true;
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
                                <b><font  face="Arial" size="2">News/Event Details</font></b></p>
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
                                                    Title</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2" style="width: 305px">
                                                    <asp:TextBox ID="txt_title" runat="server" MaxLength="100" Width="296px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top">
                                                    Description</td>
                                                <td style="vertical-align: top">
                                                    :</td>
                                                <td colspan="2" style="width: 305px">
                                                    <textarea id="txt_description" runat="server" style="width: 270px; height: 172px"></textarea></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    From date</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2" style="width: 305px">
                                                    <asp:TextBox ID="txt_student_opening" runat="server"></asp:TextBox>
                                                    <img id="btn_calender_from" runat="server" src="../admin/images/calender.jpg" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    To date</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2" style="width: 305px">
                                                    <asp:TextBox ID="txt_student_closing" runat="server"></asp:TextBox>
                                                    <img id="btn_calender_to" runat="server" src="../admin/images/calender.jpg" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Type</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2" style="width: 305px">
                                                    <asp:DropDownList ID="cmb_type" runat="server">
                                                        <asp:ListItem Value="1">News</asp:ListItem>
                                                        <asp:ListItem Value="2">Event</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Image (small)</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2" style="width: 305px">
                                                    <asp:FileUpload ID="FileUpload_image" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Image</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2" style="width: 305px">
                                                    <asp:FileUpload ID="FileUpload_image_b" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Status</td>
                                                <td>
                                                    :</td>
                                                <td colspan="2" style="width: 305px">
                                                    <asp:CheckBox ID="chk_status" runat="server" Checked="True" Text="Active" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td colspan="2" style="width: 305px">
                                                    <asp:Button ID="btn_save" runat="server" OnClick="btn_save_Click" Text="Submit" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
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

