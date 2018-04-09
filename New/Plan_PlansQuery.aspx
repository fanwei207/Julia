<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plan_PlansQuery.aspx.cs"
    Inherits="Plan_PlansQuery" %>

<%@ Import Namespace="Portal.Fixas" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
        <table cellspacing="0" cellpadding="0" bgcolor="white" border="0" style="width: 900px">
            <tr class="main_top">
                <td class="main_left">
                </td>
                <td style="width: 900px">
                    <asp:TextBox ID="txtFixasNo" runat="server" CssClass="SmallTextBox4" TabIndex="1"
                        Width="91px"></asp:TextBox><asp:TextBox ID="txtFixasName" runat="server" CssClass="SmallTextBox4"
                            TabIndex="1" Width="131px"></asp:TextBox><asp:TextBox ID="txtFixasDesc" runat="server"
                                CssClass="SmallTextBox4" TabIndex="1" Width="72px"></asp:TextBox><asp:DropDownList
                                    ID="dropType" runat="server" Width="68px" DataTextField="fixtp_name" DataValueField="fixtp_id">
                                </asp:DropDownList>
                    <asp:DropDownList ID="dropEntity" runat="server" Width="70px" DataTextField="enti_name"
                        DataValueField="enti_id">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtFixasVouDate" runat="server" CssClass="SmallTextBox4 date" TabIndex="1"
                        Width="68px"></asp:TextBox><asp:TextBox ID="txtFixasSupplier" runat="server" CssClass="SmallTextBox4"
                            TabIndex="1" Width="132px"></asp:TextBox><asp:TextBox ID="txtPlanDate" runat="server"
                                CssClass="SmallTextBox4 Date" TabIndex="1" Width="71px"></asp:TextBox><asp:DropDownList
                                    ID="dropRepairItem" runat="server" DataTextField="Name" DataValueField="ID" Width="161px">
                                </asp:DropDownList>
                </td>
                <td style="width: 696px">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="5" Text="Query"
                        OnClick="btnQuery_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle" PageSize="21" OnPreRender="gvRDW_PreRender" DataKeyNames="ID"
            OnRowDataBound="gvRDW_RowDataBound" OnPageIndexChanging="gvRDW_PageIndexChanging"
            Width="900px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="870px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="�ʲ����" Width="80px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="�ʲ�����" Width="120px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="���" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="����" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="���˹�˾" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="��Ӧ��" Width="120px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="�ƻ�����" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="ά����Ŀ" Width="150px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="FixasNo" HeaderText="�ʲ����" ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FixasName" HeaderText="�ʲ�����" ReadOnly="True">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FixasDesc" HeaderText="���" ReadOnly="True">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FixasType" HeaderText="����" ReadOnly="True">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FixasEntity" HeaderText="���˹�˾" ReadOnly="True">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FixasVouDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False" ReadOnly="True">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FixasSupplier" HeaderText="��Ӧ��" ReadOnly="True">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Date" HeaderText="�ƻ�����" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="ά����Ŀ">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text="<%# ((RepairItem)((Plan)Container.DataItem).RepairItem).Name %>"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
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
