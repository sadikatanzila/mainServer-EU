<%@ Page Language="C#" MasterPageFile="~/Online_Alumni/AlumniMaster.master" AutoEventWireup="true" 
CodeFile="_ConvoRegStdudent.aspx.cs" Inherits="admin_ConvoRegStdudent" Title="Registered Students View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_tracker" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_definition" Runat="Server">
    <div>
      <asp:Panel ID="pnlCheck" runat="server" >
    
  <!--
    <table style="width:90%; text-align: left;">
    <tr>
    <td colspan="3">
        <asp:Label ID="lblError" runat="server"  ForeColor="Red"></asp:Label>
    </td>
    </tr>
    <tr>
    <td style="width:25%">
        Convocation Year</td>
     <td style="width:2%">
         :</td>
     <td>
    
         <asp:TextBox ID="txtConvo" runat="server" Width="250px">2019</asp:TextBox>
    </td>
    </tr>
        <tr>
            <td style="width:25%">
                <asp:Label ID="lblDegree" runat="server" Text="Achieved Degree"></asp:Label>
            </td>
            <td style="width:2%"><b>:</b></td>
            <td>
                <asp:DropDownList ID="cmb_Degree" runat="server" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
    <tr>
    <td style="width:25%">
        <asp:Label ID="lblYear" runat="server" Text="Passing Year /Last Registered Year"></asp:Label>
    </td>
     <td style="width:2%;vertical-align: top">
    <b>:</b>
    </td>
     <td style="vertical-align: top">
    
         <asp:TextBox ID="txtYear" runat="server" Width="250px"></asp:TextBox>
    
    </td>
    </tr>
    <tr>
    <td style="width:25%">
        <asp:Label ID="lblSem" runat="server" Text="Passing Semister /Last Registered Semister"></asp:Label>
        </td>
     <td style="width:2%;vertical-align: top">
         <b>:</b></td>
     <td style="vertical-align: top">
    
          <asp:DropDownList ID="cmb_semester" runat="server"  Width="250px">
    <asp:ListItem Value="1">Spring</asp:ListItem>
    <asp:ListItem Value="2">Summer</asp:ListItem>
    <asp:ListItem Value="3">Fall</asp:ListItem>
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
                <a href="http://webportal.easternuni.edu.bd/Convocation/_ConvChkInfo.aspx" style="font-weight: bold; color:Blue;">Sign up for Convocation Registration</a>
               
            </td>
        </tr>
        <tr>
            <td colspan="3">
                For Technical Support please contact with <strong>mamun@easternuni.edu.bd</strong>
                or <strong>sadika@easternuni.edu.bd</strong></td>
        </tr>
    </table>
    -->
      </asp:Panel>
    <br /> 
    <table style="width:98%">
    <tr>
    <td style="width:50%">
    <a href="http://webportal.easternuni.edu.bd/Convocation/_ConvChkInfo.aspx" style="font-weight: bold; color:Blue;" >Sign up for Convocation Registration</a>
       
    </td>
    <td style="width:50%; text-align:right">
     <asp:Label ID="lblTotal" runat="server" Text="" Style="color: #a6a7a8;"  Font-Bold="true" Visible="false"></asp:Label>
       
    </td>
    </tr>
    </table>
        
             <br />
    
        <asp:DataList ID="DataList1" runat="server" BackColor="white" BorderColor="white"
            BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0" 
            Font-Names="Verdana" Font-Size="Small" GridLines="Both" RepeatColumns="7"
             RepeatDirection="Horizontal" Width="98%" >
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" Font-Size="Large" ForeColor="White"
                HorizontalAlign="Center" VerticalAlign="Middle" Height="30px"/>
            <HeaderTemplate>
               Registered Students View</HeaderTemplate>
            <ItemStyle BackColor="White" ForeColor="Black" BorderWidth="1px" VerticalAlign="Top" />
            <ItemTemplate> 
              
              <asp:Image ID="Image2" runat="server" ImageUrl='<%# Bind("PICTURE") %>' Width="150px" Height="200px"  />
                  <br />     
              <b>Student ID:</b>     <asp:Label ID="lblSID" runat="server" Text='<%# Bind("SD") %>' Visible="true"></asp:Label>                         
               <br />
                <b>Name:</b>
                
                
                <asp:Label ID="lblName" runat="server" Text='<%# Bind("SNAME") %>'></asp:Label>
                           
                <br/>
                         
                <b>Program:</b>
                
                <asp:Label ID="lblDeg2" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
               <br />
               
              
               <b> Guest Info:</b>
                <asp:Label ID="lblCity" runat="server" Text=' <%# Bind("GUEST_INFO") %>'></asp:Label>
                <br />
              <b> Dual Degree Status:</b>
                <asp:Label ID="lblDegree" runat="server" Text=' <%# Bind("Duel_status") %>'></asp:Label>
                <br />
                
              
            
            
            </ItemTemplate>
        </asp:DataList>
    </div>   
    <div style="background-color:gray; text-align:center;font-weight:bold; width:95%; font-size:12px; font-family:Verdana"></div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_content" Runat="Server">
</asp:Content>

