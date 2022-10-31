<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_viewLedger.aspx.cs" Inherits="admin_viewLedger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
  
  
    <table border="0"  width="100%">
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
                                <b><font  face="Arial" size="2">Student Ledger</font></b></p>
                              <hr style="color: #333333" />
                        </td>
                    </tr>
                    <tr>
                        <td class="k" height="1" width="100%">
                          </td>
                    </tr>
                    <tr>
                        <td class="k" style="height: 114px" width="1">
                            </td>
                        <td bgcolor="white" style="height: 114px" width="18">
                          </td>
                        <td bgcolor="#ffffff" style="vertical-align: top" width="100%">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <table>
                                            <tr>
                                                <td style="width: auto; height: auto">
                                                    Student SID</td>
                                                <td style="width: auto; height: auto">
                                                    :</td>
                                               
                                                <td style="width: auto; height: 22px">
                                                    <asp:TextBox ID="txtSID" runat="server"  Width="150px"></asp:TextBox></td>
                                                <td style="width: 102px; height: 22px">
                                                    <asp:Button ID="btn_submit" runat="server"  Text="View Ledger" OnClick="btn_submit_Click" /></td>
                                            </tr>
                                        </table>
                                        <hr />
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="large" ForeColor="Red" Text=""></asp:Label><br />
                                       
                                        
                                   
                                 
                                   
                                    
                                        
                                   
                                   
                                   
                                    </td>
                                </tr>
                            </table>
                            &nbsp;</td>
                        <td bgcolor="white" style="height: 114px" width="14">
                           
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
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

