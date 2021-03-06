<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.itemUsedList" CodeFile="itemUsedList.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="1" cellpadding="1" width="900" align="center" bgcolor="white"
            border="0">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" CssClass="LabelCenter">请输入需要查询的材料</asp:Label>
                    <asp:TextBox ID="txtItem" runat="server" Width="320px"></asp:TextBox>
                    <asp:Button ID="BtnAdd" runat="server" CssClass="SmallButton3" Width="50" Text="添加">
                    </asp:Button>
                </td>
                <td align="right">
                    导出类别:
                    <asp:RadioButton ID="radAll" runat="server" Text="所有" GroupName="AllNoStop"></asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="radNoStop" runat="server" Checked="True" Text="不包括停用" GroupName="AllNoStop">
                    </asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnExport" CssClass="smallbutton3" runat="server" Text="导出"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnClean" CssClass="smallbutton3" runat="server" Text="清除"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgUse" runat="server" Width="900px" CssClass="GridViewStyle AutoPageSize"
            AutoGenerateColumns="False" AllowPaging="True">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="itemID" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="gsort" HeaderText="<b>序号</b>">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="code" HeaderText="<b>查询显示用到以下的部件或半成品代码的产品</b>">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
          <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
