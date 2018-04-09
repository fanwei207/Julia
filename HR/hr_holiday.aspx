<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_holiday.aspx.cs" Inherits="hr_holiday" %>

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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="250px">
            <tr>
                <td style="width: 100px">
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtYear" runat="server" Width="50px" AutoPostBack="True" MaxLength="4"
                        OnTextChanged="txtYear_TextChanged"></asp:TextBox>&nbsp;<b>年</b>&nbsp;
                    <asp:DropDownList ID="ddlMonth" runat="server" Width="50px" AutoPostBack="True" Font-Size="10pt"
                        OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
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
                    &nbsp;<b>月</b>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 100px" align="right">
                    <asp:Label ID="lbl_Holiday" runat="server" Text="国假日期:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtHoliday" runat="server" Width="100px" onkeydown="event.returnValue=false;"
                        onpaste="return false;" ValidationGroup="chkHoliday" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
                <td style="height: 28px" align="center">
                    <asp:Button ID="btn_AddNew" runat="server" CssClass="SmallButton2" Text="新增" Width="50px"
                        OnClick="btn_AddNew_Click" ValidationGroup="chkHoliday" CausesValidation="true">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvHoliday" runat="server" AllowPaging="False" CssClass="GridViewStyle"
            AutoGenerateColumns="False" PageSize="25" DataKeyNames="HolidayID" OnRowDeleting="gvHoliday_RowDeleting">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="250px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="国假日期" Width="200px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="删除" Width="50px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="HolidayDate" HeaderText="国假日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="false">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:ButtonField Text="<u>删除</u>" CommandName="Delete">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
