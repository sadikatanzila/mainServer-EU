<%@ Page Language="C#" MasterPageFile="~/staffs/courses/_courses_master.master" AutoEventWireup="true" CodeFile="_attendanceSheet.aspx.cs" Inherits="staffs_courses_attendanceSheet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
   
    <script type="text/javascript" language="javascript">
function loadCalender()
{
  window.open('../html/CalenderaTT.htm','','titlebar=no,toolbar=no,scrollbars=false,resizable=false,height=170,width=300');
  return false;
}  

function save_check()
{ 
    if(document.getElementById('ctl00_ContentPlaceHolder_content_txt_date').value=="")
    {
        alert("Please enter date.");
        return false;
    }
    
    else return true;
    
}


</script>

       

   
   
    <table style="width: 95%">
        <tr>
            <td class="header" style="color: #ffa500; height: 19px; background-color: white;
                text-align: left">
                <table border="0" style="text-align: left">
                    <tr>
                        <td colspan="3" style="width: 526px; text-align: left">
                            <span style="color: #ffa500">Your location- Courses &gt; Course List &gt; Course Details
                                &gt; Class Attendance Sheet </span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="header" style="height: 44px; background-color: #ffffff; text-align: left">
                <table style="width: 80%">
                    <tr>
                        <td style="width: 80%; text-align: left">
                            Select Course
                            <asp:DropDownList ID="cmb_course" Width="250px" runat="server" >
                            </asp:DropDownList>
                           &nbsp;   &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit" /></td>
                    </tr>
                    <tr>
                        <td style="width: 80%; text-align: left">
                            <asp:Label ID="lblCourseKey" runat="server" Text="" Visible="false"></asp:Label>
                              <asp:Label ID="lblGGROUP" runat="server" Text="" Visible="false"></asp:Label>

                              <hr />
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 80%; text-align: left">
                           <p style="text-align:right; width:90%">

    <asp:Label ID="Label1" runat="server" Text="Print :" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <span style="font-size: 10pt"><span style="color: #000000">
<asp:ImageButton ID="Img1" Height="50px" ImageUrl="~/Images/PDF.png"
runat="server" onclick="Img1_Click" Visible="true"  />
    
                                                           
                                        </span>

</p></td>
                    </tr>
                    <tr>
                        <td style="width: 80%; text-align: left">
                         
                                                     <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" Visible="true"
        BestFitPage="False" ToolPanelView="None" />       </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
    
</asp:Content>

