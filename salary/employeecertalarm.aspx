<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.employeecertalarm"
    CodeFile="employeecertalarm.aspx.vb" %>

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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table2" cellspacing="0" cellpadding="0" width="1050px" align="center"
            border="0">
            <tr>
                <td align="left">
                    工号
                </td>
                <td align="left">
                    <asp:TextBox ID="txbNO" runat="server" Height="20px" CssClass="smalltextbox" Width="80px"
                        AutoPostBack="True"></asp:TextBox>
                </td>
                <td align="left">
                    姓名
                </td>
                <td align="left">
                    <asp:TextBox ID="txbName" runat="server" Height="20px" CssClass="smalltextbox" Width="80px"></asp:TextBox>
                    <asp:TextBox ID="txbid" Visible="False" runat="server" ReadOnly="True" Width="36px"></asp:TextBox>
                </td>
                <td align="left">
                    岗位
                </td>
                <td align="left">
                    <asp:TextBox ID="Txbpos" runat="server" Height="20px" CssClass="smalltextbox" Width="150"></asp:TextBox>
                </td>
                <td align="left">
                    备注&nbsp;<asp:TextBox ID="Txbmemo" runat="server" Height="20px" CssClass="smalltextbox"
                        Width="200" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    证书名称
                </td>
                <td align="left">
                    <asp:TextBox ID="Txbcert" runat="server" Height="20px" CssClass="smalltextbox" Width="200"
                        MaxLength="20"></asp:TextBox>
                </td>
                <td align="left">
                    证书起始日期
                </td>
                <td align="left">
                    <asp:TextBox ID="Txbbegin" runat="server" Height="20px" CssClass="smalltextbox Date"
                        Width="150px" onkeydown="event.returnValue=false;" onpaste="return false"></asp:TextBox>
                </td>
                <td align="left">
                    证书到期日期
                </td>
                <td align="left">
                    <asp:TextBox ID="Txbend" runat="server" Height="20px" CssClass="smalltextbox Date"
                        Width="150px" onkeydown="event.returnValue=false;" onpaste="return false"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Button ID="btnser" Text="查询" runat="server" CssClass="SmallButton2" Width="50"
                        CausesValidation="False"></asp:Button>
                    <asp:Button ID="Btncancel" Text="取消" runat="server" Width="50" CssClass="SmallButton2"
                        CausesValidation="False"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="datagrid1" runat="server" PagerStyle-Mode="NumericPages" PageSize="25"
            AllowPaging="True" AutoGenerateColumns="False" Width="1050px" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="id" Visible="False" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="userno" SortExpression="userNO" HeaderText="工号">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="username" SortExpression="userName" HeaderText="姓名">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="pos" SortExpression="pos" HeaderText="岗位">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="certname" SortExpression="certName" HeaderText="证书名称">
                    <HeaderStyle Width="250px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="begindate" SortExpression="begindate" HeaderText="证书起始日期">
                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="enddate" SortExpression="enddate" HeaderText="证书到期日期">
                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="memo" SortExpression="memo" HeaderText="备注">
                    <HeaderStyle Width="300px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;编辑&lt;/u&gt;" CommandName="Select">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;删除&lt;/u&gt;" CommandName="DeleteClick">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
        <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </div>
</body>
</html>
