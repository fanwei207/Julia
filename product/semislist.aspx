<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.SemisList" CodeFile="SemisList.aspx.vb" %>

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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="1" cellpadding="1" width="1004px">
            <tr>
                <td align="left">
                    型号
                    <asp:TextBox ID="txtCode" TabIndex="0" runat="server" Width="150px" MaxLength="30"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;分类
                    <asp:TextBox ID="txtCategory" TabIndex="0" runat="server" Width="100px" MaxLength="30"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnQuery" TabIndex="0" runat="server" CssClass="SmallButton3" Text="查询">
                    </asp:Button>&nbsp;
                    <asp:RadioButton ID="radNormal" runat="server" Text="使用" AutoPostBack="True" Checked="True"
                        GroupName="RadGroup"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="radTry" runat="server" Text="试用" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="radStop" runat="server" Text="停用" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup"></asp:RadioButton>
                </td>
                <td align="right">
                    <asp:Label ID="lblCount" runat="server"></asp:Label>&nbsp;&nbsp;
                    <asp:Button ID="BtnReplace" TabIndex="0" runat="server" CssClass="SmallButton3" Text="替换名字"
                        Width="60"></asp:Button>&nbsp;
                    <asp:Button ID="btnAddNew" runat="server" CssClass="SmallButton3" Text="增加"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgProduct" runat="server" Width="2100px" CssClass="GridViewStyle AutoPageSize"
            AutoGenerateColumns="False" AllowPaging="True" PageSize="26">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="productID" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="gsort" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="code" SortExpression="pcode" HeaderText="<b>型号</b>">
                    <HeaderStyle Width="250px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="category" SortExpression="category" HeaderText="<b>分类</b>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="status" SortExpression="status" HeaderText="<b>状态</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>编辑</u>" HeaderText="编辑" CommandName="EditBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>结构</u>" HeaderText="结构" CommandName="StruBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>用于</u>" HeaderText="用于" CommandName="UsedByBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>供货</u>" HeaderText="供货" CommandName="SupplyBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>文档</u>" HeaderText="文档" CommandName="DocByBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="Customer" SortExpression="Customer" HeaderText="<b>所属客户</b>">
                    <HeaderStyle HorizontalAlign="center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="description" SortExpression="description" HeaderText="<b>描述</b>">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script>
          <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
