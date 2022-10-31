<%@ Page Language="C#" MasterPageFile="~/Online_Alumni/AlumniMaster.master" AutoEventWireup="true" CodeFile="_AcademicCalendarSearch.aspx.cs" Inherits="admin_AcademicCalendarSearch" Title="Academic Calendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <div style="width:100%; text-align: center;">
    <p style="font-weight: bold; font-size: large; color: #0000FF">
    Academic Calendar
       </p>
       </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
<div style="text-align: left;">

 <table border="0" style="width:100%">
        <tr>
            <td style="height: 755px">
               
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <table>
                                            <tr>
                                                <td style="width: 45px; height: auto">
                                                    &nbsp;</td>
                                                <td style="width: auto; height: auto">
                                                    &nbsp;</td>
                                                <td style="width: 35px; height: 22px">
                                                    &nbsp;</td>
                                                <td style="width: 49px; height: 22px">
                                                    &nbsp;</td>
                                                <td style="width: 102px; height: 22px">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 45px; height: auto">
                                                    Select</td>
                                                <td style="width: auto; height: auto">
                                                    <asp:DropDownList ID="cmb_semester" runat="server">
                                                        <asp:ListItem Value="1">Spring</asp:ListItem>
                                                        <asp:ListItem Value="2">Summer</asp:ListItem>
                                                        <asp:ListItem Value="3">Fall</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td style="width: 35px; height: 22px">
                                                    Year</td>
                                                <td style="width: 49px; height: 22px">
                                                    <asp:TextBox ID="txt_year" runat="server" MaxLength="4" Width="40px"></asp:TextBox></td>
                                                <td style="width: 102px; height: 22px">
                                                    <asp:Button ID="btn_submit" runat="server"  Text="Search" OnClick="btn_submit_Click" /></td>
                                            </tr>
                                        </table>
                                        <hr />
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="large" ForeColor="Red" Text=""></asp:Label><br />
                                       <br /><br />
                                        <div style="width:100%; text-align: center;">
                                        <asp:Label ID="lblCalender" runat="server" Text="" Style="color: #000099; font-weight: bold;"></asp:Label>
                                        </div>
                                        
                                        <asp:GridView ID="GridView_ClenderList" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" GridLines="None"  Width="98%">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Smaller" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                  <asp:TemplateField HeaderText="Event" ItemStyle-Width="40%"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblEvent" runat="server" Text='<%# Bind("EVENT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                       
                                         <ItemStyle Width="25px" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Date" ItemStyle-Width="60%"> 
                                        <ItemTemplate>
                                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("EventFDate1") %>'></asp:Label>
                                           <!-- <asp:Label ID="lblFdate" runat="server" Text='<%# Bind("EventFDate") %>'></asp:Label>-->
                                             <asp:Label ID="lblTdate" runat="server" Text='<%# Bind("EventLDate") %>'></asp:Label>
                                          
                                             
                                             
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" />
                                         <ItemStyle Width="25px" />
                                    </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                        </asp:GridView>
                                   
                                   <br />
                                    <div style="width:100%; text-align: center;">
                                   <asp:Label ID="lblHoliday" runat="server" Text="" Style="color: #000099; font-weight: bold;"></asp:Label>
                                  </div>
                                    <asp:GridView ID="GridView_HolidayList" runat="server" AutoGenerateColumns="False" 
                                            CellPadding="4" ForeColor="#333333" GridLines="None"  Width="98%">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Smaller" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                            
                                   <asp:TemplateField HeaderText="Event" ItemStyle-Width="40%" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblEvent" runat="server" Text='<%# Bind("DAY_TITLE") %>' ForeColor="red"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                         <ItemStyle Width="25px" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Date" ItemStyle-Width="60%"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblFdate" runat="server" Text='<%# Bind("EventFDate") %>' ForeColor="red"></asp:Label>
                                             <asp:Label ID="lblTdate" runat="server" Text='<%# Bind("EventLDate") %>' ForeColor="red"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                         <ItemStyle Width="25px" />
                                    </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                        </asp:GridView>
                         
                                   
                                   
                                   
                                    </td>
                                </tr>
                            </table>
                <asp:Label ID="txt" runat="server" style="margin-left:50px;" Text="* Subject to appearance of the Moon" ForeColor="Red"></asp:Label></td>
        </tr>
    </table>

</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

