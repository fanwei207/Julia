<%@ Page Language="C#" AutoEventWireup="true" CodeFile="nwf_workflowInstanceResult.aspx.cs" Inherits="NWF_nwf_workflowInstanceResult" %>

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
        .style4
        {
            width: 85px;
        }
        .style5
        {
            width: 398px;
        }
        .style6
        {
            width: 76px;
        }
        .style7
        {
            width: 50px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>        
        
        <asp:HiddenField ID="hidCheck" runat="server"  Value=";"/>
         <table cellpadding="0" cellspacing="0" style="text-align: left" width="100%">
         <tr>
             <td colspan="5" id="tdWorkFlow" runat="server">
             流程类型
            <asp:DropDownList ID="ddlWorkFlow" runat="server" DataTextField="Flow_name" 
            DataValueField="Flow_Id"  AutoPostBack="true"
            onselectedindexchanged="ddlWorkFlow_SelectedIndexChanged"></asp:DropDownList>
             </td>
         </tr>
         <tr>
             <td align="right" class="style4">
             关键字
             </td>
             <td class="style5">
                 <asp:TextBox ID="txtCondition" runat="server" Width="379px"></asp:TextBox>(*)
             </td>
             <td align="right" class="style7">
             状态
             </td>
             <td class="style6">
                 <asp:DropDownList ID="ddlStatus" runat="server" style="margin-left: 0px">
                 <asp:ListItem Text="--" Value="" Selected="True"></asp:ListItem>
                 <asp:ListItem Text="进行中" Value="1"></asp:ListItem>
                 <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                 <asp:ListItem Text="已拒绝" Value="-1"></asp:ListItem>
                 <asp:ListItem Text="未完成" Value="0"></asp:ListItem>
                 <asp:ListItem Text="已取消" Value="-2"></asp:ListItem>
                 </asp:DropDownList>
             </td>
             <td>
             <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" onclick="btnSearch_Click" />
             <asp:Button ID="btnExport" runat="server" Text="导出" CssClass="SmallButton2" onclick="btnExport_Click" />
             </td>
         </tr>         
         <tr>
         <td colspan="5">
         <asp:GridView ID="gvDet" runat="server" Width="100%" 
                 AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle GridViewRebuild" EmptyDataText="No data" 
                 onpageindexchanging="gv_PageIndexChanging" >
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
