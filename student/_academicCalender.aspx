<%@ Page Language="C#" MasterPageFile="~/student/academics/academics_masterPage.master" AutoEventWireup="true" CodeFile="_academicCalender.aspx.cs" Inherits="student_academics_academicCalender"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Academics&gt;Academic Calender</span></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table bgcolor="#ffffff" border="0" cellpadding="1" cellspacing="1" width="95%">
        <tr>
            <td style="height: 14px; text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 14px; text-align: left; margin-left:50px">
                <table>
                                            <tr>
                                                <td style="width: 45px; height: auto">
                                                    <strong>Select</strong></td>
                                                <td style="width: auto; height: auto">
                                                    <asp:DropDownList ID="cmb_semester" runat="server">
                                                        <asp:ListItem Value="1">Spring</asp:ListItem>
                                                        <asp:ListItem Value="2">Summer</asp:ListItem>
                                                        <asp:ListItem Value="3">Fall</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                                <td style="width: 35px; height: 22px">
                                                    <strong>Year</strong></td>
                                                <td style="width: 49px; height: 22px">
                                                    <asp:TextBox ID="txt_year" runat="server" MaxLength="4" Width="40px"></asp:TextBox></td>
                                                <td style="width: 102px; height: 22px">
                                                    <asp:Button ID="btn_submit" runat="server"  Text="Search" OnClick="btn_submit_Click" /></td>
                                            </tr>
                                        </table>

            </td>
        </tr>
        <tr>
            <td style="height: 14px; text-align: left">
                 <hr />
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>

            </td>
        </tr>
        <tr>
            <td style="height: 14px; text-align: left">
                 &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 14px; text-align: center">
                  <asp:Label ID="lblCalender" runat="server" Style="color: #000099; font-weight: bold;" Font-Size="Medium"></asp:Label>

            </td>
        </tr>
        <tr style="font-size: 8pt; color: #ff0066; ">
            <td style="text-align: justify">
                
              

                <asp:GridView ID="GridView_ClenderList" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" GridLines="None"  Width="99%">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Smaller" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                  <asp:TemplateField HeaderText="Event" ItemStyle-Width="30%"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblEvent" runat="server" Text='<%# Bind("EVENT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                      <HeaderStyle Font-Size="Medium" />
                                       
                                         <ItemStyle Width="25px" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Date" ItemStyle-Width="70%"> 
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("EventFDate1") %>'></asp:Label>
                                           <!-- <asp:Label ID="lblFdate" runat="server" Text='<%# Bind("EventFDate") %>'></asp:Label>-->
                                             <asp:Label ID="lblTdate" runat="server" Text='<%# Bind("EventLDate") %>'></asp:Label>                                        
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" />
                                           <HeaderStyle Font-Size="Medium" />
                                         <ItemStyle Width="25px" />
                                    </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                        </asp:GridView>



            </td>
        </tr>
        <tr>
            <td style="height: 14px">
            </td>
        </tr>
        <tr>
            <td style="height: 14px; text-align: left">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Crimson"
                    Text="List of Holidays"></asp:Label></td>
        </tr>
        <tr>
            <td style="height: 14px; text-align: center">
                 <asp:Label ID="lblHoliday" runat="server" Text="" Style="color: #000099; font-weight: bold;" Font-Size="Medium"></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align: left;">
               
               

                <asp:GridView ID="GridView_HolidayList" runat="server" AutoGenerateColumns="False" 
                                            CellPadding="4" ForeColor="#333333" GridLines="None"  Width="99%">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Smaller" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                            
                                   <asp:TemplateField HeaderText="Event" ItemStyle-Width="30%" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblEvent" runat="server" Text='<%# Bind("DAY_TITLE") %>' ForeColor="red"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                         <HeaderStyle Font-Size="Medium" />
                                         <ItemStyle Width="25px" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Date" ItemStyle-Width="75%"> 
                                        <ItemTemplate>
                                          <asp:Label ID="lblFdate" runat="server" Text='<%# Bind("EventFDate") %>' ForeColor="red"></asp:Label>
                                             <asp:Label ID="lblTdate" runat="server" Text='<%# Bind("EventLDate") %>' ForeColor="red"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <HeaderStyle Font-Size="Medium" />
                                         <ItemStyle Width="25px" />
                                    </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                        </asp:GridView>


            </td>
        </tr>
        <tr>
            <td style="height: 14px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 14px; text-align: left;">
                *<em><span style="color: #ff0066">Subject to appearance of the moon</span></em></td>
        </tr>
        <tr>
            <td style="height: 14px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 14px; text-align: left;">
                <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

