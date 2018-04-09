<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bom_Con_Details.aspx.cs" Inherits="Bom_Con_Details" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
        <br />
        <h2 style="color:Red">BOM变更处理信息详情</h2>
        <table id="table1" cellspacing="0" cellpadding="0" width="720px">
            <tr>
                <td style="height: 20px" align="right" >
                    <asp:Button ID="btn_close" runat="server" CssClass="SmallButton3" Text="关闭窗口" Width="60"
                        OnClick="btn_close_Click" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gv_HaveCheckInfo" runat="server" AllowPaging="True" PageSize="18" AutoGenerateColumns="False"
            DataKeyNames="ps_id" OnRowCommand="gvShip_RowCommand" OnPageIndexChanging="gvShip_PageIndexChanging"
            OnRowDataBound="gvShip_RowDataBound" Width="810px" CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="ps_id" HeaderText="单号">
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="ps_con_name" HeaderText="会签人员">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="depart" HeaderText="部门">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="ps_start" HeaderText="会签日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="ps_remark" HeaderText="会签意见">
                    <ItemStyle HorizontalAlign="Left" Width="280px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="son_bom" HeaderText="提交给" Visible="false">
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="types" HeaderText="状态">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
