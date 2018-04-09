<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plan_Records.aspx.cs" Inherits="Plan_Records" %>

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
        <table cellspacing="0" cellpadding="0" style="width: 1015px" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td>
                    <asp:TextBox ID="txtFixasNo" runat="server" CssClass="SmallTextBox4" TabIndex="1"
                        Width="79px"></asp:TextBox><asp:TextBox ID="txtFixasName" runat="server" CssClass="SmallTextBox4"
                            TabIndex="1" Width="111px"></asp:TextBox><asp:TextBox ID="txtFixasDesc" runat="server"
                                CssClass="SmallTextBox4" TabIndex="1" Width="62px"></asp:TextBox><asp:DropDownList
                                    ID="dropType" runat="server" Width="62px" DataTextField="fixtp_name" DataValueField="fixtp_id">
                                </asp:DropDownList>
                    <asp:DropDownList ID="dropEntity" runat="server" Width="64px" DataTextField="enti_name"
                        DataValueField="enti_id">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtFixasVouDate" runat="server" CssClass="SmallTextBox4 Date" TabIndex="1"
                        Width="58px"></asp:TextBox><asp:TextBox ID="txtFixasSupplier" runat="server" CssClass="SmallTextBox4"
                            TabIndex="1" Width="118px"></asp:TextBox><asp:TextBox ID="txtMaintor" runat="server"
                                CssClass="SmallTextBox4" TabIndex="1" Width="61px"></asp:TextBox><asp:TextBox ID="txtMaintDate"
                                    runat="server" CssClass="SmallTextBox4 Date" TabIndex="1" Width="61px"></asp:TextBox><asp:DropDownList
                                        ID="dropRepairItem" runat="server" DataTextField="Name" DataValueField="ID" Width="141px">
                                    </asp:DropDownList>
                    <asp:TextBox ID="txtMoney" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="55px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="5" Text="Query"
                        Width="40px" OnClick="btnQuery_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton2" TabIndex="6" Text="Add"
                        Width="40px" OnClick="btnAdd_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle" PageSize="21" OnPreRender="gvRDW_PreRender" DataKeyNames="ID"
            OnRowDataBound="gvRDW_RowDataBound" OnPageIndexChanging="gvRDW_PageIndexChanging"
            OnRowCancelingEdit="gv_RowCancelingEdit" OnRowDeleting="gv_RowDeleting" OnRowEditing="gv_RowEditing"
            OnRowUpdating="gv_RowUpdating" Width="1015px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1015px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="�ʲ����" Width="80px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="�ʲ�����" Width="120px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="���" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="����" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="���˹�˾" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="��Ӧ��" Width="120px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="ά����" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="ά������" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="ά����Ŀ" Width="150px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="ά�����" Width="55px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="" Width="80px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="" Width="50px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
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
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
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
                <asp:TemplateField HeaderText="ά����">
                    <ItemTemplate>
                        <asp:Label ID="lbMaintor" runat="server" Text="<%# ((User)((Record)Container.DataItem).Maintor).Name %>"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ά������">
                    <ItemTemplate>
                        <asp:Label ID="lbMaintedDate" runat="server" Text="<%# ((User)((Record)Container.DataItem).Maintor).Date %>"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ά����Ŀ">
                    <ItemTemplate>
                        <asp:Label ID="lbRepairItem" runat="server" Text="<%# ((RepairItem)((Record)Container.DataItem).RepairItem).Name %>"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Money" HeaderText="ά�����">
                    <HeaderStyle Width="55px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="55px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:CommandField ShowEditButton="True">
                    <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                    <HeaderStyle Width="90px" />
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True">
                    <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>