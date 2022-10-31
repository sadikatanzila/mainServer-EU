<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_cngPassword.aspx.cs" Inherits="admin_cngPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 207px;
            font-weight:bold;
        }
        .auto-style2 {
            width: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Account- Student&gt; Reset Password</span></td>
        </tr>
    </table>    
<script type="text/javascript" language="javascript">
    function save_check() {
        if (document.getElementById('ctl00_ContentPlaceHolder_definition_txt_usetId').value == "") {
            alert("Please enter staff ID");
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
    
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
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
                    <b><font  face="Arial" size="2">Reset Passord</font></b></p>
            </td>
        </tr>
        <tr>
            <td  class="k" height="1" width="100%">
               </td>
        </tr>
        <tr>
            <td  class="k" height="114" width="1">
               </td>
            <td bgcolor="white" height="114" width="18">
               </td>
            <td bgcolor="#ffffff" height="114" width="100%">
                <div style="text-align: left">
                    <br />
                    <table border="0" style="width:60%">
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lbl_message" runat="server" Font-Bold="True" ForeColor="Red" Text=""></asp:Label></td>
                        </tr>
                        <tr style="color: #000000">
                            <td class="auto-style1">
                                Employee ID &nbsp;
                            </td>
                            <td class="auto-style2">
                                :</td>
                            <td>
                                <asp:DropDownList ID="cmb_EmpID" runat="server"  AutoPostBack="true"
                                                        onselectedindexchanged="cmb_EmpID_SelectedIndexChanged"  Width="150px">
                                                    </asp:DropDownList>
                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     <asp:Label ID="lblName" runat="server" Font-Bold="True" ></asp:Label>
                            </td>
                        </tr>
                        <tr style="color: #000000">
                            <td class="auto-style1">
                                Previous Password</td>
                            <td class="auto-style2">
                                :</td>
                            <td>
                                <asp:Label ID="lblPrevPassword" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                                New password</td>
                            <td class="auto-style2">
                                :</td>
                            <td>
                                <asp:TextBox ID="txt_newPass" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                                Confirm password</td>
                            <td class="auto-style2">
                                :</td>
                            <td>
                                <asp:TextBox ID="txt_confirmPass" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                            </td>
                            <td class="auto-style2">
                            </td>
                            <td>
                                <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td bgcolor="white" height="114" width="14">
                &nbsp;
                <p>
                    <img border="0" height="1" src="../images/spacer(1).gif" width="14" /></p>
            </td>
            <td  class="k" height="114" width="1">
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
                <img border="0" height="1" src="../images/spacer(1).gif" width="150" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

