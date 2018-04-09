<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bg_ApplyList.aspx.cs" Inherits="bg_ApplyList" %>

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
        <table id="tblHead" runat="server" cellspacing="0" cellpadding="0" width="920px"
            height="20px" border="0">
            <tr height="20px">
                <td align="right">
                    <asp:Label ID="lblApplicant" runat="server" Width="50px" CssClass="LabelRight" Text="申请人:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtApplicant" runat="server" Width="80px" TabIndex="1"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblDept" runat="server" Width="40px" CssClass="LabelRight" Text="部门:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtDept" runat="server" Width="80px" TabIndex="2"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblKeyword" runat="server" Width="60px" CssClass="LabelRight" Text="关键字:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtKeyword" runat="server" Width="100px" TabIndex="3"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtYear" runat="server" Width="40px" TabIndex="4" MaxLength="4"></asp:TextBox>年
                    <asp:DropDownList ID="ddlMonth" runat="server" Width="40px" TabIndex="5" Font-Size="10pt">
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
                    月
                </td>
                <td align="Center">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton3" Width="40px"
                        TabIndex="6" OnClick="btnQuery_Click" />
                </td>
                <td style="width: 240px">
                    <asp:CheckBox ID="chkShowMyApply" Text="只显示需本人审核的申请" runat="server" Checked="true"
                        AutoPostBack="True" OnCheckedChanged="chkShowMyApply_CheckedChanged" />
                    <asp:CheckBox ID="chkShowAll" runat="server" Text="显示所有" Checked="false" AutoPostBack="true"
                        OnCheckedChanged="chkShowAll_CheckedChanged" />
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="添加申请" CssClass="SmallButton3" Width="60px"
                        TabIndex="7" OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvApply" runat="server" AllowPaging="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize" 
            PageSize="22" OnPreRender="gvApply_PreRender"
            DataKeyNames="ApplyID" OnPageIndexChanging="gvApply_PageIndexChanging" Width="920px"
            OnRowCommand="gvApply_RowCommand">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="920px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="申请号" Width="80px" HorizontalAlign="center" Font-Bold="false"
                            Visible="false"></asp:TableCell>
                        <asp:TableCell Text="申请人" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="申请人所属部门" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="申请内容" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="申请金额" Width="70px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="申请时间" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="受理人" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="申请账户说明" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="明细" Width="40px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="审核" Width="40px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="实际费用" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="ApplyNo" HeaderText="申请号" Visible="false">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Applicant" HeaderText="申请人">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Dept" HeaderText="申请人所属部门">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Notes" HeaderText="申请内容" HtmlEncode="false">
                    <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle HorizontalAlign="Left" Wrap="true" />
                </asp:BoundField>
                <asp:BoundField DataField="Amount" HeaderText="申请金额" HtmlEncode="false" DataFormatString="{0:N2}">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="CreatedDate" HeaderText="申请时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Recipient" HeaderText="受理人">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AccountDesc" HeaderText="申请账户说明">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:ButtonField Text="明细" HeaderText="明细" CommandName="Detail">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:ButtonField Text="审核" HeaderText="审核" CommandName="Evaluate">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:ButtonField Text="实际费用" HeaderText="实际费用" CommandName="Actual">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
            </Columns>
        </asp:GridView> 
    </div>
    </form>
    <script type="text/javascript">
            <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
