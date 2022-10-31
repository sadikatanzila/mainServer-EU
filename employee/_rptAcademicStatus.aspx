<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_rptAcademicStatus.aspx.cs" Inherits="employee_rptAcademicStatus" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 173px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    
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
                                <b><font  face="Arial" size="2">Report for Total Academic Status</font></b> <hr />

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
                                                    <td class="auto-style1" >
                                                        Student ID</td>
                                                    <td style="width: auto; height: auto">
                                                        <span style="font-size: 10pt"><span style="color: #000000">
                                                        &nbsp;&nbsp;<asp:TextBox ID="txtSID" runat="server" MaxLength="4" Width="151px" OnTextChanged="txtSID_TextChanged"></asp:TextBox>
                                                     
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblSname" runat="server" Text=""></asp:Label>
                                                        </span></span></td>
                                                    
                                                </tr>
                                                
                                                <tr>
                                                    <td class="auto-style1"  >
                                                        &nbsp;</td>
                                                    <td style="width: auto; height: auto">
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                                        <asp:Button ID="btnsubmit" runat="server"  Text="Submit" OnClick="btn_submit_Click" />
                                                     
                                        </span></span>
                                                    </td>
                                                    
                                                </tr>
                                                
                                                <tr>
                                                    <td class="auto-style1"  >
                                                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td style="height: auto">
                                                       </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td   colspan="2">
<p style="text-align:right; width:90%">

    <asp:Label ID="Label1" runat="server" Text="Print :" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <span style="font-size: 10pt"><span style="color: #000000">
<asp:ImageButton ID="Img1" Height="50px" ImageUrl="~/Images/PDF.png"
runat="server" onclick="Img1_Click" Visible="false"  />
    
                                                           
                                        </span>

</p>
                                                                                                </span>
                                    
                                                     <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" Visible="true"
        BestFitPage="False" ToolPanelView="None" />                                   
    <br />
   
    
                                                           
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </span></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <hr />
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text=""></asp:Label></td>
                                </tr>

                                 <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: center">
                                      
                                        <br /><br />


                                    </td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
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
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

