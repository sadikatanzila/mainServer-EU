<%@ Page Title="" Language="C#" MasterPageFile="~/EUAnniversary/Anniversary.master" AutoEventWireup="true" CodeFile="_RegistrationAC.aspx.cs" Inherits="EUAnniversary_RegistrationAC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
<div style="text-align: center">
<br />
 <strong><span style="text-decoration: underline; font-size: large; color: #000099;">15th Year Celebration - Registration Form</span></strong>
 <br /><br />
  <asp:Label ID="lbl_imgMsg" runat="server" ForeColor="blue" Text=""></asp:Label><br />
<asp:Label ID="lbl_message" runat="server" ForeColor="Red" Text=""  Font-Bold="True"></asp:Label>
<br />
<asp:Label ID="lblSucess" runat="server" ForeColor="Blue" Text="" ></asp:Label>

<div style="width:100%; text-align:left">
 <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                ForeColor="Red" HeaderText="Page Errors" ShowMessageBox="True"
                ShowSummary="true" DisplayMode="List" />
                </div>
<asp:Panel ID="pnlDegreeRequired" runat="server">
   
<table style="width:100%;text-align: left; font-size: 14px;">
<tr>
<td colspan="3">
   
    
    </td>
</tr>

<tr>
<td colspan="3">
    <asp:Label ID="lblErrors" runat="server" ForeColor="Red" Text=""></asp:Label>
    <br />
    Please Insert the necessary Information:</td>
</tr>

<tr>
<td style="width:25%">
    <asp:Label ID="Label4" runat="server" Text="Student ID"></asp:Label>
</td>
<td style="width:5%">
    <b>:</b></td>
<td>


 

    <asp:TextBox ID="txtStudentID" runat="server" Width="250px"></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidatorPresentAddress" 
                runat="server" ForeColor="Red"
                ErrorMessage="Student ID is required"
                ControlToValidate="txtStudentID" Display="Dynamic" Text="*">
                </asp:RequiredFieldValidator>
</td>
</tr>

<tr>
<td style="width:25%">
    Date of Birth (DD/MM/YYYY)
  <br />  <asp:Label ID="lblBD" runat="server" Text="(as 9th December 2018 like 09/12/2018)" ForeColor="#666666"></asp:Label>
</td>
<td style="width:5%; ">
    <b>:</b></td>
<td>

  

 

    <asp:TextBox ID="txtDOB" runat="server" Width="250px" ></asp:TextBox>

       <asp:CalendarExtender ID="CalendarExtender_student_DOB" runat="server"
                                                         Format="dd/MM/yyyy" TargetControlID="txtDOB"></asp:CalendarExtender>

                                                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
     
</td>
</tr>
    <tr>
        <td style="width:25%">&nbsp;</td>
        <td style="width:5%; ">&nbsp;</td>
        <td>
             <asp:Button ID="btnCheck" runat="server" onclick="btnCheck_Click" 
                Text="Submit" />
                
                <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                Text="Cancel Registration"  style="margin-left:50px" />


        </td>
    </tr>
</table>
   
   
    <asp:Panel ID="pnlInfoFillup" runat="server" Visible="false">
       <table  style="width:100%;text-align: left;">
    <tr>
        <td colspan="6">
         <asp:Label ID="lblTrunID" runat="server" Text="" Visible="false" Font-Bold="true"></asp:Label>
        </td>
    </tr>

<tr>
<td style="width:25%">
    SID</td>
<td style="width:5%; font-weight: bold;">
    :</td>
<td colspan="4">
    <asp:Label ID="lblSID" runat="server" Text=""></asp:Label>
    </td>
