<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_leaveHistory.aspx.cs" Inherits="HR_CONTROLLER_leaveHistory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="GridView_LeaveBalance" runat="server" AutoGenerateColumns="False" ShowFooter="false"
                                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="4" Width="70%">
                                            <FooterStyle BackColor="#D9E6FF" ForeColor="#330099" />
                                            <Columns>

                              <asp:TemplateField HeaderText="Sl" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>  


                            <asp:TemplateField HeaderText="Leave Type" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblLeaveType" runat="server" Text='<%# Bind("LeaveType") %>'></asp:Label>                                         
                                </ItemTemplate>

                            </asp:TemplateField>     
                                                
                                                 
                            <asp:TemplateField HeaderText="Total" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("total") %>'></asp:Label>                                         
                                </ItemTemplate>

                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="Taken" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblTaken" runat="server" Text='<%# Bind("taken_Leave") %>'></asp:Label>                                         
                                </ItemTemplate>

                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="Balance" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblBalance" runat="server" Text='<%# Bind("Balance") %>'></asp:Label>                                         
                                </ItemTemplate>

                       
                            </asp:TemplateField>                   
                             

                                                                             
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="#330099" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#6699FF" Font-Bold="True" Font-Size="Small" ForeColor="#FFFFCC" />
                                        </asp:GridView>
    </div>
    </form>
</body>
</html>
