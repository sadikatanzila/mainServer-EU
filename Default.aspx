<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="FlashControl" Namespace="Bewise.Web.UI.WebControls" TagPrefix="Bewise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Eastern University | Web portal</title>  
    <Script Language="JavaScript">
 
        isamap = new Object();
        isamap[0] = "_df"
        isamap[1] = "_ov"
        isamap[2] = "_ot"
        isamap[3] = "_dn"
        function isimgact(id, act)
        {
	        if(document.images) document.images[id].src = eval( "isimages." + id + isamap[act] + ".src");
        }
        if (document.images) { // ensure browser can do JavaScript rollovers.
        isimages = new Object();
        isimages.admission_df = new Image();
        isimages.admission_df.src = "images/admissioneu_admission.jpg";

        isimages.admission_ov = new Image();
        isimages.admission_ov.src = "images/admissioneu_admissionov.jpg";
        } 
</Script>
   
    <style type="text/css">
        .style1
        {
            height: 30px;
            width: 358px;
        }
        .style2
        {
            width: 354px;
        }
        .style3
        {
            height: 30px;
            width: 354px;
        }
    </style>
   
</head>
<body style="margin-top:0; margin:0; font-size: 12pt;">
    <form id="form1" runat="server">
        
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="800">
            <tr>
                <td colspan="3">
                    </td>
                <td colspan="7">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="right" height="27" width="32%">
                                <a href="#"  >
                                    <img alt="Home" border="0" height="27" name="Home_button" src="images1/Home-button-down.jpg"
                                        width="57" /></a></td>
                            <td align="right" width="22%">
                                <a href="#"  >
                                    <img alt="Contact" border="0" height="27" name="contact_button" src="images1/contact-button-new.jpg"
                                        width="70" /></a></td>
                            <td align="right" height="27" width="46%">
                                <select class="dropdownhome" name="Cultural" "
                                    style="width: 145px">
                                    <option selected="selected">--- Quick Links ---</option>
                                    <option value="About_EU/mission.asp">About EU</option>
                                    <option value="Admissions/Acd_cal.asp">Academic Calendar</option>                                    
                                    <option value="Student_Services/career.asp">Career Services</option>
                                    <option value="Admissions/catalog_grad.asp">Catalog-MBA</option>
                                    <option value="Admissions/catalog_ugrad.asp">Catalog-Undegraduate</option>
                                    <option value="Academic_Programs/course_sch.asp">Course Schedules</option>                                    
                                    <option value="About_EU/job_opp.asp">Employment</option>
                                    <option value="About_EU/directory.asp">Faculty &amp; Staff Directory</option>                                    
                                    <option value="Academic_Programs/undergraduate.asp">Schools &amp; Departments</option>
                                    <option value="myEu/index.asp">My EU Login</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <img alt="" height="27" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td>
                    <img alt="logo" height="66" name="eu_logo" src="images1/eu-logo.jpg" 
                        width="175" /></td>
                <td colspan="9" rowspan="7" >
                  <object id="OBJECT1" style="POSITION: static; HEIGHT: 227px" 
                        codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,115,0" 
                        width=625  classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000">
                        <param NAME="banner" VALUE="images1/euTopBanner.swf" />
                        <PARAM NAME="movie" VALUE="images1/euTopBanner.swf"/>
                        <PARAM NAME="quality" VALUE="high"/>
                        <param name="FlashVars" value/>
                        <param name="WMode" value="Window"/>
                        <param name="Play" value="1"/>
                        <param name="Loop" value="-1"/>
                        <param name="SAlign" value=""/>
                        <param name="Menu" value="-1"/>
                        <EMBED src="images1/euTopBanner.swf" WIDTH="625px" HEIGHT="227px" NAME="banner" wmode="transparent" 
                            PLUGINSPAGE="http://www.macromedia.com/go/getflashplayer"></EMBED>
                   </OBJECT>
                </td>
                <td>
                    <img alt="" height="66" src="images1/spacer.gif" width="1" /></td>
            </tr>
            <tr>
                <td>
                
                    <a href="general/aboutEu/_overview.aspx" >
                        <img alt="About EU" border="0" height="29" name="about_button" src="images1/about-button.jpg"
                            width="175" /></a></td>
                <td>
                    <img alt="" height="29" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td>
                    <a href="general/eu foundation/_eu_foundation.aspx" >
                        <img alt="EU Foundation" border="0" height="24" name="EU_foundation" src="images1/eu_foundation.jpg"
                            width="175" /></a></td>
                <td>
                    <img alt="" height="24" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td>
                    <a href="general/eu administration/_keyPersonal.aspx" >
                        <img alt="Eu Administration" border="0" height="24" name="EU_administration" src="images1/eu_administration.jpg"
                            width="175" /></a></td>
                <td><img alt="" height="24" src="images1/spacer.gif"width="1" /></td>
            </tr>
            <tr>
                <td>
                    <a href="general/academies/_academics.aspx" >
                        <img alt="Eu Academics" border="0" height="25" name="Academics" src="images1/eu_academics.jpg"
                            width="175" /></a></td>
                <td>
                    <img alt="" height="25" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td>
                    <a href="general/faculties/_bba.aspx" >
                        <img alt="Eu Faculty" border="0" height="21" name="EU_Faculty" src="images1/eu_faculty.jpg"
                            width="175" /></a></td>
                <td>
                    <img alt="" height="21" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td rowspan="2">
                    <img border="0" height="52" name="current_future_studen" src="images1/current_future_studen.jpg" width="175" usemap="#ImageCurrentFutureMap" /></td>
                <td>
                    <img alt="" height="38" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td rowspan="15">
                    <img alt="" name="eu_cect" src="images1/eu_cect.jpg"
                      ? width="28" /></td>
                <td align="left" background="images1/a-z.jpg"
                    colspan="3" rowspan="2" valign="center">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td height="27" width="6%">
                                &nbsp;</td>
                            <td align="left" valign="bottom" width="94%">
                                <img src="images1/welcomeText.gif" /></td>
                        </tr>
                    </table>
                </td>
                <td rowspan="15">
                    <img alt="" height="472" name="dots" src="images1/dots.jpg"
                        width="19" /></td>
                <td class="header" colspan="4" height="30" rowspan="2" valign="center" style="color: #ff6600; font-family: Arial"><strong>
                    <img src="images1/eu_news.gif" /></strong>
                </td>
                <td style="font-family: Times New Roman">
                    <img alt="" height="14" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr style="font-family: Times New Roman">
                <td><img alt="Information for" border="0" height="28" name="information_for" src="images1/information_for.jpg"
                            width="175" /></td>
                <td>
                    <img alt="" height="28" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr style="font-family: Times New Roman">
                <td>
                    <a href="general/Miscellaneous/_officials.aspx" >
                        <img alt="Faculty & Staff" border="0" height="23" name="faculty_button" src="images1/faculty-button.jpg"
                            width="175" /></a></td>
                <td align="middle" background="images1/bg-index.jpg"
                    colspan="3" rowspan="13" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 97%">
                        <tr align="left" valign="top">
                            <td class="style3">
                                <div align="justify">
                                    <span class="style1" style="font-size: 9pt; font-family: Arial"> Eastern University,
                                        committed to providing quality higher education at affordable fees, warmly welcomes
                                        you to visit its web site,
                                            its offices and campuses to know about its mission, academic programs, people, resources
                                            and environment.
                                            This dynamic website </span>
                                    <a class="alinkmore_wuc" href="general/welcome/_welcome.aspx"><span style="font-size: 7pt;color: #0066cc; font-family: Arial"><strong>More» </strong></span></a>
                                </div>
                            </td>
                        </tr>
                        <tr align="left" valign="bottom">
                            <td class="style2" style="text-align: right; font-size:large;">
                                <a href="../images/2ndConvo2011Ad.jpg" style="color:Red; "><blink><Strong>Convocation 2011</Strong></blink></a>
                            </td>
                        </tr>
                        <tr align="left" valign="bottom">
                            <td class="style2" style="text-align: right; font-size:small;">
                                <a href="./AdmisionResult/2ndConvocationRegistrationForm2011(13Nov2010Final).doc"
                                    style="color: Red;"><Strong>Registration Form</Strong></a>
                            </td>
                        </tr>
                        <tr align="left" valign="bottom">
                            <td class="style2" style="line-height:14px;">
                                &nbsp
                            </td>
                        </tr>
                        <tr align="left" valign="bottom">
                            <td class="style2" style="text-align: right" ><a Href="general/academies/_admission.aspx" onmouseout="isimgact( 'admission',0)"  OnMouseOver="isimgact( 'admission',1)"  >
                                </a> </td>
                        </tr>
                        <tr align="left" valign="bottom">
                            <td class="style2" style="text-align: right">
                               <a href="general/academies/_admission.aspx"> <img src="images/admissioneu_admission.jpg" Border="0" Height="17" Width="128" Name="admission" Alt="EU Admission" /></a>
                            </td>
                        </tr>
                        
                        <tr align="left" valign="bottom">
                            
				<td class="style2" align="left">				
                                <a style="font-weight: bold; color: purple; text-align: left" href="#" >Admission&nbsp; Result :    </a></td>
				<td class="header" align="right">
                                &nbsp;</td>
			
                        </tr>
                        
                        <tr align="left" valign="bottom">
                            
				<td class="style2" align="center">				
                                <a style="font-weight:bold; color:Red; text-align: center" href= "./AdmisionResult/BBA.pdf"> BBA</a></td>
				<td class="header" align="right">
                                &nbsp;</td>
			
                        </tr>
                        
                        <tr align="left" valign="bottom">
                            
				<td class="style2" align="center">				
                                <a style="font-weight:bold; color:Red; text-align: center" href= "./AdmisionResult/E&T.pdf">E&amp;T</a></td>
				<td class="header" align="right">
                                &nbsp;</td>
			
                        </tr>
                        
                        <tr align="left" valign="bottom">
                            
				<td class="style2" align="center">				
                                <a style="font-weight:bold; color:Red; text-align: center" href= "./AdmisionResult/BA-English.pdf">English</a></td>
				<td class="header" align="right">
                                &nbsp;</td>
			
                        </tr>
                        
                        <tr align="left" valign="bottom">
                            
				<td class="style2" align="center">				
                                <a style="font-weight:bold; color:Red; text-align: center" href= "./AdmisionResult/Law.pdf">Law</a></td>
				<td class="header" align="right">
                                &nbsp;</td>
			
                        </tr>
                        
			
                        </tr>
                        <tr align="left" valign="bottom">
                            <td class="style2" style="color: #ff6600; font-family: Arial"><strong>
                                <img src="images1/eu_spotlight.gif" /></strong></td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="color: #0066cc" class="style2" >
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr align="left" >
                           <td class="style2" style="color: #ff6600; font-family: Arial"></td>
                        </tr>
                        <tr >
                            <td align="left" style=" vertical-align:top" class="style2" >
                                <strong>
                                    <img src="images/eventsevents_at_eu.gif" /></strong></td>
                        </tr>
                        <tr>
                            <td align="left" style="vertical-align: top" class="style2">
                                <asp:PlaceHolder ID="PlaceHolder_events" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                       
                        
                       
                    </table>
                </td>
                <td align="left" background="images1/bg-index.jpg"
                    colspan="4" rowspan="13" valign="top">
                    <asp:PlaceHolder ID="PlaceHolder_news_events" runat="server"></asp:PlaceHolder>
                </td>
                
                 <td>
                    <img alt="" height="23" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td rowspan="2">
                    <a href="general/Miscellaneous/_student.aspx">
                        <img alt="Student" border="0" height="22" name="Students" src="images1/eu_students.jpg"
                            width="175" /></a></td>
                <td>
                    <img alt="" height="14" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td style="height: 8px">
                    <img alt="" height="8" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td>
                    <a href="CareerOpportunitySeptember2.pdf"  >
                        <img alt="Career" border="0" height="25" name="Career" src="images1/eu_career.jpg"
                            width="175" /></a></td>
                <td>
                    <img alt="" height="25" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td rowspan="2">
                        <a href="http://webmail.easternuni.edu.bd"  >                    
                        <img alt="" border="0" height="43" name="" src="images1/Community-button.jpg"
                            width="175" /></a></td>
                <td>
                    <img alt="" height="32" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td>
                    <img alt="" height="11" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td rowspan="7">
                    <img alt="" border="0" height="317" name="left_llog" src="images1/left_llog.jpg"
                        usemap="#left_llog" width="175" /></td>
                <td>
                    <img alt="" height="91" src="images1/spacer.gif"
       ?                width="1" /></td>
            </tr>
            <tr>
                <td>
                    <img alt="" height="10" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td>
                    <img alt="" height="29" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td>
                    <img alt="" height="12" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td>
                    <img alt="" height="43" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td>
                    <img alt="" height="60" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td>
                    <img alt="" height="72" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td align="left" background="images1/copyright-new.jpg"
                    colspan="10" valign="top"
                    style ="FONT-WEIGHT: bold; FONT-SIZE: 10px; color: #0066cc;FONT-FAMILY: Arial, Helvetica, sans-serif">
                    <table border="0" cellpadding="0" cellspacing="0" height="29" width="800">
                        <tr>
                            <td width="188" style="text-align: right; vertical-align:top">
                                &nbsp;Main Campus: &nbsp;</td>
                            <td align="middle" valign="center" width="527">
                                <span >&nbsp;House#15/2, Road# 03, Dhanmondi, Dhaka, Bangladesh. Tel: +88029676031-5
                                    &nbsp;Fax: +88 02 9676031-5 &nbsp;Email: info@easternuni.edu.bd, www.easternuni.edu.bd   </td>
                            <td width="87">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td>
                    <img alt="" height="29" src="images1/spacer.gif"
                        width="1" /></td>
            </tr>
            <tr>
                <td>
                    <img alt="" height="1" src="images1/spacer.gif"
                        width="175" /></td>
                <td>
                    <img alt="" height="1" src="images1/spacer.gif"
                        width="28" /></td>
                <td>
                    <img alt="" height="1" src="images1/spacer.gif"
                        width="281" /></td>
                <td>
                    <img alt="" height="1" src="images1/spacer.gif"
                        width="57" /></td>
                <td>
                    <img alt="" height="1" src="images1/spacer.gif"
                        width="13" /></td>
                <td>
                    <img alt="" height="1" src="images1/spacer.gif"
                        width="19" /></td>
                <td>
                    <img alt="" height="1" src="images1/spacer.gif"
                        width="12" /></td>
                <td>
                    <img alt="" height="1" src="images1/spacer.gif"
                        width="76" /></td>
                <td>
                    <img alt="" height="1" src="images1/spacer.gif"
                        width="69" /></td>
                <td>
                    <img alt="" height="1" src="images1/spacer.gif"
                        width="70" /></td>
                <td>
                </td>
            </tr>
        </table>
        <!-- End ImageReady Slices -->
        <map id="asd" name="left_llog">
            <area coords="22,50,118,68"  href="student/_login.aspx" shape="RECT"
                target="_self" />
            <area coords="22,73,120,90"  href="staffs/_login.aspx" shape="RECT"
                target="_self" />
        </map>
        <map id="sasd" name=?ImageCurrentFutureMap">
            <area alt="Future student" coords="13,4,160,21" href="general/future student/_EU_atAGlance.aspx"shape="RECT" />
             <area alt="Current student" coords="13,47,173,30" href="general/current student/_academic_calender.aspx"shape="RECT" />
        </map>
        
    </form>
</body>
</html>
