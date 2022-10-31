<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="student_AdmitCard_Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1"  runat="server">
    <center>
        <div>
        </div>
        <br />
        <br />
    </center>
    <div>
        
            <asp:Label ID="Label" runat="server" Text="Student ID:"></asp:Label>   
            <asp:TextBox ID="txtID" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label1" runat="server" Text="Year:"></asp:Label>  
            <asp:TextBox ID="txtYar" runat="server" ></asp:TextBox><br />
            <asp:Label ID="Label2" runat="server" Text="Sem Value:"></asp:Label>  
             <asp:TextBox ID="txtSem" runat="server" ></asp:TextBox><br />
            <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
        <asp:Label ID="lblmsg" runat="server" Text="" Font-Bold="true" ForeColor="red"></asp:Label><br />
        <asp:Label ID="lblDue" runat="server" Text="Due"></asp:Label>
        <br />
        Due: <asp:Label ID="lblDueT" runat="server" Text="Due"></asp:Label>
        <br />
        with Gross Amount: <asp:Label ID="lblGrs" runat="server" Text="Due"></asp:Label>
        </div>
    </form>
</body>
</html>
