<%@ Page Language="C#" MasterPageFile="~/staffs/courses/_courses_master.master" AutoEventWireup="true" CodeFile="_repCourseEvaluation.aspx.cs" Inherits="staffs_courses_repCourseEvaluation" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table style="width: 95%">
        <tr>
            <td class="header" style="color: #ffa500; height: 20px; background-color: white;
                text-align: left">
                <table border="0" style="text-align: left">
                    <tr>
                        <td colspan="3" style="width: 445px; text-align: left">
                            <span style="color: #ffa500">Your location- Courses &gt; Course List &gt; Course Details
                                &gt; Student List </span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="header" style="height: 44px; background-color: #ffffff; text-align: left">
                <table style="width: auto">
                    <tr>
                        <td style="width: auto; text-align: left; height: 20px;">
                            Select Course
                           
                        </td>
                        <td style="height: 20px"> <asp:DropDownList ID="cmb_course" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                
                    <tr>
                        <td style="width: auto; text-align: left">
                            Year&nbsp;
                            </td>
                            <td><asp:TextBox ID="txtYear" runat="server">
                            </asp:TextBox></td>
                    </tr>
                
                    <tr>
                        <td style="width: auto; text-align: left">
                            Semester
                           
                           </td>
                            <td> <asp:DropDownList ID="cmbSemester" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                    <td></td>
                    <td> <asp:Button ID="btnShow" runat="server"  Text="Submit" OnClick="btnShow_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

