<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.perf_org" CodeFile="perf_org.aspx.vb" %>

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
        <asp:Table ID="Table1" runat="server" CellPadding="0" CellSpacing="0" Width="650px">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center">
                    职位
                    <asp:TextBox ID="txb1" runat="server" Width="100px" CssClass="smalltextbox"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    姓名
                    <asp:TextBox ID="txb2" runat="server" Width="100px" CssClass="smalltextbox"></asp:TextBox>
                    <asp:Button ID="btn_add" runat="server" CssClass="SmallButton3" Width="90" Text="增加">
                    </asp:Button>
                    <asp:Button ID="btn_cancel" runat="server" CssClass="SmallButton3" Width="40" Text="取消">
                    </asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="800px" OnUpdateCommand="Update"
            OnCancelCommand="Cancel" AutoGenerateColumns="False" CssClass="GridViewStyle">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="id" ReadOnly="True" HeaderText="id">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="序号">
                    <HeaderStyle Width="30px" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="职位">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        &nbsp;
                        <asp:Label ID="lblpos" runat="server" Text='<%# Container.DataItem("pos") %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        &nbsp;
                        <asp:TextBox ID="txbpos" runat="server" Text='<%# Container.DataItem("pos1") %>'
                            CssClass="SmallTextBox">
                        </asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="姓名">
                    <HeaderStyle HorizontalAlign="Center" Width="350px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblname" runat="server" Width="350px" Text='<%#container.dataitem("name")%>'>
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txbname" CssClass="SmallTextBox" runat="server" Width="350px" Text='<%# Container.DataItem("name") %>'>
                        </asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;u&gt;保存&lt;/u&gt;"
                    CancelText="&lt;u&gt;取消&lt;/u&gt;" EditText="&lt;u&gt;编辑&lt;/u&gt;">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle Width="70px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="delete">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;选择&lt;/u&gt;" HeaderText="增加下级职位" CommandName="select">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="80px"></HeaderStyle>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" />
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
