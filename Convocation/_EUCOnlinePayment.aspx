<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_EUCOnlinePayment.aspx.cs" Inherits="Convocation_EUCOnlinePayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <title>Convocation Online Payment</title>
    
     <link href="../App_themes/jind.css" rel="stylesheet" type="text/css"/>
    <link  href="../App_themes/transmenuh.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../App_themes/transmenu.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="0" cellspacing="0" height="1" width="90%" id="tbl_offered_courses" runat="server">
                    <tr>
                        <td colspan="2" height="24" rowspan="3" width="19" id="TD1" runat="server">
                            <img border="0" height="24" src="../images/lcurv.gif" width="19" /></td>
                        <td bgcolor="#6fb1d9" class="k" height="1" width="505">
                            <img border="0" height="1" src="../images/spacer(1).gif" width="100%" /></td>
                        <td align="right" colspan="2" height="24" rowspan="3" width="15">
                            <img border="0" height="24" src="../images/rcurv.gif" width="15" /></td>
                    </tr>
                    <tr>
                        <td bgcolor="#eef5fa" class="h" height="22" width="100%">
                            <p align="center">
                                <b><font color="#ffa500" face="Arial" size="2"> Payment</font></b></p>
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
                        <td bgcolor="#ffffff" height="114" style="text-align: left">
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
                                    <td style="height: 20px">
                                        &nbsp;</td>
                                    <td style="height: 20px">
                                        &nbsp;</td>
                                    <td style="height: 20px; width: 84px">
                                        &nbsp;</td>
                                    <td style="height: 20px">
                                        &nbsp;</td>
                                    <td style="height: 20px">
                                        &nbsp;</td>
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
    <asp:BoundField DataField="REGISTERFEE" HeaderText="Registration Fee" >
        <ItemStyle HorizontalAlign="left" />
        <ItemStyle Width="150px" />
        </asp:BoundField>
     <asp:BoundField DataField="GAUSTNO" HeaderText="Guest No">
        <HeaderStyle HorizontalAlign="left" />
        <ItemStyle Width="150px" />
        </asp:BoundField>
        
         <asp:BoundField DataField="TOTALFEE" HeaderText="Total Fee">
        <HeaderStyle HorizontalAlign="left" />
        <ItemStyle Width="150px" />
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
                  
                     
                         
                      
                                  
                                        
                                    </td>
                                </tr>
                                   
                  
                                    
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="3">
                                         
                                        </td>
                                </tr>
                                   
                  
                                    
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td colspan="3">
                                         
                                       <table style="width:99%">
                      <tr>
                      <td style="width:50%">
                      
<asp:Label ID="lbl_message" runat="server" Font-Size="Small" ForeColor="Red" Text="To Pay Your Payment with Online Payment, Press 'Pay Now'."></asp:Label>
                       <br />
                                        
   <input type="submit" id="ssl_pay" name="submit" value="Pay Now" runat="server" style="margin-left:50px;"/></td>
                      <td style="border-color: #000000; width:50%; border-left-style: solid; border-left-width: thin;">
                          <asp:Label ID="lbl_Confirm" runat="server" Text="" ForeColor="blue" Font-Bold="true"></asp:Label>
                      <br />
                         <asp:Label ID="lblmsg" runat="server" Font-Size="Small" ForeColor="Red" Text="To Pay Your Payment with 'EU Account Section' (offline Payment)."></asp:Label>
                       <br />
                      
                         <asp:TextBox ID="txtVourcher" runat="server" Width="125px" style="margin-left:5px" Visible="false"></asp:TextBox>
                         
                        
                         
                          <asp:Label ID="lbltextPayOff" runat="server" Text="Please Check after 2(two) Official day of Payment Date." ForeColor="Blue" Font-Bold="True"></asp:Label><br />
                          <br />
                          <asp:Button ID="Button1" runat="server" Text="Offline Payment" Font-Bold="True" OnClick="Button1_Click"  Visible="true"/>
                          </td>
                          
                      </tr>
                      </table> 
                                       
                                       </td>
                                </tr>
                                   
                  
                                    
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td colspan="3">
                                         
                                        &nbsp;</td>
                                </tr>
                            </table>
                            &nbsp;
                            <br />
                            <br />
                            <asp:Panel ID="pnlCong" runat="server" style="text-align:center" ForeColor="Blue" Visible="false">
                                <asp:Label ID="Label1" runat="server" Text="**Congratulation**" 
                                    Font-Size="Medium" Font-Bold="True"></asp:Label>
                                <br />
                                
                                  <asp:Label ID="Label2" runat="server" Text="Your Registration for 6th Convocation has been completed Sucessfully"></asp:Label>
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
    </div>
    </form>
    
    
    
     <form id="payment_gw" name="payment_gw" >

    <input type="hidden" name="value_d" value="" id="value_d" runat="server"/>   
    <input type="hidden" name="value_a" value="" id="value_a" runat="server" />  
    <input type="hidden" name="value_b" value="" id="value_b" runat="server" />   
    <input type="hidden" name="value_c" value="" id="value_c" runat="server" />  
    <input type="hidden" name="total_amount" value="" id="total_amount" runat="server" />
    <input type="hidden" name="tran_id" value="" id="tran_id" runat="server"/>
    
    
     <input type="hidden" name="store_id" value="easternuni001live" />
   
    <input type="hidden" name="success_url" value="http://webportal.easternuni.edu.bd:8080/icampus/paySuccessListener" />
    <input type="hidden" name="fail_url" value="http://webportal.easternuni.edu.bd:8080/icampus/payFailureListener" />
    <input type="hidden" name="cancel_url" value="http://webportal.easternuni.edu.bd:8080/icampus/payCancelListener" />
    <input type="hidden" name="version" value="3.00" />

    <!-- Customer Information !-->
    <input type="hidden" name="cus_name" value="" id="cus_name" runat="server">
    <input type="hidden" name="cus_email" value="" id="cus_email" runat="server">
    <input type="hidden" name="cus_phone" value="" id="cus_phone" runat="server">
    
    <input type="hidden" name="cus_add1" value="Address Line One">
    <input type="hidden" name="cus_add2" value="Address Line Two">
    <input type="hidden" name="cus_city" value="City Name">
    <input type="hidden" name="cus_state" value="State Name">
    <input type="hidden" name="cus_postcode" value="Post Code">
    <input type="hidden" name="cus_country" value="Country">
   
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
