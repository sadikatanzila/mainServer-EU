<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_finalGrading_alter.aspx.cs" Inherits="staffs_courses_finalGrading_alter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <script language="javascript" type="text/javascript">

         function printGradesheet(ccode) {
             window.open("_print_studentGrade.aspx?ccode=" + ccode, '', 'titlebar=no,toolbar=no,scrollbars,resizable=false,height=850,width=600');
             return false;
         }

 </script> 
   
  <script type="text/javascript">
      function EnableTextBoxValidation(objDDL) {

          // disableStatus();
          var ddlDOM = document.getElementById(objDDL);
          if (ddlDOM.selectedIndex > 0) {

              if (ddlDOM.selectedIndex != 9) {
                  document.getElementById(ddlDOM.id.substring(0, 24) + "ddlstatus").disabled = true;
              }
              else {
                  document.getElementById(ddlDOM.id.substring(0, 24) + "ddlstatus").disabled = false;
              }
          }
      }
      function disableStatus() {
          var gvET = document.getElementById("<%= GridView_students.ClientID %>");
          var rCount = gvET.rows.length;
          for (i = 1; i <= rCount; i++) {
              document.getElementById("ddlstatus.ClientID").disabled = true;
          }
      }
     </script>

    
    
    <form id="form1" runat="server">
    <div>
    <table style="width: 95%">
        <tr>
            <td class="header" style="color: #ffa500; height: 20px; background-color: white;
                text-align: left">
                <table border="0" style="text-align: left">
                    <tr>
                        <td colspan="3" style="width: 100%; text-align: left">
                            <span style="color: #ffa500">Your location- Courses &gt; Course List &gt; Course Details
                                &gt; Student Grading </span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="header" style="height: 44px; background-color: #ffffff; text-align: left">
                <table style="width: auto">
                    <tr>
                        <td style="width: auto; text-align: left">
                            Select Course
                            <asp:DropDownList ID="cmb_course" runat="server">
                            </asp:DropDownList>
                            <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click1" Text="Submit" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>


        <table border="0">
        <tr>
            <td colspan="3">
                <hr />
            </td>
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
                        <td align="left" bgcolor="#ffffff" height="114" width="505">
                            <br />
                            <table id="TABLE1" border="0" onclick="return TABLE1_onclick()" style="text-align: left">
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
                                    <td>
                                        Program</td>
                                    <td>
                                        <asp:Label ID="lbl_Program" runat="server" Font-Bold="true"></asp:Label></td>
                                </tr>
                            </table>
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

        <table border="0">
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
                                <b><font color="#333399" face="Arial" size="2"><span style="color: #ffa500">Student
                                    List</span></font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" width="1" style="height: 109px">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" width="18" style="height: 109px">
                            <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="18" /></td>
                        <td bgcolor="#ffffff" style="vertical-align: top; text-align: left; height: 109px;"
                            width="505">
                            <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label><br />
                           
                            
                            
                            
                         
                            
                             <asp:GridView ID="GridView_students" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="None" Width="100%" OnPageIndexChanging = "PageIndexChanging" 
                                 OnRowDataBound="GridView_students_RowDataBound" OnRowEditing="GridView_students_RowEditing">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="SID" HeaderText="Student ID">
                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SNAME" HeaderText="Student Name">
                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                
                                    <asp:TemplateField HeaderText="Grade" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="cmb_grades" runat="server"  onchange="EnableTextBoxValidation(this.id)" >                                             
                                        </asp:DropDownList>
                                        <asp:Label ID="lblgrade" Text="" runat="server" />     
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                    </asp:TemplateField>


                                 

                <asp:TemplateField HeaderText="Fail Status" >
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlstatus" runat="server" >
                            
                        </asp:DropDownList>
                        <asp:Label ID="lblFail" Text="" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("GGRADE2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("IS_FAIL") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="False"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_COURSEKEY" runat="server" Text='<%# Bind("COURSE_KEY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="False"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_REGKEY" runat="server" Text='<%# Bind("REGKEY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>



  
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                            &nbsp;
                            <br />
                            <table style="width:100%">
                                <tr>
                                    <td ></td>
                                    <td style="text-align: right" ><asp:Button ID="btn_save" runat="server" OnClick="btn_save_Click" Text="Save"  /></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="color: #FF0000; font-weight: bold"><asp:Label ID="Label3" runat="server" Text="Regular: Attaend exam, and fail with result purpose <br/>Irregular: Fail for not attending in the Class/Mid exam/Final Exam "></asp:Label></td>
                                </tr>
                                <tr>
                                    <td >&nbsp;</td>
                                    <td style="text-align: right" >&nbsp;</td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Button ID="btn_approve" runat="server" Text="Final Approved" OnClick="btn_approve_Click" />
                                        <asp:Button ID="btn_print_grade" runat="server" Text="Print Grade Sheet Summary"  /></td>
                                    <td >
                                        </td>
                                </tr>
                            </table>
                        </td>
                        <td bgcolor="white" width="14" style="height: 109px">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../../staffs/images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" width="1" style="height: 109px">
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
    </div>
    </form>
</body>
</html>
