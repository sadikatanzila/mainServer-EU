<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_course_allocation.aspx.cs" Inherits="admin_course_allocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">

<script language="javascript" type="text/javascript">

    function chech_valid() {

        if (document.getElementById("ctl00_ContentPlaceHolder_content_cmb_teacher").value.toString() == "Select") {
            alert("Please select teacher");
            return false;
        }
        else if (document.getElementById("ctl00_ContentPlaceHolder_content_txt_capacity").value.toString() == "&nbsp;" || document.getElementById("ctl00_ContentPlaceHolder_content_txt_capacity").value.toString() == "" || document.getElementById("ctl00_ContentPlaceHolder_content_txt_capacity").value.toString() == "0" || isNaN(document.getElementById("ctl00_ContentPlaceHolder_content_txt_capacity").value.toString())) {
            alert("Please enter correct student capacity.");
            return false;
        }


        else
            return true;
    }

 </script> 
   
   
  <table border="0">
        <tr>
            <td colspan="3" style="text-align: left"><asp:Label ID="lbl_message" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="Red" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="3">
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
                                <b><font  face="Arial" size="2">Course Information</font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="100%">
                            </td>
                    </tr>
                    <tr>
                        <td  class="k" height="114" width="1">
                            </td>
                        <td bgcolor="white" height="114" width="18">
                           </td>
                        <td bgcolor="#ffffff" height="114" width="100%">
                            <div style="text-align: left">
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
                                </table>
                            </div>
                        </td>
                        <td bgcolor="white" height="114" width="14">
                            &nbsp;
                            <p>
                              </p>
                        </td>
                        <td  class="k" height="114" width="1">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server"><table border="0">
    <tr>
        <td colspan="3">
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
                            <b><font  face="Arial" size="2">Teacher and Routine</font></b></p>
                    </td>
                </tr>
                <tr>
                    <td  class="k" height="1" width="100%">
                        </td>
                </tr>
                <tr>
                    <td  class="k" height="114" width="1">
                        </td>
                    <td bgcolor="white" height="114" width="18">
                       </td>
                    <td bgcolor="#ffffff" height="114" width="100%">
                        <div style="text-align: left">
                            <asp:Label ID="lbl_teacher_message" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="Red"
                                Text="Label"></asp:Label><br />
                            <asp:GridView ID="GridView_courseList" runat="server" AutoGenerateColumns="False"
                                CellPadding="4" ForeColor="#333333" GridLines="None">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField>                                       
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_select" runat="server" Checked="false" Enabled="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="STAFF_NAME" HeaderText="Name">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="110px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SECTION" HeaderText="Group" />
                                    <asp:BoundField DataField="TOTAL_CAPACITY" HeaderText="Capacity" >
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SCH_CLS_1" HeaderText="1st Class" />
                                    <asp:BoundField DataField="SCH_CLS_2" HeaderText="2nd Class" />
                                    <asp:BoundField DataField="TUT_CLS_1" HeaderText="Tutorial-1" />
                                    <asp:BoundField DataField="TUT_CLS_2" HeaderText="Tutorial-2" />
                                    <asp:TemplateField Visible="False"> 
                                        <ItemTemplate>
                                            <asp:Label ID="Label_link" runat="server" Text='<%# Bind("COURSE_TEACHER_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_teacherId" runat="server" Text='<%# Bind("TEACHER_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                   





                                   

                                </Columns>
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Smaller" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            <br />
                            &nbsp;&nbsp;<asp:Button ID="btn_bodify" runat="server" Text="Modify" OnClick="btn_bodify_Click" />
                            <asp:Button ID="btn_delete" runat="server" OnClick="btn_delete_Click" Text="Delete" /></div>
                    </td>
                    <td bgcolor="white" height="114" width="14">
                        &nbsp;
                        <p>
                          </p>
                    </td>
                    <td  class="k" height="114" width="1">
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server"><table border="0">
    <tr>
        <td colspan="3">
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
                            <b><font  face="Arial" size="2">Add Teacher and Routine</font></b></p>
                    </td>
                </tr>
                <tr>
                    <td  class="k" height="1" width="100%">
                        </td>
                </tr>
                <tr>
                    <td  class="k" height="114" width="1">
                        </td>
                    <td bgcolor="white" height="114" width="18">
                       </td>
                    <td bgcolor="#ffffff" height="114" width="100%">
                        <div style="text-align: left">
                            <br />
                            <table border="0">
                                <tr>
                                    <td>
                                        <strong>Teacher</strong></td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="cmb_teacher" runat="server"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Group</strong></td>
                                    <td>
                                        <asp:DropDownList ID="cmb_group" runat="server"></asp:DropDownList></td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Capacity</strong></td>
                                    <td>
                                        <asp:TextBox ID="txt_capacity" runat="server" Width="71px"></asp:TextBox>
                                        students</td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; border-left-color: cornflowerblue; border-bottom-color: cornflowerblue; color: white; border-top-style: solid; border-top-color: cornflowerblue; border-right-style: solid; border-left-style: solid; background-color: cornflowerblue; border-right-color: cornflowerblue; border-bottom-style: solid;"><strong>Class</strong></td>
                                    <td style="text-align: left; border-left-color: cornflowerblue; border-bottom-color: cornflowerblue; color: white; border-top-style: solid; border-top-color: cornflowerblue; border-right-style: solid; border-left-style: solid; background-color: cornflowerblue; border-right-color: cornflowerblue; border-bottom-style: solid;"><strong>Day &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Time</strong></td>
                                    <td style="text-align: left; border-left-color: cornflowerblue; border-bottom-color: cornflowerblue; color: white; border-top-style: solid; border-top-color: cornflowerblue; border-right-style: solid; border-left-style: solid; background-color: cornflowerblue; border-right-color: cornflowerblue; border-bottom-style: solid;"><strong>&nbsp; </strong></td>
                                </tr>
                                <tr>
                                    <td>First class</td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="cmb_date1" runat="server"></asp:DropDownList>&nbsp;
                                        <asp:DropDownList ID="cmb_hour1" runat="server"></asp:DropDownList>
                                        <asp:DropDownList ID="cmb_min1" runat="server"></asp:DropDownList>
                                        <asp:DropDownList ID="cmb_time1" runat="server">
                                            <asp:ListItem>AM</asp:ListItem>
                                            <asp:ListItem>PM</asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>Second class</td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="cmb_date2" runat="server">
                                    </asp:DropDownList>&nbsp;
                                        <asp:DropDownList ID="cmb_hour2" runat="server">
                                        </asp:DropDownList>&nbsp;<asp:DropDownList ID="cmb_min2" runat="server">
                                        </asp:DropDownList>&nbsp;<asp:DropDownList ID="cmb_time2" runat="server">
                                            <asp:ListItem>AM</asp:ListItem>
                                            <asp:ListItem>PM</asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>Tutorial class-1</td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="cmb_date3" runat="server">
                                    </asp:DropDownList>&nbsp;
                                        <asp:DropDownList ID="cmb_hour3" runat="server">
                                        </asp:DropDownList>&nbsp;<asp:DropDownList ID="cmb_min3" runat="server">
                                        </asp:DropDownList>&nbsp;<asp:DropDownList ID="cmb_time3" runat="server">
                                            <asp:ListItem>AM</asp:ListItem>
                                            <asp:ListItem>PM</asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>Tutorial class-2</td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="cmb_date4" runat="server">
                                    </asp:DropDownList>&nbsp;
                                        <asp:DropDownList ID="cmb_hour4" runat="server">
                                        </asp:DropDownList>&nbsp;<asp:DropDownList ID="cmb_min4" runat="server">
                                        </asp:DropDownList>&nbsp;<asp:DropDownList ID="cmb_time4" runat="server">
                                            <asp:ListItem>AM</asp:ListItem>
                                            <asp:ListItem>PM</asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" /></td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td bgcolor="white" height="114" width="14">
                        &nbsp;
                        <p>
                          </p>
                    </td>
                    <td  class="k" height="114" width="1">
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

