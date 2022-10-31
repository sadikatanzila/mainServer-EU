<%@ Page Language="C#" AutoEventWireup="true" CodeFile="teacherevaluation_result.aspx.cs" Inherits="staffs_courses_teacherevaluation_result" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Eastern University Web Portal</title>
    <link href="../../App_themes/basic_style.css" rel="stylesheet" type="text/css" />
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
    <asp:Panel ID="pnlEveReport" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" style="width:100%" >
            <tr>
                <td style="text-align:center;"><table cellpadding="0" cellspacing="5" border="0">
                        <tr>
                            <td style="font-size:20px;">EASTERN UNIVERSITY</td>            
                        </tr>
                        <tr>
                            <td style="font-weight:bold;font-size:14px">Faculty of <asp:Label ID="lbl_faculty" runat="server" Text=""></asp:Label></td>            
                        </tr>
                        <tr>
                            <td style="font-weight:bold;"><asp:Label ID="lbl_program" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="height:20px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="font-size:12px;font-weight:bold;">EVALUATION BY STUDENT</td>
                        </tr>
                        <tr>
                            <td style="font-weight:bold;">Semester : <asp:Label ID="lbl_semester" runat="server" Text=""></asp:Label></td>
                        </tr>
                    <tr>
                        <td style="font-weight: bold">
                        </td>
                    </tr>
                    </table>                
                </td>
             </tr>            
            <tr>
                <td><table border="0" cellpadding="0" cellspacing="0" style="width:100%" >
                        <tr>
                            <td style="width:50px;">&nbsp;</td>
                            <td><table border="0" cellpadding="0" cellspacing="5" style="font-weight:bold;">
                                    <tr>
                                        <td>Name of the Teacher</td>
                                        <td> : <asp:Label ID="lbl_teacherName" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Course Number</td>
                                        <td> : <asp:Label ID="lbl_courseCode" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Course Title</td>
                                        <td> : <asp:Label ID="lbl_courseTitle" runat="server" Text=""></asp:Label></td>
                                    </tr>
                            
                                </table>                            
                            </td>
                        </tr>  
                                        
                        <tr>
                            <td colspan="7"><asp:PlaceHolder ID="PlaceHolder_data" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>  
                        <tr>
                            <td style="width:50px;">&nbsp;</td>
                            <td>
                           </td>
                        </tr>                
                    </table>               
                
                </td>            
            </tr>
            <tr>
                <td></td>            
            </tr>
        </table>
        </asp:Panel>
        
        <asp:Label ID="lblmessage" runat="server" Text="" ForeColor=Red Font-Size=Large Font-Bold=true></asp:Label>
    
    </form>
</body>
</html>
