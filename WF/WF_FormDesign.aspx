<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WF_FormDesign.aspx.cs" Inherits="WF_WF_FormDesign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(function () {
            $(".txtSort").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("排序必须是正整数");
                    $(this).val("");
                }
            });
        })

        function isInt(str) {
            var reg1 = /^\d+$/; 
            return reg1.test(str);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
            <table border="0" cellpadding="5" cellspacing="0" width="600">
                <tr>
                    <td align="left">
                         <asp:Button ID="btnSave" runat="server" CausesValidation="true" CssClass="SmallButton3"
                         Text="保存" Height="25px" onclick="btnSave_Click" />
                         <asp:Button ID="btnReturn" runat="server" CssClass="SmallButton3"
                         Text="返回" Height="25px" onclick="btnReturn_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvlist" runat="server" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle GridViewRebuild"  DataKeyNames="name,Sort_Order" Width="600px" OnRowDataBound="gvlist_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField HeaderText="显示">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkShow" runat="server" Checked='<%# check(Eval("isShow")) %>'/>
                    </ItemTemplate>
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类型">
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server" Text='<%# Bind("type")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="标签名称">
                    <ItemTemplate>
                        <asp:Textbox ID="txtLabel" runat="server" Text='<%# Eval("Label")%>'></asp:Textbox>
                    </ItemTemplate>
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="输入步骤">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlNode" runat="server">
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="必填">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkReq" runat="server"  Checked='<%# check(Eval("Required")) %>'/>
                    </ItemTemplate>
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="排序">
                    <ItemTemplate>
                        <asp:Textbox ID="txtSort" runat="server" Text='<%# Eval("Sort")%>' Width="30px" CssClass="txtSort"></asp:Textbox>
                    </ItemTemplate>
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
            <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
