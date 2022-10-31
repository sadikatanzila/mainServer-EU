<%@ Page Language="C#" AutoEventWireup="true" CodeFile="class_routine.aspx.cs" Inherits="student_academics_class_routine" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <asp:Label ID="lbl_title" runat="server" Font-Bold="True" Font-Size="Larger" Text="Label" Font-Underline="True"></asp:Label><br />
        <asp:Label ID="lbl_student" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Crimson"
            Text="Label"></asp:Label><br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1"
            GridLines="None">
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <Columns>
                <asp:BoundField DataField="coursecode" HeaderText="Code">
                    <ItemStyle Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="cname" HeaderText="Title" >
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ggroup" HeaderText="Group" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="staff_name" HeaderText="Instructor" >
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="sch_cls_1" HeaderText="1st Class" >
                    <ItemStyle Width="65px" />
                </asp:BoundField>
                <asp:BoundField DataField="sch_cls_2" HeaderText="2nd Class" >
                    <ItemStyle Width="68px" />
                </asp:BoundField>
                <asp:BoundField DataField="sch_cls_3" HeaderText="3rd Class" >
                    <ItemStyle Width="65px" />
                </asp:BoundField>
                <asp:BoundField DataField="tut_cls_2" HeaderText="4th Class"  Visible="false">
                    <ItemStyle Width="65px" />
                </asp:BoundField>
            </Columns>
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        </asp:GridView>
        &nbsp;
    
    </div>
    </form>
</body>
</html>
