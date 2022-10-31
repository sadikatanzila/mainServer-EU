<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_academicStatus.aspx.cs" Inherits="staffs_advisor_academicStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EU POrtal | Academic Satatus</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0">
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table border="0" cellpadding="0" cellspacing="0" height="1" width="539">
                        <tr>
                            <td colspan="2" height="24" rowspan="3" width="19">
                                <img border="0" height="24" src="../images/lcurv.gif" width="19" /></td>
                            <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                                <img border="0" height="1" src="../images/spacer(1).gif" width="100%" /></td>
                            <td align="right" colspan="2" height="24" rowspan="3" width="15">
                                <img border="0" height="24" src="../images/rcurv.gif" width="15" /></td>
                        </tr>
                        <tr>
                            <td bgcolor="#eef5fa" class="h" height="22" width="505">
                                <p align="center">
                                    <b><font color="#ffa500" face="Arial" size="2">Academic Status</font></b></p>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                                <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                        </tr>
                        <tr>
                            <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                                <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                            <td bgcolor="white" height="114" width="18">
                                <img border="0" height="1" src="../images/spacer(1).gif" width="18" /></td>
                            <td bgcolor="#ffffff" height="114" valign="top" width="505">
                                <div style="text-align: left">
                                    <br />
                                    <asp:PlaceHolder ID="PlaceHolder_gradeSheet" runat="server"></asp:PlaceHolder>
                                </div>
                            </td>
                            <td bgcolor="white" height="114" width="14">
                                &nbsp;
                                <p>
                                    <img border="0" height="1" src="../images/spacer(1).gif" width="14" /></p>
                            </td>
                            <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                                <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                                <img border="0" height="15" src="../images/blcurv.gif" width="19" /></td>
                            <td bgcolor="white" height="1" width="505">
                                <img border="0" height="14" src="../images/spacer(1).gif" width="1" /></td>
                            <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                                <img border="0" height="15" src="../images/brcurv.gif" width="15" /></td>
                        </tr>
                        <tr>
                            <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                                <img border="0" height="1" src="../images/spacer(1).gif" width="150" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
