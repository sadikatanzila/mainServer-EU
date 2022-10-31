<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_home.aspx.cs" Inherits="student_home" %>
<%@ Register Assembly="FlashControl" Namespace="Bewise.Web.UI.WebControls" TagPrefix="Bewise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>Eastern University Web Portal</title>
    <link href="App_themes/jind.css" rel="stylesheet" type="text/css"/>
    <link  href="App_themes/transmenuh.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../student/App_themes/transmenu.js" ></script>
    
    <script language="javascript"  type="text/javascript">
 
     function open_class_routine(sid)
     {
    
      window.open("academics/class_routine.aspx?sid="+sid,'','titlebar=no,toolbar=no,scrollbars,resizable=false,height=350,width=850');
      return false; 
     }
   
 </script>
                     
    
</head>
<body style=" margin-top:0; margin:0; "  >
  <form id="form1" runat="server">
    <table align="center" width="880px">
    <tr><td>
        <div align="center">
        
        <div id="mastheadInner" style="width: 880px; height: 116px; " align="center"><%--style="width: 800px; height: 120px; "--%>
            <table class="MainHeader" id="tblHeader" cellspacing="0" cellpadding="0" width="100%"  border="0" runat="server"><%--align="right"--%>
               <tbody>
                <tr> 
                    <td valign="top" align="center" style="height: 5px;" >&nbsp;
                       </td>
                </tr>
                <tr> 
                    <td valign="top" align="center" style="height: 12px; background-color:#99ccff">
                       <img src="../images1/topimage_student.jpg" width="880px" height="100px" />
                       </td>
                </tr>
               </tbody>
            </table>
        </div>          
        
        <div id="Div2" class="div_menu_top" style="width:880px">
            <table  id="Table1" style=" text-align:left;" class="MainHeader" cellspacing="0" cellpadding="0" width="100%" align="right" border="0" runat="server">
               <tbody>
                <tr> 
                    <td style="height: 20px; background-color: inactivecaption;" >
                        <div><img src="../images1/orange-line.gif" /></div>
                        <div id="menu" onclick="return menu_onclick()" style="height:26px;">
                            <table>
                                <tr>
                                    <td><a id="home" class="mainlevel-trans" href="#">HOME</a></td>
                                    <td><a id="profile" class="mainlevel-trans" href="#">PROFILE</a></td>
                                    <td><a id="academics" class="mainlevel-trans" href="#">ACADEMICS</a></td>
                                    <td><a id="user_guid" class="mainlevel-trans" href="#">USER GUID</a></td>
                                    <td><a id="course" class="mainlevel-trans" href="#">COURSE</a></td>
                                    <td><a id="accounts" class="mainlevel-trans" href="#">FINANCE</a></td>
                                   
                                    <td><a id="application_forms" class="mainlevel-trans" href="StudentForms/_StudentAppForms.aspx" style="color: #000000;">Application Forms</a></td>
                                    <td><a id="library" class="mainlevel-trans" href="#">LIBRARY</a></td>
                                    <td><a id="logout" class="mainlevel-trans" href="_login.aspx" style="color: #000000;">LOGOUT</a></td>
                                </tr>
                            </table>
                            </div>
                        <div><img src="../images1/orange-line.gif" /></div>
                        <script type="text/javascript" language="javascript">

                                function menu_onclick() {}
                                
                                if (TransMenu.isSupported()) 
                                {                                   
                                    var ms = new TransMenuSet(TransMenu.direction.down, 1, 0, TransMenu.reference.bottomLeft);
    
	                                var menu0 = ms.addMenu(document.getElementById("home"));	                                
	                                menu0.addItem("EU Home","http://www.easternuni.edu.bd");
	                                 
                                	
                                    var menu1 = ms.addMenu(document.getElementById("profile"));
                                    menu1.addItem("Change Password", "profile/_changePassword.aspx");
                                    menu1.addItem("Profile", "profile/_profile.aspx");
                                    menu1.addItem("Upload Image", "profile/_uploadPicture.aspx"); 
	                                
                                    //================================================================================================
                                    var menu2 = ms.addMenu(document.getElementById("user_guid"));
                                    menu2.addItem("Web-portal (Student)", "userGuid/_userGuidWeb.aspx");
                                    menu2.addItem("Library", "userGuid/_userGuidLibrary.aspx");
                                    menu2.addItem("Laboratory", "userGuid/_userGuidLab.aspx"); 
                                    

                                    //================================================================================================
                                    var menu3 = ms.addMenu(document.getElementById("academics"));
                                    menu3.addItem("Semester Grade Sheet","academics/_semGradeSheet.aspx");
                                    menu3.addItem("Completed Cources","academics/_completedCourses.aspx");
                                    menu3.addItem("Academic Status","academics/_academic_status.aspx"); 
                                     menu3.addItem("Academic Calender","academics/_academicCalender.aspx"); 
                                    
                                    //================================================================================================
                                    var menu4 = ms.addMenu(document.getElementById("course"));
                                    menu4.addItem("Taken Courses", "course/_takenCourses.aspx");
                                    menu4.addItem("Course Offering", "course/_courseOffering.aspx");
                                    menu4.addItem("Course Advisor", "course/_course_advisor.aspx"); 
                                     menu4.addItem("Admit Card", "AdmitCard/_AdmitCard.aspx"); 
                                     menu4.addItem("Attendance Report", "course/_studentAttendance.aspx");

                                    //================================================================================================
                                    var menu5 = ms.addMenu(document.getElementById("accounts"));
                                    menu5.addItem("Student Ledger", "finance/_studentLedger.aspx");
                                    
                                    //================================================================================================
                                    var menu6 = ms.addMenu(document.getElementById("library"));
                                    menu6.addItem("Book Search", "library/_student_search_book.aspx");
                                   //menu6.addItem("Book request", "library/_student_book_requisition.aspx");  
                                    
	                                
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
                              </script> 

                        </td>
                </tr> 
               </tbody>
            </table>
        </div>   
    <div style="height:12px;">&nbsp;</div>
    <div>
      <table class="table_main_table" border="0" id="AutoNumber1" cellpadding="0px" cellspacing="0px" >
      <tr>  
          <td style=" width:100%; vertical-align:top; background: url(../images/bg.gif) inactivecaptiontext;"> 
             
            <div style="float:left; width:25%; height:100%;"> 
                <strong><span style="font-size: 11pt; color: #0000ff"></span></strong>
              <table>
                 <tr>
                  <td>                  
                      <strong><span style="font-size: 8pt; color: #0000ff">Student Login</span></strong></td>
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
                              <font color="#ffa500"><b>Quick Links</b></font></p>
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
                          <div align="center" style="width: 98%; padding-top: 5px; height: auto; background-color: #f9f9f9;
                              text-align: left">
                              <strong></strong>
                              <table id="table97" border="0" width="100%">
                                  <tbody>
                                      <tr>
                                          <td style="height: 17px" width="9%">
                                              <img border="0" height="14" src="../images/arrow3.gif" width="5" /></td>
                                          <td style="border-bottom: #eeeeef 2px dotted; height: 17px; text-align: left" width="140">
                                              <a href="course/_takenCourses.aspx">Taken Courses</a></td>
                                      </tr>
                                      <tr style="color: #0000ff; text-decoration: underline">
                                          <td style="height: auto" width="9%">
                                              <img border="0" height="14" src="../images/arrow3.gif" width="5" /></td>
                                          <td style="border-bottom: #eeeeef 2px dotted; height: auto; text-align: left" width="140">
                                          <a href="academics/_semGradeSheet.aspx">Grade Sheet</a></td>
                                      </tr>
                                      <tr style="color: #0000ff; text-decoration: underline">
                                          <td style="height: auto" width="9%">
                                              <img border="0" height="14" src="../images/arrow3.gif" width="5" /></td>
                                          <td style="border-bottom: #eeeeef 2px dotted; height: auto; text-align: left" width="140">
                                             <a href="finance/_studentLedger.aspx">My Account Ledger</a></td>
                                              
                                      </tr>
                                      <tr>
                                          <td style="height: auto" width="9%">
                                              <img border="0" height="14" src="../images/arrow3.gif" width="6" /></td>
                                          <td style="border-bottom: #eeeeef 2px dotted; height: auto; text-align: left" width="140">
                                              <a href="library/_student_search_book.aspx"> Book Search</a></td>
                                      </tr>
                                      <tr style="color: #0000ff; text-decoration: underline">
                                          <td style="height: auto" width="9%">
                                              <img border="0" height="14" src="../images/arrow3.gif" width="5" /></td>
                                          <td style="border-bottom: #eeeeef 2px dotted; height: auto; text-align: left" width="140">
                                              <a href="profile/_profile.aspx">My Profile</a></td>
                                      </tr>
                                      <tr style="color: #0000ff; text-decoration: underline">
                                          <td style="height: auto" width="9%">
                                              <img border="0" height="14" src="../images/arrow3.gif" width="5" /></td>
                                          <td style="border-bottom: #eeeeef 2px dotted; height: auto; text-align: left" width="140">
                                              <a href="course/_course_advisor.aspx">Course Advisor</a></td>
                                      </tr>
                                       <tr style="color: #0000ff; text-decoration: underline">
                                          <td style="height: auto" width="9%">
                                              <img border="0" height="14" src="../images/arrow3.gif" width="5" /></td>
                                          <td style="border-bottom: #eeeeef 2px dotted; height: auto; text-align: left" width="140">
                                             <a href="finance/_OnlineFinance.aspx">Online Payment</a>
                                              </td>
                                      </tr>
                                  </tbody>
                              </table>
                          </div>
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
               </tr>
                  <tr>
                      <td>
                          &nbsp;</td>
                  </tr>
              </table>              &nbsp;<asp:Calendar ID="Calendar1" runat="server" Width="125px">
                    <TodayDayStyle BackColor="InactiveCaption" ForeColor="DarkRed" />
                    <TitleStyle BackColor="InactiveCaption" Font-Bold="True" ForeColor="DarkRed" />
                </asp:Calendar>
                <br /><br />
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
                              <a  style="font-weight:bold; color:#000099 " href="notice/DMP.jpg"><b>DMP Instruction</b></a>
                              </p>
                      </td>
                  </tr>
                  <tr>
                      <td bgcolor="#eef5fa" class="h" height="22" width="228">
                          <p align="center">

                              <font color="#ffa500"><b>MESSAGE FOR YOU</b></font></p>
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
                          <div align="center" style="width: 98%; padding-top: 5px; height: auto; background-color: #f9f9f9;
                              text-align: left">
                              <strong></strong>
                              <asp:GridView ID="grdStdMsg" runat="server" AutoGenerateColumns="False" ShowHeader="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" >
                                           <Columns>
                                               <asp:TemplateField> 
                                                   <ItemTemplate>
                                                       <asp:Image ID="Image1"  ImageUrl="~/student/images/link_icons.png" runat="server" />
                                                   </ItemTemplate>
                                               </asp:TemplateField>
                                               <asp:HyperLinkField DataNavigateUrlFields="Message_ID" DataNavigateUrlFormatString="~/student/message/_message.aspx?code={0}"
                                                   DataTextField="TITLE" NavigateUrl="~/student/message/_message.aspx" Target="_blank" >
                                                   <ItemStyle Font-Bold="False" />
                                               </asp:HyperLinkField>
                                           </Columns>
                                            <AlternatingRowStyle BackColor="White" BorderColor="Silver" BorderStyle="Inset"
                                                BorderWidth="1px" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#E3EAEB" />
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                       </asp:GridView>
                          </div>
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
                
                      </div>               
             <div id="right_div" style="float:right; width:25%; height:100%; background-color: inactivecaption;">                   
                 <br />
                 <asp:Image ID="img_myPicture" runat="server" Height="136px" Width="130px" /><br />
                 <asp:Label ID="lbl_login" runat="server" Font-Bold="True" ForeColor="White" Text="Label"></asp:Label><br />
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
                                             <font color="#ffa500"><b>Go to</b></font></p>
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
                                         <img src="../student/images/link_icons.png" /><a href="../student/academics/_semGradeSheet.aspx">Semester
                                             Grader Sheet</a><br />
                                         <img src="../student/images/link_icons.png" /><a href="../student/academics/_completedCourses.aspx">Completed
                                             Courses</a><br />
                                         <img src="../student/images/link_icons.png" /><a href="../student/academics/_academic_status.aspx">Academic
                                             Status</a><br />
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
                               <strong><span style="font-size: 11pt; color: #ff0000"><span style="font-size: 10pt; color: blue;">
                                   &nbsp;</span></span></strong></td>
                       </tr>
                       <tr>
                           <td>
                               <strong><span style="font-size: 10pt; color: #0000ff">
                                   Welcome </span></strong>
                                   <asp:Label ID="lbl_name" runat="server" ForeColor="Red" Text="Label" Font-Bold="True" Font-Size="Small"></asp:Label></td>
                       </tr>
                       <tr>
                           <td style="">
                               <span ><span><strong><span style="color: #ff0066"><span style="color: blue; font-size: 10pt;">
                                   &nbsp;&nbsp; &nbsp; to Eastern Family</span></span></strong></span></span></td>
                       </tr>
                       <tr>
                           <td style="">
                               &nbsp; &nbsp; <strong><span style="font-size: 10pt; color: #0000ff"></span></strong>
                           </td>
                       </tr>
                       <tr>
                           <td style="TEXT-ALIGN: justify; height: 41px;">
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
                                <td style="width:50px"></td>
                                <td><img id="img" alt="Academic Calender" src="../images/arrow3.gif" /></td>
                                <td>
                                    <asp:HyperLink ID="hplink_classRoutine" runat="server" Font-Bold="True" NavigateUrl="#">Class Routine</asp:HyperLink></td>
                           </tr>
                           <tr>
                                <td style="width:50px"></td>
                                <td><img id="img1" alt="Academic Calender" src="../images/arrow3.gif" /></td>
                                <td><a  style="font-weight:bold " href="academics/_academicCalender.aspx">Academic Calender</a></td>
                           </tr>
                               <tr>
                                   <td style="width: 50px">
                                   </td>
                                   <td>
                                   </td>
                                   <td>
                                      
                                       <asp:Panel ID="pnlEngStudents" runat="server" Visible="false">
                                           <img id="img2" alt="Academic Calender" src="../images/arrow3.gif" />
                                           <a  style="font-weight:bold " href="StudentForms/_TransportRoute.aspx">Transformation Service Related Information Entry</a>
                                       </asp:Panel>



                                   </td>
                               </tr>
                           
                           </table>
                           </td>
                       </tr>
                   </table>
               </div>
              
               <div id="BottomRight" style="float:right; text-align:left; background-color:White; width:73.6%; height:100%;">                   
                   <table style="width:100%">
                       <tr>
                           <td style="vertical-align:top">
                               &nbsp;</td>
                           <td style="text-align:right; vertical-align:top">
                               &nbsp;</td>
                       </tr>
                       <!--<tr>
                           <td style="vertical-align:top" colspan="2">


                                 <div id="Div1" style=" margin-left:25px; margin-top:15px;text-align: center;">

                               <iframe width="420"    height="205"  src="https://www.youtube.com/embed/4zgnP0KMEBU" frameborder="0" allowfullscreen></iframe>

                                   </div>


                           </td>
                       </tr>-->
                       <tr>
                           <td style="vertical-align:top" colspan="2">


                                 &nbsp;</td>
                       </tr>
                       <tr>
                           <td style="vertical-align:top">
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
                                               <font color="#ffa500"><b>General Notice Board</b></font></p>
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
                                        <asp:GridView ID="GridView_generalNotice" runat="server" AutoGenerateColumns="False" ShowHeader="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" >
                                           <Columns>
                                               <asp:TemplateField> 
                                                   <ItemTemplate>
                                                       <asp:Image ID="Image1"  ImageUrl="~/student/images/link_icons.png" runat="server" />
                                                   </ItemTemplate>
                                               </asp:TemplateField>
                                               <asp:HyperLinkField DataNavigateUrlFields="NOTICE_ID" DataNavigateUrlFormatString="~/student/notice/_notice.aspx?code={0}"
                                                   DataTextField="TITLE" NavigateUrl="~/student/notice/_notice.aspx" Target="_blank" >
                                                   <ItemStyle Font-Bold="False" />
                                               </asp:HyperLinkField>
                                           </Columns>
                                            <AlternatingRowStyle BackColor="White" BorderColor="Silver" BorderStyle="Inset"
                                                BorderWidth="1px" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#E3EAEB" />
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                       </asp:GridView>
                                           <asp:HyperLink ID="more_genNotice" runat="server" Width="41px" Font-Underline="False" NavigateUrl="~/student/notice/_generalNotice.aspx" ForeColor="Blue">more....</asp:HyperLink></td>
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
                           <td style="text-align:right; vertical-align:top">
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
                                           <font color="#ffa500"><b>Student Notice Board</b></font></p>
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
                                       <asp:GridView ID="GridView_studentNotice" runat="server" AutoGenerateColumns="False" ShowHeader="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" >
                                           <Columns>
                                               <asp:TemplateField>                                                   
                                                   <ItemTemplate>
                                                       <asp:Image ID="Image1" ImageUrl="~/student/images/link_icons.png" runat="server" />
                                                   </ItemTemplate>
                                               </asp:TemplateField>
                                               <asp:HyperLinkField DataNavigateUrlFields="NOTICE_ID" DataNavigateUrlFormatString="~/student/notice/_notice.aspx?code={0}"
                                                   DataTextField="TITLE" NavigateUrl="~/student/notice/_notice.aspx" Target="_blank" >
                                                   <ItemStyle Font-Bold="False" />
                                               </asp:HyperLinkField>
                                           </Columns>
                                           <AlternatingRowStyle BackColor="White" BorderColor="Gray" BorderStyle="Outset"
                                               BorderWidth="1px" />
                                           <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                           <RowStyle BackColor="#E3EAEB" />
                                           <EditRowStyle BackColor="#7C6F57" />
                                           <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                           <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                           <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                       </asp:GridView>
                                       <asp:HyperLink ID="student_notice" runat="server" Font-Underline="False" NavigateUrl="~/student/notice/_studentNotice.aspx"
                                           Width="29px" ForeColor="Blue">more....</asp:HyperLink></td>
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
        <td style="background-color:inactivecaption;">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="footertext" valign="top" style="height: 24px">
                        <span style="color: #ffffff">
                        Main Campus:</span></td>
                    <td class="footertext" style="width: 483px; height: 24px;" valign="top">
                        <span style="color: #ffffff">
                        House#26, Road 5, Dhanmondi, Dhaka, Bangladesh.Tel: +880.2.9676031-5
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
    </td></tr>
    </table>
</form>
</body>
</html>

