<%@ Page Language="C#" AutoEventWireup="true" CodeFile="new_list.aspx.cs" Inherits="Performance_new_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <table id="table1" cellspacing="0" cellpadding="0" width="1060">
            <tr>
                <td style="text-align: left;">
                    公司：<asp:DropDownList ID="dropPlant" runat="server" Width="70px">
                        <asp:ListItem Value="0">--全部--</asp:ListItem>
                        <asp:ListItem Value="1">SZX</asp:ListItem>
                        <asp:ListItem Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Value="5">YQL</asp:ListItem>
                        <asp:ListItem Value="8">HQL</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;日期：<asp:TextBox ID="txtStdDate" runat="server" 
                        CssClass="SmallTextBox Date" Width="72px"></asp:TextBox>
                    -<asp:TextBox ID="txtEndDate" runat="server" CssClass="SmallTextBox Date" 
                        Width="72px"></asp:TextBox>
                    &nbsp;类别：<asp:DropDownList ID="dropType" runat="server" Width="150px" DataTextField="perft_type"
                        DataValueField="perft_id">
                    </asp:DropDownList>
                </td>
                <td style="width: 150px;">
                    <asp:Button ID="btnQeury" runat="server" CssClass="SmallButton2" Width="70px" 
                        Text="查询" onclick="btnQeury_Click">
                    </asp:Button>
                    &nbsp;<asp:Button ID="btnExport" runat="server" CssClass="SmallButton2" Width="60px" 
                        Text="导出" onclick="btnExport_Click">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AllowPaging="True" CssClass="GridViewStyle GridViewRebuild"
            AutoGenerateColumns="False" PageSize="20" DataKeyNames="perf_id" 
            onrowcommand="gv_RowCommand" onrowdatabound="gv_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="perf_date" HeaderText="日期" 
                    DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="perf_domain" HeaderText="地区">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="perf_type" HeaderText="考核类型">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="perf_dept" HeaderText="部门">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="perf_userName" HeaderText="责任人">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="perf_role" HeaderText="职位">
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="perf_result" HeaderText="处理规定">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="perf_deduct" HeaderText="扣分考核">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="perf_demerit" HeaderText="记过考核">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="perf_hrResult" HeaderText="人事考核">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkMaint" runat="server" CommandName="Maint">维护</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" 
                        Width="30px" />
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkSolution" runat="server" CommandName="Process">处理</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" 
                        Width="30px" />
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:BoundField DataField="perf_remarks" HeaderText="备注">
                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
</body>
</html>
