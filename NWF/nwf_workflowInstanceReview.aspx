<%@ Page Language="C#" AutoEventWireup="true" CodeFile="nwf_workflowInstanceReview.aspx.cs" Inherits="NWF_nwf_workflowInstanceReview" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js?ver=20160203" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.workflow.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("input[name='gvDet$ctl01$chkAll']:eq(1)").remove();
            $("#gvDet_ctl01_chkAll").click(function () {
                $("#gvDet input[type='checkbox'][id$='chk']").prop("checked", $(this).prop("checked"));
                $("#gvDet input[type='checkbox'][id$='chk']").each(GetSelectedIndex);

                event.stopPropagation();
            })
            $("#gvDet input[type='checkbox'][id$='chk']").change(GetSelectedIndex)
        })

        var GetSelectedIndex = function () {
            var index = $(this).attr("id").replace("gvDet_ctl", "").replace("_chk", "");
            if (index.indexOf("0") == 0) {
                index = index.substr(1, index.length - 1);
            }
            index = parseInt(index) - 2;
            if ($(this).prop("checked")) {
                if ($("#hidCheck").val().toString().indexOf(";" + index + ";") == -1) {
                    $("#hidCheck").val($("#hidCheck").val() + index + ";");
                }
            }
            else {
                $("#hidCheck").val($("#hidCheck").val().replace(";" + index + ";", ";"));
            }
        }

        function callMethod() 
        {
           <%-- "<%#BindData()%>";--%>
        }

        function refreshpage() 
        {
            $("#btnSearch").click();
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 398px;
        }
        .style3
        {
            width: 82px;
        }
        .style4
        {
            width: 85px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <asp:HiddenField ID="hidCheck" runat="server"  Value=";"/>
        <table cellpadding="0" cellspacing="0" style="text-align: left" width="100%">
         <tr>
             <td colspan="4" id="tdWorkFlow" runat="server">
             流程类型
            <asp:DropDownList ID="ddlWorkFlow" runat="server" DataTextField="Flow_name" 
            DataValueField="Flow_Id"  AutoPostBack="true"
            onselectedindexchanged="ddlWorkFlow_SelectedIndexChanged"></asp:DropDownList>
             </td>
         </tr>
         <tr>
             <td colspan="4">
        <asp:Menu ID="menuNode" runat="server" BackColor="#E3EAEB" DynamicHorizontalOffset="2"
         Font-Names="Verdana" ForeColor="Black" Orientation="Horizontal"
         StaticSubMenuIndent="10px" Style="position: relative" Width="100%" 
            OnMenuItemClick="menuNode_MenuItemClick">
         <StaticSelectedStyle BackColor="#b8d2f0" />
         <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
         <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
         <DynamicMenuStyle BackColor="#E3EAEB" />
         <DynamicSelectedStyle BackColor="#b8d2f0" />
         <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
         <StaticHoverStyle BackColor="#666666" ForeColor="White" />
         <Items>
         </Items>
         </asp:Menu>
         </td>
         </tr>
         <tr>
             <td align="right" class="style4">
             关键字
             </td>
             <td>
                 <asp:TextBox ID="txtCondition" runat="server" Width="379px"></asp:TextBox>(*)
             </td>
             <td>
             <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" onclick="btnSearch_Click" />
             </td>
         </tr>         
         <tr>
             <td align="right" class="style4">
             上传数据
             </td>
             <td class="style1">
                 <input id="filename" runat="server" name="filename" 
                     style=" width:400px; height: 24px;" type="file" /></td>
             <td class="style3">          
                 <asp:LinkButton ID="linkDownload" runat="server" onclick="linkDownload_Click">下载源数据</asp:LinkButton>
             </td>
             <td >
                 <asp:Button ID="btnUpload" runat="server" Text="上传" CssClass="SmallButton2" onclick="btnUpload_Click" />&nbsp
                 <asp:Button ID="btnPass" runat="server" Text="通过" CssClass="SmallButton2" 
                 onclick="btnPass_Click"/>&nbsp
                 <asp:Button ID="btnFailed" runat="server" Text="拒绝" CssClass="SmallButton2" 
                         onclick="btnFailed_Click"/>&nbsp
                 <%--<asp:Button ID="btnBack" runat="server" Text="退回" CssClass="SmallButton2"/>--%>
             </td>
         </tr>
         <tr>
         <td colspan="4">
         <asp:GridView ID="gvDet" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle GridViewRebuild" EmptyDataText="No data" 
                 onpageindexchanging="gvDet_PageIndexChanging">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                    </asp:GridView>
         </td>
         </table>
         
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
    </form>
</body>
</html>
