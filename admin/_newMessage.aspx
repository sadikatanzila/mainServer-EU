<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_newMessage.aspx.cs" Inherits="admin_newMessage" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Message&gt; Indivisual Message</span></td>
        </tr>
    </table>
<script language="javascript" type="text/javascript">
  
  function loadCalender_stOpening()
    {
      window.open('html/CalenderST_opening.htm','','titlebar=no,toolbar=no,scrollbars=false,resizable=false,height=170,width=300');
      return false;
    } 
    
    
    function loadCalender_BstOpening()
    {
      window.open('html/CalenderST_closing.htm','','titlebar=no,toolbar=no,scrollbars=false,resizable=false,height=170,width=300');
      return false;
    } 
    
    function loadCalender_GstOpening()
    {
      window.open('html/CalenderTH_opening.htm','','titlebar=no,toolbar=no,scrollbars=false,resizable=false,height=170,width=300');
      return false;
    } 
    
    function loadCalender_AstOpening()
    {
      window.open('html/CalenderTH_closing.htm','','titlebar=no,toolbar=no,scrollbars=false,resizable=false,height=170,width=300');
      return false;
    } 
    
    function check_data()
    {
      if(document.getElementById("ctl00$ContentPlaceHolder_definition$txt_student_opening").value==""
      || document.getElementById("ctl00$ContentPlaceHolder_definition$txt_student_closing").value==""
      || document.getElementById("ctl00$ContentPlaceHolder_definition$txt_teacher_opening").value==""
      || document.getElementById("ctl00$ContentPlaceHolder_definition$txt_teacher_closing").value=="")
      {
        alert("Please enter the publish date.");
        return false;
      }
      else
         return true
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
                                <b><font  face="Arial" size="2">Message Details</font></b></p>
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
                            <asp:Label ID="lblErrorMsg" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label><br />
                            <asp:Label ID="lblHeadline" runat="server" Text="Select Message Sending System : " Font-Bold="true"></asp:Label>   
                        <asp:DropDownList ID="ddlMsgSystem" runat="server" width="150" AutoPostBack="true" 
                                onselectedindexchanged="ddlMsgSystem_SelectedIndexChanged">
                            <asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Specific Student" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Specific Batch" Value="2"></asp:ListItem>    
                            <asp:ListItem Text="Selected Students" Value="3"></asp:ListItem>
                             <asp:ListItem Text="To All" Value="4"></asp:ListItem>
                      </asp:DropDownList>
                      
                      <p align="left"> <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" Font-Bold="true" ForeColor="Red" Text="Label"></asp:Label>
                      </p>
                     <!-- send message to specific student ------------------------------------------------------------            -->
                      
                      
                            <asp:Panel ID="pnl_specificStd" runat="server" Visible="false">
                            
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <table id="tbl_dates" runat="server">
                                            <tr>
                                                <td colspan="3"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    Message to Specific Student</td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #666666">
                                                <td>To (Student ID)</td>
                                                <td>:</td>
                                                <td style="width: 305px"><asp:TextBox ID="txt_StdID" runat="server" Width="150px" MaxLength="100"></asp:TextBox></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #666666">
                                                <td>Title</td>
                                                <td>:</td>
                                                <td style="width: 305px"><asp:TextBox ID="txt_title" runat="server" Width="296px" 
                                                        MaxLength="100"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top">Description</td>
                                                <td style="vertical-align:top">:</td>
                                                <td style="width: 305px">
                                                    <textarea id="txt_description" style="width: 270px; height: 172px" runat="server"></textarea></td>
                                            </tr>
                                            <tr>
                                                <td>Publish date</td>
                                                <td>:</td>
                                                <td style="width: 305px"><asp:TextBox ID="txt_student_opening" runat="server" Width="150px"></asp:TextBox>
                                                     <asp:CalendarExtender ID="CalendarExtender_student_opening" runat="server"
                                                         Format="dd-MMM-yyyy" TargetControlID="txt_student_opening"></asp:CalendarExtender>

                                                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager></td>
                                            </tr>
                                            <tr>
                                                <td>Status</td>
                                                <td>:</td>
                                                <td style="width: 305px">
                                                    <asp:CheckBox ID="chk_status" runat="server" Text="Active" Checked="True" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td style="width: 305px"><asp:Button ID="btn_save" runat="server" OnClick="btn_save_Click" Text="Submit" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            
                            </asp:Panel>
                            
                            
                      
                      
                      
                      
                      
                      
                      
                      
                      
                      
                      
                       <!-- send message to specific Batch ------------------------------------------------------------            -->
                            <asp:Panel ID="pnlBatch" runat="server" Visible="false">
                              <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <table id="Table1" runat="server">
                                            <tr>
                                                <td colspan="3"><asp:Label ID="erMsg_Batch" runat="server" Font-Size="X-Small" ForeColor="Red" Text=""></asp:Label></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #666666">
                                                <td colspan="3">Message to Specific Batch</td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #666666">
                                                <td>
                                                    To (Batch)</td>
                                                <td>
                                                    :</td>
                                                <td style="width: 305px">
                                                    <asp:TextBox ID="txtBatchID" runat="server" MaxLength="100" Width="150px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #666666">
                                                <td>Title</td>
                                                <td>:</td>
                                                <td style="width: 305px"><asp:TextBox ID="txt_BTitle" runat="server" Width="296px" 
                                                        MaxLength="100"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top">Description</td>
                                                <td style="vertical-align:top">:</td>
                                                <td style="width: 305px">
                                                    <textarea id="txt_Bdescription" style="width: 270px; height: 172px" runat="server"></textarea></td>
                                            </tr>
                                            <tr>
                                                <td>Publish date</td>
                                                <td>:</td>
                                                <td style="width: 305px"><asp:TextBox ID="txt_student_closing" runat="server" Width="150px"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender_student_closing" runat="server"
                                                         Format="dd-MMM-yyyy" TargetControlID="txt_student_closing"></asp:CalendarExtender></td>
                                            </tr>
                                            <tr>
                                                <td>Status</td>
                                                <td>:</td>
                                                <td style="width: 305px">
                                                    <asp:CheckBox ID="chk_statusB" runat="server" Text="Active" Checked="True" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td style="width: 305px"><asp:Button ID="btnSumbitBatch" runat="server" OnClick="btnSumbitBatch_Click" Text="Submit" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            </asp:Panel>  
                          
                          
                          
                          
                          
                          
                          
                          
                          
                          
                            <!-- send message to Some specific Students (students group) ------------------------------------------------------------            -->
                            <asp:Panel ID="pnlGroup" runat="server" Visible="false">
                              <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <table id="Table3" runat="server">
                                            <tr>
                                                <td colspan="3"><asp:Label ID="erMsg_Group" runat="server" Font-Size="X-Small" ForeColor="Red" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    Message to Some Selected Students</td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #666666">
                                                <td>To (Student IDs)</td>
                                                <td>:</td>
                                                <td style="width: 305px"><asp:TextBox ID="txt_StdIDG" runat="server" Width="296px" Height="50px" TextMode="MultiLine" ></asp:TextBox></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #666666">
                                                <td>Title</td>
                                                <td>:</td>
                                                <td style="width: 305px"><asp:TextBox ID="txt_titleG" runat="server" Width="296px" 
                                                        MaxLength="100"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top">Description</td>
                                                <td style="vertical-align:top">:</td>
                                                <td style="width: 305px">
                                                    <textarea id="txt_descriptionG" style="width: 270px; height: 172px" runat="server"></textarea></td>
                                            </tr>
                                            <tr>
                                                <td>Publish date</td>
                                                <td>:</td>
                                                <td style="width: 305px"><asp:TextBox ID="txt_teacher_opening" runat="server" Width="150px"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender_teacher_opening" runat="server"
                                                         Format="dd-MMM-yyyy" TargetControlID="txt_teacher_opening"></asp:CalendarExtender></td>
                                            </tr>
                                            <tr>
                                                <td>Status</td>
                                                <td>:</td>
                                                <td style="width: 305px">
                                                    <asp:CheckBox ID="chk_statusG" runat="server" Text="Active" Checked="True" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td style="width: 305px"><asp:Button ID="btnSumbitGroup" runat="server" OnClick="btnSumbitGroup_Click" Text="Submit" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            </asp:Panel>  
                          
                          
                          
                          
                          
                          
                          
                          
                          
                          
                          
                          
                          
                          
                          
                          
                          <!-- send message to All------------------------------------------------------------            -->
                            <asp:Panel ID="pnlMessage_All" runat="server" Visible="false">
                              <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <table id="Table2" runat="server">
                                            <tr>
                                                <td colspan="3"><asp:Label ID="ErMsgAll" runat="server" Font-Size="X-Small" ForeColor="Red" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    Message to All Students</td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #666666">
                                                <td>Title</td>
                                                <td>:</td>
                                                <td style="width: 305px"><asp:TextBox ID="txt_TitleAll" runat="server" Width="296px" 
                                                        MaxLength="100"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top">Description</td>
                                                <td style="vertical-align:top">:</td>
                                                <td style="width: 305px">
                                                    <textarea id="txt_DescriptionAll" style="width: 270px; height: 172px" runat="server"></textarea></td>
                                            </tr>
                                            <tr>
                                                <td>Publish date</td>
                                                <td>:</td>
                                                <td style="width: 305px"><asp:TextBox ID="txt_teacher_closing" runat="server" Width="150px"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender_teacher_closing" runat="server"
                                                         Format="dd-MMM-yyyy" TargetControlID="txt_teacher_closing"></asp:CalendarExtender></td>
                                            </tr>
                                            <tr>
                                                <td>Status</td>
                                                <td>:</td>
                                                <td style="width: 305px">
                                                    <asp:CheckBox ID="chk_statusA" runat="server" Text="Active" Checked="True" /></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td style="width: 305px"><asp:Button ID="btnSumbitAll" runat="server" OnClick="btnSumbitAll_Click" Text="Submit" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            </asp:Panel>    
                            
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

