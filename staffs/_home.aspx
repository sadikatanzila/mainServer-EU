<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_home.aspx.cs" Inherits="staffs_home" %>

<%@ Register Assembly="FlashControl" Namespace="Bewise.Web.UI.WebControls" TagPrefix="Bewise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=7" />
     <title>Eastern University Web Portal</title>
    <link href="../student/App_themes/jind.css" rel="stylesheet" type="text/css"/>
    <link  href="../student/App_themes/transmenuh.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../student/App_themes/transmenu.js" ></script>
  
  <script language="javascript" type="text/javascript">
  
       function loadAcademicCalender()
        {
          window.open('_academic_calender.aspx','','titlebar=yes,toolbar=no,scrollbars,resizable=true,height=650,width=650');
          return false;
        } 
        
    
     function open_class_routine()
     {
    
      window.open("courses/class_routine.aspx",'','titlebar=no,toolbar=no,scrollbars,resizable=false,height=350,width=850');
      return false; 
     }
   
  
 
 </script> 
    
</head>
<body style=" margin-left:0; margin-top:0; margin:0; "  >
  <form id="form1" runat="server">
  <table width="800px" align="center"><tr><td>
  <div align="center">
      <div id="mastheadInner" style="width: 800px; height: 134px; " align="center">
            <table class="MainHeader" id="tblHeader" cellspacing="0" cellpadding="0" width="100%" align="right" border="0" runat="server">
               <tbody>
               <tr> 
                    <td valign="top" align="center" style="height: 5px;">
                        &nbsp;</td>
                          
                </tr>
                <tr> 
                    <td valign="top" align="center" style="height: 12px; background-color:#99ccff">
                        <img src="../images1/topimage_teachereastern_uni.jpg" /></td>
                         
                </tr>
               </tbody>
            </table>
         </div>          
 
         <div id="Div2" class="div_menu_top" style="width:800px">
            <table  id="Table1" style=" text-align:left;" class="MainHeader" cellspacing="0" cellpadding="0" width="100%" align="right" border="0" runat="server">
               <tbody>
                <tr> 
                    <td style="height: 20px; background-color: #336699;" >
                        <div id="menu" onclick="return menu_onclick()" style="height: 26px;">
                            <table>
                                <tr>
                                    <td><a id="home" class="mainlevel-trans" href="#">HOME</a></td>
                                    <td><a id="profile" class="mainlevel-trans" href="#">PROFILE</a></td>
                                    <td><a id="courses" class="mainlevel-trans" href="courses/_course_list.aspx">COURSES</a></td>       
                                   <td><a id="HR" class="mainlevel-trans" href="#">HR</a></td>  
                                     <td><a id="FORMS" class="mainlevel-trans" href="#">FORMS</a></td>                                  
                                    <td><a id="library" class="mainlevel-trans" href="#">LIBRARY</a></td>
                                     <td><a id="advisor" class="mainlevel-trans" href="#">ADVISORSHIP</a></td>
                                    <td><a id="link" class="mainlevel-trans" href="#">LINKS</a></td>
                                   <td><a id="EVALUATION" class="mainlevel-trans" href="#">EVALUATION</a></td>
                                   
                                    <td><a id="logout" class="mainlevel-trans" href="_login.aspx">LOGOUT</a></td>
                                </tr>
                            </table>
                            <img src="../images1/orange-line.gif" /></div>
                        
                        <script type="text/javascript" language="javascript">

                                function menu_onclick() {}
                                
                                if (TransMenu.isSupported()) 
                                {                                   
                                    var ms = new TransMenuSet(TransMenu.direction.down, 1, 0, TransMenu.reference.bottomLeft);
    
	                               var menu0 = ms.addMenu(document.getElementById("home"));
	                                //menu0.addItem("Teacher's Home","#");
	                                menu0.addItem("EU Home","http://www.easternuni.edu.bd");
	                                 
                                	
                                    var menu1 = ms.addMenu(document.getElementById("advisor"));
                                    //menu1.addItem("Academic Background", "advisor/_academic_background.aspx"); 
                                    //menu1.addItem("Completed Courses", "advisor/_completed_courses.aspx"); 
                                    menu1.addItem("Show Student", "advisor/_show_students.aspx"); 
                                    //menu1.addItem("Student Details", "advisor/_studentDetails.aspx");
	                                
                                    //================================================================================================
                                   var menu2 = ms.addMenu(document.getElementById("profile"));
                                   menu2.addItem("Change Password", "profile/_changePassword.aspx");
                                   menu2.addItem("Profile", "profile/_profile.aspx");
                                   menu2.addItem("Upload Image", "profile/_uploadPicturet.aspx");
                                    

                                    //================================================================================================
                                    var menu3 = ms.addMenu(document.getElementById("library"));
                                    menu3.addItem("Information","library/_information.aspx");
                                    menu3.addItem("Book Search","library/_book_search.aspx");
                                    menu3.addItem("Book Requisition","library/_book_requisition.aspx"); 
                                    

                                    var menu4 = ms.addMenu(document.getElementById("HR"));
                                    menu4.addItem("Attendance Report", "Attendance/_staffAttendanceaspx.aspx");
                                    menu4.addItem("Faculty wise Attendance Report", "Attendance/_showAttendance.aspx");
                                    // menu4.addItem("Faculty wise Attendance Report", "Attendance/_staffAttendanceaspx.aspx");
                                    //================================================================================================
                                    //var menu4 = ms.addMenu(document.getElementById("supervisor"));
                                   // menu4.addItem("Intern Supervisor", "supervisor/_supervisor.aspx");
                                    
                                    //================================================================================================
                                    var menu5 = ms.addMenu(document.getElementById("FORMS"));
                                    menu5.addItem("HR", "Forms/_formsHR.aspx");
                                    menu5.addItem("Accounts and Finance", "Forms/_formsAcntFinance.aspx");
                                    menu5.addItem("Procurement and Logistics", "Forms/_formsLogistics.aspx");
                                    menu5.addItem("Exam Controller’s Office", "Forms/_formsExamController.aspx");
                                    menu5.addItem("Dean/ Chairperson/ Coordinator", "Forms/_formsDean.aspx");
	                                
                                    var menu6 = ms.addMenu(document.getElementById("EVALUATION"));
                                    menu6.addItem("Teacher Evaluation", "Evaluation/_show_Evaluation.aspx");
                                    menu6.addItem("Evaluation Summary", "Evaluation/_course_teacherEvalSummery.aspx");
	                                 //================================================================================================
                                    // write drop downs into page
                                    //==================================================================================================
                                    // this method writes all the HTML for the menus into the page with document.write(). It must be
                                    // called within the body of the HTML page.
                                    //==================================================================================================
  
                                    TransMenu.renderAll();
                                } 
			                            init1=function(){TransMenu.initialize();}
			                            if (window.attachEvent) {
				                            window.attachEvent("onload", init1);
			                            }else{
				                            TransMenu.initialize();			
			                            }
                              </script> </td>
                </tr> 
               </tbody>
            </table>
        </div>   
 <div style="height:12px;">&nbsp;</div>
    <div>
      <table class="table_main_table" border="0" id="AutoNumber1" cellpadding="0px" cellspacing="0px" >
      <tr>  
          <td style=" width:100%; vertical-align:top; background:url(images/bg.gif) #ccccff;"> 
             
            <div style="float:left; width:25%; height:100%;"> &nbsp;<asp:Label ID="lbltypes" runat="server" ForeColor="Crimson" Text="Label" Font-Bold="True"></asp:Label><br />
              <table>
                 <tr>
                  <td>                  
                    </td>
                      </tr>                  
                  <tr>
                  <td>              
               <table border="0" style=" float:left;" cellpadding="0" cellspacing="0" height="1" width="162">
                  <tr>
                      <td colspan="2" height="24" rowspan="3" width="19">
                          <img border="0" height="24" src="images/lcurv.gif"
                                      width="19" /></td>
                      <td bgcolor="#6fb1d9" class="k" height="1" width="228">
                          <img border="0" height="1" src="images/spacer(1).gif"
                                      width="100%" /></td>
                      <td align="right" colspan="2" height="24" rowspan="3" width="15">
                          <img border="0" height="24" src="images/rcurv.gif"
                                      width="15" /></td>
                  </tr>
                  <tr>
                      <td bgcolor="#eef5fa" class="h" height="22" width="228">
                          <p align="center">
                              <font color="#ffa500"><b>General Notice</b></font></p>
                      </td>
                  </tr>
                  <tr>
                      <td bgcolor="#6fb1d9" class="k" height="1" width="228">
                          <img border="0" height="1" src="images/spacer(1).gif"
                                      width="1" /></td>
                  </tr>
                  <tr>
                      <td bgcolor="#6fb1d9" class="k" height="63" width="1">
                          <img border="0" height="1" src="images/spacer(1).gif"
                                      width="1" /></td>
                      <td bgcolor="white" height="63" width="18">
                          <img border="0" height="1" src="images/spacer(1).gif"
                                      width="18" /></td>
                      <td align="left" bgcolor="#ffffff"  valign="top" width="228">
                          <br />
                                           <asp:GridView ID="GridView_generalNotice" runat="server" AutoGenerateColumns="False"
                                               CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeader="False" Width="100%">
                                               <Columns>
                                                   <asp:TemplateField>
                                                       <ItemTemplate>
                                                           <asp:Image ID="Image1" runat="server" ImageUrl="~/student/images/link_icons.png" />
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:HyperLinkField DataNavigateUrlFields="NOTICE_ID" DataNavigateUrlFormatString="~/staffs/notice/_notice.aspx?code={0}"
                                                       DataTextField="TITLE" NavigateUrl="~/staffs/notice/_notice.aspx" Target="_blank">
                                                       <ItemStyle Font-Bold="False" />
                                                   </asp:HyperLinkField>
                                               </Columns>
                                               <AlternatingRowStyle BackColor="White" BorderColor="Silver" BorderStyle="Inset" BorderWidth="1px" />
                                               <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                               <RowStyle BackColor="#E3EAEB" />
                                               <EditRowStyle BackColor="#7C6F57" />
                                               <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                               <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                               <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                           </asp:GridView> 
                          <br />
                                           <asp:HyperLink ID="more_genNotice" runat="server" Font-Underline="False" NavigateUrl="~/staffs/notice/_generalNotice.aspx"
                                               Width="41px">more....</asp:HyperLink></td>
                      <td bgcolor="white" height="63" width="14">
                          &nbsp;
                          <p>
                              <img border="0" height="1" src="images/spacer(1).gif"
                                          width="14" /></p>
                      </td>
                      <td bgcolor="#6fb1d9" class="k" height="63" width="1">
                          <img border="0" height="1" src="images/spacer(1).gif"
                                      width="1" /></td>
                  </tr>
                  <tr>
                      <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                          <img border="0" height="15" src="images/blcurv.gif"
                                      width="19" /></td>
                      <td bgcolor="white" height="1" width="228">
                          <img border="0" height="14" src="images/spacer(1).gif"
                                      width="1" /></td>
                      <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                          <img border="0" height="15" src="images/brcurv.gif"
                                      width="15" /></td>
                  </tr>
                  <tr>
                      <td bgcolor="#6fb1d9" class="k" height="1" width="228">
                      <img border="0" height="1" src="images/spacer(1).gif" width="150" /></td>
                  </tr>
              </table>
                </td>
               </tr>
              </table>              
                <br />
                &nbsp;</div>               
             <div id="right_div" style="float:right; width:25%; height:100%; background-color: #336699;">                   
                 <br />
                 <asp:Image ID="img_myPicture" runat="server" Height="136px" Width="130px" ImageUrl="~/student/profile/student_images/no_image.gif" /><br />
                 <asp:Label ID="lbl_login" runat="server" Font-Bold="True" ForeColor="LavenderBlush" Text="Label"></asp:Label><br />
                 <table>
                     <tr>
                         <td style="height: 106px">
                             <table border="0" style=" float:left;" cellpadding="0" cellspacing="0" height="1" width="162">
                                 <tr>
                                     <td colspan="2" height="24" rowspan="3" width="19">
                                         <img border="0" height="24" src="images/lcurv.gif"
                                          width="19" /></td>
                                     <td bgcolor="#6fb1d9" class="k" height="1" style="width: 228px">
                                         <img border="0" height="1" src="images/spacer(1).gif"
                                          width="100%" /></td>
                                     <td align="right" colspan="2" height="24" rowspan="3" width="15">
                                         <img border="0" height="24" src="images/rcurv.gif"
                                          width="15" /></td>
                                 </tr>
                                 <tr>
                                     <td bgcolor="#eef5fa" class="h" height="22" style="width: 228px">
                                         <p align="center">
                                             <font color="#ffa500"><b>Message</b></font></p>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td bgcolor="#6fb1d9" class="k" height="1" style="width: 228px">
                                         <img border="0" height="1" src="images/spacer(1).gif"
                                          width="1" /></td>
                                 </tr>
                                 <tr>
                                     <td bgcolor="#6fb1d9" class="k" width="1" style="height: 63px">
                                         <img border="0" height="1" src="images/spacer(1).gif"
                                          width="1" /></td>
                                     <td bgcolor="white" width="18" style="height: 63px">
                                         <img border="0" height="1" src="images/spacer(1).gif"
                                          width="18" /></td>
                                     <td align="left" bgcolor="#ffffff" valign="top" style="width: 228px; height: 63px;">
                                         <img src="../student/images/link_icons.png" /><a href="#">Semester Grade Sheet</a><br />
                                         <img src="../student/images/link_icons.png" /><a href="#">Completed
                                             Courses</a><br />
                                         <img src="../student/images/link_icons.png" /><a href="#">Academic
                                             Status</a>
                                         <br />
                                     </td>
                                     <td bgcolor="white" width="14" style="height: 63px">
                                         &nbsp;
                                         <p>
                                             <img border="0" height="1" src="images/spacer(1).gif"
                                              width="14" /></p>
                                     </td>
                                     <td bgcolor="#6fb1d9" class="k" width="1" style="height: 63px">
                                         <img border="0" height="1" src="images/spacer(1).gif"
                                          width="1" /></td>
                                 </tr>
                                 <tr>
                                     <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                                         <img border="0" height="15" src="images/blcurv.gif"
                                          width="19" /></td>
                                     <td bgcolor="white" height="1" style="width: 228px">
                                         <img border="0" height="14" src="images/spacer(1).gif"
                                          width="1" /></td>
                                     <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                                         <img border="0" height="15" src="images/brcurv.gif"
                                          width="15" /></td>
                                 </tr>
                                 <tr>
                                     <td bgcolor="#6fb1d9" class="k" height="1" style="width: 228px">
                                         <img border="0" height="1" src="images/spacer(1).gif"
                                          width="150" /></td>
                                 </tr>
                             </table>
                         </td>
                     </tr>
                 </table>
             </div>
               
               <div id="content_div" style="float:right; text-align:left; padding-right:5px; background-color:White; width:48%; height:100%;">                   
                   <table style="text-align:left;">
                       <tr>
                           <td>
                               &nbsp;</td>
                       </tr>
                       <tr>
                           <td style="">
                               <span ><span style="font-size: 10pt"><strong><span style="color: #ff0066"><span style="color: #0000ff">
                                   Welcome 
                                   <asp:Label ID="lbl_name" runat="server" Font-Bold="True" ForeColor="Crimson" Text="Label"></asp:Label></span> </span> <span style="color: #0000ff"></span></strong></span></span></td>
                       </tr>
                       <tr>
                           <td>
                               <strong><span style="font-size: 10pt; color: #0000ff">&nbsp; &nbsp;&nbsp; to Eastern
                                   Family</span></strong></td>
                       </tr>
                       <tr>
                           <td style="">
                               &nbsp;</td>
                       </tr>
                       <tr>
                           <td style="TEXT-ALIGN: justify">
                               Eastern University
                                   is a government approved private university founded
                                   in 2003 by Mr. Abul Kasem Haider.
                                   The university is an independent organization with
                                   its own Board of Trustees</td>
                       </tr>
                       <tr>
                           <td>
                           </td>
                       </tr>
                       <tr>
                           <td style="height:auto; text-align: justify">
                               Eastern University envisions to promote and create a learning environment through state-of-the-art
                                   facilities and tools; highly competent faculties and staff; expanded frontier of
                                   research-based knowledge; and international standards supportive of the new horizons
                                   in the diverse fields of disciplines and the enunciated development perspectives
                                   of the country.</td>
                       </tr>
                       <tr>
                           <td style="height: 15px; text-align: justify">
                           </td>
                       </tr>
                       <tr>
                           <td style="height: auto; text-align: justify">
                               <table>
                                   <tr>
                                       <td style="width: 50px">
                                       </td>
                                       <td>
                                           <img id="img" alt="Academic Calender" src="../images/arrow3.gif" /></td>
                                       <td>
                                           <asp:HyperLink ID="hplink_classRoutine" runat="server" Font-Bold="True" NavigateUrl="#">Class Routine</asp:HyperLink></td>
                                   </tr>
                                   <tr style="color: #0000ff; text-decoration: underline">
                                       <td style="width: 50px">
                                       </td>
                                       <td>
                                           <img id="img1" alt="Academic Calender" src="../images/arrow3.gif" /></td>
                                       <td> <asp:HyperLink ID="hpLink_academic_calender" runat="server" Font-Bold="True" NavigateUrl="#">Academic Calendar</asp:HyperLink></td>
                                   </tr>
                                   <tr style="color: #0000ff; ">
                                       <td style="width: 50px">
                                       </td>
                                       <td>
                                           <img id="img2" alt="Academic Calender" src="../images/arrow3.gif" /></td>
                                       <td>
                                           <a style="font-weight: bold; color: purple; text-align: right" href="profile/Guidelines.pdf" >Guidelines</a>&nbsp;</td>
                                   </tr>
                               </table>
                           </td>
                       </tr>
                   </table>
                   &nbsp;
               </div>
              
               <div id="BottomRight" style="float:right; text-align:left; background-color:White; width:73.6%; height:100%;">                   
                   <table style="width:100%">
                       <tr>
                           <td style=" vertical-align:top">
                               <table border="0" style=" float:right;" cellpadding="0" cellspacing="0" height="1" width="272">
                                   <tr>
                                       <td colspan="2" height="24" rowspan="3" width="19">
                                           <img border="0" height="24" src="images/lcurv.gif"
                                      width="19" /></td>
                                       <td bgcolor="#6fb1d9" class="k" height="1" width="228">
                                           <img border="0" height="1" src="images/spacer(1).gif"
                                      width="100%" /></td>
                                       <td align="right" colspan="2" height="24" rowspan="3" width="15">
                                           <img border="0" height="24" src="images/rcurv.gif"
                                      width="15" /></td>
                                   </tr>
                                   <tr>
                                       <td bgcolor="#eef5fa" class="h" height="22" width="228">
                                           <p align="center">
                                               <font color="#ffa500"><b>Student Advising Request</b></font></p>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td bgcolor="#6fb1d9" class="k" height="1" width="228">
                                           <img border="0" height="1" src="images/spacer(1).gif"
                                      width="1" /></td>
                                   </tr>
                                   <tr>
                                       <td bgcolor="#6fb1d9" class="k" height="63" width="1">
                                           <img border="0" height="1" src="images/spacer(1).gif"
                                      width="1" /></td>
                                       <td bgcolor="white" height="63" width="18">
                                           <img border="0" height="1" src="images/spacer(1).gif"
                                      width="18" /></td>
                                       <td align="left" bgcolor="#ffffff"  valign="top" width="228">
                                           <asp:HyperLink ID="hpLink_advising_list" runat="server" Font-Underline="False" NavigateUrl="#">more....</asp:HyperLink><br />
                                           <br />
                                           <asp:GridView ID="GridView_advising" runat="server" AutoGenerateColumns="False"
                                               CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeader="False" Width="100%">
                                               <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                               <Columns>
                                                   <asp:TemplateField>
                                                       <ItemTemplate>
                                                           <asp:Image ID="Image1" runat="server" ImageUrl="~/images1/email.png" />
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:HyperLinkField DataNavigateUrlFields="code" DataNavigateUrlFormatString="~/staffs/advisor/_courseAdvisingDetails.aspx?code={0}"
                                                       DataTextField="SID" NavigateUrl="~/staffs/advisor/_courseAdvisingDetails.aspx" HeaderText="ID">
                                                       <ItemStyle Font-Bold="False" />
                                                   </asp:HyperLinkField>
                                                   
                                                   <asp:BoundField DataField="SEMESTERN" HeaderText="Semester" />
                                                   <asp:BoundField DataField="YEAR" HeaderText="Year" />
                                                   <asp:BoundField DataField="SPROGRAM" HeaderText="Program" />
                                               </Columns>
                                               <RowStyle BackColor="#E3EAEB" />
                                               <EditRowStyle BackColor="#7C6F57" />
                                               <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                               <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                               <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                               <AlternatingRowStyle BackColor="White" BorderColor="Silver" BorderStyle="Inset" BorderWidth="1px" />
                                           </asp:GridView>
                                           </td>
                                       <td bgcolor="white" height="63" width="14">
                                           &nbsp;
                                           <p>
                                               <img border="0" height="1" src="images/spacer(1).gif"
                                          width="14" /></p>
                                       </td>
                                       <td bgcolor="#6fb1d9" class="k" height="63" width="1">
                                           <img border="0" height="1" src="images/spacer(1).gif"
                                      width="1" /></td>
                                   </tr>
                                   <tr>
                                       <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                                           <img border="0" height="15" src="images/blcurv.gif"
                                      width="19" /></td>
                                       <td bgcolor="white" height="1" width="228">
                                           <img border="0" height="14" src="images/spacer(1).gif"
                                      width="1" /></td>
                                       <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                                           <img border="0" height="15" src="images/brcurv.gif"
                                      width="15" /></td>
                                   </tr>
                                   <tr>
                                       <td bgcolor="#6fb1d9" class="k" height="1" width="228">
                                           <img border="0" height="1" src="images/spacer(1).gif" width="150" /></td>
                                   </tr>
                               </table>
                           </td>
                           <td style="text-align:right;vertical-align:top">
                           <table border="0" style=" float:left;" cellpadding="0" cellspacing="0" height="1" width="272">
                               <tr>
                                   <td colspan="2" height="24" rowspan="3" width="19">
                                       <img border="0" height="24" src="images/lcurv.gif"
                                      width="19" /></td>
                                   <td bgcolor="#6fb1d9" class="k" height="1" width="228">
                                       <img border="0" height="1" src="images/spacer(1).gif"
                                      width="100%" /></td>
                                   <td align="right" colspan="2" height="24" rowspan="3" width="15">
                                       <img border="0" height="24" src="images/rcurv.gif"
                                      width="15" /></td>
                               </tr>
                               <tr>
                                   <td bgcolor="#eef5fa" class="h" height="22" width="228">
                                       <p align="center">
                                           <font color="#ffa500"><b>Teacher Notice Board</b></font></p>
                                   </td>
                               </tr>
                               <tr>
                                   <td bgcolor="#6fb1d9" class="k" height="1" width="228">
                                       <img border="0" height="1" src="images/spacer(1).gif"
                                      width="1" /></td>
                               </tr>
                               <tr>
                                   <td bgcolor="#6fb1d9" class="k" height="63" width="1">
                                       <img border="0" height="1" src="images/spacer(1).gif"
                                      width="1" /></td>
                                   <td bgcolor="white" height="63" width="18">
                                       <img border="0" height="1" src="images/spacer(1).gif"
                                      width="18" /></td>
                                   <td align="left" bgcolor="#ffffff"  valign="top" width="228">
                                       <br />
                                       <asp:GridView ID="GridView_teacher" runat="server" AutoGenerateColumns="False"
                                               CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeader="False" Width="100%">
                                           <Columns>
                                               <asp:TemplateField>
                                                   <ItemTemplate>
                                                       <asp:Image ID="Image1" runat="server" ImageUrl="~/student/images/link_icons.png" />
                                                   </ItemTemplate>
                                               </asp:TemplateField>
                                               <asp:HyperLinkField DataNavigateUrlFields="NOTICE_ID" DataNavigateUrlFormatString="~/staffs/notice/_notice.aspx?code={0}"
                                                       DataTextField="TITLE" NavigateUrl="~/staffs/notice/_notice.aspx" Target="_blank">
                                                   <ItemStyle Font-Bold="False" />
                                               </asp:HyperLinkField>
                                           </Columns>
                                           <AlternatingRowStyle BackColor="White" BorderColor="Silver" BorderStyle="Inset" BorderWidth="1px" />
                                           <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                           <RowStyle BackColor="#E3EAEB" />
                                           <EditRowStyle BackColor="#7C6F57" />
                                           <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                           <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                           <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                       </asp:GridView>
                                       <br />
                                       <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="False" NavigateUrl="~/staffs/notice/_staffNotice.aspx"
                                           Width="41px">more....</asp:HyperLink></td>
                                   <td bgcolor="white" height="63" width="14">
                                       &nbsp;
                                       <p>
                                           <img border="0" height="1" src="images/spacer(1).gif"
                                          width="14" /></p>
                                   </td>
                                   <td bgcolor="#6fb1d9" class="k" height="63" width="1">
                                       <img border="0" height="1" src="images/spacer(1).gif"
                                      width="1" /></td>
                               </tr>
                               <tr>
                                   <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                                       <img border="0" height="15" src="images/blcurv.gif"
                                      width="19" /></td>
                                   <td bgcolor="white" height="1" width="228">
                                       <img border="0" height="14" src="images/spacer(1).gif"
                                      width="1" /></td>
                                   <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                                       <img border="0" height="15" src="images/brcurv.gif"
                                      width="15" /></td>
                               </tr>
                               <tr>
                                   <td bgcolor="#6fb1d9" class="k" height="1" width="228">
                                   <img border="0" height="1" src="images/spacer(1).gif" width="150" /></td>
                               </tr>
                           </table>
                           </td>
                       </tr>
                       <tr>
                           <td style="HEIGHT: 15px"></td>
                           <td style="HEIGHT: 15px"></td>
                       </tr>
                   </table>
               </div>
              
              
          </td>  
      </tr>
      
      <tr>
        <td style="background-color:#336699;">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="footertext" valign="top" style="height: 24px">
                        <span style="color: #ffffff">
                        Main Campus:</span></td>
                    <td class="footertext" style="width: 536px; height: 24px;" valign="top">
                        <span style="color: #ffffff">House#26, Road# 05, Dhanmondi, Dhaka, Bangladesh.Tel: +880.2.9676031-5
                        <br />
                        Fax: +88 02 9676031-5, +880.2.9676031-5. Email: info@easternuni.edu.bd </span>
                    </td>
                </tr>
            </table>
            
        </td>
      </tr>
      </table>  
    </div>
</div>
  </td></tr></table>

</form>
</body>
</html>

