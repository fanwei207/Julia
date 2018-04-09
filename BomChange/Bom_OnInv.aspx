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
        <h2 style="color:Red">������;��ϸ</h2>
        <table id="table1" cellspacing="0" cellpadding="0" width="720px">
            <tr>
                <td style="height: 20px" align="right" >
                    <asp:Button ID="btn_close" runat="server" CssClass="SmallButton3" Text="�رմ���" Width="60"
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
                <asp:BoundField DataField="pod_domain" HeaderText="��">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="pod_site" HeaderText="�ص�">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="pod_nbr" HeaderText="������|������">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="pod_line" HeaderText="�к�|ID��">
                    <ItemStyle HorizontalAlign="Center" Width="65px" />
                    <HeaderStyle HorizontalAlign="Center" Width="65px" />
                </asp:BoundField>
                <asp:BoundField DataField="pod_part" HeaderText="���Ϻ�">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="qty" HeaderText="����">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="pod_status" HeaderText="״̬">
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
