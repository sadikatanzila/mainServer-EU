<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" 
CodeFile="_ConvoVourcherNo.aspx.cs" Inherits="admin_ConvoVourcherNo" Title="Convo Student Vourcher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <div style="width:98%;text-align:left">
<br />
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="Medium" 
                                            ForeColor="#0000CC" Text="Convocation Student Vourcher No" Font-Bold="True"></asp:Label>
                               
<hr />
        Previous Convocation Vouchar List:<br />
        Year : 
        <asp:TextBox ID="txtYear" runat="server"></asp:TextBox>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        <br />
<br />
        <asp:GridView ID="GridView_student" runat="server" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="4" Width="100%" OnRowDataBound="GridView_student_RowDataBound">
                                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                            <Columns>
                                            
      <asp:TemplateField HeaderText="Sl" ItemStyle-Width="1%">
                        <ItemTemplate>
                            <asp:Label ID="lblSerial" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" />
        </asp:TemplateField>
                          <asp:TemplateField HeaderText="ID" ItemStyle-Width="10%"> 
                            <ItemTemplate>  
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("SID") %>'></asp:Label>  </ItemTemplate>
                            </asp:TemplateField>
                                              
                    <asp:BoundField DataField="SNAME" HeaderText="Name" ItemStyle-Width="25%" />
                     <asp:BoundField DataField="PHONE" HeaderText="Contact" ItemStyle-Width="10%" />
                    <asp:BoundField DataField="VOUCARNO" HeaderText="VOUCAR NO" ItemStyle-Width="20%"/>
                    <asp:BoundField DataField="PDATE" HeaderText="Date of Imported" ItemStyle-Width="15%" />
                    <asp:BoundField DataField="CREATED" HeaderText="Updated in Bank" ItemStyle-Width="15%"/>
                    
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="#330099" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#666666" Font-Bold="True" Font-Size="Small" ForeColor="#FFFFCC" />
                                        </asp:GridView>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

