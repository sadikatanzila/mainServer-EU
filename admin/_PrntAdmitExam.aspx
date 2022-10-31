<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_PrntAdmitExam.aspx.cs" Inherits="admin_PrntAdmitExam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
   
    
      <script language="javascript" type="text/javascript">
          function chech_valid() {
              if (document.getElementById("ctl00$ContentPlaceHolder_definition$txt_year").value.toString() == "") {
                  alert("Please enter the year");
                  return false;
              }
              else
                  return true;
          }
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0" style="width:80%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="99%">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                           </td>
                        <td  class="k" height="1" width="505">
                           </td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            </td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" height="22" width="505">
                            <p align="center">
                                <b><font color="#ffa500" face="Arial" size="2">Print Admit Card</font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="505">
                          </td>
                    </tr>
                    <tr>
                        <td  class="k" style="height: 114px" width="1">
                            <img border="0" height="1" src="images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" style="height: 114px" width="18">
                            <img border="0" height="1" src="images/spacer(1).gif" width="18" /></td>
                        <td bgcolor="#ffffff" style="vertical-align: top; text-align: left" width="505">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                            <br />
                                           <table style="width:100%">
                                           <tr>
                                           <td  colspan="3">
                                               <asp:Label ID="lbl_message" runat="server" ForeColor="#0000CC"></asp:Label>
                                           </td>
                                           </tr>
                                           
                                           <tr>
                                           <td style="width: 132px; font-weight: bold; font-size: medium;">
                                               Student ID
                                           </td>
                                           <td style="width: 20px;font-weight: bold;">
                                               :
                                           </td>
                                           <td>
                                           
                                               <asp:TextBox ID="txtSid" runat="server"  Height="20px" AutoPostBack="true"
                                                   ontextchanged="txtSid_TextChanged"></asp:TextBox>
                                                   
                                                    <asp:TextBox ID="txtStdName" runat="server"  Height="20px" 
                                                     ReadOnly="true" style="margin-left:5px" Width="150px"></asp:TextBox>
                                           
                                           </td>
                                           </tr>
                                           
                                           <tr>
                                           <td style="width: 132px; font-weight: bold;">
                                               Year&nbsp;</td>
                                           <td style="width: 20px;font-weight: bold;">
                                               :</td>
                                           <td>
                                           
                                               <asp:TextBox ID="txt_year" runat="server"  Width="200px"></asp:TextBox></td>
                                           </tr>
                                               <tr>
                                                   <td style="font-weight: bold; width: 132px">
                                                       Semester</td>
                                                   <td style="font-weight: bold; width: 20px">
                                                       :</td>
                                                   <td>
                                                      
                                                    <asp:DropDownList ID="cmb_semester" runat="server">
                                                        <asp:ListItem Value="1">Spring</asp:ListItem>
                                                        <asp:ListItem Value="2">Summer</asp:ListItem>
                                                        <asp:ListItem Value="3">Fall</asp:ListItem>
                                                    </asp:DropDownList>
                                                       </td>
                                               </tr>
                                               <tr>
                                                   <td style="font-weight: bold; width: 132px">
                                                       Exam Type</td>
                                                   <td style="font-weight: bold; width: 20px">
                                                       :</td>
                                                   <td>
                                                      <asp:DropDownList ID="ddlExamtype" runat="server"  >
                                                <asp:ListItem Value="1">Final Examination</asp:ListItem>
                                                <asp:ListItem Value="2">Mid Term Examination</asp:ListItem>
                               
                                                    </asp:DropDownList>
                                                    </td>
                                               </tr>
                                           
                                           <tr>
                                           <td style="font-weight: bold; text-align: center;" colspan="3">
                                               <asp:Button ID="btnSubmit" runat="server" Height="33px" Text="Submit" 
                                                   Width="136px" Font-Bold="True" Font-Size="Large" 
                                                   onclick="btnSubmit_Click" />
                                               </td>
                                           </tr>
                                           
                                           <tr>
                                           <td style="font-weight: bold;" colspan="3">
                                               &nbsp;</td>
                                           </tr>
                                           
                                           
                                
                                           
                                           </table>
                                        </span></span>
                                    </td>
                                </tr>
                               
                                </table>
                        </td>
                        <td bgcolor="white" style="height: 114px" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1"  width="14" /></p>
                        </td>
                        <td  class="k" style="height: 114px" width="1">
                            <img border="0" height="1" width="1" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                          </td>
                        <td bgcolor="white" height="1" width="505">
                            <img border="0" height="14" width="1" /></td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                        </td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="505">
                            <img border="0" height="1" src="images/spacer(1).gif" width="150" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

