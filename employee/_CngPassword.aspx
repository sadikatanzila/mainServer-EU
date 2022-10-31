<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_CngPassword.aspx.cs" Inherits="employee_CngPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">

  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">

      <script type="text/javascript" language="javascript">

          function save_check() {
              if (document.getElementById('ctl00_ContentPlaceHolder_definition_txt_previousPass').value == "") {
                  alert("Please enter current password.");
                  return false;
              }
              else if (document.getElementById('ctl00_ContentPlaceHolder_definition_txt_newPass').value == "") {
                  alert("Please enter new password.");
                  return false;
              }
              else if (document.getElementById('ctl00_ContentPlaceHolder_definition_txt_confirmPass').value == "") {
                  alert("Please enter confirm password.");
                  return false; txt_confirmPass
              }
              else if (document.getElementById('ctl00_ContentPlaceHolder_definition_txt_newPass').value.toString() != document.getElementById('ctl00_ContentPlaceHolder_definition_txt_confirmPass').value.toString()) {
                  alert("Confirm password is not correct.");
                  return false; txt_confirmPass
              }

              else return true;

          }
</script>
    <table style="width:100%">
        <tr>
            <td>
                <p align="center">
                                <b><font face="Arial" size="2">Change Passord</font></b></p>
                             <hr style="color: #333333" />
            </td>
        </tr>
        <tr>
            <td>
                    <asp:Label ID="lbl_message" runat="server" Font-Bold="True" ForeColor="Red" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div id="cngPass">
                    <table style="width:75%">
                        <tr>
                            <td colspan="3">

                                        

                            </td>
                        </tr>

                        <tr>
                            <td style="width:25%">

                                            Current password &nbsp;</td>
                            <td style="width:20px; font-weight: bold;">

                                :</td>
                            <td>

                                            <asp:TextBox ID="txt_previousPass" runat="server" TextMode="Password"></asp:TextBox>

                            </td>
                        </tr>

                        <tr>
                            <td style="width:25%">

                                            New password</td>
                            <td style="width:20px; font-weight: bold;">

                                :</td>
                            <td>

                                            <asp:TextBox ID="txt_newPass" runat="server" TextMode="Password"></asp:TextBox>

                            </td>
                        </tr>

                        <tr>
                            <td style="width:25%">

                                            Confirm password</td>
                            <td style="width:20px; font-weight: bold;">

                                :</td>
                            <td>

                                            <asp:TextBox ID="txt_confirmPass" runat="server" TextMode="Password"></asp:TextBox>

                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" style="text-align: center">

                                            <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" Height="30px" Width="125px" />

                            </td>
                        </tr>

                    </table>

                </div>
            </td>

        </tr>

    </table>
     
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

