<%@ Page Language="C#" MasterPageFile="~/staffs/advisor/MasterPage_advisor.master" AutoEventWireup="true" CodeFile="_show_students.aspx.cs" Inherits="staffs_advisor_show_students"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Advisoship &gt; Student List</span></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <script language="javascript" type="text/javascript">
  
  function check_valid()
  {
 
  
  if (document.getElementById("ctl00_ContentPlaceHolder_definition_txt_batch").value.toString()=="" )
  {
        alert("Please enter the batch");
        return false;
  }

  else
        return true;
  }  
  
  </script> 
   
   
    <table border="0">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="539">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                            <img border="0" height="24" src="../images/lcurv.gif"
                                width="19" /></td>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif"
                                width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../images/rcurv.gif"
                                width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" height="22" width="505">
                            <p align="center" style="text-align: center">
                                <b><font color="#ffa500" face="Arial" size="2">Show Student List</font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif"
                                width="1" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" style="height: 114px" width="1">
                            <img border="0" height="1" src="../images/spacer(1).gif"
                                width="1" /></td>
                        <td bgcolor="white" style="height: 114px" width="18">
                            <img border="0" height="1" src="../images/spacer(1).gif"
                                width="18" /></td>
                        <td bgcolor="#ffffff" style="vertical-align: top" width="505">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                <table>
                    <tr>
                     <td >
                         <strong>Batch</strong>
                        <asp:TextBox ID="txt_batch" runat="server" MaxLength="4" Width="40px"></asp:TextBox>
                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" />
                     </td>
                  </tr>
                </table>
                <hr />
                                        <asp:GridView ID="GridView_studentList" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" GridLines="None" Visible="False">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                                <asp:HyperLinkField DataTextField="sid" HeaderText="Student Id" NavigateUrl="~/staffs/advisor/_studentDetails.aspx" DataNavigateUrlFields="sid" DataNavigateUrlFormatString="~/staffs/advisor/_studentDetails.aspx?ids={0}" >
                                                    <ItemStyle Width="100px" />
                                                </asp:HyperLinkField>
                                                <asp:BoundField DataField="sname" HeaderText="Name">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="PHONE" HeaderText="Contact">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="COMP_CHRS" HeaderText="Comp. Credit">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="WCHRS" HeaderText="Waiver Credit">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>


                                                 <asp:BoundField DataField="TOTALCHRS" HeaderText="Total Credit">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>

                                                 <asp:BoundField DataField="reqch" HeaderText="Req. Credit">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="cgpa" HeaderText="CGPA" Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>

                                                 <asp:BoundField DataField="FINAL_CGPA" HeaderText="CGPA">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>

                                                 <asp:BoundField DataField="Grad_Status" HeaderText="Status">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
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
                                <img border="0" height="1" src="../images/spacer(1).gif"
                                    width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" style="height: 114px" width="1">
                            <img border="0" height="1" src="../images/spacer(1).gif"
                                width="1" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                            <img border="0" height="15" src="../images/blcurv.gif"
                                width="19" /></td>
                        <td bgcolor="white" height="1" width="505">
                            <img border="0" height="14" src="../images/spacer(1).gif"
                                width="1" /></td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            <img border="0" height="15" src="../images/brcurv.gif"
                                width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif"
                                width="150" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

