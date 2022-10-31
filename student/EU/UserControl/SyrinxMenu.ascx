<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SyrinxMenu.ascx.cs"
    Inherits="EUPortalWeb.UserControl.SyrinxMenu" %>
<%@ Register Assembly="SyrinxMenu" Namespace="Syrinx.Gui.AspNet" TagPrefix="syx" %>
<style>
    .TestMMenu
    {
        background-color: #464646; /*Black;*/
        border-top: solid 1px gray;
        border-bottom: solid 1px gray;
    }
    .TestMMenuItem a
    {
        padding: 10px 12px 10px 12px;
        font-family: verdana;
        font-size: 10pt;
        text-decoration: none;
        color: White;
    }
    .TestMMenuItem a:hover
    {
        padding-bottom: 6px;
        border-bottom: solid 4px #F3B900;
        color: #F3B900;
    }
    .TestMSubMenu
    {
        background-color: #464646; /*Black;*/
        padding: 5px 0px 5px 0px;
    }
    .TestMSubMenuItem a
    {
        color: white;
        padding: 3px 10px 3px 10px;
        text-decoration: none;
        font-family: verdana;
        font-size: 10pt;
    }
    .TestMSubMenuItem a:hover
    {
        background-color: #F3B900;
    }
</style>
<div>
    <syx:Menu runat="server" ID="mnuMain" Orientation="Horizontal" MenuShowEffect="fade"
        CssClass="TestMMenu" ItemCssClass="TestMMenuItem" SubMenuCssClass="TestMSubMenu"
        SubMenuItemCssClass="TestMSubMenuItem" ExternalLinkDefaultTarget="_blank">
        <syx:MenuItem Text="Home" NavigateUrl="Home.aspx">
        </syx:MenuItem>
        <syx:MenuItem Text="Notice" NavigateUrl="NoticeSetup.aspx">
        </syx:MenuItem>
        <syx:MenuItem Text="News & Events" NavigateUrl="NewsAndEventsSetup.aspx">
        </syx:MenuItem>
        <syx:MenuItem Text="Message" NavigateUrl="MessageSetup.aspx">
        </syx:MenuItem>
        <syx:MenuItem Text="Welcome Message" NavigateUrl="WelcomeMessageSetup.aspx">
        </syx:MenuItem>
        <syx:MenuItem Text="Logout" NavigateUrl="Logout.aspx">
        </syx:MenuItem>
        
        <%--<syx:MenuItem Text="Administrative Setup" NavigateUrl="#">
            <syx:MenuItem Text="User Setup" NavigateUrl="UserSetup.aspx">
            </syx:MenuItem>
            <syx:MenuItem Text="Assign User Shop" NavigateUrl="AssignUserShop.aspx">
            </syx:MenuItem>
            <syx:MenuItem Text="Stock Issue" NavigateUrl="StockIssue.aspx">
            </syx:MenuItem>
            <syx:MenuItem Text="Shop Setup" NavigateUrl="ShopSetup.aspx">
            </syx:MenuItem>
            <syx:MenuItem Text="Stock Setup" NavigateUrl="StockSetup.aspx">
            </syx:MenuItem>
            <syx:MenuItem Text="Model Setup" NavigateUrl="ModelSetup.aspx">
            </syx:MenuItem>
            <syx:MenuItem Text="Sync Setup" NavigateUrl="SyncSetup.aspx">
            </syx:MenuItem>
            <syx:MenuItem Text="Tax Setup" NavigateUrl="TaxSetup.aspx">
            </syx:MenuItem>
            
        </syx:MenuItem>
        
        <syx:MenuItem Text="Reports" NavigateUrl="#">
            <syx:MenuItem Text="Sales Report" NavigateUrl="SalesReport.aspx" />
            <syx:MenuItem Text="Stock Issue Report" NavigateUrl="StockIssueReport.aspx" />
        </syx:MenuItem>
        <syx:MenuItem Text="Logout" NavigateUrl="Logout.aspx">
        </syx:MenuItem>--%>
    </syx:Menu>
</div>
