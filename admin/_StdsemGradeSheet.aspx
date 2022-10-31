<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_StdsemGradeSheet.aspx.cs" Inherits="admin_StdsemGradeSheet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
   <script type="text/javascript" language="javascript">
 
function save_check()
{ 
    if(document.getElementById('ctl00_ContentPlaceHolder_definition_txt_year').value=="")
    {
        alert("Please enter the year");
        return false;
    }
    else return true;
    
}
</script>
   
   
   
   
   
   
   
   
    <table border="0" style="width:95%">
        <tr>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="95%">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                           </td>
                        <td  class="k" height="1" >
                           </td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td   class="h" height="22" >
                            <p align="center">
                                <b><font   face="Arial" size="2">Semester Grade Sheet</font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" >
                            <img border="0" height="1"   width="1" /></td>
                    </tr>
                    <tr>
                        <td  class="k" height="114" width="1">
                            <img border="0" height="1"   width="1" /></td>
                        <td bgcolor="white" height="114" width="18">
                            &nbsp;</td>
                        <td bgcolor="#ffffff" height="114"  valign="top">
                            <div style="text-align: left">
                                <br />
                                <table >
                                    <tr>
                                        <td style="width: 45px; height: auto">
                                            SID</td>
                                        <td style="height: auto" colspan="4">
                                            : &nbsp; <asp:TextBox ID="txtSID" runat="server"  Width="100px"></asp:TextBox></td>
                                    </tr>
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
                                            <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" /></td>
                                    </tr>
                                </table>
                                <hr />
                                <asp:Label ID="lbl_message" runat="server"  ForeColor="Red" Text="" Font-Bold="True"></asp:Label><br />
                                <br />
                                <br />
                                <br />
                                <asp:Panel ID="pnlAcademicStatus" runat="server" Style="text-align:left; margin-left:50px" Visible="false">

        <table style="width:95%">
            <tr>
                <td style="width:20%">

                    <asp:Label ID="Label1" runat="server" Text="Student ID" style="font-weight: 700"></asp:Label>
                </td>
                <td style="width:25%"> : &nbsp;
                    <asp:Label ID="lblSid" runat="server" Text=""></asp:Label>

                </td>
                <td style="width:10%">


                </td>
                <td style="width:15%">
                    <asp:Label ID="Label2" runat="server" Text="Faculty" style="font-weight: 700"></asp:Label>
                    
                </td>
                <td style="width:30%">: &nbsp;

                     <asp:Label ID="lblFaculty" runat="server" Text=""></asp:Label>
                </td>

            </tr>

            <tr>
                <td style="width:20%">
                    <asp:Label ID="Label3" runat="server" Text="Student Name" style="font-weight: 700"></asp:Label>
                </td>
                <td style="width:25%">: &nbsp;
                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                </td>
                <td style="width:10%">&nbsp;</td>
                <td style="width:15%">
                    <asp:Label ID="Label4" runat="server" Text="Program" style="font-weight: 700"></asp:Label>
                </td>
                <td style="width:30%">: &nbsp;


                    <asp:Label ID="lblProgram" runat="server" Text=""></asp:Label>


                </td>
            </tr>

            <tr>
                <td style="width:20%">
                    <asp:Label ID="Label5" runat="server" Text="Semester of Admission" style="font-weight: 700"></asp:Label>
                </td>
                <td style="width:25%">: &nbsp;<asp:Label ID="lblAdmissionYS" runat="server" Text=""></asp:Label>
                </td>
                <td style="width:10%">&nbsp;</td>
                <td style="width:15%">
                    <asp:Label ID="Label6" runat="server" Text="Major" style="font-weight: 700"></asp:Label>
                </td>
                <td style="width:30%">:
                    &nbsp; <asp:Label ID="lblMajor" runat="server" Text=""></asp:Label></td>
            </tr>

        </table>
        <br />
                                    <asp:Label ID="lbl_semester" runat="server" Font-Bold="True" Text=""></asp:Label>
                                    <br />
                             <asp:PlaceHolder ID="PlaceHolder_gradeSheet" runat="server"></asp:PlaceHolder>
    </asp:Panel>
                               
                                <br />
                            </div>
                        </td>
                        <td bgcolor="white" height="114" width="14">
                            &nbsp;
                            <p>
                                &nbsp;</p>
                        </td>
                        <td  class="k" height="114" width="1">
                            <img border="0" height="1"   width="1" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                            &nbsp;</td>
                        <td bgcolor="white" height="1" >
                            &nbsp;</td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" >
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

