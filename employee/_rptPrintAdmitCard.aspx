<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_rptPrintAdmitCard.aspx.cs" Inherits="employee_rptPrintAdmitCard" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                                <b><font  face="Arial" size="2">Report for Eligible Admid Card for Exam</font></b> <hr />

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
                                                    <td class="auto-style1">
                                                     Select  Semester</td>
                                                    <td style="width: auto; height: auto">
                                                        <asp:DropDownList ID="ddlSemester" runat="server" Height="16px" Width="150px">
                                                            <asp:ListItem Value="1">Spring</asp:ListItem>
                                                            <asp:ListItem Value="2">Summer</asp:ListItem>
                                                            <asp:ListItem Value="3">Fall</asp:ListItem>
                                                        </asp:DropDownList>
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        Year&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtYear" runat="server" MaxLength="4" Width="100px"></asp:TextBox>
                                                     
                                        &nbsp;&nbsp;&nbsp;&nbsp;</span></span></td>
                                                    
                                                </tr>
                                                <tr>
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                                    <td class="auto-style1" __designer:mapid="159">
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                                        Exam Type</span></span></td>
                                                    <td style="width: auto; height: auto" __designer:mapid="15a">
                                                        <span style="font-size: 10pt" __designer:mapid="15b"><span style="color: #000000" __designer:mapid="15c">
                                                        <asp:DropDownList ID="ddlExamType" runat="server" Height="16px" Width="150px">
                                                            <asp:ListItem Value="M">Mid</asp:ListItem>
                                                            <asp:ListItem Value="F">Final</asp:ListItem>
                                                        </asp:DropDownList>      
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;      
                                        </span></span>
                                                    </td>
                                                    
                                        </span></span>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="auto-style1">
                                                        &nbsp;</td>
                                                    <td style="width: auto; height: auto">
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                                        <asp:Button ID="btnsubmitYearSem" runat="server"  Text="Submit" OnClick="btnsubmitYearSem_Click" />
                                                     
                                        </span></span>
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1" colspan="2">
                                                        <asp:Panel ID="pnlCourse" runat="server" Visible="false">
                                                            <table style="width:95%">
                                                                <tr>
                                                                    <td class="auto-style1">Course</td>
                                                                    <td style="width: auto; height: auto"><span style="font-size: 10pt"><span style="color: #000000">
                                                                        <asp:DropDownList ID="ddlCourse" runat="server" Height="16px" Width="150px">
                                                                        </asp:DropDownList>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Section<asp:TextBox ID="txtSection" runat="server" MaxLength="4" Width="100px"></asp:TextBox>
                                                                        </span></span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style1">&nbsp;</td>
                                                                    <td style="width: auto; height: auto"><span style="font-size: 10pt"><span style="color: #000000">
                                                                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Student List" Width="116px" />
                                                                        <asp:Button ID="btn_Clear" runat="server" OnClick="btn_Clear_Click" Text="Clear" />
                                                                        
                                                                         </span></span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style1">&nbsp;</td>
                                                                    <td style="width: auto; height: auto"> </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    
                                                </tr>
                                                
                                                <tr>
                                                    <td class="auto-style1">
                                                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td style="height: auto">
                                                       </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="auto-style1" colspan="2">
<p style="text-align:right; width:90%">

    <asp:Label ID="Label1" runat="server" Text="Print :" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <span style="font-size: 10pt"><span style="color: #000000">
<asp:ImageButton ID="Img1" Height="50px" ImageUrl="~/Images/PDF.png"
runat="server" onclick="Img1_Click" Visible="true"  />
    
                                                           
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

