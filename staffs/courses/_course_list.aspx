<%@ Page Language="C#" MasterPageFile="~/staffs/courses/_courses_master.master" AutoEventWireup="true" CodeFile="_course_list.aspx.cs" Inherits="staffs_courses_course_list" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <table border="0" style="text-align: left">
        <tr>
            <td colspan="3" style="text-align: left">
                <span style="color: #ffa500">Your location- Courses &gt; Course List</span></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    &nbsp;
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
<script type="text/javascript" language="javascript">
 
function save_check()
{ 
    if(document.getElementById('ctl00_ContentPlaceHolder_content_txt_year').value=="")
    {
        alert("Please enter year.");
        return false;
    } 
    
    else return true;
    
}
</script>




    <table border="0">
        <tr>
            <td  >
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="99%">
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
                            <p align="center">
                                <b><font color="#ffa500" face="Arial" size="2">Course List</font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif"
                                width="1" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" width="1" style="height: 114px">
                            <img border="0" height="1" src="../images/spacer(1).gif"
                                width="1" /></td>
                        <td bgcolor="white" width="18" style="height: 114px">
                            <img border="0" height="1" src="../images/spacer(1).gif"
                                width="18" /></td>
                        <td bgcolor="#ffffff" width="100%" style=" vertical-align:top">
                            <table style="width: 100%;" border="0">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <table>
                                            <tr>
                                                <td style="width:45px; height:auto">Select</td>
                                                <td style="width:auto; height: auto;">
                                                    <asp:DropDownList ID="cmb_semester" runat="server">
                                                        <asp:ListItem Value="1">Spring</asp:ListItem>
                                                        <asp:ListItem Value="2">Summer</asp:ListItem>
                                                        <asp:ListItem Value="3">Fall</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td style="width:35px; height: 22px;">
                                                    Year</td>
                                                <td style="width:49px; height: 22px;">
                                                    <asp:TextBox ID="txt_year" runat="server" MaxLength="4" Width="40px" Height="15px"></asp:TextBox></td>
                                                <td style="width:102px; height: 22px;">
                                                    <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" /></td>
                                            </tr>
                                        </table>
                                      <hr />
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text="Label"></asp:Label><br />
                                        <asp:GridView ID="GridView_courseList" runat="server" CellPadding="4" GridLines="None" 
                                            AutoGenerateColumns="False" Visible="False" ForeColor="#333333" Width="100%">
                                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="Smaller" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:HyperLinkField DataTextField="COURSECODE" HeaderText="Code" 
                                                    NavigateUrl="~/staffs/courses/_singe_course.aspx" DataNavigateUrlFields="COURSE_TEACHER_ID"
                                                     DataNavigateUrlFormatString="~/staffs/courses/_singe_course.aspx?code={0}" >
                                                    <ItemStyle Width="60px" />
                                                </asp:HyperLinkField>
                                                <asp:BoundField DataField="CNAME" HeaderText="Name" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CHNG_CHOURS" HeaderText="Credit">
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TOTAL_STUDENT" HeaderText="No of Student">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SECTION" HeaderText="Section" >
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="sch_cls_1" HeaderText="1st Class" >
                                                <ItemStyle Width="65px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="sch_cls_2" HeaderText="2nd Class" >
                                                <ItemStyle Width="68px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="sch_cls_3" HeaderText="3rd Class" Visible="false" >
                                                <ItemStyle Width="65px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                               <tr>
                                 <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                 </td>
                               
                               </tr>
                            </table>
                            &nbsp;</td>
                        <td bgcolor="white" width="14" style="height: 114px">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../images/spacer(1).gif"
                                    width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" width="1" style="height: 114px">
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
</asp:Content>

