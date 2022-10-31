<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master"
     AutoEventWireup="true" CodeFile="_CourseOfferingClearanceDUE.aspx.cs" Inherits="admin_CourseOfferingClearanceDUE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: auto;
            width: 167px;
        }
    </style>

      <script  type="text/javascript">
          function SelectAllChkb(headerchk) {
              var grdmovie = document.getElementById('ContentPlaceHolder_definition_GridView_student');
              var i;
              if (headerchk.checked) {
                  for (i = 0; i < grdmovie.rows.length; i++) {
                      var inputs = grdmovie.rows[i].getElementsByTagName('input');
                      inputs[0].checked = true;
                  }
              }
              else {
                  for (i = 0; i < grdmovie.rows.length; i++) {
                      var inputs = grdmovie.rows[i].getElementsByTagName('input');
                      inputs[0].checked = false;
                  }
              }
          }
          function CheckChildchkb(gv, Chkitem) {
              var grdmovie = gv.parentNode.parentNode.parentNode;
              var selectAll = grdmovie.rows[0].cells[Chkitem].getElementsByTagName("input")[0];
              if (!gv.checked) {
                  selectAll.checked = false;
              }
              else {
                  var checked = true;
                  for (var i = 1; i < grdmovie.rows.length; i++) {
                      var chb = grdmovie.rows[i].cells[Chkitem].getElementsByTagName("input")[0];
                      if (!chb.checked) {
                          checked = false;
                          break;
                      }
                  }
                  selectAll.checked = checked;
              }
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
   
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0" style="width:95%">
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
                                <b><font  face="Arial" size="2">Save Course Offering Clearence DUE</font></b> <hr />

</p>
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
                                            <table style="width:75%">
                                                <tr>
                                                    <td class="auto-style1">
                                                        Select Registatus</td>
                                                    <td style="width: auto; height: auto">
                                                        <asp:DropDownList ID="cmb_semester" runat="server">
                                                            <asp:ListItem Value="1">Spring</asp:ListItem>
                                                            <asp:ListItem Value="2">Summer</asp:ListItem>
                                                            <asp:ListItem Value="3">Fall</asp:ListItem>
                                                        </asp:DropDownList>
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        Year&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txt_year" runat="server" MaxLength="4" Width="100px"></asp:TextBox>
                                        </span></span>
                                                    </td>
                                                    <td style="width: 35px; height: 22px">
                                                        &nbsp;</td>
                                                    <td style="width: 49px; height: 22px">
                                                        &nbsp;</td>
                                                    <td style="width: 102px; height: 22px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1">
                                                        Batch or Student ID</td>
                                                    <td colspan="4" style="height: auto">
                                                        <asp:TextBox ID="txt_batch" runat="server"  Width="100px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1">
                                                    </td>
                                                    <td colspan="4" style="height: auto">
                                                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" /></td>
                                                </tr>
                                            </table>
                                        </span></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <hr />
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <asp:GridView ID="GridView_student" runat="server" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="4" Width="60%">
                                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                            <Columns>

                                                  <asp:TemplateField HeaderText="CheckAll">
                <HeaderTemplate>
                <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAllChkb(this);" />
                </HeaderTemplate>
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chk_select" runat="server" onclick="CheckChildchkb(this,0)" />
                    </ItemTemplate>                                       
               </asp:TemplateField>
                                              
                                                <asp:BoundField DataField="sid" HeaderText="ID" >
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="sname" HeaderText="Name" />
                                                <asp:CheckBoxField HeaderText="Is Cleared" DataField="acStatus" >
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:CheckBoxField>
                                                
                                                  
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="#330099" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#6699FF" Font-Bold="True" Font-Size="Small" ForeColor="#FFFFCC" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <asp:Button ID="btn_cleared" runat="server" OnClick="btn_cleared_Click" Text="Calculate DUE" /></td>
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
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>


