<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_Student_AcademicStatus.aspx.cs" Inherits="admin_Student_AcademicStatus"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 248px;
        }
        </style>
    
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
       <table border="0" style="width:95%">
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
                                <b><font  face="Arial" size="2"> Students' Academic Status (Details Transcript)</font></b> <hr />

</p>
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
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                            <br />
                                            <table style="width:75%">
                                                
                                                <tr>
                                                    <td class="auto-style1" colspan="2">
                                                        Student ID<span style="font-size: 10pt"><span style="color: #000000">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtSID" runat="server"  Width="150px"></asp:TextBox>
                                                     
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Academic Status" Width="149px" />
                                                         &nbsp;&nbsp;&nbsp;&nbsp;               <asp:Button ID="btn_Clear" runat="server" OnClick="btn_Clear_Click" Text="Clear" />
                                                                        
                                                        </span></span></td>
                                                    
                                                </tr>
                                                
                                                
                                                <tr>
                                                    <td class="auto-style1">
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                                        <asp:Label ID="lblErrorMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        </span></span>
                                                    </td>
                                                    <td style="width: auto; height: auto">
                                                        &nbsp;</td>
                                                    
                                                </tr>
                                                
                                                
                                                </table>
                                       



                                        <hr />
                                        
                            
                                    </td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text=""></asp:Label></td>
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
                    </table>
            </td>
        </tr>
    </table>
  
    <asp:Panel ID="pnlAcademicStatus" runat="server" Style="text-align:left; margin-left:50px" Visible="false">

        <table style="width:95%">
            <tr>
                <td style="width:20%">

                    <asp:Label ID="Label1" runat="server" Text="Student ID" style="font-weight: 700"></asp:Label>
                </td>
                <td style="width:20%"> : &nbsp;
                    <asp:Label ID="lblSid" runat="server" Text=""></asp:Label>

                </td>
                <td style="width:10%">


                </td>
                <td style="width:20%">
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
                <td style="width:20%">: &nbsp;
                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                </td>
                <td style="width:10%">&nbsp;</td>
                <td style="width:20%">
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
                <td style="width:20%">: &nbsp;<asp:Label ID="lblAdmissionYS" runat="server" Text=""></asp:Label>
                </td>
                <td style="width:10%">&nbsp;</td>
                <td style="width:20%">
                    <asp:Label ID="Label6" runat="server" Text="Major" style="font-weight: 700"></asp:Label>
                </td>
                <td style="width:30%">:
                    &nbsp; <asp:Label ID="lblMajor" runat="server" Text=""></asp:Label></td>
            </tr>

        </table>
        <br /><br />
                                <asp:PlaceHolder ID="PlaceHolder_gradeSheet" runat="server"></asp:PlaceHolder>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

