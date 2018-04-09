<%@ Page Language="VB" AutoEventWireup="false" CodeFile="computermanage.aspx.vb"
    Inherits="computermanage_computermanage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="left">
        <form id="form1" runat="server" method="post">
        <table id="table1" width="1000" border="0">
            <tr>
                <td>
                    类&nbsp;&nbsp;&nbsp;&nbsp;型<asp:DropDownList ID="selecttypedropdown" runat="server"
                        Width="180">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txbid" Visible="False" runat="server" ReadOnly="True"></asp:TextBox>
                    资产编号<asp:TextBox ID="txbassetno" runat="server" Width="180" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td>
                    连网情况<asp:DropDownList ID="ddlinternet" runat="server" Width="180">
                        <asp:ListItem Value="0" Text="----"></asp:ListItem>
                        <asp:ListItem Value="1" Text="连接"></asp:ListItem>
                        <asp:ListItem Value="2" Text="未连接"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    CPU&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txbcpu" runat="server" Width="180"
                        CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td>
                    内&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;存<asp:TextBox ID="txbmemory" runat="server"
                        Width="180" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td>
                    硬&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;盘<asp:TextBox ID="Txbharddisk" runat="server"
                        Width="180" CssClass="smalltextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    显示器<asp:TextBox ID="txbdisplay" runat="server" Width="180" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td>
                    键&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;盘<asp:TextBox ID="txbkeyboard" runat="server"
                        Width="180" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td>
                    鼠&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;标<asp:TextBox ID="txbmouse" runat="server"
                        Width="180" CssClass="smalltextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    IP&nbsp;地址<asp:TextBox ID="txbip" runat="server" Width="180" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td>
                    MAC&nbsp;地址<asp:TextBox ID="txbmac" runat="server" Width="180" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td>
                    操作系统<asp:TextBox ID="txbos" runat="server" Width="180" CssClass="smalltextbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    描&nbsp;&nbsp;&nbsp;&nbsp;述<asp:TextBox ID="txbdes" runat="server" Width="576px" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Button ID="Btnadd" Text="增加" runat="server" Width="50" CssClass="SmallButton2">
                    </asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="Btnmodify" Text="修改" runat="server" CssClass="SmallButton2" Width="50">
                    </asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="Btncancel" Text="取消" runat="server" CssClass="SmallButton2" Width="50"
                        CausesValidation="False"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="btnser" Text="查询" runat="server" CssClass="SmallButton2" Width="50"
                        CausesValidation="False"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="datagrid1" runat="server" Width="1200px" PageSize="20" AllowPaging="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="cid" Visible="False" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="typename" HeaderText="类型">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="assetno" HeaderText="资产编号">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cpu" HeaderText="CPU">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="memory" HeaderText="内存">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="harddisk" HeaderText="硬盘">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="display" HeaderText="显示器">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="keyboard" HeaderText="键盘">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mouse" HeaderText="鼠标">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ip" HeaderText="IP地址">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mac" HeaderText="MAC地址">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="os" HeaderText="操作系统">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="internet" HeaderText="因特网">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;详细&lt;/u&gt;" CommandName="Detail1">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;编辑&lt;/u&gt;" CommandName="Select">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;删除&lt;/u&gt;" CommandName="DeleteClick">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="description" HeaderText="描述">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server" EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
