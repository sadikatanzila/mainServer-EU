<%@ Page Language="C#" MasterPageFile="~/Convocation/ConvoMaster.master" AutoEventWireup="true" CodeFile="_EUConForm.aspx.cs" Inherits="Convocation_EUConForm" Title="Convocation Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
<script language="javascript" type="text/javascript">
function toggle_panel(chkbox) {

 var panel=document.getElementById('<%=Panel3.ClientID %>');
    if (chkbox.checked == true) {
        var txtRegFee = document.getElementById('ContentPlaceHolder_definition_txtRegFee').value;
       // var txtRegFee = ContentPlaceHolder_definition_txtRegFee.value;
        panel.style.display = 'inline';
        var result = parseInt(1000) + parseInt(txtRegFee)
        ContentPlaceHolder_definition_txtTotalFee.value = result;
        
        
    }
    else {
        ContentPlaceHolder_definition_txtGstName1.value = null;
        ContentPlaceHolder_definition_txtGstRl.value = null;
        ContentPlaceHolder_definition_txtGstPhn.value = null;
        ContentPlaceHolder_definition_txtGstAdd.value = null;

        var txtRegFee = document.getElementById('ContentPlaceHolder_definition_txtRegFee').value;
        panel.style.display = 'none';
        ContentPlaceHolder_definition_txtTotalFee.value = txtRegFee;
       
    }
}  
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
<div style="text-align: left; float:left; width:95%; margin-left:15px; ">
                                <br />
                                <table border="0" cellpadding="0" cellspacing="0" width="90%" style="text-align: left; margin-left:50px">
                                    <tr>
                                        <td colspan="7" style="text-align: center; height: 20px;">
                                        <strong><span style="text-decoration: underline; font-size: large;">6th Convocation Registration Form</span></strong></td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            <asp:Label ID="lbl_message" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            <br />
                                           <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                ForeColor="Red" HeaderText="Page Errors" ShowMessageBox="True"
                ShowSummary="true" DisplayMode="List" />
                                           </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" style="height: 20px">
                                            <asp:Label ID="lbldue" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:Label ID="Label1" runat="server" Text="Personal Information:" 
                                                Font-Bold="True" Font-Size="14px"></asp:Label>
                                            <asp:Label ID="lblTrunID" runat="server" Visible="false"></asp:Label>

                                              <asp:Label ID="lblGradType" runat="server" Visible="false"></asp:Label>
                                        </td>
     <td colspan="2" rowspan="7" style="width:350px">
          <table style="width:350px">
                                        <tr>
                                        <td style="width:50%; border-color:Black; border-bottom-width:thin">
                                        Please Upload an Image for Convocation Registration
                                        <br />
                                        
                                        <asp:Image ID="img_myProfile" runat="server" Height="200px" Width="150px" Visible="false" />  
                                        <br />             
                  <input type="file" id="File1" name="File1" runat="server" />
                  
                  <input type="submit" id="Submit1" value="Upload" runat="server" name="Submit1"/>

                 <asp:TextBox ID="txtPictureLocation" runat="server" Width="180px" Visible="false"></asp:TextBox>
                     
                
                <asp:Label ID="lblmsg" runat="server" ForeColor="Blue" ></asp:Label>
                                        </td>
                                        <td style="width:50%;">
                                            <strong>
                                       
    Photo: </strong>
    <br />
    Width: 1.5 inch, 
                                            <br />
                                            Height: 2.0 inch<br />
                                            Formal PP size 
                                            <br />
                                            Photograph Resolution: 300 DPI
                                        </td>
                                        </tr>
                                        </table>
                                           
                                             
                
                                            </td>
                                            
                                           
                                                   
                                    </tr>
                                    
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; width: 20%">
                                        </td>
                                        <td style="font-weight: bold; width: 2%">
                                        </td>
                                        <td style="width: 28%">
                                        </td>
                                        <td style="width: 2%">
                                        </td>
                                        <td style="font-weight: bold; width: 20%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:20%; font-weight: bold;">ID</td>
                                        <td style="width:2%;font-weight: bold;">:&nbsp;</td>
                                        <td style="width:28%"><asp:Label ID="lbl_id" runat="server" Text=""></asp:Label></td>
                                        <td style="width:2%">&nbsp;</td>
                                        <td style="width:20%; font-weight: bold;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:20%; font-weight: bold;">Name</td>
                                        <td style="font-weight: bold;">:</td>
                                        <td colspan="3"><asp:Label ID="lbl_name" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width:20%; font-weight: bold;">Email</td>
                                        <td style="font-weight: bold;">:</td>
                                        <td>
                                        <asp:TextBox ID="txtMail" runat="server" Width="200px"></asp:TextBox>
                                        
<asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" 
                runat="server" ForeColor="Red"
                ErrorMessage="Email Address is required"
                ControlToValidate="txtMail" Display="Dynamic" Text="*">
                </asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" 
        ControlToValidate="txtMail" ErrorMessage="Invalid Email Format" 
        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                        </td>
                                        <td>&nbsp;</td>
                                        <td style="font-weight: bold;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:20%; font-weight: bold; height: 21px;">Contact</td>
                                        <td style="font-weight: bold; height: 21px;">:</td>
                                        <td style="height: 21px">
                                            <asp:TextBox ID="txtContact" runat="server" Width="200px"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidatorMobile" 
       ControlToValidate="txtContact"   runat="server" Text="*" ForeColor="Red" 
                 ErrorMessage="Mobile No is required in 11 digit (01xxxxxxxxx)" Display="Dynamic" 
        validationexpression="^[0-9]{11}$">
        
          <asp:RegularExpressionValidator runat="server" id="rexNumber" controltovalidate="txtContact"
                                   Text="*" ForeColor="Red" validationexpression="^[0-9]{11}$" errormessage="Please enter your Mobile No as 11 digit(01xxxxxxxxx)!" />

                               </asp:RequiredFieldValidator></td><td style="height: 21px">&nbsp;</td><td style="font-weight: bold; height: 21px;">&nbsp;</td></tr><tr>
                                        <td style="width:20%; font-weight: bold; vertical-align: top; height: 74px;">Mailing Address</td><td style="font-weight: bold;vertical-align: top; height: 74px;">:</td><td colspan="3" style="height: 74px; vertical-align: top;">
                                            <asp:TextBox ID="txtAddress" runat="server" Width="500px" 
                                            TextMode="MultiLine" Height="50px"></asp:TextBox></td><td style="height: 74px; color: #3333FF;" colspan="2">
                                         N.B.: Please note that the photo you upload will be used in the Convocation Souvenir. <br />So, please upload the photo accordingly. Photo taken in formal attire is preferable. </td></tr><tr>
                                        <td style="width:20%; font-weight: bold;">&nbsp;</td><td style="font-weight: bold;">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td style="font-weight: bold;">&nbsp;</td><td style="font-weight: bold; width: 21px;">&nbsp;</td><td style="width: 249px">&nbsp;</td></tr><tr>
                                        <td style="font-weight: bold;" colspan="7">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="14px" 
                                                Text="Academic Details:"></asp:Label></td></tr><tr>
                                        <td style="font-weight: bold; height: 5px;" colspan="7">
                                            &nbsp;</td></tr><tr>
                                        <td style="width:20%; font-weight: bold;">Degree</td><td style="font-weight: bold;">:</td><td colspan="5"><asp:Label ID="lbl_DegNa" runat="server" Text=""></asp:Label></td></tr><tr>
                                        <td  colspan="3">
                                        <asp:Label  ID="Label4" runat="server" Text="Credit Waived/ Transferred :" 
                                                Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp; <asp:Label ID="lblWcrt" runat="server" Text=""></asp:Label></td><td>&nbsp;</td><td style="font-weight: bold;">Credit at EU</td><td style="font-weight: bold; width: 21px;">:</td><td style="width: 249px"><asp:Label ID="lblEcrdt" runat="server" Text=""></asp:Label></td></tr><tr>
                                        <td style="width:20%; font-weight: bold;">Total Credit</td><td style="font-weight: bold;">:</td><td><asp:Label ID="lblTcrdt" runat="server" Text=""></asp:Label></td><td>&nbsp;</td><td style="font-weight: bold;">CGPA</td><td style="font-weight: bold; width: 21px;">:</td><td style="width: 249px"><asp:Label ID="lblCGPA" runat="server" Text=""></asp:Label></td></tr><tr>
                                        <td  colspan="7" style="height: 20px">
                                        <asp:Label  ID="Label3" runat="server" Text=" Achieved Degree Semester & Year :" 
                                                Font-Bold="True"></asp:Label><asp:Label  ID="lblDegSem" runat="server" Text="" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Label  ID="lbl_DegSem" runat="server" Text=""></asp:Label>&nbsp;&nbsp;<asp:Label  ID="lbl_DegYear" runat="server" Text=""></asp:Label></td></tr><tr>
                                        <td style="width:20%; font-weight: bold;">&nbsp;</td><td style="font-weight: bold;">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td style="font-weight: bold;">&nbsp;</td><td style="font-weight: bold; width: 21px;">&nbsp;</td><td style="width: 249px">&nbsp;</td></tr><tr>
                                        <td style="width:20%; font-weight: bold;">Employment Staus</td><td style="font-weight: bold;">:</td><td> 
