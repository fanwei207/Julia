<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wl_analysis.aspx.cs" Inherits="wl_analysis" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="980px">
            <tr>
                <td>
                    <asp:DropDownList ID="ddl_site" runat="server" Width="100px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_site_SelectedIndexChanged">
                        <asp:ListItem Selected="true" Value="5">����ǿ�� YQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="2">��ǿ�� ZQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="8">����ǿ�� HQL</asp:ListItem>
                        <asp:ListItem Selected="false" Value="1">�Ϻ����� SZX</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp; �ɱ�����
                    <asp:DropDownList ID="ddl_cc" runat="server" Width="90px" AutoPostBack="false">
                    </asp:DropDownList>
                    ��<asp:TextBox ID="txb_year" runat="server" Width="50" TabIndex="3" Height="22"></asp:TextBox>
                    ��<asp:TextBox ID="txb_month" runat="server" Width="30" TabIndex="3" Height="22"></asp:TextBox>
                    ��<asp:TextBox ID="txb_day" runat="server" Width="30" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                    �Ƚ�����<asp:DropDownList ID="ddl_type" runat="server" Width="120px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_type_SelectedIndexChanged">
                        <asp:ListItem Selected="true" Value="1">��ʱAB--����AB</asp:ListItem>
                        <asp:ListItem Selected="false" Value="2">��ʱA--����A</asp:ListItem>
                        <asp:ListItem Selected="false" Value="3">��ʱA--����AB</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btn_search" runat="server" Width="40" CssClass="SmallButton3" Text="��ѯ"
                        TabIndex="4" OnClick="btn_search_Click"></asp:Button>&nbsp;
                    <asp:Button ID="btn_list" runat="server" Width="40" CssClass="SmallButton3" Text="ˢ��"
                        TabIndex="4" OnClick="btn_list_Click"></asp:Button>&nbsp;
                    <asp:Button ID="btn_excel" runat="server" Width="40" CssClass="SmallButton3" Text="����ͼ"
                        TabIndex="24" OnClick="btn_excel_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;<asp:Label ID="lbl_qty" runat="server"></asp:Label>&nbsp;
                </td>
                <td>
                    &nbsp;<asp:Label ID="lbl_time" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvBadProd" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="20" OnPreRender="gvBadProd_PreRender"
            OnPageIndexChanging="gvBadProd_PageIndexChanging" Width="980px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="955px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="�ɱ�����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="������⹤ʱ" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����������ʱ" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�ƻ��������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�ƻ��⹤ʱ" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���ڹ�ʱ" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����%" Width="60px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="group_x" Visible="False" />
                <asp:BoundField DataField="group_cc" HeaderText="�ɱ�����">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_date" HeaderText="����">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="group_qty" HeaderText="���������">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_total" HeaderText="������⹤ʱ">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_qty2" HeaderText="����������">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_total2" HeaderText="����������ʱ">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_qty3" HeaderText="�ƻ��������">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_total3" HeaderText="�ƻ��⹤ʱ">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_qty_atten" HeaderText="��������">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_atten" HeaderText="���ڹ�ʱ">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="group_pass" HeaderText="����%" SortExpression="group_pass"
                    DataFormatString="{0:##0.##}" HtmlEncode="False">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
