<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_Inquiry.aspx.cs" Inherits="admin_Inquiry"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
   
     <script src="SearchScripts/jquery-1.3.2.min.js"
        type="text/javascript"></script>
   
    


    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtUni]');
            var $ddl = $('select[id$=ddl_Institution]');
            var $items = $('select[id$=ddl_Institution] option');

            $txt.keyup(function () {
                 if($txt.val().length >=4)
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
          
        });
    </script>
  <!-- <script type="text/javascript">

       

       function GetSelectedSubject(e) {
           //get label controls to set value/text
           var lblSelectedText = document.getElementById("lblSelectedText");
           var lblSelectedValue = document.getElementById("lblSelectedValue");
           document.getElementById("ContentPlaceHolder_definition_txtRef_Name").style.visibility = "hidden";
           //get selected value and check if subject is selected else show alert box
           var SelectedValue = e.options[e.selectedIndex].value;
           if (SelectedValue > 0) {
               //get selected text and set to label
               var SelectedText = e.options[e.selectedIndex].text;
               lblSelectedText.innerHTML = SelectedText;
               document.getElementById("ContentPlaceHolder_definition_txtRef_Name").style.visibility = "visible";
               //set selected value to label
               lblSelectedValue.innerHTML = SelectedValue;
           } else {
               //reset label values and show alert
               lblSelectedText.innerHTML = "";
               lblSelectedValue.innerHTML = "";
           
               alert("Please select valid subject.");
           }
       }