<asp:DropDownList ID="ddlEmployment" runat="server"  Width="250px">
    <asp:ListItem Value="E">Employed</asp:ListItem><asp:ListItem Value="S">Self-employed</asp:ListItem><asp:ListItem Value="N">Not yet employed</asp:ListItem></asp:DropDownList></td><td>&nbsp;</td><td style="font-weight: bold;">&nbsp;</td><td style="font-weight: bold; width: 21px;">&nbsp;</td><td style="width: 249px">&nbsp;</td></tr><tr>
                                        <td style="font-weight: bold;" colspan="3">If Employed/ Self Employed</td><td>&nbsp;</td><td style="font-weight: bold;">&nbsp;</td><td style="font-weight: bold; width: 21px;">&nbsp;</td><td style="width: 249px">&nbsp;</td></tr><tr>
                                        <td style="width:20%; font-weight: bold;vertical-align: top;">Current Designation</td><td style="font-weight: bold;vertical-align: top;">:</td><td>
                                        <asp:TextBox ID="txtdesg" runat="server" Width="200px"
                                        TextMode="MultiLine" Height="30px"></asp:TextBox></td><td>&nbsp;</td><td style="font-weight: bold;vertical-align: top;">Organization Name</td><td style="font-weight: bold;vertical-align: top; width: 21px;">:</td><td style="width: 249px">
                                        <asp:TextBox ID="txtOrgN" runat="server" Width="200px"
                                        TextMode="MultiLine" Height="30px"></asp:TextBox></td></tr><tr>
                                        <td style="width:20%; font-weight: bold;">Address</td><td style="font-weight: bold;">:</td><td>
                                        <asp:TextBox ID="txtOrgAdd" runat="server" Width="200px"></asp:TextBox></td><td>&nbsp;</td><td style="font-weight: bold;">Phone/ Contact</td><td style="font-weight: bold; width: 21px;">:</td><td style="width: 249px">
                                        <asp:TextBox ID="txtOrgphn" runat="server" Width="200px"></asp:TextBox></td></tr><tr>
                                        <td style="width:20%; font-weight: bold;">&nbsp;</td><td style="font-weight: bold;">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td style="font-weight: bold;">&nbsp;</td><td style="font-weight: bold; width: 21px;">&nbsp;</td><td style="width: 249px">&nbsp;</td></tr><tr>
                                        <td style="font-weight: bold; color: #FF0000;" colspan="7">Pickup Point:</td></tr><tr>
                                        <td style="font-weight: bold; " colspan="7">
                                          

<asp:RadioButtonList runat="server" ID="Radioyesno" RepeatDirection="Vertical" Width="90%" Style="margin-left:50px">
            <asp:ListItem Text="Dhanmondi Campus" Value="1" Selected="True"></asp:ListItem><asp:ListItem Text="Mirpur 1" Value="2"></asp:ListItem><asp:ListItem Text="Abdullahpur" Value="3"></asp:ListItem><asp:ListItem Text="Birulia Bridge" Value="4"></asp:ListItem><asp:ListItem Text="Self Transport" Value="5"></asp:ListItem></asp:RadioButtonList><asp:Label ID="Label6" runat="server" Text="Please Select at least one Pick up Point  , Time will be declare later"></asp:Label></td></tr><tr>
                                        <td style="font-weight: bold; color: #FF0000;" colspan="7">&nbsp;</td></tr><tr>
                                        <td style="font-weight: bold; color: #FF0000;" colspan="7">Payment Amount :</td></tr><tr>
                                        <td style="font-weight: bold;" colspan="7">
                                         <asp:Panel ID="pnlPayRpt" runat="server" Visible="false" >
                                        
                                        Registration Fee :&nbsp;&nbsp;<asp:Label ID="lblRegfee" runat="server" Text="Label"></asp:Label><asp:Label ID="lblGstfee" runat="server" Text="Label" Visible="false"></asp:Label><br />Guest Number :&nbsp;&nbsp;<asp:Label ID="lblGstNum" runat="server" Text="Label"></asp:Label><br />Total Fee :&nbsp;&nbsp; <asp:Label ID="lblTotFee" runat="server" Text=""></asp:Label></asp:Panel></td></tr><tr>
                                        <td style="font-weight: bold;" colspan="7">
                                         
                                         
                                          <asp:Panel ID="pnlPayment" runat="server" Visible="true">
                                        
                                         
                                          <table style="width:100%">
                                            <tr>
                                        <td style="width:20%; font-weight: bold;">Registration Fee</td><td style="font-weight: bold;">:</td><td>
                                            <asp:TextBox ID="txtRegFee" runat="server" Width="200px" ReadOnly="true"></asp:TextBox></td><td style="vertical-align: top; width: 3px;">
                                        
                                             </td>
                                    </tr>
                                            <tr>
                                        <td style="font-weight: bold;" colspan="3">
                                            <asp:Label ID="Label5" runat="server" Text="Are you going to bring a Guest? "></asp:Label><asp:CheckBox ID="chkboxPanel" runat="server" onclick="toggle_panel(this);" Text="yes" />



                                           <br />
                                           
