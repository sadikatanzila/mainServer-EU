<%@ Page Language="C#" MasterPageFile="~/staffs/courses/_courses_master.master" AutoEventWireup="true" 
CodeFile="_upload_lectures.aspx.cs" Inherits="staffs_courses_upload_handouts"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">

<script type="text/javascript" language="javascript">
 
function save_check()
{ 
    if(document.getElementById('ctl00_ContentPlaceHolder_content_txt_title').value=="")
    {
        alert("Please enter title.");
        return false;
    }
     
    else if(document.getElementById('ctl00_ContentPlaceHolder_content_fu_lectures').value=="")
    {
        alert("Please add attachment.");
        return false;
    }
    else return true;
    
}
</script>

    <table style="width: 95%">
        <tr>
            <td class="header" style="color: #ffa500; height: 20px; background-color: white;
                text-align: left">
                <table border="0" style="text-align: left">
                    <tr>
                        <td colspan="3" style="width: 512px; text-align: left">
                            <span style="color: #ffa500">Your location- Courses &gt; Course List &gt; Course Details
                                &gt; Class Lecture&nbsp;</span></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="header" style="height: 44px; background-color: #ffffff; text-align: left">
                <table style="width:auto;">
                    <tr>
                        <td style="width: auto; text-align: left">
                            Select Course
                            <asp:DropDownList ID="cmb_course" runat="server" ></asp:DropDownList>
                            <asp:Button ID="btn_submit" runat="server" Text="Submit" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <table border="0">
        <tr>
            <td colspan="3" style="text-align:left;" id="lbl_message">
                <hr />
                <asp:Label ID="lbl_message" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="Red"
                    Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="3">
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="539">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                            <img border="0" height="24" src="../../staffs/images/lcurv.gif" width="19" /></td>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../../staffs/images/rcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" height="22" width="505">
                            <p align="center">
                                <b><font color="#333399" face="Arial" size="2"><span style="color: #ffa500">Course Information</span></font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" height="114" width="18">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="18" /></td>
                        <td bgcolor="#ffffff" height="114" align="left" width="505">
                            <br />
                            <table border="0">
                                <tr>
                                    <td style="background-color: aliceblue">
                                        Course Code</td>
                                    <td style="background-color: aliceblue">
                                        <asp:Label ID="lbl_course_code" runat="server" Font-Bold="true"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        Course Name</td>
                                    <td>
                                        <asp:Label ID="lbl_course_name" runat="server" Font-Bold="true"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="background-color: aliceblue">
                                        Credit hours</td>
                                    <td style="background-color: aliceblue">
                                        <asp:Label ID="lbl_credit_hours" runat="server" Font-Bold="true"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        Semester</td>
                                    <td>
                                        <asp:Label ID="lbl_semester" runat="server" Font-Bold="true"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="background-color: aliceblue">
                                        Section</td>
                                    <td style="background-color: aliceblue">
                                        <asp:Label ID="lbl_section" runat="server" Font-Bold="true"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        Total Student</td>
                                    <td>
                                        <asp:Label ID="lbl_total_student" runat="server" Font-Bold="true"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Lectures</strong></td>
                                    <td>
                                        <asp:Label ID="lbl_assignment_msg" runat="server" Font-Bold="True" Font-Size="X-Small"
                                            ForeColor="Red" Text="Label"></asp:Label></td>
                                </tr>
                            </table>
                            <asp:GridView ID="GridView_assignment_list" runat="server" AutoGenerateColumns="False"
                                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="COURSE_MATERIALS_ID"
                                 OnRowDeleting="gvAdStd_RowDeleting">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                
                                 <asp:TemplateField Visible="false"> 
                                        <ItemTemplate>
                                            <asp:Label ID="Label_link" runat="server" Text='<%# Bind("COURSE_MATERIALS_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    <asp:HyperLinkField DataNavigateUrlFields="COURSE_MATERIALS_ID" DataNavigateUrlFormatString="~/staffs/courses/_lecture_write.aspx?code={0}"
                                        DataTextField="TITLE" HeaderText="Title" NavigateUrl="~/staffs/courses/_lecture_write.aspx">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                    
                                    
                                    <asp:BoundField DataField="up_date" HeaderText="Upload Date">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    
                                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="5%">
                                  <ItemTemplate>
                                <asp:ImageButton ID="imgButton" runat="server" ImageUrl="../../images/Delete.gif" CommandName="Delete" Width="25px" Height="25px"
                                     ToolTip="Delete" OnClientClick="return confirm('Are you sure to delete?')" />
                                    </ItemTemplate>
                                <ItemStyle CssClass="grid_header" />
                                 </asp:TemplateField>
                                     
                                </Columns>
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </td>
                        <td bgcolor="white" height="114" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                            <img border="0" height="15" src="../../staffs/images/blcurv.gif" width="19" /></td>
                        <td bgcolor="white" height="1" width="505">
                            <img border="0" height="14" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            <img border="0" height="15" src="../../staffs/images/brcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="150" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
    <table border="0">
        <tr>
            <td colspan="3" style="width: 542px">
                <table border="0" cellpadding="0" cellspacing="0" height="1" style="float: left"
                    width="262">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                            <img border="0" height="24" src="../../staffs/images/lcurv.gif" width="19" /></td>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="228">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../../staffs/images/rcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" height="22" width="228">
                            <p align="center">
                                <font color="#ffa500"><b>Upload Lecture</b></font></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="228">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="63" width="1">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" height="63" width="18">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="18" /></td>
                        <td align="left" bgcolor="#ffffff" height="auto" valign="top" width="228">
                            &nbsp;<br />
                            <table>
                                <tr>
                                    <td>
                                        Title</td>
                                    <td>
                                        <asp:TextBox ID="txt_title" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        Attach file</td>
                                    <td>
                                        <asp:FileUpload ID="fu_lectures" runat="server" Width="156px" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        Comments</td>
                                    <td>
                                        <textarea id="txt_comments" rows="2" style="width: 146px" runat="server"></textarea></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_fupload_submit" runat="server" Text="Submit" OnClick="btn_fupload_submit_Click" /></td>
                                </tr>
                            </table>
                        </td>
                        <td bgcolor="white" height="63" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" height="63" width="1">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                            <img border="0" height="15" src="../../staffs/images/blcurv.gif" width="19" /></td>
                        <td bgcolor="white" height="1" width="228">
                            <img border="0" height="14" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            <img border="0" height="15" src="../../staffs/images/brcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="228">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="150" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="width: 542px">
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
</asp:Content>

