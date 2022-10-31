<%@ Page Title="" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_LeaveEntrywithAlter.aspx.cs" Inherits="HR_CONTROLLER_LeaveEntrywithAlter" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    <div style=" text-align: left">
    <div style="font-family: Arial, Helvetica, sans-serif">

         

      

       

        <br />

      <p style="font-size: large; text-align: center; font-weight: bold; font-family: Arial, Helvetica, sans-serif;"> Leave Application Form </p>
       
<br />
    <table style="width:95%; margin-left:25px; ">
        <tr>
            <td colspan="6">
                
                <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text=""></asp:Label>
                &nbsp; &nbsp; &nbsp; &nbsp;<asp:Label ID="lbl_TranID" runat="server" Font-Size="Large" ForeColor="Red" Text=""></asp:Label>
                </td>
        </tr>

        <tr>
            <td style="vertical-align: top">
                

                Select Employee</td>
             <td class="auto-style1" style="vertical-align: top">
                

                 :</td>
             <td>
                
                   <asp:TextBox ID="txtUni" runat="server" Width="150px" AutoPostBack="true" OnTextChanged="txtUni_TextChanged"></asp:TextBox>
                   <asp:DropDownList ID="ddlEmployee" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
    </asp:DropDownList>  
                

                  <asp:Label ID="Label5" runat="server" Text="If you dont found the Employee ID Please Contact with IT office" ForeColor="Gray"></asp:Label>
                                           
                 <script>
                     function run() {
                       
                         var e = document.getElementById("ContentPlaceHolder_tracker_ddlEmployee");
                         var strUser = e.options[e.selectedIndex].value;
                     }
