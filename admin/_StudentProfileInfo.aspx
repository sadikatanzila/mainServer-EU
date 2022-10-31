<%@ Page Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_StudentProfileInfo.aspx.cs" Inherits="admin_noticeList"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
    AdvisorID<style type="text/css">
        .auto-style1 {
            width: 248px;
        }
        .auto-style2 {
            width: 13px;
            font-weight: bold;
        }
        .auto-style3 {
            font-weight: bold;
        }
        .auto-style4 {
            width: 250px;
            font-weight: bold;
            font-size:12px;
        }
        
        .auto-style6 {
             font-size:13px;
            width: 350px;
        }
    </style>
    
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
       <table border="0" style="width:95%">
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
                        <td  class="h" height="22" width="100%">
                            <p align="center">
                                <b><font  face="Arial" size="2"> Student Profile</font></b> <hr />

</p>
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
                        <td bgcolor="#ffffff" style="vertical-align: top; text-align: left" width="100%">
                            <table border="0" style="width: 100%">
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                            <br />
                                            <table style="width:75%">
                                                
                                                <tr>
                                                    <td class="auto-style1" colspan="2">
                                                        Student ID<span style="font-size: 10pt"><span style="color: #000000">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtSID" runat="server"  Width="150px"></asp:TextBox>
                                                     
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="View Profile" Width="149px" />
                                                         &nbsp;&nbsp;&nbsp;&nbsp;               <asp:Button ID="btn_Clear" runat="server" OnClick="btn_Clear_Click" Text="Clear" />
                                                                        
                                                        </span></span></td>
                                                    
                                                </tr>
                                                
                                                
                                                <tr>
                                                    <td class="auto-style1">
                                        <span style="font-size: 10pt"><span style="color: #000000">
                                                        <asp:Label ID="lblErrorMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        </span></span>
                                                    </td>
                                                    <td style="width: auto; height: auto">
                                                        &nbsp;</td>
                                                    
                                                </tr>
                                                
                                                
                                                </table>
                                       



                                        <hr />
                                        
                            
                                    </td>
                                </tr>
                                <tr>
                                    <td class="header" style="vertical-align: top; background-color: #ffffff; text-align: left">
                                        <asp:Label ID="lbl_message" runat="server" Font-Size="X-Small" ForeColor="Red" Text=""></asp:Label></td>
                                </tr>

                                 </table>
                        </td>
                        <td bgcolor="white" style="height: 114px" width="14">
                            &nbsp;
                            <p>
                              </p>
                        </td>
                        <td  class="k" style="height: 114px" width="1">
                            </td>
                    </tr>
                    </table>
            </td>
        </tr>
    </table>
    <br /><br />
    <table style="border: 1px solid #000000; width:80%; text-align: left;  font-size: 12px;">

       
