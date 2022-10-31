<%@ Page Language="C#" AutoEventWireup="true" CodeFile="accountLedger.aspx.cs" Inherits="student_finance_accountLedger" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Finalcial Status | Student Ledger</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="  font-size: 9pt; font-family:Verdana">
       <table style="width:98%">
           <tr>
               <td style="width:15%">

                    <asp:Label ID="Label1" runat="server" Text="SID :" style="font-weight: 700"></asp:Label>
               </td>
               <td style="width:35%">
                     <asp:Label ID="lblSID" runat="server" Text=""></asp:Label>

               </td>
                <td style="width:15%">

                       <asp:Label ID="Label3" runat="server" Text="Program :" style="font-weight: 700"></asp:Label>
              
               </td>
               <td style="width:35%">
                    <asp:Label ID="lblProgram" runat="server" Text=""></asp:Label>

               </td>
           </tr>


           <tr>
               <td style="width:15%">

                     <asp:Label ID="Label2" runat="server" Text="Name :" style="font-weight: 700"></asp:Label>
             


          

                   </td>
           
               <td style="width:35%">
                      <asp:Label ID="lblName" runat="server" Text=""></asp:Label></td>
                <td style="width:15%">

                        <asp:Label ID="Label4" runat="server" Text="Credit Rate :" style="font-weight: 700"></asp:Label>
              </td>
               <td style="width:35%">
                     <asp:Label ID="lblCRate" runat="server" Text=""></asp:Label></td>
           </tr>


       </table>
       <br /><br />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        &nbsp;
    
    </div>
    </form>
</body>
</html>
