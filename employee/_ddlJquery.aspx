<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_ddlJquery.aspx.cs" Inherits="employee_ddlJquery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>jQuery Cascading Dropdown Example</title>
<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
<tr>
<td>Country</td>
<td>
<asp:DropDownList ID="ddlcountries" runat="server"></asp:DropDownList>
</td>
</tr>
<tr>
<td>State</td>
<td>
<asp:DropDownList ID="ddlstate" runat="server"></asp:DropDownList>
</td>
</tr>
<tr>
<td>Region</td>
<td>
<asp:DropDownList ID="ddlcity" runat="server"></asp:DropDownList>
</td>
</tr>
</table>
    </div>
    </form>

    <script type="text/javascript">
        $(function () {
            $('#<%=ddlstate.ClientID %>').attr('disabled', 'disabled');
    $('#<%=ddlcity.ClientID %>').attr('disabled', 'disabled');
    $('#<%=ddlstate.ClientID %>').append('<option selected="selected" value="0">Select State</option>');
    $('#<%=ddlcity.ClientID %>').empty().append('<option selected="selected" value="0">Select Region</option>');
    $('#<%=ddlcountries.ClientID %>').change(function () {
        var country = $('#<%=ddlcountries.ClientID%>').val()
    $('#<%=ddlstate.ClientID %>').removeAttr("disabled");
    $('#<%=ddlcity.ClientID %>').empty().append('<option selected="selected" value="0">Select Region</option>');
    $('#<%=ddlcity.ClientID %>').attr('disabled', 'disabled');
    $.ajax({
        type: "POST",
        url: "jQueryCascadingDropdownExample.aspx/BindStates",
        data: "{'country':'" + country + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var j = jQuery.parseJSON(msg.d);
            var options;
            for (var i = 0; i < j.length; i++) {
                options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>'
            }
            $('#<%=ddlstate.ClientID %>').html(options)
},
    error: function (data) {
        alert('Something Went Wrong')
    }
});
});
    $('#<%=ddlstate.ClientID %>').change(function () {
        var stateid = $('#<%=ddlstate.ClientID%>').val()
    $('#<%=ddlcity.ClientID %>').removeAttr("disabled");
    $.ajax({
        type: "POST",
        url: "jQueryCascadingDropdownExample.aspx/BindRegion",
        data: "{'state':'" + stateid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var j = jQuery.parseJSON(msg.d);
            var options;
            for (var i = 0; i < j.length; i++) {
                options += '<option value="' + j[i].optionValue + '">' + j[i].optionDisplay + '</option>'
            }
            $('#<%=ddlcity.ClientID %>').html(options)
},
    error: function (data) {
        alert('Something Went Wrong')
    }
});
})
})
</script>
</body>
</html>
