<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" 
CodeFile="_AdmitCardClearance.aspx.cs" Inherits="admin_AdmitCardClearance" Title="Admit Card Clearance" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
   
    
      <script language="javascript" type="text/javascript">
  function chech_valid()
  {  
      if (document.getElementById("ctl00$ContentPlaceHolder_definition$txt_year").value.toString()=="" )
      {
            alert("Please enter the year");
            return false;
      }
      else
            return true;
  }  
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0" width="100%">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="99%">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                            </td>
                        <td  class="k" height="1" width="100%">
                            </td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            </td>
                    </tr>
                    <tr>
                        <td  class="h" height="22" width="100%">
                            <p align="center">
                                <b><font  face="Arial" size="2">Admit Card Clearance</font></b></p> <hr style="color: #333333" />
                        </td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="100%">
                            </td>
                    </tr>
                    <tr>
                        <td  class="k" style="height: 114px" width="1">
                            </td>
                        <td bgcolor="white" style="height: 114px" width="18">
                           </td>
                        <td bgcolor="#ffffff" style="vertical-align: top; text-align: left" width="100%">
                          
                            <table  border="0" style="width: 100%">
                                    <tr>
                                           <td style="width: 132px; font-weight: bold;">
                                               Year&nbsp;</td>
                                           <td style="width: 20px;font-weight: bold;">
                                               :</td>
                                           <td>
                                           
                                               <asp:TextBox ID="txtYear" runat="server"  Width="200px" Text="2018"></asp:TextBox></td>
                                           </tr>
                                               <tr>
                                                   <td style="font-weight: bold; width: 132px">
                                                       Semester</td>
                                                   <td style="font-weight: bold; width: 20px">
                                                       :</td>
                                                   <td>
                                                      
                                                    <asp:DropDownList ID="cmb_semester" runat="server">
                                                        <asp:ListItem Value="1">Spring</asp:ListItem>
                                                        <asp:ListItem Value="2">Summer</asp:ListItem>
                                                        <asp:ListItem Value="3">Fall</asp:ListItem>
                                                    </asp:DropDownList>
                                                       </td>
                                               </tr>

                                               <tr>
                                                   <td style="font-weight: bold; width: 132px">
                                                       &nbsp;</td>
                                                   <td style="font-weight: bold; width: 20px">
                                                       &nbsp;</td>
                                                   <td>
                                                      
                                        
                                               <asp:Button ID="btnCheck" runat="server" Height="33px" Text="Check" 
                                                   Width="136px" Font-Bold="True" Font-Size="Large" 
                                                   onclick="btnCheck_Click" />
                                       
                                                   </td>
                                               </tr>

                            </table>

                            <asp:Panel ID="pnlClearance" runat="server" Visible="false">


                              <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                            <br />
                                           <table style="width:100%">
                                           <tr>
                                           <td  colspan="3">
                                               <asp:Label ID="lblmsg" runat="server" ForeColor="#0000CC"></asp:Label>
                                           </td>
                                           </tr>
                                           
                                           <tr>
                                           <td style="width: 132px; font-weight: bold; font-size: medium;">
                                               Student ID
                                           </td>
                                           <td style="width: 20px;font-weight: bold;">
                                               :
                                           </td>
                                           <td>
                                           
                                               <asp:TextBox ID="txtSid" runat="server"  Height="20px" AutoPostBack="true"
                                                   ontextchanged="txtSid_TextChanged"></asp:TextBox>
                                                   
                                                    <asp:TextBox ID="txtStdName" runat="server"  Height="20px" 
                                                     ReadOnly="true" style="margin-left:5px" Width="150px"></asp:TextBox>
                                           
                                           </td>
                                           </tr>
                                           
                                       
                                               <tr>
                                                   <td style="font-weight: bold; width: 132px">
                                                       Exam Type</td>
                                                   <td style="font-weight: bold; width: 20px">
                                                       :</td>
                                                   <td>
                                                      <asp:DropDownList ID="cmb_exam" runat="server">
                                                        <asp:ListItem Value="M">Mid</asp:ListItem>
                                                        <asp:ListItem Value="F">Final</asp:ListItem>
                                                    </asp:DropDownList>
                                                    </td>
                                               </tr>
                                           
                                           <tr>
                                           <td style="font-weight: bold; text-align: center;" colspan="3">
                                               <asp:Button ID="btnSubmit" runat="server" Height="33px" Text="Submit" 
                                                   Width="136px" Font-Bold="True" Font-Size="Large" 
                                                   onclick="btnSubmit_Click" />
                                               </td>
                                           </tr>
                                           
                                           <tr>
                                           <td style="font-weight: bold;" colspan="3">
                                               &nbsp;</td>
                                           </tr>
                                           
                                           <tr>
                                    <td class="header" colspan="3" 
                                                   style="vertical-align: top; background-color: #ffffff; text-align: center">
                                        <hr />
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="Medium" 
                                            ForeColor="#0000CC" Text="Admit Card Due Clearance Student List" Font-Bold="True"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="header" colspan="3" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <asp:GridView ID="GridView_student" runat="server" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="4" Width="100%">
                                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                            <Columns>
                                               
                                                <asp:BoundField DataField="sid" HeaderText="ID" >
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="sname" HeaderText="Name" />
                                                <asp:BoundField DataField="SPROGRAM" HeaderText="Program" />
                                                <asp:BoundField DataField="SemisterName" HeaderText="Semester" />
                                                <asp:BoundField DataField="YEAR" HeaderText="Year" />
                                                <asp:BoundField DataField="EXAMTYPE_Name" HeaderText="Exam Type" />
                                               
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="#330099" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#666666" Font-Bold="True" Font-Size="Small" ForeColor="#FFFFCC" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                           
                                           </table>
                                        </span></span>
                                    </td>
                                </tr>
                               
                                </table>


</asp:Panel>

                              
                        </td>
                        <td bgcolor="white" style="height: 114px" width="14">
                            &nbsp;
                            <p>
                              </p>
                        </td>
                        <td  class="k" style="height: 114px" width="1">
                            </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                           </td>
                        <td bgcolor="white" height="1" width="100%">
                           </td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            </td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="100%">
                           </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

