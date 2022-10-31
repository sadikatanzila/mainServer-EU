<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_PrintAdmitCard.aspx.cs"    Inherits="student_AdmitCard_PrintAdmitCard" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"><html xmlns="http://www.w3.org/1999/xhtml"><head runat="server">    <title>Admit Card</title></head><body>
  

    <form id="form1" runat="server">
    <div style="padding-left: 10px; padding-right: 10px; padding-top:10px">
    
    <asp:Panel ID="pnlExamCourse" runat="server" Visible="false">
   
    <table style="width:100%">
    <tr>
    <td colspan="2">
      <h2 style="text-align:center">
                    
        <asp:Label ID="lblEU" runat="server" Text="Eastern University"></asp:Label>
       </h2>
    </td>
    </tr>
    
        <tr>
            <td colspan="2">
              
                &nbsp;</td>
        </tr>
    
        <tr>
            <td style="width:80%">
                <h3 style="text-align:center">
                    <asp:Label ID="lblAdmit" runat="server" Text="Admit Card"></asp:Label>
                </h3>
                <h4 style="text-align:center">
                    <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                </h4>
            </td>
            <td>
              <asp:Image ID="img_myPicture" runat="server" Height="136px" Width="130px" /><br />
                
            </td>
        </tr>
    
    </table>
       
        <table style="width:90%; margin-left:50px">
            <tr>
                <td style="width: 13%">
                   ID:
                </td>
                <td style="padding-left: 10px;width: 37%">
                    <asp:Label ID="lblStudentId" runat="server" Text=""></asp:Label>
                </td>


                <td style="padding-left: 10px; width: 13%">
                     Faculty:
                </td>
                <td style="padding-left: 10px;width: 37%">
                    <asp:Label ID="lblFaculty" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 13%">
                   Name:
                </td>
                <td style="padding-left: 10px;width: 37%">
                    <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                </td>
                <td style="padding-left: 10px; width: 13%">
                    Program:</td>
                <td style="padding-left: 10px;width: 37%">
                    <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td style="padding-left: 10px" colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                   
                </td>
                <td style="padding-left: 10px" colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">
                    <div style="line-height:50px">
                        <strong>Exam Course List :</strong>
                    </div>
                    <asp:GridView RowStyle-Height="25" Width="99%"
                    ID="GridView_courseList" runat="server" AutoGenerateColumns="False">
                        <Columns>
                                       
                            <asp:BoundField DataField="COURSECODE" HeaderText="Course Code" ItemStyle-Width="20%">
                            <ItemStyle HorizontalAlign="Center" />
                              </asp:BoundField>
                            <asp:BoundField DataField="cName" HeaderText="Course" ItemStyle-Width="60%">
                              <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CHOURS" HeaderText="Credit Hour" ItemStyle-Width="20%">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                                    </Columns>
                    </asp:GridView>
                    
					
                   
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" >
                   
                   <strong style="text-decoration: underline"> Instruction for Examinees:</strong><br />
                    <div style="margin-left:25px">
                    * Examinees should enter the examination room/ hall before 10 minutesof starting 
                    of the examination.<br />
                    * No examinees shall be allowed to sit the examination without Admit Card.<br />
                   * No Examinees shall be allowed to carry any papers expect Admit Card.<br />
                    * Cellular phone will not be allowed in the examination room/ hall.<br />
                    </div>
                    </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 37px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" style="height: 21px">
                </td>
            </tr>
            <tr>
                <td colspan="4" style="width: 100%">
                    <div style="width: 100%">
                       <div style="float: left; width: 50%; padding-left: 10px; margin-top: 1px; border-top-style: solid; border-top-width: 2px; border-top-color: #000000;">
                            <strong>Signature By the Controller of Examination</strong></div>
                    </div>
                </td>
            </tr>
        </table>
        
         </asp:Panel>
    </div>
    </form>
</body>
</html>





