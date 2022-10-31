<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_ConvoStdInfoChk.aspx.cs" 
Inherits="admin_ConvoStdInfoChk" Title="Permitted Convocation Student List" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Convocation&gt; Permitted Convocation Student</span></td>
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
                        <td  class="k" height="1" width="95%">
                            </td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            </td>
                    </tr>
                    <tr>
                        <td  class="h" height="22" width="95%">
                            <p align="center">
                                <b><font  face="Arial" size="2">Permitted Convocation Student Info</font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="95%">
                            </td>
                    </tr>
                    <tr>
                        <td  class="k" style="height: 114px" width="1">
                            </td>
                        <td bgcolor="white" style="height: 114px" width="18">
                           </td>
                        <td bgcolor="#ffffff" style="vertical-align: top; text-align: left" width="95%">
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
                                               Convocation Year</td>
                                           <td style="width: 20px;font-weight: bold;">
                                               :</td>
                                           <td>
                                           
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                           
                                       
                                                    
                                           
                                               2019</span></span></td>
                                           </tr>
                                           
                                           <tr>
                                           <td style="width: 132px; font-weight: bold; font-size: medium;">
                                              Batch
                                           </td>
                                           <td style="width: 20px;font-weight: bold;">
                                               :
                                           </td>
                                           <td>
                                           
                                               <asp:TextBox ID="txtBatch" runat="server"  Height="20px" ></asp:TextBox>
                                                   
                                                    
                                           
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                               <asp:Button ID="btnSubmit" runat="server" Height="22px" Text="Show" 
                                                   Width="83px" Font-Bold="True" onclick="btnSubmit_Click" />
                                        </span></span>
                                                   
                                                    
                                           
                                           </td>
                                           </tr>
                                           
                                           <tr>
                                           <td style="font-weight: bold; text-align: center;" colspan="3">
                                               &nbsp;</td>
                                           </tr>
                                           
                                           <tr>
                                    <td class="header" colspan="3" 
                                                   style="vertical-align: top; background-color: #ffffff; text-align: center">
                                        <hr />
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="Medium" 
                                            ForeColor="#0000CC" Text="Permitted Convocation Student List" Font-Bold="True"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="header" colspan="3" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <asp:GridView ID="GridView_student" runat="server" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="4" Width="100%"  OnRowDataBound="GridView_student_RowDataBound">
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
                                                <asp:BoundField DataField="SNAME" HeaderText="Name" />
                                                <asp:BoundField DataField="COLLEGENAME" HeaderText="Department" />
                                                <asp:BoundField DataField="phone" HeaderText="Contact" />
                                                <asp:BoundField DataField="LRY" HeaderText="Last Reg Year" />
                                                <asp:BoundField DataField="LSEMN" HeaderText="Last Reg Sem" />
                                                <asp:BoundField DataField="CGPA" HeaderText="CGPA" />
                                                 <asp:BoundField DataField="TCGPA" HeaderText="TCGPA" />
                                                   <asp:BoundField DataField="DUE" HeaderText="DUE" />
                                                 <asp:BoundField DataField="LOAN_AMOUNT" HeaderText="LOAN" />
                                               
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
                        <td bgcolor="white" height="1" width="95%">
                           </td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            </td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="95%">
                           </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

