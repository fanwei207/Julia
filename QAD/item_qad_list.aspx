<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.Item_Qad_List" CodeFile="Item_Qad_List.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
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
        <table cellspacing="1" cellpadding="1">
            <tr>
                <td align="left">
                    &nbsp;QAD号
                    <asp:TextBox ID="txtQad" runat="server" Width="90px" CssClass="SmallTextBox"></asp:TextBox>&nbsp;零件号
                    <asp:TextBox ID="txtCode" runat="server" Width="150px" CssClass="SmallTextBox"></asp:TextBox>
                    &nbsp;<asp:Button ID="BtnQuery" runat="server" CssClass="SmallButton3" Text="查询"></asp:Button>
                    &nbsp;<asp:Button ID="BtnCheck" TabIndex="0" runat="server" CssClass="SmallButton3" Text="结构检查"
                        Width="60px"></asp:Button>
                    &nbsp;<asp:Button ID="BtnUnique" TabIndex="0" runat="server" CssClass="SmallButton3" Text="唯一性检查"
                        Width="70px"></asp:Button>
                    <asp:CheckBox ID="NotChk" runat="server" Text="没有QAD BOM" Checked="false"></asp:CheckBox>
                    <asp:Button ID="BtnExport" TabIndex="0" runat="server" CssClass="SmallButton3" Text="结构导出"
                        Width="60px"></asp:Button>
                    &nbsp;<asp:Button ID="StruExport" TabIndex="0" runat="server" CssClass="SmallButton3" Text="QAD结构导出"
                        Width="70px"></asp:Button>
                    &nbsp;<asp:Button ID="RepExport" TabIndex="0" runat="server" CssClass="SmallButton3" Text="替代子零件导出"
                        Width="90px"></asp:Button>
                    &nbsp;<asp:Button ID="AlterExport" TabIndex="0" runat="server" CssClass="SmallButton3"
                        Text="替代结构导出" Width="80px"></asp:Button>
                </td>
                <td align="right">
                    &nbsp;
                    <asp:Label ID="lblCount" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgQAD" runat="server" Width="1202px" PageSize="22" AllowPaging="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="gsort" ReadOnly="True"></asp:BoundColumn>
                <asp:ButtonColumn HeaderText="<b>QAD号</b>" CommandName="QADBtn" 
                    DataTextField="qad" DataTextFormatString="&lt;u&gt;{0}&lt;/u&gt;">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px" Font-Bold="False" 
                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                        Font-Underline="True"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>结构</u>" CommandName="StruBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>QAD结构</u>" CommandName="QADStruBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="DelBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>替代子零件</u>" CommandName="RepBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>替代结构</u>" CommandName="AlterBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>用于</u>" CommandName="UserByBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="code" SortExpression="code" HeaderText="<b>老编号</b>">
                    <HeaderStyle Width="230px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="230px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="desc1" SortExpression="desc1" HeaderText="<b>QAD描述1</b>">
                    <HeaderStyle Width="250px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="desc2" SortExpression="desc2" HeaderText="<b>QAD描述2</b>">
                    <HeaderStyle Width="250px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>状态</u>" HeaderText="状态" CommandName="StatusBtn" DataTextField="Chk">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="ChkBy" SortExpression="ChkBy" HeaderText="<b>QAD BOM</b>">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="qad" ReadOnly="True"></asp:BoundColumn>
                <asp:ButtonColumn Text="<u>比较</u>" CommandName="CompBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
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
