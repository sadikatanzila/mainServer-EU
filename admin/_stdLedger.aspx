<%@ Page Title="Student Ledger" Language="C#" MasterPageFile="~/employee/CommonStaffFacuty.master" AutoEventWireup="true" CodeFile="_stdLedger.aspx.cs" Inherits="admin_stdLedger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <div style="width:95%;  font-size: 9pt; font-family:Verdana; margin-left:-20px">
       <table style="width:80%">
           <tr>
               <td style="width:15%">

                    <asp:Label ID="Label1" runat="server" Text="SID :" style="font-weight: 700"></asp:Label>
               </td>
               <td style="width:35%">
                     <asp:Label ID="lblSID" runat="server" Text=""></asp:Label>

               </td>
                <td style="width:15%">

                       <asp:Label ID="Label3" runat="server" Text="Program :" style="font-weight: 700"></asp:Label>
              
               </td>
               <td style="width:35%">
                    <asp:Label ID="lblProgram" runat="server" Text=""></asp:Label>

               </td>
           </tr>


           <tr>
               <td style="width:15%">

                     <asp:Label ID="Label2" runat="server" Text="Name :" style="font-weight: 700"></asp:Label>
             


          

                   </td>
           
               <td style="width:35%">
                      <asp:Label ID="lblName" runat="server" Text=""></asp:Label></td>
                <td style="width:15%">

                        <asp:Label ID="Label4" runat="server" Text="Credit Rate :" style="font-weight: 700"></asp:Label>
              </td>
               <td style="width:35%">
                     <asp:Label ID="lblCRate" runat="server" Text=""></asp:Label></td>
           </tr>


       </table>
       <br /><br />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server" ></asp:PlaceHolder>
        &nbsp;
    
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

