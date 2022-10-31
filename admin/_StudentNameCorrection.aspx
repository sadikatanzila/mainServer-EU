<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" 
CodeFile="_StudentNameCorrection.aspx.cs" Inherits="admin_StudentNameCorrection" Title="Student Name Spelling Correction" %>




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
                                               Student Name</td>
                                           <td style="width: 20px;font-weight: bold;">
                                               :</td>
                                           <td>
                                           
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                                   
                                                    <asp:TextBox ID="txtStdUpName" runat="server"  Height="20px" 
                                                     style="margin-left:5px" Width="308px"></asp:TextBox>
                                           
                                        </span></span>
                                           
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
                                            ForeColor="#0000CC" Text="Name Change Student List" Font-Bold="True"></asp:Label></td>
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
                                                <asp:BoundField DataField="SPROGRAM" HeaderText="Program" />
                                                <asp:BoundField DataField="CNG_NAME" HeaderText="Changed Name" />
                                                <asp:BoundField DataField="SNAME_PREV" HeaderText="Prev Name" />
                                                <asp:BoundField DataField="UPDATEDBY" HeaderText="Update by" Visible="false"/>
                                                <asp:BoundField DataField="UPDATETIME" HeaderText="Update Time" />
                                                
                                               
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

                            <asp:Panel ID="pnlClearance" runat="server" Visible="false">


                              


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

