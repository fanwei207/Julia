<%@ Page Language="C#" AutoEventWireup="true" CodeFile="nwf_workflowInstanceDetail.aspx.cs" Inherits="NWF_nwf_workflowInstanceDetail" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.workflow.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#gvDet input[type='checkbox'][id$='chkAll']").click(function () {
                $("#gvDet input[type='checkbox'][id$='chk']").prop("checked", $(this).prop("checked"));
                $("#gvDet input[type='checkbox'][id$='chk']").each(GetSelectedIndex);
            })
            $("#gvDet input[type='checkbox'][id$='chk']").click(GetSelectedIndex)
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
    </script>
    <style type="text/css">
        .style3
        {
            width: 82px;
        }
        .style4
        {
            width: 85px;
        }
        .style5
        {
            width: 397px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="hidCheck" runat="server"  Value=";"/>
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
         <table cellpadding="0" cellspacing="0" style="text-align: left" width="100%">
         <tr>
             <td align="right" class="style4">
             关键字
             </td>
             <td class="style5">
                 <asp:TextBox ID="txtCondition" runat="server" Width="379px"></asp:TextBox>(*)
             </td>
             <td>
             <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" onclick="btnSearch_Click" />
<%--             <asp:Button ID="btnBack" runat="server" Text="退回" CssClass="SmallButton2" 
                     onclick="btnBack_Click"/>--%>
             </td>
             <td class="style3">          
                 <asp:LinkButton ID="linkDownload" runat="server" onclick="linkDownload_Click">下载源数据</asp:LinkButton>
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
                        <Columns>
                           <%--<asp:TemplateField>
                                <HeaderTemplate>
                                     <input id="chkAll" type="checkbox">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server"/>
                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>--%>
                        </Columns>
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
