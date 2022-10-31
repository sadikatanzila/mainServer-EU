<%@ Page Language="C#" MasterPageFile="~/student/AdmitCard/Admit_masterPage.master" 
AutoEventWireup="true" CodeFile="_AdmitCardPrev.aspx.cs" Inherits="student_AdmitCard_AdmitCardPrev" Title="Admit Card" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
<table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Admit Card&gt;Print Admit Card</span></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
   <table border="0" id="TABLE1" runat="server" width="540">
        <tr>
            <td style="text-align: left">
                <table>
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
                            Exam Type</td>
                        <td style="width: 102px; height: 22px">
                            <asp:DropDownList ID="ddlExamtype" runat="server">
                                <asp:ListItem Value="1">Final Examination</asp:ListItem>
                                <asp:ListItem Value="2">Mid Term Examination</asp:ListItem>
                               
                            </asp:DropDownList></td>
                        <td style="width: 102px; height: 22px">
                            <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" /></td>
                    </tr>
                </table>
                <hr />
                <asp:Label ID="lbl_message" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red" Text="Label"></asp:Label></td>
        </tr>
       <tr>
           <td style="height: 15px; text-align: left">
               
               
              
               
               </td>
       </tr>
       <tr>
           <td style="height: 15px; text-align: left">
               
               
                     &nbsp;</td>
       </tr>
        </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