</script>

            </td>
             <td>
                

            </td>
             <td>
                

            </td>
             <td>
                

            </td>
        </tr>

        <tr>
            <td>
                

                ID</td>
             <td class="auto-style1">
                

                 :</td>
             <td colspan="4">
                
    <asp:TextBox ID="txtEmployeeID" runat="server" ReadOnly="true" ></asp:TextBox>
              <!--   <input type="text" id="txtEmployeeID" placeholder="get value on option select" vi/>-->

                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 Name:  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEmployeeName" runat="server" ReadOnly="true" ></asp:TextBox>

               <!--  <input type="text" id="txtEmployeeName" placeholder="get value on option select"/>--></td>
        </tr>

        <tr>
            <td>
                

                Department</td>
             <td class="auto-style1">
                

                 :</td>
             <td colspan="4">
                

                 <asp:TextBox ID="txtDept" runat="server" ReadOnly="true" ></asp:TextBox>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Designation:  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtDesignation" runat="server" ></asp:TextBox>
                

            </td>
        </tr>

        <tr>
            <td>
                

                Leave Type</td>
             <td class="auto-style1">
                

                 :</td>
             <td>
                

                   <asp:DropDownList ID="ddlLeave" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlLeave_SelectedIndexChanged">
    </asp:DropDownList>

             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>

                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label ID="lblTaken" runat="server" Text=""></asp:Label>

                 
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label ID="lblRemaining" runat="server" Text=""></asp:Label>
&nbsp;&nbsp;

             </td>
             <td>
                

                 &nbsp;</td>
             <td>
                

                 &nbsp;</td>
             <td>
                

                 &nbsp;</td>
        </tr>

        <tr>
            <td colspan="6">
                

                &nbsp;</td>
        </tr>

        <tr>
            <td>
                

                From Date</td>
             <td class="auto-style1">
                

                 &nbsp;</td>
             <td colspan="4">
                



                 <asp:TextBox ID="txtfromDate" runat="server" ></asp:TextBox>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;To Date:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtToDate" runat="server"  ></asp:TextBox>
                
                  <asp:CalendarExtender ID="CalendarExtender_txtToDate" runat="server"  OnClientDateSelectionChanged="checkDate"
                                                         Format="dd/MMM/yyyy" TargetControlID="txtToDate"></asp:CalendarExtender>
                
                  
                          <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
                 <asp:CalendarExtender ID="CalendarExtender_txtfromDate" runat="server"
                                                         Format="dd/MMM/yyyy" TargetControlID="txtfromDate"></asp:CalendarExtender>



                 <script>

                     function checkDate(sender, args) {

                         var startDate = new Date(document.getElementById("txtfromDate").value);

                         var endDate = new Date(document.getElementById("txtToDate").value);

                         document.getElementById("txtDays").value = DateDiff(endDate, startDate).toString();
                         //   document.getElementById("lblTotaldays").value = DateDiff(endDate, startDate).toString();

                     }
                     function DateDiff(sDate1, sDate2) {
                         iDays = parseInt(Math.abs(sDate1 - sDate2) / 1000 / 60 / 60 / 24) + 1;
                         if ((sDate1 - sDate2) < 0) {
                             return -iDays;
                         }
                         return iDays;
                     }

            </script>
            </td>
        </tr>

        <tr>
            <td>
                

                Reason for Leave</td>
             <td class="auto-style1">
                

                 :</td>
             <td>
                

                 <asp:TextBox ID="txtReason" runat="server" Width="284px" Height="50px"></asp:TextBox>
                

            </td>
             <td>
                

                 &nbsp;</td>
             <td>
                

                 &nbsp;</td>
             <td>
                

                 &nbsp;</td>
        </tr>

        <!--
        <tr>
            <td>
                

                Total days</td>
             <td class="auto-style1">
                

                 :</td>
             <td>
                

                 <asp:Label ID="lblTotaldays" runat="server" Text=""></asp:Label>
                

                 <asp:TextBox ID="txtDays" runat="server" ReadOnly="true" ></asp:TextBox>
                
                 
                
                  
            </td>
             <td>
                

                 &nbsp;</td>
             <td>
                

                 &nbsp;</td>
             <td>
                

                 &nbsp;</td>
        </tr>

        <tr>
            <td>
                

                &nbsp;</td>
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
            <td>
                

                Contact Address</td>
             <td class="auto-style1">
                

                 :</td>
             <td colspan="4">
                

                 <asp:TextBox ID="txtAddress" runat="server" Width="500px"></asp:TextBox>
                

            </td>
        </tr>

        <tr>
            <td>
                

                Leave time Contact</td>
             <td class="auto-style1">
                

                 :</td>
             <td colspan="4">
                

                 <asp:TextBox ID="txtLvContact" runat="server" Width="500px"></asp:TextBox>
                

            </td>
        </tr>

        <tr>
            <td>
                

                Responsibility Handled on:&nbsp;&nbsp;
                Employee Name<br />
                </td>
             <td class="auto-style1">
                

                 :</td>
             <td colspan="4">
                
                   <asp:DropDownList ID="ddlHandledEmpID" runat="server" Width="250px">
    </asp:DropDownList>
                
            </td>
        </tr>

        -->

        <tr>
            <td>
                

                &nbsp;</td>
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
                <asp:Button ID="btnView" runat="server" Text="Previous Leave" OnClick="btnView_Click" />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   

                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </td>
        </tr>
         <tr>
            <td colspan="6" style="text-align: left; ">
                

                <table style="width:100%">
                    <tr>
                        <td style="text-align: center">
                            &nbsp;</td>
                        <td style="width:30%;text-align: center" >
                             &nbsp;</td>

                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblLvdtl" runat="server" Text="Leave Taken Details" Font-Size="14px" Visible="False" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width:30%;text-align: center" >
                             <asp:Label ID="lblLvSum" runat="server" Text="Leave Taken Summary" Font-Size="14px" Visible="false" Font-Bold="True"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView_LeaveBalance" runat="server" AutoGenerateColumns="False" ShowFooter="false"
                                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="4" Width="80%" OnRowDataBound="GridView_LeaveBalance_RowDataBound">
                                            <FooterStyle BackColor="#D9E6FF" ForeColor="#330099" />
                                            <Columns>

                              <asp:TemplateField HeaderText="Sl" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerialdtl" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>  


                            <asp:TemplateField HeaderText="Leave Type" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblLeaveType" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>                                         
                                </ItemTemplate>

                            </asp:TemplateField>     
                                                
                                                 
                            <asp:TemplateField HeaderText="From Date" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblfrmDate" runat="server" Text=' <%# Eval("FROM_DATE", "{0:dd MMM yyyy}")%>' ></asp:Label>                                         
                                </ItemTemplate>

                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="To Date" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblToDate" runat="server" Text=' <%# Eval("TO_DATE", "{0:dd MMM yyyy}")%>' ></asp:Label>                                         
                                </ItemTemplate>

                            </asp:TemplateField>
                            
  <asp:TemplateField HeaderText="Taken" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblTAKEN" runat="server" Text='<%# Bind("TAKEN") %>'></asp:Label>                                               
                                </ItemTemplate>

                            </asp:TemplateField>
                                                                             
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="#330099" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#6699FF" Font-Bold="True" Font-Size="Small" ForeColor="#FFFFCC" />
                                        </asp:GridView>

                        </td>
                        <td style="width:30%">
                           <asp:GridView ID="grdLvBalance" runat="server" AutoGenerateColumns="False" ShowFooter="false"
                                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="4" Width="70%"  OnRowDataBound="grdLvBalance_RowDataBound">
                                            <FooterStyle BackColor="#D9E6FF" ForeColor="#330099" />
                                            <Columns>

                              <asp:TemplateField HeaderText="Sl" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>  


                            <asp:TemplateField HeaderText="Leave Type" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblLeaveType" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>                                         
                                </ItemTemplate>

                            </asp:TemplateField>     
                                                
                                                 
                           

                           <asp:TemplateField HeaderText="Total Taken" ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblTAKEN" runat="server" Text='<%# Bind("TAKEN") %>'></asp:Label>                                               
                                </ItemTemplate>

                            </asp:TemplateField>
                            

                                                                             
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="#330099" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#6699FF" Font-Bold="True" Font-Size="Small" ForeColor="#FFFFCC" />
                                        </asp:GridView>

                        </td>

                    </tr>

                </table>
                 


               
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

