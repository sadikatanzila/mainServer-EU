<%@ Page Language="C#" MasterPageFile="~/EUPortalMasterPageGeneric.Master" AutoEventWireup="true"
    CodeBehind="index.aspx.cs" Inherits="EUPortalWeb.index" Title="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pagewidth">
                <div id="main-content">
                    <div id="left-area">
                        <!-- InstanceBeginEditable name="left-content" -->
                        <div class="left-product">
                            <h2 align="center">
                                <img src="Images/welcomeText.gif" width="248" height="39" />
                            </h2>
                            <ul id="info" style="width: 680px; text-align: center; vertical-align: top;">
                                <embed src="Images/euTopBanner.swf" quality="high" type="application/x-shockwave-flash"
                                    wmode="transparent" width="701" height="300" allowfullscreen="true" pluginspage="http://www.macromedia.com/go/getflashplayer"
                                    allowscriptaccess="always" />
                                </object>
                            </ul>
                        </div>
                        <div class="left-product">
                            <h2 class="style1">
                                Welcome</h2>
                            <ul>
                                <li>
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <div id="welcomeMessage" runat="server" align="left">
                                                    <%--<img align="absmiddle" alt="logo" height="25" name="eu_logo" src="Images/eu-logo.jpg" width="70" />--%>
                                                    <img align="absmiddle" alt="" height="25" name="eu_logo" src="#" width="70" />
                                                    
                                                 <%--   Eastern University, committed to providing quality higher education at 
                                                    affordable fees, warmly welcomes you to visit its web site, its offices and 
                                                    campuses to know about its mission, academic programs, people, resources and 
                                                    environment. This dynamic website is a store house of information for the 
                                                    prospective and present students, their parents&nbsp; and all others who are 
                                                    interested in the University.--%>
                                                    
                                                    <br />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%--<div align="left">
                                                    <br />
                                                    It is also an inter-active media for the students, faculty and administration 
                                                    for instant dissemination of information, communication, decision-making and 
                                                    action. For example, the present students can get advised on line, get their 
                                                    semester results, reserve books in the library, know fee payment status, check 
                                                    instructions and course materials provided by course teachers and so on.</div>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </li>
                                <%--<div style="float: right;">
                                    <a href="#">MORE&gt;&gt;</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>--%>
                                <br />
                                <br />
                            </ul>
                        </div>
                        <div align="center" class="left-product">
                            <h2>
                                <img src="Images/eu_spotlight.gif" width="178" height="26" /></h2>
                            <p align="center">
                                <table border="0" style="border-color: Red; border-width: 2px; border-style: Dashed;">
                                    <tr>
                                        <td valign="top">
                                            <img src="images\arrow3.gif" style="border-width: 0px;" />
                                        </td>
                                        <td style="color: #0066cc; font-family: Arial; font-size: 9pt; text-decoration: none;">
                                            <a href="general/newsEvents/_notice.aspx?nid=NTC_000000054"><span style="font-size: 9pt;
                                                color: #0066cc; font-family: Arial">Extended schedule for Online Course 
                                            Advising For Summer Semester 2010</span></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <img src="images\arrow3.gif" style="border-width: 0px;" />
                                        </td>
                                        <td style="color: #0066cc; font-family: Arial; font-size: 9pt; text-decoration: none;">
                                            <a href="general/newsEvents/_notice.aspx?nid=NTC_000000053"><span style="font-size: 9pt;
                                                color: #0066cc; font-family: Arial">Registration and Course Advising 
                                            Schedule for Summer Semester 2010</span></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <img src="images\arrow3.gif" style="border-width: 0px;" />
                                        </td>
                                        <td style="color: #0066cc; font-family: Arial; font-size: 9pt; text-decoration: none;">
                                            <a href="general/newsEvents/_notice.aspx?nid=NTC_000000051"><span style="font-size: 9pt;
                                                color: #0066cc; font-family: Arial">Revised Payment Schedule for Spring 2010</span></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <img src="images\arrow3.gif" style="border-width: 0px;" />
                                        </td>
                                        <td style="color: #0066cc; font-family: Arial; font-size: 9pt; text-decoration: none;">
                                            <a href="general/newsEvents/_notice.aspx?nid=NTC_000000050"><span style="font-size: 9pt;
                                                color: #0066cc; font-family: Arial">Workshop on Parliamentary Debate&#8221;</span></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <img src="images\arrow3.gif" style="border-width: 0px;" />
                                        </td>
                                        <td style="color: #0066cc; font-family: Arial; font-size: 9pt; text-decoration: none;">
                                            <a href="general/newsEvents/_notice.aspx?nid=NTC_000000049"><span style="font-size: 9pt;
                                                color: #0066cc; font-family: Arial">Urgent Notice</span></a>
                                        </td>
                                    </tr>
                                </table>
                            </p>
                        </div>
                        <div class="left-product">
                            <h2>
                                <span class="more"><a href="#" target="_self">
                                    <img src="style/style_blue/images/btn-title-more.gif" alt="Flash Downloader For Firefox Testimonials"
                                        border="0" title="Testimonials of DHTML Menu" /></a></span><img src="Images/eventsevents_at_eu.gif"
                                            width="145" height="20" /></h2>
                            <p>
                                <span class="bold">New Events</span>:
                            </p>
                            <ul>
                                <li>Change name from &quot;Change name from Change &quot; to &quot;Change name from Change name 
                                    from &quot;.
                                    <img src="style/style_blue/images/icon_new.gif" width="24" height="14" /></li>
                                <li>Change name from Change name from
                                    <img src="style/style_blue/images/icon_new.gif" width="24" height="14" /></li>
                                <li>Provides Traditional Provides Traditional Provides Traditional Provides 
                                    Traditional
                                    <img src="style/style_blue/images/icon_new.gif" width="24" height="14" /></li>
                                <li><a href="#" target="_self">See more events and gallery at eu &gt;&gt;</a> </li>
                            </ul>
                            <p>
                                <span class="red"><strong>Please Note:</strong></span> For the users who want to 
                                upgrade to the latest For the users who want to upgrade to the latest For the 
                                users who want to upgrade to the latest For the users who want to upgrade to the 
                                latest For the users who want to upgrade to the latest .
                            </p>
                        </div>
                        <div class="left-product">
                            <h2 class="style2">
                                PARTNERS &amp; EDUCATION PARTNERS
                            </h2>
                            <img src="Images/images/product/flashdecompiler/award/award-flashdecompiler.gif"
                                alt="Flash Downloader For Firefox Awards" width="700" height="90" />
                        </div>
                        <!-- InstanceEndEditable -->
                    </div>
                    <div id="right-area">
                        <!-- InstanceBeginEditable name="right-area" -->
                        <div class="right-info">
                            <h2>
                                Messages</h2>
                            <div class="related-products">
                                <div class="product-logo">
                                    <img src="Images/images/icon/sothink-SWF-to-Video-Conver-32.png" alt="Sothink SWF to Video Converter"
                                        width="32" height="32" /></div>
                                <dl>
                                    <dt><a href="Pages/DetailMessage.aspx?id=1" target="Pages/DetailMessage.aspx?id=1">MESSAGE FROM THE VICE CHANCELLOR : </a></dt>
                                    <dd>
                                        <%--<a href="http://www.sothink.com/product/swftovideoconverter/index.htm"></a>--%>
                                    </dd>
                                    <dd id="msgFromViceChancellor" runat="server">
                                        <%--Test message Test message Test message Test message Test message Test message--%>
                                        </dd>
                                </dl>
                            </div>
                            <div class="related-products">
                                <div class="product-logo">
                                    <img src="Images/images/icon/fvec_win_32.png" alt="Sothink Video Encoder for Adobe Flash"
                                        width="32" height="32" /></div>
                                <dl>
                                    <dt><a href="Pages/DetailMessage.aspx?id=2" target="Pages/DetailMessage.aspx?id=2">MESSAGE FROM THE CHAIRMAN : </a></dt>
                                    <dd id="msgFromChairman" runat="server">
                                       <%-- Test Message Test Message Test Message Test Message Test Message Test Message 
                                        Test Message--%>
                                        </dd>
                                </dl>
                            </div>
                            <div class="related-products">
                                <div class="product-logo">
                                    <img src="Images/images/icon/sothink-Quicker-32.png" alt="Sothink SWF Quicker"
                                        width="32" height="32" /></div>
                                <dl>
                                    <dt><a href="Pages/DetailMessage.aspx?id=3" target="Pages/DetailMessage.aspx?id=3">WHY CHOOSE EU ? </a></dt>
                                    <dd id="msgFromWhy" runat="server">
                                        <%--Eastern University Eastern University Eastern University Eastern University 
                                        Eastern University--%>
                                        <br />
                                    </dd>
                                </dl>
                            </div>
                            <div class="related-products" style="border: none;">
                                <div class="product-logo">
                                    <img src="Images/images/icon/sothink-logo-maker-32.png" alt="Sothink Logo Maker"
                                        width="32" height="32" /></div>
                                <dl>
                                    <dt><a href="Pages/DetailMessage.aspx?id=4" target="Pages/DetailMessage.aspx?id=4">EU 
                                        CONVOCATION : </a></dt>
                                    <dd id="msgFromConvocation" runat="server">
                                        <%--Help even novice to fast DIY unique fast DIY unique fast DIY unique fast DIY 
                                        unique!<br />--%>
                                    </dd>
                                </dl>
                            </div>
                        </div>
                        <div id="divNotice" runat="server" class="right-info">
                            <h2>
                                Notice Board
                            </h2>
                            <%--<dl>
                                <dd>
                                    <a id="ctlNoticeBoard1_dlNews_ctl00_lnkNotice" href="#">E-MAIL NOTICE FOR 
                                    STUDENTS</a><a
                                        href="http://www.sothink.com/product/swfeasy/index.htm" target="_self"></a></dd>
                                <dd>
                                    <a id="ctlNoticeBoard1_dlNews_ctl01_lnkNotice" href="#">REGISTRATION 
                                    CONFIRMATION FLOW CHART FOR SUMMER 09-10</a><a href="http://www.sothink.com/product/photo-album-maker/index.htm"
                                            target="_self"></a></dd>
                                <dd>
                                    <a id="ctlNoticeBoard1_dlNews_ctl07_lnkNotice" href="#">ENERGY FOR FUTURE</a><a href="http://www.sothink.com/product/flash-decompiler-for-mac/index.htm"
                                        target="_self"></a></dd>
                                <dd>
                                    <a id="ctlNoticeBoard1_dlNews_ctl00_lnkNotice" href="#">E-MAIL NOTICE FOR 
                                    STUDENTS</a><a
                                        href="http://www.sothink.com/product/swfeasy/index.htm" target="_self"></a></dd>
                                <dd>
                                    <a id="ctlNoticeBoard1_dlNews_ctl01_lnkNotice" href="#">REGISTRATION 
                                    CONFIRMATION FLOW CHART FOR SUMMER 09-10</a><a href="http://www.sothink.com/product/photo-album-maker/index.htm"
                                            target="_self"></a></dd>
                                <dd>
                                    <a id="ctlNoticeBoard1_dlNews_ctl07_lnkNotice" href="#">ENERGY FOR FUTURE</a></dd>
                            </dl>--%>
                        </div>
                        <div id="divNewsAndEvents" runat="server" class="right-info">
                            <h2>
                                News &amp; Events</h2>
                            <%--<strong ><font color="#669900">
                                <dl >
                                    <a id="n1" runat="server" href="#">Blood Donation Program on the occasion of 7th Foundation Day</a>
                                </dl>
                                <dl >
                                    <a id="n2" runat="server" href="#">7th Foundation Day-2010</a>
                                </dl>
                                <dl >
                                    <a id="n3" runat="server" href="#">E &amp; T Students Fresher Reception &amp; Farewell Program-2010</a>
                                </dl>
                            </font>
                            </strong>
                            <strong><font color="#669900">
                                <dl >
                                    <a id="n4" runat="server" href="#">Blood Donation Program on the occasion of 7th Foundation Day</a>
                                </dl>
                                <dl >
                                    <a id="n5" runat="server" href="#">7th Foundation Day-2010</a>
                                </dl>
                                <dl >
                                    <a id="n6" runat="server" href="#">E &amp; T Students Fresher Reception &amp; Farewell Program-2010</a>
                                </dl>
                            </font></strong><strong><font color="#669900">
                                <dl >
                                    <a id="n7" runat="server" href="#">Blood Donation Program on the occasion of 7th Foundation Day </a>
                                </dl>
                                <dl >
                                    <a id="n8" runat="server" href="#">7th Foundation Day-2010</a>
                                    <br />
                                </dl>
                                <div id="more" style="float: right;" runat="server">
                                    <a href="Pages/NewsAndEventsArchieve.aspx">MORE&gt;&gt;</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </div>
                            </font></strong><strong><font color="#669900"></font></strong>--%>
                        </div>
                        <!-- InstanceEndEditable -->
                    </div>
                </div>
            </div>
</asp:Content>
