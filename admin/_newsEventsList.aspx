<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_newsEventsList.aspx.cs" Inherits="admin_newsEventsList"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- News/Events&gt; News/Event List</span></td>
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
      if (document.getElementById("ctl00_ContentPlaceHolder_definition_txt_student_opening").value.toString()=="" )
      {
            alert("Please enter from date.");
            return false;
      }
      else  if (document.getElementById("ctl00_ContentPlaceHolder_definition_txt_student_closing").value.toString()=="" )
      {
            alert("Please enter to date.");
            return false;
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
                                <b><font  face="Arial" size="2">Nnews/Events List</font></b></p>
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
                        <td bgcolor="#ffffff" style="vertical-align: top; text-align: left" width="100%">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <span style="font-size: 10pt"><span style="color: #000000">From</span> </span>
                                        <asp:TextBox ID="txt_student_opening" runat="server" ReadOnly="True"></asp:TextBox><img
                                            id="btn_from" runat="server" src="../admin/images/calender.jpg" />
                                        &nbsp;<span style="font-size: 10pt; color: #000000"> to&nbsp;
                                            <asp:TextBox ID="txt_student_closing" runat="server" ReadOnly="True"></asp:TextBox><img
                                                id="btn_to" runat="server" src="../admin/images/calender.jpg" /></span></td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <span style="font-size: 10pt; color: #000000"></span>&nbsp; &nbsp; &nbsp; &nbsp;
                                        <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" /></td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <hr />
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <asp:GridView ID="GridView_noticeList" runat="server" Width="100%" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="4">
                                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                            <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="NEWS_EVENT_ID" DataNavigateUrlFormatString="~/admin/_newNewsEvent.aspx?nid={0}"
                                                    DataTextField="TITLE" HeaderText="Title" NavigateUrl="~/admin/_newNewsEvent.aspx" />
                                                <asp:BoundField DataField="newsOrEvent" HeaderText="Type" />
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="#330099" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#990000" Font-Bold="True" Font-Size="Small" ForeColor="#FFFFCC" />
                                        </asp:GridView>
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

