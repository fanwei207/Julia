<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_Salary_Leaver.aspx.cs"
    Inherits="hr_Salary_Leaver" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 42px;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" style="width: 700px">
            <tr>
                <td class="style1">
                    文件:
                </td>
                <td>
                    <input id="filename1" type="file" name="filename1" runat="server" />
                    &nbsp;&nbsp;<asp:Button runat="server" id="BtnRouting" Width="54px" 
                        CssClass="SmallButton3" Text="导入" onclick="BtnRouting_Click"/>
                </td>
                <td align="right">
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton3" Text="导出" 
                        onclick="btnExport_Click" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    下载:
                </td>
                <td style="height: 20px; text-align: left;">
                    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="11px"
                        Font-Underline="True" NavigateUrl="~/docs/SalaryLeaverUsers.xls" Width="62px">模板</asp:HyperLink>
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3">
                    暂存年月：<asp:TextBox ID="txtYear" runat="server" Width="36px"></asp:TextBox>年 -
                    <asp:TextBox ID="txtMonth" runat="server" Width="28px"></asp:TextBox>月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 工号：<asp:TextBox ID="txtUserNo" runat = "server" Width="60px"></asp:TextBox>
                    部门：<asp:DropDownList ID="dropDept" runat="server" Width="100px">
                    </asp:DropDownList>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" Text="查询" Width="60px"
                        OnClick="btnQuery_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnLock" runat="server" CssClass="SmallButton3" Text="锁定" Width="60px"
                        OnClick="btnLock_Click" />
                    &nbsp;
                    <asp:Button ID="btnUnLock" runat="server" CssClass="SmallButton3" Text="解锁" Width="60px"
                        OnClick="btnUnLock_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            OnRowDataBound="gv_RowDataBound" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
            PageSize="22" Width="700px" OnRowDeleting="gv_RowDeleting" DataKeyNames="userID">
            <Columns>
                <asp:BoundField HeaderText="暂存年月" DataField="slDate">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工号" DataField="userNo">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="姓名" DataField="userName">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="部门" DataField="deptName">
                    <HeaderStyle Width="200px" />
                    <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工资" DataField="salary">
                    <HeaderStyle Width="70px" />
                    <ItemStyle Width="70px" HorizontalAlign="right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="离职日期" DataField="leavDate">
                    <HeaderStyle Width="130px" />
                    <ItemStyle Width="130px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="锁定日期" DataField="lockDate">
                    <HeaderStyle Width="130px" />
                    <ItemStyle Width="130px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:ButtonField Text="<u>删除</u>" CommandName="Delete">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
            </Columns>
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
