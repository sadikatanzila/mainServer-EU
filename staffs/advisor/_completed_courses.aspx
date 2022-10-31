<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_completed_courses.aspx.cs" Inherits="staffs_advisor_completed_courses" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
      <title>COMPLETED COURSES</title>     
    <link href="../../App_themes/jind.css" rel="stylesheet" type="text/css"/>
    <link  href="../../App_themes/transmenuh.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../../App_themes/transmenu.js" ></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" height="1" width="539">
                        <tr>
                            <td colspan="2" height="24" rowspan="3" width="19">
                                <img border="0" height="24" src="../../staffs/images/lcurv.gif" width="19" /></td>
                            <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="100%" /></td>
                            <td align="right" colspan="2" height="24" rowspan="3" width="15">
                                <img border="0" height="24" src="../../staffs/images/rcurv.gif" width="15" /></td>
                        </tr>
                        <tr>
                            <td bgcolor="#eef5fa" class="h" height="22" width="505">
                                <p align="center">
                                    <b><font color="#333399" face="Arial" size="2"><span style="color: #ffa500">Student Information</span></font></b></p>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        </tr>
                        <tr>
                            <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                            <td bgcolor="white" height="114" width="18">
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="18" /></td>
                            <td align="left" bgcolor="#ffffff" height="114" width="505">
                                <br />
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="3" style="text-align: center">
                                            <strong><span style="text-decoration: underline">Admission Information</span></strong></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ID</td>
                                        <td>
                                            :&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_id" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Name</td>
                                        <td>
                                            :</td>
                                        <td>
                                            <asp:Label ID="lbl_name" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Program</td>
                                        <td>
                                            :</td>
                                        <td>
                                            <asp:Label ID="lbl_program" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Major</td>
                                        <td style="font-weight: bold">
                                            :</td>
                                        <td>
                                            <asp:Label ID="lbl_major" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Admission
                                        </td>
                                        <td>
                                            :</td>
                                        <td>
                                            <asp:Label ID="lbl_admission" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Admission as</td>
                                        <td>:</td>
                                        <td ><asp:Label ID="lbl_admissionAs" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Completed Credit&nbsp;
                                        </td>
                                        <td>
                                            :</td>
                                        <td>
                                            <asp:Label ID="lbl_completd_hrs" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CGPA</td>
                                        <td>
                                            :</td>
                                        <td>
                                            <asp:Label ID="lbl_cgpa" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: center">
                                            &nbsp; <span style="text-decoration: underline"><strong>Personal Information</strong></span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Gender</td>
                                        <td>
                                            :</td>
                                        <td>
                                            <asp:Label ID="lbl_gender" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Date of barth</td>
                                        <td style="font-weight: bold; font-size: 10pt; color: #333399; font-family: Arial">
                                            :</td>
                                        <td>
                                            <asp:Label ID="lbl_dob" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Address</td>
                                        <td>
                                            :</td>
                                        <td>
                                            <asp:Label ID="lbl_address" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Phone</td>
                                        <td>
                                            :</td>
                                        <td>
                                            <asp:Label ID="lbl_phone" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            E-mail</td>
                                        <td>
                                            :</td>
                                        <td>
                                            <asp:Label ID="lbl_email" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Advisor</td>
                                        <td>
                                            :</td>
                                        <td>
                                            <asp:Label ID="lbl_advisor" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Comments</td>
                                        <td>
                                            :</td>
                                        <td>
                                            <asp:Label ID="lbl_comments" runat="server" Text="Label"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                            <td bgcolor="white" height="114" width="14">
                                &nbsp;
                                <p>
                                    <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="14" /></p>
                            </td>
                            <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                                <img border="0" height="15" src="../../staffs/images/blcurv.gif" width="19" /></td>
                            <td bgcolor="white" height="1" width="505">
                                <img border="0" height="14" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                            <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                                <img border="0" height="15" src="../../staffs/images/brcurv.gif" width="15" /></td>
                        </tr>
                        <tr>
                            <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="150" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" height="1" width="539">
                        <tr>
                            <td colspan="2" height="24" rowspan="3" width="19">
                                <img border="0" height="24" src="../../staffs/images/lcurv.gif" width="19" /></td>
                            <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="100%" /></td>
                            <td align="right" colspan="2" height="24" rowspan="3" width="15">
                                <img border="0" height="24" src="../../staffs/images/rcurv.gif" width="15" /></td>
                        </tr>
                        <tr>
                            <td bgcolor="#eef5fa" class="h" height="22" width="505">
                                <p align="center">
                                    <b><font color="#333399" face="Arial" size="2"><span style="color: #ffa500">Completed Courses</span></font></b></p>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        </tr>
                        <tr>
                            <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                            <td bgcolor="white" height="114" width="18">
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="18" /></td>
                            <td align="left" bgcolor="#ffffff" height="114" valign="top" width="505">
                                <br />
                                <asp:Localize ID="Localize1" runat="server"></asp:Localize>
                                <asp:PlaceHolder ID="PlaceHolder_001" runat="server"></asp:PlaceHolder>
                            </td>
                            <td bgcolor="white" height="114" width="14">
                                &nbsp;
                                <p>
                                    <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="14" /></p>
                            </td>
                            <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                                <img border="0" height="15" src="../../staffs/images/blcurv.gif" width="19" /></td>
                            <td bgcolor="white" height="1" width="505">
                                <img border="0" height="14" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                            <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                                <img border="0" height="15" src="../../staffs/images/brcurv.gif" width="15" /></td>
                        </tr>
                        <tr>
                            <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="150" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
