<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_studentAttendanceP.aspx.cs" Inherits="staffs_courses_studentAttendanceP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     ORIGINAL Table:    
  <asp:GridView ID="GridView1" runat="server">
  </asp:GridView><br /><br />
  PIVOTED Table:
  <asp:GridView ID="GridView2" runat="server"  ShowHeader="false">
  </asp:GridView>



          <asp:GridView ID="gvStdAttendance" runat="server" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="3" Width="100%" OnDataBinding="gvStdAttendance_DataBinding">
                            <Columns>
                                                              
               <asp:TemplateField  HeaderText="SID">
                         <ItemTemplate>
                             <asp:Label runat="server" style="padding:12px 5px;"   ID="lblSID"></asp:Label>
                         </ItemTemplate>
                          <ItemStyle HorizontalAlign="left" />
                          <HeaderStyle HorizontalAlign="left" />
                     </asp:TemplateField>
                                   
               <asp:TemplateField HeaderText="Date" >
                         <ItemTemplate>
                             <asp:Label runat="server" style="padding:12px 5px;"  ID="lblDate"></asp:Label>
                         </ItemTemplate>
                          <ItemStyle HorizontalAlign="left" />
                          <HeaderStyle HorizontalAlign="left" />
                     </asp:TemplateField>
  </Columns>
              <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <RowStyle ForeColor="#000066" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="AntiqueWhite" />
                        </asp:GridView>
    </div>
    </form>
</body>
</html>
