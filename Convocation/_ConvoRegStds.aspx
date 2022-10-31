<%@ Page Language="C#" MasterPageFile="~/Convocation/ConvoMaster.master" AutoEventWireup="true" CodeFile="_ConvoRegStds.aspx.cs"
 Inherits="Convocation_ConvoRegStds" Title="5th Convocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
  <div>
      <table style="width:90%; text-align: left;">
    <tr>
    <td colspan="3">
        <asp:Label ID="lblError" runat="server"  ForeColor="Red"></asp:Label>
    </td>
    </tr>
    <tr>
    <td style="width:25%">
        <asp:Label ID="lblFaculty" runat="server" Text="Faculty"></asp:Label>
        </td>
     <td style="width:2%">
       <b>:</b></td>
     <td>
    
         <asp:DropDownList ID="ddlFaculty" runat="server" Width="250px" AutoPostBack="true" 
             onselectedindexchanged="ddlFaculty_SelectedIndexChanged">
    </asp:DropDownList>
        </td>
    </tr>
    <tr>
    <td style="width:25%">
        <asp:Label ID="lblDegree" runat="server" Text="Program"></asp:Label>
    </td>
     <td style="width:2%">
        <b>:</b></td>
     <td>
    
         <asp:DropDownList ID="ddlDept" runat="server" Width="250px">
    </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td style="width:25%">
        &nbsp;</td>
     <td style="width:2%">
         &nbsp;</td>
     <td>
    
          <asp:Button ID="btnSearch" runat="server" Text="Search" Width="150px" 
              onclick="btnSearch_Click" />
              
               <asp:Button ID="btnCancel" runat="server" Text="View All" Width="150px"  
              style="margin-left:75px" onclick="btnCancel_Click" />
</td>
    </tr>
        <tr>
            <td style="width: 25%">
            </td>
            <td style="width: 2%">
            </td>
            <td>
               
            </td>
        </tr>
        <tr>
            <td colspan="3">
              </td>
        </tr>
    </table>
  
           <br /><br />    
               <table style="width:95%; text-align:left; margin-left:25px">
               <tr>
               <td style="width:50%; font-size:large">
                   <asp:Panel ID="pndReg" runat="server" Height="50px" Width="125px" Visible="false">
                     For Technical Support please contact with <strong>mamun@easternuni.edu.bd</strong>
                or <strong>sadika@easternuni.edu.bd</strong>
                
                
                    <a href="http://webportal.easternuni.edu.bd:8080/Convocation/_ConvChkInfo.aspx" 
                       style="font-weight: bold; color:Blue;" >Sign up for 5th Convocation Registration</a>
                   </asp:Panel>
              
               
               
             
               </td>
               <td style="text-align:right; margin-left:25px">
                <asp:Label ID="lblTotal" runat="server" Text="" Style="color: #a6a7a8;margin-left:100px"  Font-Bold="true"  Visible="false"></asp:Label>
                                                      
               </td>
               </tr>
               </table>
    <br />
        <asp:DataList ID="DataList1" runat="server" BackColor="white" BorderColor="white"
            BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0" 
            Font-Names="Verdana" Font-Size="Small" GridLines="Both" RepeatColumns="7"
             RepeatDirection="Horizontal" Width="95%">
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" Font-Size="Large" ForeColor="White"
                HorizontalAlign="Center" VerticalAlign="Middle" Height="30px"/>
            <HeaderTemplate>
              Registered Students List for 5th Convocation</HeaderTemplate>
            <ItemStyle BackColor="White" ForeColor="Black" BorderWidth="1px" VerticalAlign="Top" />
            <ItemTemplate> 
              
              <asp:Image ID="Image2" runat="server" ImageUrl='<%# Bind("PICTURE") %>' Width="120px" Height="163px"  />
                  <br />     
              <b>Student ID:</b>     <asp:Label ID="lblSID" runat="server" Text='<%# Bind("SID") %>' Visible="true"></asp:Label>                         
               <br />
                <b>Name:</b>              
                
                <asp:Label ID="lblName" runat="server" Text='<%# Bind("SNAME") %>'></asp:Label>
                <br />           
                        
                <b>Program:</b>
                
                <asp:Label ID="lblDeg2" runat="server" Text='<%# Bind("SPROGRAM") %>'></asp:Label>
              
                
              
            
            
            </ItemTemplate>
        </asp:DataList>
    </div>   
    <div style="background-color:gray; text-align:center;font-weight:bold; width:95%; font-size:12px; font-family:Verdana"></div>
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

