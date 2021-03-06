<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.selectreplace" CodeFile="selectreplace.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link media="all" href="../script/main.css" rel="stylesheet">
    <script language="javascript">
        function post() {
            var s = "";
            //if (obj.checked) s="&all=true";
            if (document.getElementById("btnOK").checked) s = "&all=true";
            //if (document.getElementById("specialWorkType").checked)t="&sWT=1";
            s = "/part/calPartsbyStockOrder.aspx"
            window.opener.location.href = s;
        }			
    </script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table style="border-top-style: none; border-right-style: none; border-left-style: none;
            border-bottom-style: none" bordercolor="lightgrey" cellspacing="0" cellpadding="0"
            width="996" border="0">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="984" align="center" bgcolor="white"
                        border="0">
                        <tbody>
                            <tr width="100%" height="50">
                                <td align="left" width="60%" colspan="6">
                                    产品代码为&nbsp;&nbsp;<asp:Label ID="lblProdCode" ForeColor="#ff0033" Font-Size="18px"
                                        Font-Bold="True" runat="server"></asp:Label>&nbsp;的结构 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                        ID="btnOK" runat="server" Text="确定" CssClass="SmallButton3"></asp:Button>
                                </td>
                            </tr>
                            <tr width="100%">
                                <td colspan="6">
                                    <asp:Panel ID="Panel1" Style="overflow-y: scroll; overflow-x: scroll" runat="server"
                                        Width="996px" Height="650px" BorderColor="Black" BorderWidth="1px">
                                        <asp:DataGrid ID="dgPart" runat="server" Width="3020px" PageSize="8" AllowPaging="false"
                                            AutoGenerateColumns="False" CssClass="GridViewStyle">
                                            <FooterStyle CssClass="GridViewFooterStyle" />
                                            <ItemStyle CssClass="GridViewRowStyle" />
                                            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                                            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                                            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                                            <Columns>
                                                <asp:BoundColumn DataField="ProdStruID" ReadOnly="true" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="partcode" SortExpression="partcode" HeaderText="部件号">
                                                    <HeaderStyle HorizontalAlign="Center" Width="300px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="partReplace" SortExpression="partReplace" HeaderText="替代品"
                                                    Visible="False">
                                                    <HeaderStyle Width="200px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn SortExpression="drpRep" HeaderText="替代品">
                                                    <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="drpRep" Height="15px" Width="100px" DataValueField="repid"
                                                            DataTextField="repcode" OnSelectedIndexChanged="drprepchange" runat="server"
                                                            AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="status" SortExpression="status" HeaderText="状态">
                                                    <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="type" SortExpression="type" HeaderText="分类">
                                                    <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="partQty" SortExpression="partQty" HeaderText="数量">
                                                    <HeaderStyle Width="120px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="partPos" SortExpression="partPos" HeaderText="位号">
                                                    <HeaderStyle Width="200px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="partMemo" SortExpression="partMemo" HeaderText="备注">
                                                    <HeaderStyle Width="200px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="partdesc" SortExpression="partdesc" HeaderText="部件描述">
                                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn Visible="false" DataField="pID"></asp:BoundColumn>
                                                <asp:BoundColumn Visible="false" DataField="cID" ReadOnly="True"></asp:BoundColumn>
                                                <asp:BoundColumn Visible="false" DataField="RID" ReadOnly="True"></asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" DataField="gsort" ReadOnly="True"></asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
          <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
