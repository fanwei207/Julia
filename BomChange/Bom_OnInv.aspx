<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bom_OnInv.aspx.cs" Inherits="Bom_Inv" %>

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
        <h2 style="color:Red">物料在途明细</h2>
        <table id="table1" cellspacing="0" cellpadding="0" width="720px">
            <tr>
                <td style="height: 20px" align="right" >
                    <asp:Button ID="btn_close" runat="server" CssClass="SmallButton3" Text="关闭窗口" Width="60"
                        OnClick="btn_close_Click" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gv_oninv" runat="server" AllowPaging="True" PageSize="18" AutoGenerateColumns="False"
            DataKeyNames="pod_domain" OnRowCommand="gv_oninv_RowCommand" OnPageIndexChanging="gv_oninv_PageIndexChanging"
            OnRowDataBound="gv_oninv_RowDataBound" Width="810px" CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="pod_domain" HeaderText="域">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="pod_site" HeaderText="地点">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="pod_nbr" HeaderText="订单号|工单号">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="pod_line" HeaderText="行号|ID号">
                    <ItemStyle HorizontalAlign="Center" Width="65px" />
                    <HeaderStyle HorizontalAlign="Center" Width="65px" />
                </asp:BoundField>
                <asp:BoundField DataField="pod_part" HeaderText="物料号">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="qty" HeaderText="数量">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="pod_status" HeaderText="状态">
                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                    <HeaderStyle HorizontalAlign="Center" Width="25px" />
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
