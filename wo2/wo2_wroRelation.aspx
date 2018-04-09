<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_wroRelation.aspx.cs"
    Inherits="wo2_wo2_wroRelation" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="width: 762px; height: 30px; margin: 0 auto; background-image: url(../images/banner01.jpg);">
            <tr>
                <td style="width: 20%; height: 26px;">
                    <asp:Label ID="Label1" runat="server" Text="父工序"></asp:Label>
                    <asp:TextBox ID="txbParent" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td style="width: 25%; height: 26px;">
                    <asp:Label ID="Label3" runat="server" Text="名称"></asp:Label>
                    <asp:TextBox ID="txbParentName" runat="server" CssClass="SmallTextBox" Width="130px"></asp:TextBox>
                </td>
                <td style="width: 30%; height: 26px;">
                    <asp:Label ID="Label2" runat="server" Text="子工序"></asp:Label>
                    <asp:DropDownList ID="dropChild" runat="server" Width="166px">
                    </asp:DropDownList>
                </td>
                <td style="width: 10%; height: 26px;">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" Width="70px"
                        OnClick="btnSearch_Click" />
                </td>
                <td style="height: 26px">
                    <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="SmallButton2" Width="70px"
                        OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvWo2Relation" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            AllowPaging="True" AllowSorting="True" PageSize="25" OnRowCommand="gvWo2Relation_RowCommand"
            OnPageIndexChanging="gvWo2Relation_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="640px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="父工序" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="父工序名称" Width="200px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="子工序" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="子工序名称" Width="200px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="创建人" Width="80px" HorizontalAlign="Center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="wro_id" Visible="False" HeaderText="ID"></asp:BoundField>
                <asp:BoundField DataField="parentCode" HeaderText="父工序">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="parentName" HeaderText="父工序名称">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="180px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="180px" />
                </asp:BoundField>
                <asp:BoundField DataField="childCode" HeaderText="子工序">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="childName" HeaderText="子工序名称">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="180px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="180px" />
                </asp:BoundField>
                <asp:BoundField DataField="createdName" HeaderText="创建人">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkadd" Text="<u>子工序</u>" runat="server" CommandArgument='<%# Eval("wro_id") %>'
                            CommandName="myAdd" />
                    </ItemTemplate>
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkedit" Text="<u>编辑</u>" runat="server" CommandArgument='<%# Eval("parentCode") %>'
                            CommandName="myEdit" />
                    </ItemTemplate>
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkdelete" Text="<u>删除</u>" runat="server" CommandArgument='<%# Eval("wro_id") %>'
                            CommandName="myDelete" />
                    </ItemTemplate>
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
