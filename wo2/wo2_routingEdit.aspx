<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_routingEdit.aspx.cs"
    Inherits="wo2_wo2_routingEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div align="center">
        <table style="width: 1004px">
            <tr>
                <td>
                    工艺名称<asp:TextBox ID="txbRouting" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    大工序<asp:TextBox ID="txbMop" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                    
                </td>
                <td>
                    加工时间<asp:TextBox ID="txbRun" runat="server" CssClass="SmallTextBox" 
                        Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="BtnSearch" runat="server" CssClass="SmallButton3" Text="查询" Width="60px"
                        OnClick="BtnSearch_Click" />&nbsp;
                    <asp:Button ID="BtnAdd" runat="server" CssClass="SmallButton3" Text="新增" Width="60px"
                        ValidationGroup="chkAll" CausesValidation="true" OnClick="BtnAdd_Click" />&nbsp;
                    <asp:Button ID="BtnModify" runat="server" CssClass="SmallButton3" Text="修改" Width="60px"
                        ValidationGroup="chkAll" CausesValidation="true" OnClick="BtnModify_Click" />
                    <asp:Label ID="LblRID" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvRouting" runat="server" AllowPaging="True" PageSize="22" AutoGenerateColumns="False"
            DataKeyNames="ID" Width="984px" OnRowCommand="gvRouting_RowCommand" OnPageIndexChanging="gvRouting_PageIndexChanging"
            OnRowDataBound="gvRouting_RowDataBound" 
            CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField DataField="routing" HeaderText="工艺名称">
                    <ItemStyle HorizontalAlign="left" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="mop" HeaderText="大工序">
                    <ItemStyle HorizontalAlign="left" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="mopname" HeaderText="工序描述">
                    <ItemStyle HorizontalAlign="left" Width="200px" />
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="run" HeaderText="加工时间">
                    <ItemStyle HorizontalAlign="right" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:ButtonField CommandName="Edit1" Text="<u>编辑</u>">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:ButtonField CommandName="Del1" Text="<u>删除</u>">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
