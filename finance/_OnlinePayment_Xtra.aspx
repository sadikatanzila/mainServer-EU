<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_OnlinePayment_Xtra.aspx.cs" 
Inherits="student_finance_OnlinePayment_Xtra" EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Eastern University</title>
</head>
<body>

<script type="text/javascript">
function GetClientId(strid)
{
     var count=document.forms[0].length;
     var i = 0 ;
     var eleName;
     for (i = 0 ;  i < count ;  i++ )
     {
       eleName = document.forms[0].elements[i].id;
       pos=eleName.indexOf(strid) ;
       if(pos >= 0)  break;
     }
    return eleName;
 }

 function GetTotal(lTotal_id) {
    document.getElementById(GetClientId("AmountTotal")).value = 0;    
    var obj_lTotal = document.getElementById(lTotal_id);
    if (obj_lTotal.value != "" && obj_lTotal.value != "") {
        
        obj_lTotal.value = parseInt(obj_lTotal.value) ;
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
    for (var i = 0; i < inputs.length; i++) 
    {

        // In this case we are looping through all the Dek Volume and then the Mcf volume boxes in the grid and not an individual one and totalling them
        if (inputs[i].name.indexOf("Amount") > 1) 
        {
            if (inputs[i].value != "" || inputs[i].value != 0) 
            {
              
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


<script type="text/javascript" language="javascript">
function ssl_pay1_onclick() {
$("#Submit").click();
}
</script>


    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="pnlStudent" runat="server" Visible="false">
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
                                        <asp:TextBox ID="txtYear" runat="server" ></asp:TextBox>
    
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
                                        
   <input type="submit" id="ssl_pay" name="submit" value="Pay Now" visible="false" runat="server" style="margin-left:50px;"/>
              
                                        </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="color: #0000ff" colspan="3">
                                    
                                    
                                   
                                   
                                   
                                   
                                   
                                   
                                   
                                   
                                   
                                   
                                   
<asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="HEADSN"  Font-Names="Palatino Linotype" Font-Size="12pt" 
                    OnRowDataBound="GridView4_RowDataBound" ShowFooter="True" CellSpacing="8" GridLines="None">
                    <Columns>
                    
<asp:TemplateField HeaderText="Sl." SortExpression="Sl.">                         
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Bind("HEADSN") %>' ID="lblHEADSN"></asp:Label>
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
                                   
                                   
                                   
                                   
                                   
                  <tr>
                    <td colspan="5">
                    <input type="submit" id="ssl_pay1" name="submit" value="Submit" 
                    onclick="return ssl_pay1_onclick()" style="width:500px"/>
                    
                        <asp:Button ID="Submit" runat="server" Text="Button" Visible="false"
                        OnClick="Submit_onclick"  />
                        <br />
               <!--<input type="submit" id="Submit1" name="submit" value="Submit"  />-->
              
                    <p id="demo"></p>


                   </td>
                </tr>
                                   
                                   
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
   </asp:Panel>
    </div>
    </form>
    
    
    <form id="payment_gw" name="payment_gw" >
   
<input type="text" name="value_d" value="StudentName" id="value_d" runat="server"/>   
<input type="text" name="value_a" value="StudentID" id="value_a" runat="server" />  
<input type="text" name="value_b" value="Year2016" id="value_b" runat="server" />   
<input type="text" name="value_c" value="Fall" id="value_c" runat="server" />   
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

    <!-- Optional Parameters which will be stored and returned at the end !-->
     <!-- SUBMIT REQUEST  !-->
  
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
