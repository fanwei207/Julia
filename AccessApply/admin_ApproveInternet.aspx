<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_ApproveInternet.aspx.cs"
    Inherits="admin_ApproveInternet" %>

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
    <div align="center">
        <form id="form1" method="post" runat="server">
        <table style="width: 753px" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left" style="width: 277px; height: 10px;">
                </td>
                <td style="width: 217px; height: 10px;">
                </td>
                <td style="height: 10px">
                </td>
            </tr>
            <tr valign="middle">
                <td align="left" style="width: 650px; height: 26px;">
                    公司:
                    <asp:DropDownList ID="ddlPlantCode" runat="server">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">SZX</asp:ListItem>
                        <asp:ListItem Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Value="5">YQL</asp:ListItem>
                        <asp:ListItem Value="8">HQL</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp; 申请人:<asp:TextBox ID="txtApplyName" runat="server" Width="75px" CssClass="SmallTextBox"></asp:TextBox>&nbsp;
                    <asp:CheckBox ID="chkb_displayToApprove" runat="server" Text="仅显示待审批" AutoPostBack="true"
                        Width="158px" OnCheckedChanged="chkb_displayToApprove_CheckedChanged" Checked="True" />&nbsp;
                </td>
                <td align="left" style="width: 190px; height: 26px;">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" CssClass="smallbutton2"
                        Width="50px" />
                </td>
                <td align="left" style="height: 26px">
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <asp:GridView ID="gv" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False"
                        PageSize="25" CssClass="GridViewStyle" runat="server" Width="900px" DataKeyNames="id,applyUserId,approveUserId"
                        OnRowDataBound="gv_RowDataBound" OnRowCommand="gv_RowCommand" OnRowDeleting="gv_RowDeleting" 
                        OnPageIndexChanging="gv_PageIndexChanging" >
                        <Rowstyle cssclass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <alternatingRowstyle cssclass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <selectedRowstyle cssclass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="900px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="域" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="工号" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="申请人" Width="90px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="外网访问状态" Width="100px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="申请时间" Width="90px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="审批" Width="90px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="审批人" Width="90px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="申批时间" Width="90px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="申请理由"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="无符合条件的申请信息" ColumnSpan="9"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="域" DataField="plantCode" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="工号" DataField="applyloginName" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="申请人" DataField="applyUserName" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="访问状态" DataField="isInternetAccess" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="申请时间" DataField="applyDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="审批">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnApprove" Text="批准" ForeColor="black" Font-Underline="true"
                                        Enabled="false" Font-Size="12px" runat="server" CommandArgument='<%# Eval("id") %>'
                                        CommandName="Approve" OnClientClick="return confirm('你确定要通过其外网访问申请吗?')" />
                                    &nbsp; &nbsp;
                                    <asp:LinkButton ID="btnCancel" Text="取消" ForeColor="black" Font-Underline="true"
                                        Enabled="false" Font-Size="12px" runat="server" CommandArgument='<%# Eval("id") %>'
                                        CommandName="myCancel" />
                                </ItemTemplate>
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="审批人id" DataField="approveUserId" ReadOnly="True" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Width="65px" />
                                <ItemStyle HorizontalAlign="Center" Width="65px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="审批人" DataField="approveUserName" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="65px" />
                                <ItemStyle HorizontalAlign="Center" Width="65px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="申批时间" DataField="approveDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="申请理由" DataField="applyAccReason" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" Text="Del" ForeColor="black" Font-Underline="true"
                                        Enabled="false" Font-Size="12px" runat="server" CommandArgument='<%# Eval("id") %>'
                                        CommandName="Delete" OnClientClick="return confirm('你确定删除外网访问申请记录?')" />
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 277px">
                </td>
                <td style="width: 217px">
                </td>
                <td>
                </td>
            </tr>
        </table>
        &nbsp;
        </form>
        <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </div>
</body>
</html>
