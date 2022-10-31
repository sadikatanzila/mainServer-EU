<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_ConStdClearance.aspx.cs" Inherits="admin_ConStdClearance" 
Title="Convocation Student Clearance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Clearence&gt; Convocation Student</span></td>
        </tr>
    </table>
    
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
    <table border="0">
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
                                <b><font  face="Arial" size="2">Convocation Student Clearance</font></b></p>
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
                                           <td style="width: 132px; font-weight: bold; font-size: medium;">
                                               Convocation Year</td>
                                           <td style="width: 20px;font-weight: bold;">
                                               :</td>
                                           <td>
                                           
                                               <asp:Label ID="lblYear" runat="server" Text="2019" Font-Size="Large" ></asp:Label>
                                           
                                           </td>
                                           </tr>
                                           
                                           <tr>
                                           <td style="font-weight: bold;" colspan="3">
                                               <asp:CheckBox ID="chkPaper" runat="server" Text="Paper Missing"   ForeColor="#0000CC" />
                                                <asp:CheckBox ID="chkVarification" runat="server" Text="Paper Varification"  ForeColor="#0000CC" style="margin-left:35px" />
                                                 <asp:CheckBox ID="chkLibrary" runat="server" Text="Library"  ForeColor="#0000CC" style="margin-left:35px" />
                                           </td>
                                           </tr>
                                           
                                           <tr>
                                           <td style="width: 132px; font-weight: bold;">
                                               &nbsp;</td>
                                           <td style="width: 20px;font-weight: bold;">
                                               &nbsp;</td>
                                           <td>
                                           
                                               &nbsp;</td>
                                           </tr>
                                           
                                           <tr>
                                           <td style="font-weight: bold; text-align: center;" colspan="3">
                                               <asp:Button ID="btnSubmit" runat="server" Height="33px" Text="Clear" 
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
                                            ForeColor="#0000CC" Text="Convocation Block Student List" Font-Bold="True"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="header" colspan="3" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <asp:GridView ID="GridView_student" runat="server" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="4" Width="100%" OnRowDataBound="GridView_student_RowDataBound">
                                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblSerial" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" />
        </asp:TemplateField>  
                                                <asp:BoundField DataField="sid" HeaderText="ID" >
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="sname" HeaderText="Name" />
                                                <asp:BoundField DataField="PAPERMISSING" HeaderText="Papermissing" />
                                                <asp:BoundField DataField="VARIFICATION" HeaderText="Varification" />
                                                <asp:BoundField DataField="LIBRARY" HeaderText="Library" />
                                               
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

