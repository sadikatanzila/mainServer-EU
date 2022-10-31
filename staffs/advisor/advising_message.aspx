<%@ Page Language="C#" MasterPageFile="~/staffs/advisor/MasterPage_advisor.master" AutoEventWireup="true" CodeFile="advising_message.aspx.cs" Inherits="staffs_advisor_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <br />
    <br />
    <br />
    <asp:Label ID="lbl_message" runat="server" Font-Bold="True" ForeColor="Blue" Text="Label"></asp:Label>
     <asp:Label ID="lblSemester" runat="server"  Visible="false" Text="Label"></asp:Label>
     
     
     <div style="line-height:50px">
                        <strong>The following Course(s) are approved by Advisor:</strong>
                    </div>
                    <!--
                    <asp:GridView RowStyle-Height="25" ID="gvStudentCourse" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Course Code" HeaderStyle-Width="100px" ItemStyle-VerticalAlign="Top"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("COURSEKEY").ToString().Substring(5) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="cName" HeaderText="Course Name" ItemStyle-VerticalAlign="Top"
                                HeaderStyle-Width="250px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="STAFF_NAME" HeaderText="Teacher" ItemStyle-VerticalAlign="Top"
                                HeaderStyle-Width="180px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="CHOURS" HeaderText="Credit" ItemStyle-VerticalAlign="Top"
                                HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GGROUP" HeaderText="Group" ItemStyle-VerticalAlign="Top"
                                HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="SCH_CLS_1" HeaderText="1st Class" ItemStyle-VerticalAlign="Top"
                                HeaderStyle-Width="120px" />
                            <asp:BoundField DataField="SCH_CLS_2" HeaderText="2nd Class" ItemStyle-VerticalAlign="Top"
                                HeaderStyle-Width="120px" />
                        </Columns>
                    </asp:GridView>
                    -->
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" NavigateUrl="~/staffs/advisor/_courseAdvisingList.aspx"><br></br>Click here to complete advising others</asp:HyperLink><br />
    <br />
    <br />
    <br />
    <br />
    <asp:Button ID="btnPrint" runat="server" Text="Print Approval Course List" 
                                            onclick="btnPrint_Click" Height="40px" 
        Width="217px" />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

