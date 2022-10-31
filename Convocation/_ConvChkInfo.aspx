<%@ Page Language="C#" MasterPageFile="~/Convocation/ConvoMaster.master" AutoEventWireup="true" CodeFile="_ConvChkInfo.aspx.cs" Inherits="Convocation_ConvChkInfo" Title="Convocation Check Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">

<div style="text-align: center">
<br />
 <strong><span style="text-decoration: underline; font-size: large; color: #000099;">6th Convocation Registration Form</span></strong>
 <br /><br />
  <asp:Label ID="lbl_message" runat="server" ForeColor="Red" Text=""  Font-Bold="True"></asp:Label>
<div style="width:100%; text-align:left">
 <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                ForeColor="Red" HeaderText="Page Errors" ShowMessageBox="True"
                ShowSummary="true" DisplayMode="List" />
              
<asp:Panel ID="pnlDegreeRequired" runat="server">
   
<table style="width:100%;text-align: left; ">
<tr>
<td colspan="6">
   
    
    </td>
    <td colspan="1">
    </td>
    <td colspan="1" style="width: 671px">
    </td>
</tr>

<tr>
<td colspan="6">
    <asp:Label ID="lblErrors" runat="server" ForeColor="Red" Text=""></asp:Label>
    </td>
    <td colspan="1">
    </td>
    <td colspan="1" style="width: 671px">
       <a href="http://webportal.easternuni.edu.bd/Convocation/_EUCinstruction.aspx" style="font-weight: bold; color: blue; ">Instruction for filling up registration form for Registration</a>
</td>
</tr>

<tr>
<td style="width:13%">
    <asp:Label ID="Label4" runat="server" Text="Student ID"></asp:Label>
</td>
<td style="width:5%">
    <b>:</b></td>
<td colspan="4">


 

    <asp:TextBox ID="txtStudentID" runat="server" Width="100px"></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidatorPresentAddress" 
                runat="server" ForeColor="Red"
                ErrorMessage="Student ID is required"
                ControlToValidate="txtStudentID" Display="Dynamic" Text="*">
                </asp:RequiredFieldValidator>
</td>
    <td colspan="1">
    </td>
    <td colspan="1" style="width: 671px" rowspan="5">
    <b>Registration Fee</b><br />
        <asp:Label ID="lblInfo" runat="server" Text="Tk. 6,000 for those who have taken transcript and provisional certificate.<br/>Tk. 6,300 for those who have not taken transcript and provisional certificate"   ForeColor="#3333FF" >
                                               </asp:Label>
                                               </td>
</tr>

<tr>
<td style="width:13%">
    Faculty</td>
<td style="width:5%; font-weight: bold;">
    :</td>
<td colspan="4">
    <asp:DropDownList ID="cmb_Faculty" runat="server" Width="200px">
    </asp:DropDownList>
    </td>
    <td colspan="1">
    </td>
</tr>

<tr>
<td style="width:13%">
    <asp:Label ID="Label1" runat="server" Text="CGPA"></asp:Label>
    </td>
<td style="width:5%; font-weight: bold;">
    <b>:</b></td>
<td colspan="4">
    <asp:TextBox ID="txtCGPA" runat="server" Width="100px"></asp:TextBox>
       <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtPrePass" 
                runat="server" ForeColor="Red"
                ErrorMessage="CGPA is required"
                ControlToValidate="txtCGPA" Display="Dynamic" Text="*" >
                </asp:RequiredFieldValidator>
                
    </td>
    <td colspan="1">
    </td>
</tr>

<tr>
<td colspan="6"></td>
    <td colspan="1">
    </td>
    <td colspan="1">
    </td>
</tr>



    <tr>
        <td style="width:15%; height: 17px;">
            Last Registered Year</td>
        <td style="width:5%; height: 17px;">
            :&nbsp;</td>
        <td style="height: 17px">
             <asp:TextBox ID="txtGrYear" runat="server" Width="100px"></asp:TextBox>
       <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtNewPass" runat="server" 
           ControlToValidate="txtGrYear" Display="Dynamic" 
           ErrorMessage="Achived Degree Year is required" ForeColor="Red" Text="*">
       </asp:RequiredFieldValidator>
       
       </td>
        <td style="height: 17px; width: 63px;">
            Semester&nbsp;</td>
        <td style="height: 17px">
            :&nbsp;</td>
        <td style="height: 17px; width: 26px;">
            <asp:DropDownList ID="cmb_semester" runat="server"  Width="100px">
    <asp:ListItem Value="1">Spring</asp:ListItem>
    <asp:ListItem Value="2">Summer</asp:ListItem>
    <asp:ListItem Value="3">Fall</asp:ListItem>
</asp:DropDownList></td>
        <td style="width: 26px; height: 17px">
        </td>
    </tr>
    <tr>
        <td style="width: 25%; height: 17px">
        </td>
        <td style="width: 5%; height: 17px">
        </td>
        <td style="height: 17px">
        </td>
        <td style="width: 63px; height: 17px">
        </td>
        <td style="height: 17px">
        </td>
        <td style="width: 26px; height: 17px">
        </td>
        <td style="width: 26px; height: 17px">
        </td>
        <td style="width: 671px; height: 17px">
        </td>
    </tr>

    <tr>
        <td style="width:15%; ">
            &nbsp;</td>
        <td style="width:5%; ">
            &nbsp;</td>
        <td colspan="4">
            <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                Text="Submit" />
            <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                style="margin-left:50px" Text="Cancel" />
        </td>
        <td colspan="1">
        </td>
        <td colspan="1" style="width: 671px">
        </td>
    </tr>
    <tr>
        <td colspan="8">
        </td>
    </tr>
    <tr>
        <td colspan="8">
           For Technical Support please contact with 
<strong style="color: #0000ff">mamun@easternuni.edu.bd</strong> or <strong style="color: #0000ff">sadika@easternuni.edu.bd</strong>
</td>
    </tr>
    <tr>
        <td colspan="8">
        </td>
    </tr>

</table>

</asp:Panel>


  </div>

</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

