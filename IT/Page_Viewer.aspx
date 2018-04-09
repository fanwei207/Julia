<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Page_Viewer.aspx.cs" Inherits="Page_Viewer" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(function () {
            $("input[name='gvDet$ctl01$chkAll']:eq(1)").remove();

            $("#gv_ctl01_chkAll").click(function () {
                $("#gv input[type='checkbox'][id$='chkItem']").prop("checked", $(this).prop("checked"));
                $("#gv input[type='checkbox'][id$='chkItem']").each(GetSelectedIndex);

                event.stopPropagation();
            })
            $("#gv input[type='checkbox'][id$='chkItem']").change(GetSelectedIndex);






        })

        var GetSelectedIndex = function () {
            var index = $(this).attr("id").replace("gv_ctl", "").replace("_chkItem", "");
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
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
            <input id="hidPageID" runat="server" type="hidden" />
            <input id="hidBackID" runat="server" type="hidden" />
            <input id="hidDB" runat="server" type="hidden" />
            <input id="hidTable" runat="server" type="hidden" />
            <input id="hidPassProc" type="hidden" runat="server" />
            <input id="hidRefuseProc" type="hidden" runat="server" />
            <input id="hidCheck" type="hidden" runat="server" value=";" />
            <table id="tblQuery" runat="server" cellspacing="0" cellpadding="0" class="main_top">
                <tr>
                    <td class="main_left">&nbsp;
                    </td>
                    <td style="text-align: right;">
                        <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" OnClick="btnQuery_Click"
                            TabIndex="0" Text="Query" Width="40px" />
                        &nbsp;<asp:Button ID="btnBack" runat="server" CssClass="SmallButton3" OnClick="btnBack_Click"
                            TabIndex="0" Text="Back" Width="40px" Visible="False" />
                        &nbsp;<asp:Button ID="btnRefuse" runat="server" CssClass="SmallButton3" OnClick="btnRefuse_Click"
                            TabIndex="0" Text="拒绝" Visible="False" Width="40px" />
                        <asp:Button ID="btnPass" runat="server" CssClass="SmallButton3" OnClick="btnPass_Click"
                            TabIndex="0" Text="同意" Visible="False" Width="40px" />
                        &nbsp;<asp:Button ID="btnNew" runat="server" CssClass="SmallButton3" OnClick="btnNew_Click"
                            TabIndex="0" Text="新增" Width="40px" />
                        &nbsp;<asp:Button ID="btnExport" runat="server" CssClass="SmallButton3" OnClick="btnExport_Click"
                            TabIndex="0" Text="Excel" Width="40px" />
                    </td>
                    <td class="main_right"></td>
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
                OnRowCancelingEdit="gv_RowCancelingEdit" OnRowDeleting="gv_RowDeleting" OnRowEditing="gv_RowEditing"
                OnRowUpdating="gv_RowUpdating" OnRowDataBound="gv_RowDataBound" PageSize="20"
                AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                CaptionAlign="Top">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table3" Width="100%" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="Owner" Width="100%" HorizontalAlign="center">No Data</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                        EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                        <HeaderStyle Width="70px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:CommandField>
                </Columns>
                <PagerStyle CssClass="GridViewPagerStyle" />
            </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        $(function(){
        
            $("#tblQuery").width($("#gv").width());

            //用window打开
            $("span[pagemaker-src]").click(function(){
                var _href = $(this).attr("pagemaker-src");
                var _title = $(this).attr("pagemaker-linkTitle");
                $.window(_title, 900, 600, _href, "", true);
            });

            //弹出confirm窗口 pagemaker-confirm
            $("A:has(u)").click(function(){
                if($("u", $(this)).hasClass("pagemaker-confirm")){
                    if(!confirm("继续操作？")){
                        $.loading("none");
                        return false;
                    }
                }
            });
        })

        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
