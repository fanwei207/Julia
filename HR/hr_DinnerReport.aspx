<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_DinnerReport.aspx.cs"
    Inherits="hr_DinnerReport" %>

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
    <form id="form1" runat="server">
    <div align="center">
        <table runat="server" id="table1" cellspacing="0" cellpadding="1" width="500" align="center">
            <tr style="height: 20px">
                <td style="width: 2px" align="center">
                    &nbsp;
                </td>
                <td style="width: 200px" align="center">
                    年&nbsp;&nbsp;份:&nbsp;<asp:TextBox ID="txtYear" runat="server" Width="50px" AutoPostBack="true"
                        MaxLength="4" OnTextChanged="txtYear_TextChanged" TabIndex="1"></asp:TextBox>&nbsp;&nbsp;
                    月&nbsp;&nbsp;份:&nbsp;
                    <asp:DropDownList ID="ddlMonth" runat="server" Width="50px" AutoPostBack="true" Font-Size="10pt"
                        OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" TabIndex="2">
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
                </td>
                <td align="center">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" Width="40px"
                        TabIndex="3" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvDinner" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="DinnerDate" CssClass="GridViewStyle AutoPageSize" PageSize="20"
            OnPreRender="gvDinner_PreRender" Width="500px" OnRowCommand="gvDinner_RowCommand"
            OnPageIndexChanging="gvDinner_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="table2" Width="500px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="就餐日期" Width="200px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="就餐人数" Width="200px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="明细" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="DinnerDate" HeaderText="就餐日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DinnerCount" HeaderText="就餐人数">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle Width="200px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:ButtonField Text="<u>明细</u>" CommandName="Detail">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
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
