<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_OnlinePayment_Prev.aspx.cs" 
Inherits="student_finance_OnlinePayment_Prev" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <title>Eastern University</title>
    
     <link href="../App_themes/jind.css" rel="stylesheet" type="text/css"/>
    <link  href="../App_themes/transmenuh.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../App_themes/transmenu.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
     <center>	
  
      <div id="mastheadInner" style="width: 780px; height: 120px; " align="center">
            <table class="MainHeader" id="tblHeader" cellspacing="0" cellpadding="0" width="100%" align="right" border="0" runat="server">
               <tbody>
               <tr> 
                    <td valign="top" align="center" style="height: 5px;">
                        </td>
                </tr>
                <tr> 
                    <td valign="top" align="center" style="height: 12px; background-color:#99ccff">
                        <img src="../../images1/topimage_student.jpg" /></td>
                </tr>
               </tbody>
            </table>
          </div>   
           
           
           <div id="Div2" class="div_menu_top">
            <table  id="Table2" style="background:url(../../images/bg.gif) #eeeeef; text-align:left;" class="MainHeader" cellspacing="0" cellpadding="0" width="100%" align="right" border="0" runat="server">
               <tbody>
                <tr> 
                     <td style="height: 30px; background-color: inactivecaption;" >
                        <div id="menu" onclick="return menu_onclick()" style="height:26px;">
                         
                            <img src="../../images1/orange-line.gif" /><br />
                            <table>
                                <tr>
                                    <td><a id="home" class="mainlevel-trans" href="">HOME</a></td>
                                    <td><a id="profile" class="mainlevel-trans" href="#">PROFILE</a></td>
                                    <td><a id="user_guid" class="mainlevel-trans" href="#">USER GUID</a></td>
                                    <td><a id="academics" class="mainlevel-trans" href="#">ACADEMICS</a></td>
                                    <td><a id="course" class="mainlevel-trans" href="#">COURSE</a></td>
                                    <td><a id="finance" class="mainlevel-trans" href="../finance/_OnlineFinance.aspx" style="color: #000000;">Online Finance</a></td>
                                     <td><a id="application_forms" class="mainlevel-trans" href="../StudentForms/_StudentAppForms.aspx" style="color: #000000;">Application Forms</a></td>
                                   
                                    <td><a id="library" class="mainlevel-trans" href="#">LIBRARY</a></td>                                   
                                    <td><a id="logout" class="mainlevel-trans" href="../_login.aspx">LOGOUT</a></td>
                                </tr>
                            </table>
                        </div>
                        
                        <script type="text/javascript" language="javascript">

                                function menu_onclick() {}
                                
                                if (TransMenu.isSupported()) 
                                {                                   
                                    var ms = new TransMenuSet(TransMenu.direction.down, 1, 0, TransMenu.reference.bottomLeft);
    
	                                var menu0 = ms.addMenu(document.getElementById("home"));
	                                menu0.addItem("Student Home","../_home.aspx");
	                                menu0.addItem("EU Home","../../Default.aspx");
	                                 
                                	
                                    var menu1 = ms.addMenu(document.getElementById("profile"));
                                    menu1.addItem("Change Password", "../profile/_changePassword.aspx");
                                    menu1.addItem("Profile", "../profile/_profile.aspx");
                                    menu1.addItem("Upload Image", "../profile/_uploadPicturet.aspx"); 
	                                
                                    //================================================================================================
                                    var menu2 = ms.addMenu(document.getElementById("user_guid"));
                                    menu2.addItem("Web-portal (Student)", "../userGuid/_userGuidWeb.aspx");
                                    menu2.addItem("Library", "../userGuid/_userGuidLibrary.aspx");
                                    menu2.addItem("Laboratory", "../userGuid/_userGuidLab.aspx"); 
                                    

                                    //================================================================================================
                                    var menu3 = ms.addMenu(document.getElementById("academics"));
                                    menu3.addItem("Semester Grade Sheet","../academics/_semGradeSheet.aspx");
                                    menu3.addItem("Completed Cources","../academics/_completedCourses.aspx");
                                    menu3.addItem("Academic Status","../academics/_academic_status.aspx"); 
                                     menu3.addItem("Academic Calender","../academics/_academicCalender.aspx"); 
                                    
                                    //================================================================================================
                                    var menu4 = ms.addMenu(document.getElementById("course"));
                                   menu4.addItem("Taken Courses", "../course/_takenCourses.aspx");
                                    menu4.addItem("Course Offering", "../course/_courseOffering.aspx");
                                    menu4.addItem("Course Advisor", "../course/_course_advisor.aspx"); 
                                    
                                    
                                    //================================================================================================
//                                    var menu5 = ms.addMenu(document.getElementById("accounts"));
//                                    menu5.addItem("Student Ledger", "#");
                                    
                                    //================================================================================================
                                    var menu6 = ms.addMenu(document.getElementById("library"));
                                    menu6.addItem("Book Search", "../library/_student_search_book.aspx");
                                  //  menu6.addItem("Book request", "../library/_student_book_requisition.aspx"); 
	                                
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

                        <img src="../../images1/orange-line.gif" /></td>
                </tr> 
               </tbody>
            </table>
        </div>
    <div>
      <asp:Panel ID="pnlStudent" runat="server" Visible="false">
    <table border="0" id="TABLE1" runat="server" width="540">
        <tr>
            <td colspan="3" style="text-align: left">
                
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="539" id="tbl_offered_courses" runat="server">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19" id="TD1" runat="server">
                            <img border="0" height="24" src="../images/lcurv.gif" width="19" /></td>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../images/rcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" height="22" width="505">
                            <p align="center">
                                <b><font color="#ffa500" face="Arial" size="2">Online Payment</font></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" height="114" width="18">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="18" /></td>
                        <td bgcolor="#ffffff" height="114" width="505" style="text-align: left">
                            <div style="text-align: left">
                                &nbsp;</div>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="height: 20px">
                                        </td>
                                    <td style="height: 20px">
                                        </td>
                                    <td style="height: 20px; width: 84px">
                                      <b>Student ID</b>  
    
    </td>
                                    <td style="height: 20px">
    <b>:</b>
    </td>
                                    <td style="height: 20px">
                                        <asp:Label ID="lblStdID" runat="server" Text="Label"></asp:Label>
                                        
                                       
                                        
    
    </td>
                                </tr>
                                <tr>
                                    <td style="height: 21px">
                                        </td>
                                    <td style="height: 21px">
                                        </td>
                                    <td style="height: 21px; width: 84px">
                                        <b>Year</b>
                                            
    </td>
                                    <td style="height: 21px">
     <b>:</b>
    </td>
                                    <td style="height: 21px">
                                        &nbsp;<asp:Label ID="lblYear" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="height: 21px">
                                        </td>
                                    <td style="height: 21px">
                                        </td>
                                    <td style="height: 21px; width: 84px">
                                        <b>Semester</b></td>
                                    <td style="height: 21px">
     <b>:</b>
    </td>
                                    <td style="height: 21px">
                                        &nbsp;<asp:Label ID="lblSem" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                               
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="color: #0000ff" colspan="3">
                                   
                                   
                                   
                                   
<asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="TRANID"  Font-Names="Palatino Linotype" Font-Size="12pt" 
                     CellSpacing="8" GridLines="None">
                    <Columns>
                    
<asp:TemplateField Visible="false">                         
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Bind("TRANID") %>' ID="lblHEADSN"></asp:Label>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="left" />
    <HeaderStyle HorizontalAlign="Center" />
     <HeaderStyle ForeColor="#804000" />
 </asp:TemplateField>  
     <asp:BoundField DataField="HEADNAME" HeaderText="Particulars">
        <HeaderStyle HorizontalAlign="left" />
        <ItemStyle Width="150px" />
        </asp:BoundField>

        <asp:BoundField DataField="AMOUNT" HeaderText="Amount" >
        <ItemStyle HorizontalAlign="left" />
        <ItemStyle Width="100px" />
        </asp:BoundField>
        <%--   <asp:TemplateField HeaderText="Particulars" SortExpression="Particulars">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("HEADNAME") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("HEADNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle ForeColor="#804000" />
                        </asp:TemplateField>
                      
                       
                        <asp:TemplateField HeaderText="Amount" SortExpression="Amount">
                            <EditItemTemplate>
                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>' ReadOnly="true"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="Amount" runat="server" Text='<%# Bind("AMOUNT") %>' Width="80px" ReadOnly="true"></asp:TextBox>
                            </ItemTemplate>
                            
                            <HeaderStyle ForeColor="#804000" />
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
                 <div style="margin-left:100px;">
                                        <asp:Label ID="lblMsg" runat="server" Text="Total Amount: "></asp:Label>       <asp:Label ID="lblTotalAmount" runat="server" Text="" ></asp:Label>
                    </div>  
                      <br />                 
<asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text="To Pay Your Payment with Online Payment, Press 'Pay Now'."></asp:Label>
                                        
   <input type="submit" id="ssl_pay" name="submit" value="Pay Now" runat="server" style="margin-left:50px;"/>              
                                       </td>
                                </tr>
                                   
                  
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="text-align: right" colspan="3">
                                            <asp:Label ID="lbl_total_credit" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="3">
                                         
                                        </td>
                                </tr>
                            </table>
                            &nbsp;
                            <br />
                            <br />
                            
                        </td>
                        <td bgcolor="white" height="114" width="14">
                            &nbsp;
                            <p>
                                <img border="0" height="1" src="../images/spacer(1).gif" width="14" /></p>
                        </td>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                            <img border="0" height="15" src="../images/blcurv.gif" width="19" /></td>
                        <td bgcolor="white" height="1" width="505">
                            <img border="0" height="14" src="../images/spacer(1).gif" width="1" /></td>
                        <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                            <img border="0" height="15" src="../images/brcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="150" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </asp:Panel>
              
              
    </div>
    
    </center>
    </form>
    
    
   <form id="payment_gw" name="payment_gw" >


<input type="hidden" name="total_amount" value="1150.00" id="total_amount" runat="server" />
    <input type="hidden" name="store_id" value="easternuni001live"  />
    <input type="hidden" name="tran_id" value="578e05a14579e" />
    
    <input type="hidden" name="success_url" value="http://old.easternuni.edu.bd:9090/icampusnew/paySuccessListener" />
    <input type="hidden" name="fail_url" value="http://old.easternuni.edu.bd:9090/icampusnew/payFailureListener" />
    <input type="hidden" name="cancel_url" value="http://old.easternuni.edu.bd:9090/icampusnew/payCancelListener" />
    
    <input type="hidden" name="version" value="3.00" />

    <!-- Customer Information !-->
    <input type="hidden" name="cus_name" value="ABC XYZ">
    <input type="hidden" name="cus_email" value="abc.xyz@mail.com">
    <input type="hidden" name="cus_add1" value="Address Line One">
    <input type="hidden" name="cus_add2" value="Address Line Two">
    <input type="hidden" name="cus_city" value="City Name">
    <input type="hidden" name="cus_state" value="State Name">
    <input type="hidden" name="cus_postcode" value="Post Code">
    <input type="hidden" name="cus_country" value="Country">
    <input type="hidden" name="cus_phone" value="01111111111">
    <input type="hidden" name="cus_fax" value="01711111111">

    <!-- Shipping Information !-->
    <input type="hidden" name="ship_name" value="ABC XYZ">
    <input type="hidden" name="ship_add1" value="Address Line One">
    <input type="hidden" name="ship_add2" value="Address Line Two">
    <input type="hidden" name="ship_city" value="City Name">
    <input type="hidden" name="ship_state" value="State Name">
    <input type="hidden" name="ship_postcode" value="Post Code">
    <input type="hidden" name="ship_country" value="Country">

    <!-- Optional Parameters which will be stored and returned at the end !-->
    <input type="hidden" id="StudentID" runat="server" name="value_a" value="StudentID">   <!-- Student ID !-->
    <input type="hidden" id="Hidden1" runat="server" name="value_b" value="Year2016">   <!-- Payable Year !-->
    <input type="hidden" id="lblSemister" runat="server" name="value_c" value="Fall">   <!-- Payable Semester !-->
    <input type="hidden" id="StdName" runat="server" name="value_d" value="StudentName">   <!-- Student Name !-->

    <!-- SUBMIT REQUEST  !-->
    <input type="button" id="Button1" name="submit" value="Pay Now" runat="server"
                                           onclick="btn_Submit_Click" />
   <!-- <input type="submit" id="ssl_pay" name="submit" value="Pay Now" runat="server" />-->
</form>




<script>
    Shaz = window.Shaz || function () {
                (Shaz.q = Shaz.q || []).push(arguments);
            };
    (function () {;
        var s = document.createElement('script');
        s.type = 'text/javascript';
        s.async = true;
        s.src = 'https://easy.com.bd/widget_library/widget-eu.js';
        var x = document.getElementsByTagName('script')[0];
        x.parentNode.insertBefore(s, x);
    })();
    Shaz('appkey', 'EASTERN-UNI');
</script>
</body>
</html>
