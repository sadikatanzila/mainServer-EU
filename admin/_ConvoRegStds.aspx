<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" 
CodeFile="_ConvoRegStds.aspx.cs" Inherits="admin_ConvoRegStds" Title="Registered Convocation Student List" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Convocation&gt; Registerd Convocation Student</span></td>
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
    <div  style="width:90%">
    <table border="0" style="width:100%">
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
                                <b><font  face="Arial" size="2">Registered Convocation Student Info</font></b></p>
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
                                                   <td>
                                                       Convocation Year&nbsp;&nbsp; :
                                                       <asp:TextBox ID="txtYear" runat="server">2019</asp:TextBox>
                                                       
                                                       </td>
                                               </tr>
                                               <tr>
                                                   <td>
                                                       Department&nbsp; : &nbsp; &nbsp;<asp:DropDownList ID="cmb_Faculty" runat="server" Width="150px">
                                                     </asp:DropDownList>
                                                     
                                                       <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                                       <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" style="margin-left:20px" />
                                                       
                                                       <asp:Label ID="lblTotal" runat="server" Text="" Style="color: #a6a7a8;margin-left:100px"  Font-Bold="true" Visible="false"></asp:Label>
                                                       
                                                       </td>
                                               </tr>
                                           <tr>
                                           <td>
                                               <asp:Label ID="lblmsg" runat="server" ForeColor="#0000CC"></asp:Label>
                                           </td>
                                          
                                           </tr>
                                           
                                           <tr>
                                    <td class="header" 
                                                   style="vertical-align: top; background-color: #ffffff; text-align: center">
                                        <hr />
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="Medium" 
                                            ForeColor="#0000CC" Text="Registered Convocation Student List" Font-Bold="True"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="header" 
                                        style="vertical-align: top; background-color: #ffffff; text-align: left">
            <asp:GridView ID="GridView_student" runat="server" AutoGenerateColumns="False"
                BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                CellPadding="4" Width="100%"  OnRowDataBound="GridView_student_RowDataBound">
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <Columns>
                                            
            <asp:TemplateField HeaderText="Sl" ItemStyle-Width="1%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSerial" runat="server" ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" />
        </asp:TemplateField>  
                    
                          <asp:TemplateField HeaderText="Student" ItemStyle-Width="7%"> 
                            <ItemTemplate>  
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("SD") %>'></asp:Label>
                             </ItemTemplate>
                            </asp:TemplateField>
                           <asp:BoundField DataField="SNAME" HeaderText="Name" ItemStyle-Width="10%" /> 
                      <asp:TemplateField HeaderText="Graduation Date" ItemStyle-Width="15%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblGRADUATIONDATE" runat="server" Text='<%# Eval("GRADUATIONDATE", "{0:dd-MMM-yyyy}") %>'></asp:Label>                                        
                                </ItemTemplate>  
                            </asp:TemplateField>   
                                 
                            <asp:BoundField DataField="PHONE" HeaderText="Mobile" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="NAME" HeaderText="Dept" ItemStyle-Width="10%"/>
                                 <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" ItemStyle-Width="10%"/>                    
                            <asp:BoundField DataField="Duel_status" HeaderText="Duel Status" ItemStyle-Width="10%"/>     
                            <asp:BoundField DataField="STATUS" HeaderText="Status" ItemStyle-Width="10%"/>
                     <asp:BoundField DataField="Duel_Clearnce" HeaderText="Dual Clearance" ItemStyle-Width="10%"/>
                                    <asp:BoundField DataField="GUEST_INFO" HeaderText="Guest" ItemStyle-Width="10%"/>                  
                            <asp:TemplateField HeaderText="Photo" ItemStyle-Width="15%"> 
                            <ItemTemplate>  
                            <asp:Image ID="Image2" runat="server" ImageUrl='<%# Bind("PICTURE") %>' Width="75px" Height="100px"  />
                            </ItemTemplate>
                            </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="#330099" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#666666" Font-Bold="True" Font-Size="Small" ForeColor="#FFFFCC" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                           
                                           </table>

                                            <!--
                                                     <asp:TemplateField HeaderText="Last Enrolled" ItemStyle-Width="42%"> 
                                <ItemTemplate>
                                    Reg. :<asp:Label ID="lblRegsem" runat="server" Text='<%# Bind("LastRegYS") %>'></asp:Label>
                                    <br />
                                   Grd. : <asp:Label ID="lblGrdSem" runat="server" Text='<%# Bind("GRYS") %>'></asp:Label>                                            
                                </ItemTemplate>
                            </asp:TemplateField>
                                           
                                                
                                                <asp:BoundField DataField="CGPA" HeaderText="CGPA" ItemStyle-Width="5%" />

                                                -->
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
    
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

