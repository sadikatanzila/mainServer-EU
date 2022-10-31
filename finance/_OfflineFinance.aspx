<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_OfflineFinance.aspx.cs" 
Inherits="student_finance_OfflineFinance" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Eastern University</title>
    
     <link href="../App_themes/jind.css" rel="stylesheet" type="text/css"/>
    <link  href="../App_themes/transmenuh.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../App_themes/transmenu.js" ></script>
    
<script type="text/javascript">
    function doAlert() {
        document.getElementById("demo").innerHTML = "Hello World";
        return true;

    }

</script>

    <style type="text/css">
        .style112
        {
            width: 17px;
        }
        .style114
        {}
        .style115
        {
        }
        .style116
        {
            width: 11px;
        }
        .style117
        {
        }
        .auto-style1 {
        }
        .auto-style2 {
            width: 9px;
        }
        </style>

</head>
<body>
    



<script type="text/javascript">
    function GetClientId(strid) {
        var count = document.forms[0].length;
        var i = 0;
        var eleName;
        for (i = 0 ; i < count ; i++) {
            eleName = document.forms[0].elements[i].id;
            pos = eleName.indexOf(strid);
            if (pos >= 0) break;
        }
        return eleName;
    }

    function GetTotal(lTotal_id) {
        document.getElementById(GetClientId("AmountTotal")).value = 0;
        var obj_lTotal = document.getElementById(lTotal_id);
        if (obj_lTotal.value != "" && obj_lTotal.value != "") {

            obj_lTotal.value = parseInt(obj_lTotal.value);
        }

        var txtTotal = 0;
        var passed = false;
        var id = 0;
        totalDTH = 0;
        totalMCF = 0;

        // Get the gridview
        var grid = document.getElementById("<%= GridView4.ClientID%>");

     // Get all the input controls (can be any DOM element you would like)
    var inputs = grid.getElementsByTagName("input");

     // Loop through all the DOM elements we grabbed
    for (var i = 0; i < inputs.length; i++) {

        // In this case we are looping through all the Dek Volume and then the Mcf volume boxes in the grid and not an individual one and totalling them
        if (inputs[i].name.indexOf("Amount") > 1) {
            if (inputs[i].value != "" || inputs[i].value != 0) {

                totalDTH = totalDTH + parseInt(inputs[i].value);
                //  document.getElementById(GetClientId("Amount")).value = parseInt(inputs[i].value);

            }
        }
    }
    document.getElementById(GetClientId("AmountTotal")).value = totalDTH;

    return false;
}



function GrandTotal() {
    document.getElementById(GetClientId("AmountTotal")).value = 0;


    var txtTotal = 0;
    var passed = false;
    var id = 0;
    totalDTH = 0;
    totalMCF = 0;

    // Get the gridview
    var grid = document.getElementById("<%= GridView4.ClientID%>");

        // Get all the input controls (can be any DOM element you would like)
    var inputs = grid.getElementsByTagName("input");

        // Loop through all the DOM elements we grabbed
    for (var i = 0; i < inputs.length; i++) {

        // In this case we are looping through all the Dek Volume and then the Mcf volume boxes in the grid and not an individual one and totalling them
        if (inputs[i].name.indexOf("Amount") > 1) {
            if (inputs[i].value != "" || inputs[i].value != 0) {
                totalDTH = totalDTH + parseInt(inputs[i].value);
            }
        }
    }
    document.getElementById(GetClientId("AmountTotal")).value = totalDTH;
    return false;
}

</script>
<script type="text/javascript">
    document.getElementById(GetClientId("total_amount")).value