</tr>

           <tr>
               <td style="width:25%">Name</td>
               <td style="width:5%; font-weight: bold;">:</td>
               <td>
                   <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
               </td>
               <td>Faculty</td>
               <td>:</td>
               <td>
                   <asp:Label ID="lblProgram" runat="server" Text=""></asp:Label>
               </td>
           </tr>

           <tr>
               <td style="width:25%">Admitted Year</td>
               <td style="width:5%; font-weight: bold;">:</td>
               <td>
                 
                   <asp:Label ID="lblYear" runat="server" Text=""></asp:Label>
               </td>
               <td>Semester</td>
               <td>:</td>
               <td>
                    <asp:DropDownList ID="ddlSem" runat="server"  Width="250px">
                       <asp:ListItem Value="1">Spring</asp:ListItem>
                        <asp:ListItem Value="2">Summer</asp:ListItem>
                        <asp:ListItem Value="3">Fall</asp:ListItem>

                    </asp:DropDownList>
                   <asp:Label ID="lblSem" runat="server" Text="" Visible="false"></asp:Label>
               </td>
           </tr>

           <tr>
               <td style="width:25%">&nbsp;</td>
               <td style="width:5%; font-weight: bold;">&nbsp;</td>
               <td colspan="4">&nbsp;</td>
           </tr>
           <tr>
               <td style="width:25%">&nbsp;</td>
               <td style="width:5%; font-weight: bold;">&nbsp;</td>
               <td colspan="4">&nbsp;</td>
           </tr>

    <tr>
        <td style="width:25%"><b>Personal Information</b>&nbsp;</td>
        <td style="width:5%; font-weight: bold;">&nbsp;</td>
        <td colspan="4">&nbsp;</td>
    </tr>



    <tr>
        <td style="width:25%; height: 17px;">
            Present Address</td>
        <td style="width:5%; height: 17px;">
            :</td>
        <td style="height: 17px">
            <asp:TextBox ID="txtAddress" runat="server" Height="50px" TextMode="MultiLine" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress" Display="Dynamic" ErrorMessage="Present Address is required" ForeColor="Red" Text="*">
                </asp:RequiredFieldValidator>
        </td>
        <td style="height: 17px">
            Mobile No</td>
        <td style="height: 17px">
            &nbsp;</td>
        <td style="height: 17px">
            <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorMobile" runat="server" ControlToValidate="txtMobile" Display="Dynamic" ErrorMessage="Mobile No is required" ForeColor="Red" Text="*" validationexpression="^[0-9]{11}$">
       
         
          <asp:RegularExpressionValidator runat="server" id="rexNumber" controltovalidate="txtMobile"
                                   Text="*" ForeColor="Red" validationexpression="^[0-9]{11}$" errormessage="Please enter your Mobile No!" />

                               </asp:RequiredFieldValidator></td></tr><tr><td style="width:25%; height: 17px;">Email Address</td><td style="width:5%; height: 17px;">:</td><td style="height: 17px"><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Email Address is required" ForeColor="Red" Text="*">
                </asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid Email Format" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td><td style="height: 17px">&nbsp;</td><td style="height: 17px">&nbsp;</td><td style="height: 17px">&nbsp;</td></tr><tr><td style="width:25%; height: 17px;">Employment Status</td><td style="width:5%; height: 17px;">:</td><td style="height: 17px">
                        <asp:DropDownList ID="ddlEmployment" runat="server"  Width="250px">
    <asp:ListItem Value="E">Employed</asp:ListItem><asp:ListItem Value="S">Self-employed</asp:ListItem><asp:ListItem Value="N">Not yet employed</asp:ListItem></asp:DropDownList></td><td style="height: 17px">&nbsp;</td><td style="height: 17px">&nbsp;</td><td style="height: 17px">&nbsp;</td></tr><tr><td style="width:25%; height: 17px;">&nbsp;</td><td style="width:5%; height: 17px;">&nbsp;</td><td style="height: 17px">&nbsp;</td><td style="height: 17px">&nbsp;</td><td style="height: 17px">&nbsp;</td><td style="height: 17px">&nbsp;</td></tr><tr>
        <td style="width:25%; height: 17px;">
            <b>Payment Status</b>&nbsp;&nbsp;</td><td style="width:5%; height: 17px;">
            &nbsp;</td><td style="height: 17px">
            <asp:Label ID="lblPaymentMsg" runat="server" Text=""></asp:Label></td><td style="height: 17px">
            &nbsp;</td><td style="height: 17px">
            &nbsp;</td><td style="height: 17px">
            &nbsp;</td></tr><tr><td style="width:25%; height: 17px;">Registration Fee</td><td style="width:5%; height: 17px;">:</td><td style="height: 17px"><asp:Label ID="lblRegFee" runat="server" Text=""></asp:Label></td><td style="height: 17px">&nbsp;</td><td style="height: 17px">&nbsp;</td><td style="height: 17px">&nbsp;</td></tr><tr><td colspan="6" style="height: 17px; text-align: right; color: #808080;">For the Running Student Registration Fee 500 BDT<br />and for Graduated Student Registration Fee 2000 BDT</td></tr><tr><td colspan="6" style="height: 17px;">&nbsp;</td></tr><tr>
        <td style="width:25%">
            &nbsp;</td><td style="width:5%">
            &nbsp;</td><td colspan="4">
           
        <asp:Button ID="btnSubmit" runat="server" Height="30px" onclick="btnSubmit_Click" Text="Submit" Width="107px" /></td>
    </tr>

</table>
</asp:Panel>

 </asp:Panel>
<div style="width:100%; text-align:left">





</div>


</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

