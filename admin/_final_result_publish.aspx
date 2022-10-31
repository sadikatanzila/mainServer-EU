<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_final_result_publish.aspx.cs" Inherits="admin_final_result_publish" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
    <tr>
        <td colspan="3" style="text-align: left">
            <span style="color: #ffa500">Your location- Result&gt;Result Publish</span></td>
    </tr>
</table>
  
<script language="javascript" type="text/javascript">

function chech_valid()
{  
  if (document.getElementById("ctl00_ContentPlaceHolder_definition_txt_year").value.toString()=="" ) 
  {
        alert("Please enter the year");
        return false;
  }
  else
        return confirm('Do you want to publish result?');
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
                            <b><font  face="Arial" size="2">Result Publish</font></b></p>
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
                                                <asp:Button ID="btn_submit" runat="server"  Text="Publish"  OnClick="btn_submit_Click" /></td>
                                        </tr>
                                    </table>
                                    <hr />
                                    <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text="Label"></asp:Label><br />
                                    
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