<asp:DataList ID="dsStudentProfile" runat="server" style="margin-left: 29px" >

       <ItemTemplate>
          
        <tr>
            <td class="auto-style4">
                Student ID</td>

            <td class="auto-style2">

                :</td>

            <td   class="auto-style6">

                  <label> <%#Eval("SID") %> </label>

            </td>

            <td rowspan="9" class="auto-style4" style="vertical-align: top">
  <asp:Image ID="PicLoc1" runat="server"  ImageUrl='<%# Bind("PHOTO_LOCATION") %>' Width="150px"  AlternateText=" "/>   
                      
                </td>

        </tr>

        <tr>
            <td class="auto-style4">
                <asp:Label ID="lblName" runat="server" Text="Student Name"></asp:Label>

            </td>

            <td class="auto-style2">

               :</td>

            <td class="auto-style6">

                  <label> <%#Eval("SNAME") %> </label>


            </td>

        </tr>

        <tr>
            <td class="auto-style4">
                Program</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">

                   <label> <%#Eval("SPROGRAM") %> </label>(
                  <label> <%#Eval("NAME") %> </label>)
            </td>

        </tr>

        <tr>
            <td class="auto-style4">
                Year of Admission</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                 <label> <%#Eval("ADMINSEMETER_Name") %> </label> &nbsp;/  &nbsp;<label> <%#Eval("ADMINYEAR") %> </label>
                 </td>

        </tr>

        <tr>
            <td class="auto-style4">
                Date of Birth</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                 <asp:Label ID="DOB" runat="server" Text='<%# Eval("DOB", "{0:dd-MMM-yyyy}") %>'></asp:Label>  
              

            </td>

        </tr>

        <tr>
            <td class="auto-style4">
                Gender</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">

                 <label> <%#Eval("GENDERDTL") %> </label> </td>

        </tr>

        <tr>
            <td class="auto-style4">
                Nationality </td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">

                    <label> <%#Eval("NATIONALITY") %> </label></td>

        </tr>

        <tr>
            <td class="auto-style4">
                Blood Group</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">

                 <label> <%#Eval("BLOODGROUP") %> </label> </td>

        </tr>

        <tr>
            <td class="auto-style4">
               Marital Status</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">

                  <label> <%#Eval("MARITIALSTATUS") %> </label> </td>

        </tr>

        <tr>
            <td class="auto-style4">
               Father's Name</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">

                  <label> <%#Eval("SFNAME") %> </label> </td>

            <td >

               </td>

        </tr>

        <tr>
            <td class="auto-style4">
               Mother's Name</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">

                 <label> <%#Eval("SMNAME") %> </label></td>

            <td>

                &nbsp;</td>

        </tr>
                <tr>
            <td class="auto-style4">
                Email Address</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                  <label> <%#Eval("EMAIL") %> </label>
                  &nbsp;</td>

            <td>

                &nbsp;</td>

        </tr>
        <tr>
            <td class="auto-style4">
                Telephone No</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                  <label> <%#Eval("PHONE") %> </label>
                  &nbsp;</td>

            <td>

                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style4">
                Emergency Contact No</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                 <label> <%#Eval("SEPHONE") %> </label>
                  &nbsp;</td>

            <td>

                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style4">
                Mailing Address</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                 <label> <%#Eval("ADDRESS") %> </label>
                  &nbsp;</td>

            <td>

                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style4">
                Permanent Address</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                 <label> <%#Eval("SPADDRESS") %> </label>
                  &nbsp;</td>

            <td>

                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style3" colspan="4">
                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style3" colspan="4" style="font-size: large">
                SSC or Equivalent Result</td>

        </tr>

        <tr>
            <td class="auto-style4">
                School</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">

                  <label>
                  <%#Eval("SCHOOL") %> 
                  </label>
            </td>

            <td>

                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style4">
                Pass Year</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                 <label>
                  <%#Eval("SPASSYEAR") %> 
                  </label>
                  &nbsp;</td>

            <td>

                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style4">
                Marks or CGPA</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                <label>
                  <%#Eval("SGPA") %> 
                  </label>&nbsp;&nbsp;
                 <label>
                  <%#Eval("STMARKS") %> 
                  </label>
                  </td>

            <td>

                &nbsp;</td>

        </tr>
        <tr>
            <td class="auto-style3" colspan="4">
                &nbsp;</td>

        </tr>
          <tr>
            <td class="auto-style3" colspan="4" style="font-size: large">
                HSC or Equivalent Result</td>

        </tr>

              <tr>
            <td class="auto-style4">
                College</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                   <label>
                  <%#Eval("COLLEGE") %> 
                  </label>
                  &nbsp;</td>

            <td>

                &nbsp;</td>

        </tr>

              <tr>
            <td class="auto-style4">
                Pass Year</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                   <label>
                  <%#Eval("HPASSYEAR") %> 
                  </label>
                  &nbsp;</td>

            <td>

                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style4">
                Marks or CGPA</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                <label>
                  <%#Eval("HGPA") %> 
                  </label>&nbsp;&nbsp;
                   <label>
                  <%#Eval("HTMARKS") %> 
                  </label>
                  &nbsp;</td>

            <td>

                &nbsp;</td>

        </tr>
          <tr>
            <td class="auto-style3" colspan="4">
                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style3" colspan="4" style="font-size: large">
                Bachelor Degree</td>

        </tr>

        <tr>
            <td class="auto-style4">
                University / College</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">

              <label>
                  <%#Eval("BUNIVERSITY") %> 
                  </label>

            </td>

            <td>

                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style4">
                Pass Year</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                   <label>
                  <%#Eval("BYEAROFPASSING") %> 
                  </label>
                  &nbsp;</td>

            <td>

                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style4">
                Marks or CGPA</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                
                 <label>
                  <%#Eval("BMARKS") %> 
                  </label>  &nbsp; &nbsp;
                <label>
                  <%#Eval("BCLASS") %> 
                  </label> 
            </td>

            <td>

                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style4">
                Degree</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                <label>
                  <%#Eval("BDEGREE") %> 
                  </label> 
                  &nbsp;</td>

            <td>

                &nbsp;</td>

        </tr>
        <tr>
            <td class="auto-style3" colspan="4">
                &nbsp;</td>

        </tr>
          <tr>
            <td class="auto-style3" colspan="4" style="font-size: large">
                Masters Degree</td>

        </tr>

        <tr>
            <td class="auto-style4">
                University / College</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">

               <label>
                  <%#Eval("MUNIVERSITY") %> 
                  </label> 

            </td>

            <td>

                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style4">
                Pass Year</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                 <label>
                  <%#Eval("MYEAROFPASSING") %> 
                  </label> 
                  &nbsp;</td>

            <td>

                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style4">
                Marks or CGPA</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6"><label>
                  <%#Eval("MMARKS") %> 
                  </label> 
                
                  &nbsp;
                <label>
                  <%#Eval("MCLASS") %> 
                  </label></td>

            <td>

                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style4">
                Degree</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6"><label>
                  <%#Eval("MDEGREE") %> 
                  </label> 
                
                  &nbsp;</td>

            <td>

                &nbsp;</td>

        </tr>
            <tr>
            <td class="auto-style3" colspan="4">
                &nbsp;</td>

        </tr>
          <tr>
            <td class="auto-style3" colspan="4" style="font-size: large">
                Current Information</td>

        </tr>

              <tr>
            <td class="auto-style4">
                Advisor Name</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                   <label>
                  <%#Eval("ADVISOR_NAME") %> 
                  </label> &nbsp;
                (<label>
                  <%#Eval("AdvisorID") %> 
                  </label>)
                 </td>

            <td>

                &nbsp;</td>

        </tr>

              <tr>
            <td class="auto-style4">
                CGPA</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                   <label>
                  <%#Eval("FINAL_CGPA") %> 
                  </label>
                  &nbsp;</td>

            <td>

                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style4">
                Completed CH</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                <label>
                  <%#Eval("COMP_CHRS") %> 
                  </label></td>

            <td>

                &nbsp;</td>

        </tr>

            <tr>
            <td class="auto-style4">
                Required CH</td>

            <td class="auto-style2">

                :</td>

            <td class="auto-style6">
                <label>
                  <%#Eval("REQCR") %> 
                  </label></td>

            <td>

                &nbsp;</td>

        </tr>
        <tr>
            <td class="auto-style4">
               Major</td>

            <td class="auto-style2">

               :</td>

            <td class="auto-style5">

                  <label>
                  <%#Eval("MAJOR") %> 
                  </label></td></td>

            <td>

                &nbsp;</td>

        </tr>

        <tr>
            <td class="auto-style4">
                &nbsp;</td>

            <td class="auto-style2">

                &nbsp;</td>

            <td class="auto-style6">

                  &nbsp;</td>

            <td>

                &nbsp;</td>

        </tr>
        
 </ItemTemplate>
    </asp:DataList>
       
    </table>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