<asp:Panel ID="Panel3" runat="server" Style="display: none"><!--Style="display: none"-->
     <table style="width:100%">
         <tr>
                                         <td style="font-weight: bold;" colspan="3">
                                             
                                             <asp:Label ID="lblguestFee" runat="server" Text=" BDT 1000 for guest (only one guest will be allowed)"   ForeColor="#3333FF" >
                                               </asp:Label></td><td style="font-weight: bold; width: 3px;" colspan="1" rowspan="4"></td>
                                    </tr>
                                            <tr><td style="width:20%; font-weight: bold;">Guest Name</td><td style="font-weight: bold;">:</td><td><asp:TextBox ID="txtGstName1" runat="server" Width="200px"></asp:TextBox></td></tr><tr>
                                        <td style="width:20%; font-weight: bold;">Relationship</td><td style="font-weight: bold;">:</td><td>
                                            <asp:TextBox ID="txtGstRl" runat="server" Width="200px"></asp:TextBox></td></tr><tr>
                                        <td style="width:20%; font-weight: bold;">Guest Contact No</td><td style="font-weight: bold;">:</td><td>
                                            <asp:TextBox ID="txtGstPhn" runat="server" Width="200px"></asp:TextBox></td></tr><tr>
                                        <td style="width:20%; font-weight: bold;">Address</td><td style="font-weight: bold;">:</td><td>
                                            <asp:TextBox ID="txtGstAdd" runat="server" Width="200px"></asp:TextBox></td></tr></table></asp:Panel></td><td style="vertical-align: top; width: 3px;">
                                        
                                             &nbsp;</td></tr><tr>
                                        <td style="width:20%; font-weight: bold; vertical-align: top;">Total Fee</td><td style="font-weight: bold;vertical-align: top;">:</td><td style="font-weight: bold;vertical-align: top;">
                                      <asp:TextBox ID="txtTotalFee" runat="server" ReadOnly="true" Width="200px" 
                                                 Height="18px"></asp:TextBox><br /><asp:Label ID="lblInfo" runat="server" Text="Tk. 6,000 for those who have taken transcript and provisional certificate.<br/>Tk. 6,300 for those who have not taken transcript and provisional certificate"   ForeColor="#3333FF" >
                                               </asp:Label></td><td colspan="1" style="width: 3px">

                                         </td>
                                    </tr>
                                            </table>
                                            
  
                                         
                                          </asp:Panel>
                                          
                                          
                                          
                                        
 
                                        
                                            </td>
                                    </tr>
                                    
                                     <tr>
                                        <td style="width:20%; font-weight: bold;">&nbsp;</td><td style="font-weight: bold;">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td style="font-weight: bold;">&nbsp;</td><td style="font-weight: bold; width: 21px;">&nbsp;</td><td style="width: 249px">&nbsp;</td></tr><tr >
                                        <td style="font-weight: bold; text-align: center;" colspan="7">
                                            <asp:Button ID="btnSubmit" runat="server" Height="30px" Text="Submit" 
                                                Width="107px" onclick="btnSubmit_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold;" colspan="7">	
For Technical Support please contact with <strong style="color: #0000ff">mamun@easternuni.edu.bd</strong> or <strong style="color: #0000ff">sadika@easternuni.edu.bd</strong> </td></tr></table></div></asp:Content><asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

