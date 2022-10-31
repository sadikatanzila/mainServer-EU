<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_msgSending.aspx.cs" Inherits="employee_msgSending" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div>   
        <asp:Label ID="lblmsg" runat="server" Text="Label"></asp:Label><br />

            <br />
        Add Student ID :<asp:TextBox ID="txtSID" runat="server"></asp:TextBox>    
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Contact Number :
       <asp:TextBox ID="txtContact" runat="server"></asp:TextBox>    
            <br />
            <br />
            <asp:Button runat="server" ID="btnAdd" Text="Add Student" OnClick="btnsAdd_Click"></asp:Button>    
        <br />
        <br />

        <asp:GridView ID="GridView_studentList" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" GridLines="None">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                                	<asp:BoundField HeaderText="SID" DataField="SID">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                
                                                <asp:BoundField DataField="CONTACT" HeaderText="CONTACT">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="170px" />
                                                </asp:BoundField>
                                                
											 <asp:BoundField DataField="DUE" HeaderText="DUE">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="170px" />
                                                </asp:BoundField>
                                            
                                            </Columns>
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Smaller" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
        <br />

           <!-- <asp:Label ID="Label1" runat="server" Text="Enter Mobile Number"></asp:Label>    
            <asp:TextBox ID="txtmono1" runat="server"></asp:TextBox>    
            <br />  -->
       <asp:Label ID="Label2" runat="server" Text="Enter Message"></asp:Label>       
        <asp:TextBox ID="txtmsg" runat="server" Height="30px" Width="250px"></asp:TextBox>  <br />
            <asp:Button runat="server" ID="btnSent" Text="Sent SMS" OnClick="btnsend_Click"></asp:Button>    
         </div>  
    </form>
</body>
</html>
