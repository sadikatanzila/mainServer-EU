<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_add_student_advisor.aspx.cs" Inherits="admin_add_student_advisor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
  
  <script language="javascript" type="text/javascript">
  
  function chech_valid()
  {
  
  if (document.getElementById("ctl00_ContentPlaceHolder_tracker_txt_batch").value.toString()=="" )
  {
        alert("Please enter the batch");
        return false;
  }
//  else if(NaN(document.getElementById("ctl00_ContentPlaceHolder_tracker_txt_batch").value.toString()))
//  {
//        alert("Enter a valid batch");
//        return false;
//  }
  else
        return true;
  }  
  
  </script> 
  
  
  
  
  
  
    <table style="width: 95%">
        <tr>
            <td class="header" style="color: #ffa500; height: 20px; background-color: #eef5fa;
                text-align: left">
                Advisor&gt;Student List</td>
        </tr>
        <tr>
            <td class="header" style="height: 44px; background-color: #ffffff; text-align: left; font-weight: bold;">
                <table style="width: auto">
                    <tr>
                        <td style="width: auto; text-align: left">
                            Advisor Faculty
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAdvisorFaculty" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlAdvisorFaculty_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: auto; text-align: left">
                            Select advisor
                        </td>
                        <td>
                            <asp:DropDownList ID="cmb_advisor" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            Department &nbsp;&nbsp; &nbsp;<asp:DropDownList ID="cmb_department" runat="server">
                            </asp:DropDownList>
                                Batch
                            <asp:TextBox ID="txt_batch" runat="server" MaxLength="4" Width="40px"></asp:TextBox></td>
                    </tr>
                    
                    <tr>
                        <td>
                            Tranferred/ Regular:
                            
               <asp:DropDownList ID="ddlTranfered" runat="server" Width="200px">
                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                <asp:ListItem Text="Tranferred" Value="1"></asp:ListItem>
                <asp:ListItem Text="Regular" Value="2"></asp:ListItem>
            </asp:DropDownList>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btn_show" runat="server" OnClick="btn_submit_Click" Text="Show" />
                            &nbsp;&nbsp;<asp:Button ID="btn_set_advisor" runat="server" Text="Set Advisor" Width="115px" OnClick="btn_set_advisor_Click" /></td>
                    </tr>
                </table>
                                        <hr />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="100%">
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
                                <b><font  face="Arial" size="2"> Student List</font></b></p>
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
                        <td bgcolor="#ffffff" style="vertical-align: top" width="100%">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <br />
                                        <asp:GridView ID="GridView_studentList" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" GridLines="None" Visible="False">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                                <asp:TemplateField> 
													<HeaderTemplate>
                                                        <asp:CheckBox ID="chkAllOrNone" runat="server" AutoPostBack="True" 
                                                            oncheckedchanged="chkAllOrNone_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="Student Id">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hp_link_sid" runat="server" NavigateUrl='<%# Eval("sid", "~/staffs/advisor/_studentDetails.aspx?ids={0}") %>'
                                                            Text='<%# Eval("sid") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="sname" HeaderText="Name">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="170px" />
                                                </asp:BoundField>
                                                
												<asp:BoundField HeaderText="Comp. Credit" DataField="credit">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <%--<asp:BoundField HeaderText="CGPA" DataField="CGPA">
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                                                        
                                                 <asp:BoundField HeaderText="Transferred Subject" DataField="transferred">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>--%>
                                            </Columns>
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Smaller" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            &nbsp;</td>
                        <td bgcolor="white" style="height: 114px" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../images/spacer(1).gif" width="14" /></p>
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

