<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NWF_WorkFlowInProgress.aspx.cs" Inherits="NWF_NWF_WorkFlowInProgress" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>显示正在进行中的工作流</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
            <br />
<%--            <table cellspacing="0" cellpadding="0" width="600px" bgcolor="white" border="0">
                <tr>
                    <td align="left" colspan="1" style="width: 40px; height: 27px">流名
                    </td>
                    <td align="left" colspan="1" style="height: 27px; width: 100px;">
                        <asp:DropDownList ID="ddlFlow" runat="server" Width="200px"
                            DataValueField="Flow_Id" DataTextField="Flow_Name" AutoPostBack="true" OnSelectedIndexChanged="ddlFlow_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    </td>
                    <td align="left" colspan="1" style="width: 40px; height: 27px">步骤名
                    </td>
                    <td align="left" colspan="1" style="height: 27px; width: 100px;">
                        <asp:DropDownList ID="ddlStep" runat="server" Width="200px"
                            DataValueField="StepID" DataTextField="StepName" AutoPostBack="true" OnSelectedIndexChanged="ddlStep_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label ID="lblId" runat="server" Text="Label" Visible="false"></asp:Label>
                    </td>
                    <td align="right" style="width: 45px; height: 27px">
                        <asp:Button ID="btnSearch" runat="server" CausesValidation="False" CssClass="SmallButton3"
                            OnClick="btnSearch_Click" Text="查询" />
                    </td>
                </tr>

            </table>--%>
            <asp:GridView ID="DataGrid1" runat="server"
                Width="600px" CssClass="GridViewStyle AutoPageSize" AllowPaging="True"
                PageSize="25" DataKeyNames="FlowID,StepName"
                AutoGenerateColumns="False" OnRowCommand="DataGrid1_RowCommand">
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                        GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="center" Text="流名" Width="110px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="步骤名" Width="110px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="转到" Width="100px"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="FlowName" HeaderText="流名">
                        <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="StepName" HeaderText="步骤名">
                        <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="转到" Visible="true">
                        <ItemStyle HorizontalAlign="center" Width="100px" />
                        <ItemTemplate>
                            <asp:LinkButton ID="linkNo" CssClass="no" runat="server" Text="转到" CommandArgument='<%# Bind("FlowID") %>'
                                CommandName="ViewEdit" Style="text-decoration: solid"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Underline="True" />
                        <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