</script>
    -->

     <script type="text/javascript">
         function ddl_page(ddl) {
             var txt = document.getElementById('<%=txtRef_Name.ClientID %>');
             var txt1 = document.getElementById('<%=txtRef_Contact.ClientID %>');
             if (!(ddl.value > 0)) {
                 txt.style.display = 'none';
                 txt1.style.display = 'none';
                 alert("Please select valid Reference.");
             }
             else {
                 txt.style.display = '';
                 txt1.style.display = '';
             }
         }

         ddlChanged(document.getElementById('<%=ddlSubject.ClientID %>'));
	</script>


     <script type="text/javascript">
         function ddlChanged(ddl) {
             var txt = document.getElementById('<%=txtRef_Name.ClientID %>');
             var txt1 = document.getElementById('<%=txtRef_Contact.ClientID %>');
             if (!(ddl.value > 0)) {
                 txt.style.display = 'none';
                 txt1.style.display = 'none';
                 lblSelectedText.innerHTML = '';
                 lblSelectedValue.innerHTML = '';
                 alert("Please select valid Reference.");
            }
            else {
                 txt.style.display = '';
                 txt1.style.display = '';
                 lblSelectedText.innerHTML = 'Name :';
                 lblSelectedValue.innerHTML = 'Contact :';
            }
        }

        ddlChanged(document.getElementById('<%=ddlSubject.ClientID %>'));
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server" >
    <table border="0" style="width:100%">
        <tr>
            <td>
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
                        <td  class="h" height="22" width="100%"  >
                            <p align="center">
                                <b><font  face="Arial" size="2">Prospective Students' Information :</font></b> <hr /></p>
                             
                        </td>
                    </tr>
                    <tr>
                        <td  class="k" height="1" width="100%">
                            </td>
                    </tr>
                    <tr>
                        <td  class="k" style="height: 114px" width="1">
                            </td>
                        <td bgcolor="white" style="height: 114px" width="18">
                           </td>
                        <td bgcolor="#ffffff" style="vertical-align: top" width="100%">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="height: 10px; background-color: #ffffff; text-align: left">
                                        <table id="tbl_dates" runat="server" style="width:95%">
                                            <tr>
                                                <td colspan="5">
                                                    <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text=""></asp:Label></td>
                                            </tr>
                                            <tr style="font-size: 8pt; color: #666666">
                                                <td class="auto-style2">
                                                   Contact Number</td>
                                                <td>
                                                    :</td>
                                                <td>
                                                    <asp:TextBox ID="txt_Contact" runat="server" Width="186px" ></asp:TextBox>
                                                    <asp:Label ID="lblUpdate" runat="server" Text="" Visible="false"></asp:Label>
                                                    <asp:Label ID="Label1" runat="server" Text="*" Font-Bold="True" Font-Size="14px" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td>
                                                   Name </td>
                                                <td>
                                                    <span class="auto-style3"><strong>:</strong></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txt_name" runat="server" Width="186px"></asp:TextBox>

                                                    <asp:Label ID="Label4" runat="server" Text="*" Font-Bold="True" Font-Size="14px" ForeColor="Red"></asp:Label>

                                                </td>
                                            </tr>

                                              <tr>
                                                <td class="auto-style2">
                                                    Program</td>
                                                <td>
                                                   :</td>
                                                <td>
                                                    <asp:DropDownList ID="cmb_Faculty" runat="server" Width="200px" >
                                                    </asp:DropDownList> 
                                                    <asp:Label ID="Label12" runat="server" Text="*" Font-Bold="True" Font-Size="14px" ForeColor="Red"></asp:Label>
                                                  </td>
                                                <td>
                                                    Addresss</td>
                                                <td>
                                                                                                      
                                                  <span class="auto-style3"><strong>:</strong></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txt_LivingArea" runat="server" Width="300px"></asp:TextBox>

                                                  
                                                  </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style2">
                                                    Year</td>
                                                <td>
                                                    :</td>
                                                <td style="vertical-align: middle">
                                                      <asp:TextBox ID="txt_Year" runat="server" Width="150px"></asp:TextBox>
                                                    <asp:Label ID="Label2" runat="server" Text="*" Font-Bold="True" Font-Size="14px" ForeColor="Red"></asp:Label>

                                               


                                                </td>
                                                <td style="vertical-align: middle">
                                                    Semester</td>
                                                <td style="vertical-align: middle"><span class="auto-style3"><strong>:</strong></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:DropDownList ID="cmb_semester" runat="server" Width="150px">
                                                        <asp:ListItem Value="1">Spring</asp:ListItem>
                                                        <asp:ListItem Value="2">Summer</asp:ListItem>
                                                        <asp:ListItem Value="3">Fall</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:Label ID="Label3" runat="server" Text="*" Font-Bold="True" Font-Size="14px" ForeColor="Red"></asp:Label>

                                               


                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="auto-style2">
                                                    District</td>
                                                <td>
                                                    :</td>
                                                <td colspan="3">

                                            <asp:DropDownList ID="ddl_District" runat="server" Width="200px">
                                                    </asp:DropDownList>

                                                    <asp:Label ID="Label10" runat="server" Text="*" Font-Bold="True" Font-Size="14px" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style2">
                                                    College/ University</td>
                                                <td>
                                                    :

                                                </td>
                                                <td colspan="3">

                                                    <asp:TextBox ID="txtUni" runat="server" Width="150px"></asp:TextBox>
                                                     <asp:DropDownList ID="ddl_Institution" runat="server" Width="200px"> </asp:DropDownList>
                                                    
                                                    <asp:Label ID="Label11" runat="server" Text="*" Font-Bold="True" Font-Size="14px" ForeColor="Red"></asp:Label>

                                                  
                                                    <asp:Label ID="Label5" runat="server" Text="If there is not any College/Institution press or select 'NONE'" ForeColor="Gray"></asp:Label>
                                              </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style2">
                                                    Others Institution:</td>
                                                <td>
                                                    :</td>
                                                <td colspan="3">

                                                    <asp:TextBox ID="txtUni_others" runat="server" Width="150px"></asp:TextBox>
                                              </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style2">
                                                    &nbsp;</td>
                                                <td colspan="4">
                                                    <p id="para">
                                                    </p>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style2">
                                                   Referred By</td>
                                                <td>
                                                    :</td>
                                                <td colspan="3">
                                                   <asp:DropDownList ID="ddlSubject" runat="server" > </asp:DropDownList>


 

                                            
                                                

                                                     
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style2">

                                                    <asp:Label ID="lblSelectedText" runat="server" ClientIDMode="Static" Text="Name :">
                               </asp:Label>         </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtRef_Name" runat="server" Width="150px"></asp:TextBox>

                                            <asp:Label ID="lblSelectedValue" runat="server" ClientIDMode="Static" Text="Contact :">
                               </asp:Label>         <asp:TextBox ID="txtRef_Contact" runat="server" Width="150px" ></asp:TextBox>


 

                                            
                                                

                                                     
                                                </td>
                                            </tr>
                                         <tr>
                                                <td class="auto-style2">
                                                    Remarks</td>
                                                <td>
                                                </td>
                                                <td colspan="3">
                                                     <asp:TextBox ID="txtNote" runat="server" Width="300px"></asp:TextBox></td>
                                            </tr>
                                        
                                            <tr>
                                                <td class="auto-style2">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td colspan="3">
                                                
 

 
     <!--   Tip: College/University get filtered as characters are entered in the textbox.
        Search is not case-sensitive-->
   
                                                    </td>
                                            </tr>
                                        
                                            <tr>
                                                <td class="auto-style2">
                                                </td>
                                                <td>
                                                </td>
                                                <td colspan="3">
                                                    <asp:Button ID="btn_save" runat="server" OnClick="btn_save_Click" Text="Submit" /></td>
                                            </tr>
                                        
                                            <tr>
                                                <td class="auto-style2" colspan="5">
                                                    &#39;<asp:Label ID="Label9" runat="server" Text="*" Font-Bold="True" Font-Size="12px" ForeColor="Red"></asp:Label>&#39; required fields
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            &nbsp;</td>
                        <td bgcolor="white" style="height: 114px" width="14">
                            &nbsp;
                            <p>
                              </p>
                        </td>
                        <td  class="k" style="height: 114px" width="1">
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

<asp:Content ID="Content4" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style2 {
        }
        .auto-style3 {
            font-weight: normal;
        }
    </style>
</asp:Content>


