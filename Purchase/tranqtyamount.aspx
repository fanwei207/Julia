<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.TranQtyAmount" CodeFile="TranQtyAmount.aspx.vb" %>

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
        <table width="460" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:TextBox ID="year" runat="server" Width="50px" AutoPostBack="True"></asp:TextBox>年&nbsp;
                    <asp:DropDownList ID="month" runat="server" Width="50px" AutoPostBack="True" Font-Size="10pt"
                        CssClass="smallbutton2">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                    月&nbsp;
                </td>
                <td>
                    工号:<asp:TextBox runat="server" Width="100px" ID="txtUserNo"></asp:TextBox>
                </td>
                <td>
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3"></asp:Button>
                </td>
                <td align="right" width="30%">
                    <asp:Label ID="lblCount" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgList" runat="server" CssClass="GridViewStyle AutoPageSize" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="20" Width="460px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" HeaderText="序号">
                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userNo" HeaderText="工号">
                    <HeaderStyle HorizontalAlign="Center" Width="130px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="130px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userName" HeaderText="姓名">
                    <HeaderStyle HorizontalAlign="Center" Width="130px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="130px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="enter_date" HeaderText="录入日期">
                    <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="account" HeaderText="数量">
                    <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
</body>
</html>