</script>

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
                                    
                                     <td><a id="application_forms" class="mainlevel-trans" href="../StudentForms/_StudentAppForms.aspx" style="color: #000000;">Application Forms</a></td>
                                   
                                    <td><a id="library" class="mainlevel-trans" href="#">LIBRARY</a></td>                                   
                                    <td><a id="logout" class="mainlevel-trans" href="../_login.aspx">LOGOUT</a></td>
                                </tr>
                            </table>
                        </div>
                        
                        <script type="text/javascript" language="javascript">

                            function menu_onclick() { }

                            if (TransMenu.isSupported()) {
                                var ms = new TransMenuSet(TransMenu.direction.down, 1, 0, TransMenu.reference.bottomLeft);

                                var menu0 = ms.addMenu(document.getElementById("home"));
                                menu0.addItem("Student Home", "../_home.aspx");
                                menu0.addItem("EU Home", "../../Default.aspx");


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
                                menu3.addItem("Semester Grade Sheet", "../academics/_semGradeSheet.aspx");
                                menu3.addItem("Completed Cources", "../academics/_completedCourses.aspx");
                                menu3.addItem("Academic Status", "../academics/_academic_status.aspx");
                                menu3.addItem("Academic Calender", "../academics/_academicCalender.aspx");

                                //================================================================================================
                                var menu4 = ms.addMenu(document.getElementById("course"));
                                menu4.addItem("Taken Courses", "../course/_takenCourses.aspx");
                                menu4.addItem("Course Offering", "../course/_courseOffering.aspx");
                                menu4.addItem("Course Advisor", "../course/_course_advisor.aspx");
                                menu4.addItem("Admit Card", "../AdmitCard/_AdmitCard.aspx");
                                menu4.addItem("Attendance Report", "course/_studentAttendance.aspx");

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
                            init1 = function () { TransMenu.initialize(); }
                            if (window.attachEvent) {
                                window.attachEvent("onload", init1);
                            } else {
                                TransMenu.initialize();
                            }
                              </script> 

                        <img src="../../images1/orange-line.gif" /></td>
                </tr> 
               </tbody>
            </table>
        </div>
        
        <div >
      <table class="table_main_table" border="5px" id="AutoNumber1" cellpadding="0px" cellspacing="0px" >
      <tr>  
          <td style=" width:100%; vertical-align:top; background:url(../images/bg.gif) inactivecaptiontext;  "> 
             
            <div   style="float:left; width:24%;  height:100%;"> 
              <table>
                 <tr>
                  <td> 
              
                  <table border="0" style=" float:left;" cellpadding="0" cellspacing="0" height="1" width="162">
                      <tr>
                          <td colspan="2" height="24" rowspan="3" width="19">
                              <img border="0" height="24" src="../images/lcurv.gif"
                                          width="19" /></td>
                          <td bgcolor="#6fb1d9" class="k" height="1" style="width: 228px">
                              <img border="0" height="1" src="../images/spacer(1).gif"
                                          width="100%" /></td>
                          <td align="right" colspan="2" height="24" rowspan="3" width="15">
                              <img border="0" height="24" src="../images/rcurv.gif"
                                          width="15" /></td>
                      </tr>
                      <tr>
                          <td bgcolor="#eef5fa" class="h" height="22" style="width: 228px">
                              <p align="center">
                                  <font color="#ffa500"><b>Finance</b></font></p>
                          </td>
                      </tr>
                      <tr>
                          <td bgcolor="#6fb1d9" class="k" height="1" style="width: 228px">
                              <img border="0" height="1" src="../images/spacer(1).gif"
                                          width="1" /></td>
                      </tr>
                      <tr>
                          <td bgcolor="#6fb1d9" class="k" height="63" width="1">
                              <img border="0" height="1" src="../images/spacer(1).gif"
                                          width="1" /></td>
                          <td bgcolor="white" height="63" width="18">
                              <img border="0" height="1" src="../images/spacer(1).gif"
                                          width="18" /></td>
                          <td align="left" bgcolor="#ffffff" height="63" valign="top" style="width: 228px">
                              <br />
                              &gt;<a href="_studentLedger.aspx">Student Ledger</a><br />
                              <br />
                              &gt;<a href="_OfflineFinance.aspx">Offline Payment</a><br />
                              </td>
                          <td bgcolor="white" height="63" width="14">
                              &nbsp;
                              <p>
                                  <img border="0" height="1" src="../images/spacer(1).gif"
                                              width="14" /></p>
                          </td>
                          <td bgcolor="#6fb1d9" class="k" height="63" width="1">
                              <img border="0" height="1" src="../images/spacer(1).gif"
                                          width="1" /></td>
                      </tr>
                      <tr>
                          <td align="left" colspan="2" height="1" rowspan="2" valign="top" width="19">
                              <img border="0" height="15" src="../images/blcurv.gif"
                                          width="19" /></td>
                          <td bgcolor="white" height="1" style="width: 228px">
                              <img border="0" height="14" src="../images/spacer(1).gif"
                                          width="1" /></td>
                          <td align="right" colspan="2" height="1" rowspan="2" valign="top" width="15">
                              <img border="0" height="15" src="../images/brcurv.gif"
                                          width="15" /></td>
                      </tr>
                      <tr>
                          <td bgcolor="#6fb1d9" class="k" height="1" style="width: 228px">
                              <img border="0" height="1" src="../images/spacer(1).gif"
                                          width="150" /></td>
                      </tr>
                  </table>
                  
                    </td>
                      </tr>
                  
                    
                  
                  
                  
                  <tr>
                  <td>
                  
              
                </td>
                  </tr>
              </table>
              
              </div>     
              
              
  <asp:Panel ID="pnlStudent" runat="server" >
    <table border="0" id="TABLE1" runat="server" width="74%">
        <tr>
            <td colspan="3" style="text-align: left">
                
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <table border="0" cellpadding="0" cellspacing="0" height="1" width="98%" 
                    id="tbl_offered_courses" runat="server" style="background-color: #FFFFFF">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19">
                           </td>
                        <td  width="505">
                            </td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            </td>
                    </tr>
                    <tr>
                        <td  width="505">
                            <p align="center">
                                <b><font color="#ffa500" face="Arial" size="2">Student Offline Payment</font></b></p>
                                <hr />
                        </td>
                    </tr>
                    <tr>
                        <td width="505">
                           </td>
                    </tr>
                    <tr>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="1" /></td>
                        <td bgcolor="white" height="114" width="18">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="18" /></td>
                        <td bgcolor="#ffffff" height="114" width="505" style="text-align: left">

                             <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                ForeColor="Red" HeaderText="Page Errors" ShowMessageBox="True"
                ShowSummary="true" DisplayMode="List" />

                        <asp:Panel ID="pnlCheckInfo" runat="server" >    
                           <table  border="0" cellpadding="0" cellspacing="0" width="90%">
                           <tr>
                           <td class="style115" colspan="3">
                           
                            Check your Student related info. If blank Update the necessary info below:
                              
                              </td>
                           </tr>
                               <tr>
                                   <td class="style117">
                                       &nbsp;</td>
                                   <td class="style116">
                                       &nbsp;</td>
                                   <td>
                                       &nbsp;</td>
                               </tr>
                               <tr>
                                   <td class="style117" style="height: 13px">
                                       <b>Name</b></td>
                                   <td class="style116" style="height: 13px">
                                       :</td>
                                   <td style="height: 13px">
                                       <asp:Label ID="lblStdName" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                                   </td>
                               </tr>
                               <tr>
                                   <td class="style117">
                                       &nbsp;</td>
                                   <td class="style116">
                                       &nbsp;</td>
                                   <td>
                                       &nbsp;</td>
                               </tr>
                               <tr>
                                   <td class="style117">
                                       <b>Mobile</b>&nbsp;</td>
                                   <td class="style116">
                                       :</td>
                                   <td>
                                       <asp:TextBox ID="txtCusMobile" runat="server" Width="160px"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidatorMobile" 
                runat="server" ForeColor="Red" validationexpression="^[0-9]{11}$"
                ErrorMessage="Mobile No is required"
                ControlToValidate="txtCusMobile" Display="Dynamic" Text="*">
                </asp:RequiredFieldValidator>
                                   </td>
                               </tr>
                               <tr>
                                   <td class="style117">
                                       &nbsp;</td>
                                   <td class="style116">
                                       &nbsp;</td>
                                   <td>
                                       &nbsp;</td>
                               </tr>
                               <tr>
                                   <td class="style117">
                                       <b>Email</b>&nbsp;</td>
                                   <td class="style116">
                                       :</td>
                                   <td>
                                  
                                       <asp:TextBox ID="txtCusEmail" runat="server" Width="160px"></asp:TextBox>
                                       
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" 
                runat="server" ForeColor="Red"
                ErrorMessage="Email Address is required"
                ControlToValidate="txtCusEmail" Display="Dynamic" Text="*">
                </asp:RequiredFieldValidator>
    
 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  ForeColor="Blue"
      ControlToValidate="txtCusEmail" ErrorMessage="Please Enter Correct Email Address" 
      ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                   </td>
                               </tr>
                               <tr>
                                   <td class="style117" colspan="3">
                                       <asp:Label ID="lblErrmsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                   </td>
                               </tr>
                               <tr>
                                   <td class="style117">
                                       &nbsp;</td>
                                   <td class="style116">
                                       &nbsp;</td>
                                   <td>
                                       &nbsp;</td>
                               </tr>
                               <tr>
                                   <td class="style114" colspan="3">
                                       <asp:Button ID="Button1" runat="server" Text="Save" style="margin-left:200px;" 
                                           onclick="Button1_Click"/>
                                   </td>
                               </tr>
                               <tr>
                                   <td class="style117">
                                       &nbsp;</td>
                                   <td class="style116">
                                       &nbsp;</td>
                                   <td>
                                       &nbsp;</td>
                               </tr>
                           </table>
                          
                           </asp:Panel> 
                            <div style="text-align: left">
                                &nbsp;</div>
                            <asp:Panel ID="pnlNet" runat="server">



                            </asp:Panel>
                                  <asp:Panel ID="pnlStdPaymentView" runat="server" Visible="false">
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
                                        <asp:TextBox ID="txtYear" runat="server" MaxLength="4" ></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidatorYear" 
                runat="server" ForeColor="Red" ValidationExpression="^[0-9]{4}$"
                ErrorMessage="Payment Year is Required"
                ControlToValidate="txtYear" Display="Dynamic" Text="*">
                </asp:RequiredFieldValidator>
    
                                     
    </td>
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
     <asp:DropDownList ID="cmb_semester" runat="server">
                                <asp:ListItem Value="1">Spring</asp:ListItem>
                                <asp:ListItem Value="2">Summer</asp:ListItem>
                                <asp:ListItem Value="3">Fall</asp:ListItem>
                            </asp:DropDownList>
    
    </td>
                                </tr>
                                <tr>
                                    <td style="height: 54px">
                                    </td>
                                    <td style="height: 54px">
                                    </td>
                                    <td colspan="3" style="height: 54px"> &nbsp;<asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text=""></asp:Label>
                                 
                      <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_onclick"  />               
                                        
   <input type="submit" id="ssl_pay" name="submit" value="Pay Now" visible="false" runat="server" style="margin-left:50px;"/>
              
                                        </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                    </td>
                                    
 </tr>

    <tr>
    <td colspan="5">
    
    
                                 
<asp:Panel ID="pnlStdAmount" runat="server" Visible="false" >

<table style="width:100%">
<tr>
<td style="width:20%; height: 20px;">
    <asp:Label ID="Label2" runat="server" Text="Name :" Font-Bold="True"></asp:Label></td>
<td style="width:80%; height: 20px;">
    <asp:Label ID="lblCusName" runat="server" Text="" Font-Bold="False"></asp:Label></td>

</tr>
    <tr>
        <td style="width: 20%; height: 21px">
            <asp:Label ID="Label3" runat="server" Text="Mobile :" Font-Bold="True"></asp:Label></td>
        <td style="width: 80%; height: 21px">
            <asp:Label ID="lblCusMbl" runat="server" Text="" Font-Bold="False"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 20%; height: 20px">
            <asp:Label ID="Label4" runat="server" Text="Email :" Font-Bold="True"></asp:Label></td>
        <td style="width: 80%; height: 20px">
            <asp:Label ID="lblEmail" runat="server" Text="" Font-Bold="False"></asp:Label></td>
    </tr>

</table>

<asp:GridView ID="grdStdAmount" runat="server" AutoGenerateColumns="False"
        DataKeyNames="SID"  Font-Names="Palatino Linotype" Font-Size="12pt" 
        CellSpacing="8" GridLines="None">
                    <Columns>
                    
<asp:TemplateField Visible="false">                         
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Bind("SID") %>' ID="lblSID"></asp:Label>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="left" />
    <HeaderStyle HorizontalAlign="Center" />
     <HeaderStyle ForeColor="#804000" />
 </asp:TemplateField>  
    <asp:BoundField DataField="Year" HeaderText="Year">
        <HeaderStyle HorizontalAlign="left" /> 
        <ItemStyle Width="75px" />      
        </asp:BoundField>

        <asp:BoundField DataField="Sem" HeaderText="Semester" >
        <ItemStyle HorizontalAlign="left" />
        <ItemStyle Width="75px" />
        </asp:BoundField>
 
 
     <asp:BoundField DataField="PAYABLE" HeaderText="Payable">
        <HeaderStyle HorizontalAlign="left" />
        <ItemStyle Width="150px" />
        </asp:BoundField>

        <asp:BoundField DataField="PAID" HeaderText="Paid" >
        <ItemStyle HorizontalAlign="left" />
        <ItemStyle Width="100px" />
        </asp:BoundField>
        
         <asp:BoundField DataField="due" HeaderText="Due" >
        <ItemStyle HorizontalAlign="left" />
        <ItemStyle Width="100px" />
        </asp:BoundField>
       
                    </Columns>
                </asp:GridView>
    <br />
    <asp:Label ID="lblDue" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="blue"></asp:Label><br />
    <br />
      <div style="width:100%; text-align: right;">
    <table style="width:60%; text-align: left; margin-left:40%">
        <tr>
            <td class="auto-style1" colspan="3">
                <strong>Details of Registration Fees taka 7000/-
                <br />
                <br />
                Please select following particulars while payment of Registration Fees</strong></td>
        </tr>

      <tr>
            <td class="auto-style4">Registration Fee (from Fall 2019)</td>
            <td class="auto-style2">:</td>
            <td>3000 tk</td>
        </tr>
        <tr>
            <td class="auto-style4">or Registration Fee (before Fall 2019)</td>
            <td class="auto-style2">:</td>
            <td>2000 tk</td>
        </tr>
        <tr>
            <td class="auto-style4"><span style="color: rgb(0, 0, 0); font-family: Verdana; font-size: 11px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Development Fee</span></td>
            <td class="auto-style2">:</td>
            <td>2000 tk</td>
        </tr>
        <tr>
            <td class="auto-style4">Lab Fee</td>
            <td class="auto-style2">:</td>
            <td>500 tk</td>
        </tr>
        <tr>
            <td class="auto-style4">Library Fee</td>
            <td class="auto-style2">:</td>
            <td>500 tk</td>
        </tr>
        <tr>
            <td class="auto-style4">Semester Activity Fee</td>
            <td class="auto-style2">:</td>
            <td>1000 tk</td>
        </tr>

    </table>
</div>

                          <br />              
            <asp:Label ID="Label5" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="red"></asp:Label><br />
                            
<asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="HEADSN"  Font-Names="Palatino Linotype" Font-Size="12pt" 
                    OnRowDataBound="GridView4_RowDataBound" ShowFooter="True" CellSpacing="8" GridLines="None">
                    <Columns>
                    
<asp:TemplateField  Visible="false">                         
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Bind("HEADSN") %>' ID="lblHEADSN"></asp:Label>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="left" />
    <HeaderStyle HorizontalAlign="Center" />
     <HeaderStyle ForeColor="#804000" />
 </asp:TemplateField>  

<asp:TemplateField HeaderText="Sl." SortExpression="Sl.">                         
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Bind("serial") %>' ID="lblSerial"></asp:Label>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="left" />
    <HeaderStyle HorizontalAlign="Center" />
     <HeaderStyle ForeColor="#804000" />
 </asp:TemplateField>  
           <asp:TemplateField HeaderText="Particulars" SortExpression="Particulars">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("HEADNAME") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("HEADNAME") %>'></asp:Label>
                                  <asp:TextBox ID="HEADNAME" runat="server" Text='<%# Bind("HEADNAME") %>'  Visible="false"></asp:TextBox>
                            </ItemTemplate>
                            
                            <HeaderStyle ForeColor="#804000" />
                        </asp:TemplateField>
                      
                       
                       
                       
                       
                        <asp:TemplateField HeaderText="Year" SortExpression="Year">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtYear" runat="server" Text='<%# Bind("Year") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="YEAR" runat="server" Text='<%# Bind("Year") %>' Width="70px"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle ForeColor="#804000" />
                        </asp:TemplateField>
                       
                       
          <asp:TemplateField HeaderText="Semester">  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txtSemester" runat="server"></asp:TextBox>  
                    </EditItemTemplate>  
                    <ItemTemplate>  
                        <asp:DropDownList ID="ddlsemester" runat="server"> 
                            <asp:ListItem Value="1">Spring</asp:ListItem>
                            <asp:ListItem Value="2">Summer</asp:ListItem>
                            <asp:ListItem Value="3">Fall</asp:ListItem> 
                        </asp:DropDownList>  
                    </ItemTemplate>  
                   <HeaderStyle ForeColor="#804000" />
          </asp:TemplateField>
                       
                       
                       
                       
                        <asp:TemplateField HeaderText="Amount" SortExpression="Amount">
                            <EditItemTemplate>
                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("amount") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="Amount" runat="server" Text='<%# Bind("amount") %>' Width="80px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                           Amount Total:  <asp:TextBox ID="AmountTotal" runat="server" Width="75px" Enabled="false"></asp:TextBox>
                                </FooterTemplate>
                            <HeaderStyle ForeColor="#804000" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                    <input type="submit" id="ssl_pay1" name="submit" value="Submit" 
                    onclick="return ssl_pay1_onclick()" style="width:75px; height: 29px;"/>
                    
                        <asp:Button ID="Submit" runat="server" Text="Button" Visible="false"
                        OnClick="Submit_onclick"  />
                        
                        
                        </asp:Panel>
             
             
              </td>
    </tr>
    
   
                                
                                   
                                   
                                   
                                   
                                   
                  <tr style="text-align: center;">
                    <td colspan="5">
                        &nbsp;
                        <br />
               <!--<input type="submit" id="Submit1" name="submit" value="Submit"  />-->
              
                    <p id="demo"></p>


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
                            </asp:Panel>
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
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            &nbsp;</td>
                        <td bgcolor="white" height="114" width="18">
                            &nbsp;</td>
                        <td bgcolor="#ffffff" height="114" style="text-align: left" width="505">
                            &nbsp;</td>
                        <td bgcolor="white" height="114" width="14">
                            &nbsp;</td>
                        <td bgcolor="#6fb1d9" class="k" height="114" width="1">
                            &nbsp;</td>
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
                        Fax: +88 02 9676031-5, +880.2.9676031-5. Email: info@eastern.edu.bd </span>
                    </td>
                </tr>
            </table>
            
        </td>
      </tr>
      </table>  
    </div>
    
    </center>
    </form>
    
    
    

<form id="payment_gw" name="payment_gw" >

    <input type="hidden" name="value_d" value="StudentName" id="value_d" runat="server"/>   
    <input type="hidden" name="value_a" value="StudentID" id="value_a" runat="server" />  
    <input type="hidden" name="value_b" value="Year2016" id="value_b" runat="server" />   
    <input type="hidden" name="value_c" value="Fall" id="value_c" runat="server" />  
    <input type="hidden" name="total_amount" value="1150.00" id="total_amount" runat="server" />
    
    
    
    <input type="hidden" name="store_id" value="testbox"  />
    <input type="hidden" name="tran_id" value="578e05a14579e" />
    <input type="hidden" name="success_url" value="https://sandbox.sslcommerz.com/developer/success.php" />
    <input type="hidden" name="fail_url" value="https://sandbox.sslcommerz.com/developer/fail.php" />
    <input type="hidden" name="cancel_url" value="https://sandbox.sslcommerz.com/developer/cancel.php" />
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

      

   
</form>


<script type="text/javascript" language="javascript">
    function ssl_pay_onclick() {
        $("#Submit").click();
    }
</script>
<script type="text/javascript" language="javascript">
    function ssl_pay1_onclick() {
        $("#Submit").click();
    }
</script>


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
