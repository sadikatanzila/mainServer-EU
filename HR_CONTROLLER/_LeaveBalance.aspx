<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_LeaveBalance.aspx.cs" Inherits="HR_CONTROLLER_LeaveBalance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">

        function loadstudentLedger(id) {
            window.open('_leaveHistory.aspx', 'titlebar=yes,toolbar=no,scrollbars,resizable=true,height=650,width=900');
            return false;
        }
 </script>   

    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtUni]');
            var $ddl = $('select[id$=ddlEmployee]');
            var $items = $('select[id$=ddlEmployee] option');

            $txt.keyup(function () {
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
    

       <script  type="text/javascript">
           $(function () {

               var gvET = document.getElementById("<%= grdLeavebalance.ClientID %>");
               var rCount = gvET.rows.length;
               for (i = 0; i <= rCount; i++) {
                   document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Casual_" + i).disabled = true;
                   document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Medical_" + i).disabled = true;
                   document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Earned_" + i).disabled = true;
                   document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Leave_without_Pay_" + i).disabled = true;
                   document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Semester_Brk_" + i).disabled = true;
                   document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Duty_Lv_" + i).disabled = true;
                   document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Maternity_Lv_" + i).disabled = true;
               }
             
           }
           )

           function SelectAllChkb(headerchk) {
               var grdmovie = document.getElementById('ContentPlaceHolder_tracker_grdLeavebalance');
               var i;
               if (headerchk.checked) {
                   for (i = 0; i < grdmovie.rows.length; i++) {
                       var inputs = grdmovie.rows[i].getElementsByTagName('input');
                       inputs[0].checked = true;
                   }
               }
               else {
                   for (i = 0; i < grdmovie.rows.length; i++) {
                       var inputs = grdmovie.rows[i].getElementsByTagName('input');
                       inputs[0].checked = false;
                   }
               }
           }
           function CheckChildchkb(gv) {
                var grdmovie = gv.parentNode.parentNode.parentNode;
                  for (var i = 0; i < grdmovie.rows.length; i++) {                       
                       var chb = grdmovie.rows[i+1].cells[0].getElementsByTagName("input")[0];
                       if (chb.checked) {
                           document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Casual_" + i).disabled = false;
                           document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Medical_" + i).disabled = false;
                           document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Earned_" + i).disabled = false;
                           document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Leave_without_Pay_" + i).disabled = false;
                           document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Semester_Brk_" + i).disabled = false;
                           document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Duty_Lv_" + i).disabled = false;
                           document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Maternity_Lv_" + i).disabled = false;
                       } else {
                           document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Casual_" + i).disabled = true;
                           document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Medical_" + i).disabled = true;
                           document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Earned_" + i).disabled = true;
                           document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Leave_without_Pay_" + i).disabled = true;
                           document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Semester_Brk_" + i).disabled = true;
                           document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Duty_Lv_" + i).disabled = true;
                           document.getElementById("ContentPlaceHolder_tracker_grdLeavebalance_Maternity_Lv_" + i).disabled = true;
                       }
                   }                   
               }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server"   >
    <div style=" text-align: left" >
    <div style="font-family: Arial, Helvetica, sans-serif">

         

      

       

        <br />

      <p style="font-size: large; text-align: center; font-weight: bold; font-family: Arial, Helvetica, sans-serif;"> Leave Entitlement Entry </p>
       
<br />
    <table style="width:95%; margin-left:25px; ">
        <tr>
            <td colspan="6">
                
                Year &nbsp;&nbsp;:
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtYear" runat="server" ></asp:TextBox> 
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                &nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="ddlStaff" runat="server" Width="150px" Visible="false">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               
                </td>
        </tr>

       

      

        <tr>
            <td colspan="6">
                
                <asp:Label ID="lblmsg" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Blue"></asp:Label>
                </td>
        </tr>

       

      

        <tr>
            <td colspan="6" style="text-align: center">
                
                <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" />
                </td>
        </tr>

       

      

        <tr>
            <td colspan="6" style="text-align: center">
                
                &nbsp;</td>
        </tr>

       

      

        <tr>
            <td>
                
                <!--OnRowEditing="grdLeavebalance_RowEditing"-->
                <asp:GridView ID="grdLeavebalance" runat="server" AutoGenerateColumns="False" Width="95%"  
                    BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"   AllowPaging="true" 
                                            CellPadding="4" PageSize="50" OnRowDataBound="grdLeavebalance_RowDataBound" 
                    OnPageIndexChanging="grdLeavebalance_PageIndexChanging" OnRowEditing="grdLeavebalance_RowEditing">
                                            <FooterStyle BackColor="#D9E6FF" ForeColor="#330099" />
                  
                    <Columns>
                    <asp:TemplateField HeaderText="CheckAll">
                <HeaderTemplate >
                <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAllChkb(this);"  Visible="false"/>
                </HeaderTemplate>
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <ItemTemplate>
                    <asp:CheckBox ID="chkBoxPages" runat="server" onclick="CheckChildchkb(this)" />
                    </ItemTemplate>
        </asp:TemplateField> 
<asp:TemplateField  Visible="false">                         
    <ItemTemplate>
        <asp:Label runat="server" Text='<%# Bind("STAFF_ID") %>' ID="lblSTAFF_ID"></asp:Label>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="left" />
    <HeaderStyle HorizontalAlign="Center" />
     
 </asp:TemplateField>  

<asp:TemplateField HeaderText="Sl." SortExpression="Sl.">                         
    <ItemTemplate>
        <asp:Label runat="server"  ID="lblSerial"></asp:Label>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="left" />
    <HeaderStyle HorizontalAlign="Center" />
     
 </asp:TemplateField>  
           <asp:TemplateField HeaderText="Employee ID" >
                            <EditItemTemplate>
                                <asp:TextBox ID="txtVALUE" runat="server" Text='<%# Bind("VALUE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblVALUE" runat="server" Text='<%# Bind("VALUE") %>'></asp:Label>
                                  <asp:TextBox ID="VALUE" runat="server" Text='<%# Bind("VALUE") %>'  Visible="false"></asp:TextBox>
                            </ItemTemplate>
                            
                            
                        </asp:TemplateField>
                      

                          <asp:TemplateField HeaderText="Name" >
                            <EditItemTemplate>
                                <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("STAFF_NAME") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("STAFF_NAME") %>'></asp:Label>
                                  <asp:TextBox ID="STAFF_NAME" runat="server" Text='<%# Bind("STAFF_NAME") %>'  Visible="false"></asp:TextBox>
                            </ItemTemplate>
                            
                            
                        </asp:TemplateField>
                       
                       
                        <asp:TemplateField HeaderText="Joining Date" >
                            <EditItemTemplate>
                                <asp:TextBox ID="txtJOIN_DATE" runat="server" Text='<%# Bind("JOIN_DATE", "{0:dd-MMM-yyyy}") %>' ></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblJOIN_DATE" runat="server" Text='<%# Bind("JOIN_DATE", "{0:dd-MMM-yyyy}") %>' ></asp:Label>
                                  <asp:TextBox ID="JOIN_DATE" runat="server" Text='<%# Bind("JOIN_DATE", "{0:dd-MMM-yyyy}") %>'   Visible="false"></asp:TextBox>
                            </ItemTemplate>
                            
                            
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Confirmation Date" >
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCONFIRMATION_DATE" runat="server" Text='<%# Bind("CONFIRMATION_DATE", "{0:dd-MMM-yyyy}") %>' ></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCONFIRMATION_DATE" runat="server" Text='<%# Bind("CONFIRMATION_DATE", "{0:dd-MMM-yyyy}") %>' ></asp:Label>
                                  <asp:TextBox ID="CONFIRMATION_DATE" runat="server" Text='<%# Bind("CONFIRMATION_DATE", "{0:dd-MMM-yyyy}") %>'  Visible="false"></asp:TextBox>
                            </ItemTemplate>
                            
                            
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Casual" SortExpression="Casual">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCasual" runat="server" Text='<%# Bind("Casual") %>'  ></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="Casual" runat="server" Text='<%# Bind("Casual") %>' Width="70px"  ></asp:TextBox>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Medical" SortExpression="Medical">
                            <EditItemTemplate>
                                <asp:Label ID="lblMedical" runat="server" Text='<%# Eval("Medical") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="Medical" runat="server" Text='<%# Bind("Medical") %>' Width="80px"></asp:TextBox>
                            </ItemTemplate>
                           
                            
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Earned" SortExpression="Earned">
                            <EditItemTemplate>
                                <asp:Label ID="lblEarned" runat="server" Text='<%# Eval("Earned") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="Earned" runat="server" Text='<%# Bind("Earned") %>' Width="80px"></asp:TextBox>
                            </ItemTemplate>
                           
                            
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Leave without Pay" SortExpression="Leave without Pay">
                            <EditItemTemplate>
                                <asp:Label ID="lblLeave_without_Pay" runat="server" Text='<%# Eval("Leave_without_Pay") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="Leave_without_Pay" runat="server" Text='<%# Bind("Leave_without_Pay") %>' Width="80px"></asp:TextBox>
                            </ItemTemplate>
                           
                            
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Semester Break" SortExpression="Semester Break">
                            <EditItemTemplate>
                                <asp:Label ID="lblSemester_Brk" runat="server" Text='<%# Eval("Semester_Brk") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="Semester_Brk" runat="server" Text='<%# Bind("Semester_Brk") %>' Width="80px"></asp:TextBox>
                            </ItemTemplate>
                           
                            
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Duty Leave" SortExpression="Duty Leave">
                            <EditItemTemplate>
                                <asp:Label ID="lblDuty_Lv" runat="server" Text='<%# Eval("Duty_Lv") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="Duty_Lv" runat="server" Text='<%# Bind("Duty_Lv") %>' Width="80px"></asp:TextBox>
                            </ItemTemplate>
                           
                            
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Maternity Leave" SortExpression="Maternity Leave">
                            <EditItemTemplate>
                                <asp:Label ID="lblMaternity_Lv" runat="server" Text='<%# Eval("Maternity_Lv") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="Maternity_Lv" runat="server" Text='<%# Bind("Maternity_Lv") %>' Width="80px"></asp:TextBox>
                            </ItemTemplate>
                           
                            
                        </asp:TemplateField>
                    </Columns>
               
                  <RowStyle BackColor="White" ForeColor="#330099" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    
                                            <HeaderStyle BackColor="#6699FF" Font-Bold="True" Font-Size="Small" ForeColor="#FFFFCC" />  
                     </asp:GridView>


                <!--
         
                       
                       
                       
                       
                       
                        -->
            </td>
             <td class="auto-style1">
                

                 &nbsp;</td>
             <td>
                

                 &nbsp;</td>
             <td>
                

                 &nbsp;</td>
             <td>
                

                 &nbsp;</td>
             <td>
                

                 &nbsp;</td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center">
                 &nbsp;</td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center">
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   

                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </td>
        </tr>
         <tr>
            <td colspan="6" style="text-align: left; margin-left:70px">
                
                 


               
            </td>
        </tr>
    </table>
        &nbsp;</div>
</div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

