<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_WorkHoursDisplay_up.aspx.cs"
    Inherits="wo2_WorkHoursDisplay_up" %>

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
        <form id="form1" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" style="width: 1180px;">
            <tr>
                <td align="left">
                    ����:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtStart" runat="server" MaxLength="10" Width="80px" CssClass="Date"></asp:TextBox>
                    --
                    <asp:TextBox ID="txtEnd" runat="server" MaxLength="10" Width="80px" CssClass="Date"></asp:TextBox>
                    <asp:CheckBox ID="chkClose" runat="server" Text="����" />
                </td>
                <td>
                    ����:
                </td>
                <td>
                    <asp:DropDownList ID="dropDept" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td>
                    ����:
                </td>
                <td>
                    <asp:TextBox ID="txtUserNo" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    ����:
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td rowspan="1" align="center">
                    <asp:Button ID="btnSearch" runat="server" Width="60px" CssClass="SmallButton3" Text="��ѯ"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    �ص�:
                </td>
                <td>
                    <asp:TextBox ID="txtSite" Width="80px" runat="server"></asp:TextBox>
                </td>
                <td>
                    �ɱ�����:
                </td>
                <td>
                    <asp:TextBox ID="txtCenter" Width="100px" runat="server"></asp:TextBox>
                </td>
                <td>
                    �ӹ���:
                </td>
                <td>
                    <asp:TextBox ID="txtWorkOrder" Width="100px" runat="server"></asp:TextBox>
                </td>
                <td>
                    ID��:
                </td>
                <td>
                    <asp:TextBox ID="txtID" Width="100px" runat="server"></asp:TextBox>
                </td>
                <td rowspan="1" align="center">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" AllowPaging="True" AutoGenerateColumns="False" CssClass="GridViewStyle"
            runat="server" PageSize="21" Width="1180px" OnPageIndexChanging="gv_PageIndexChanging"
            OnRowCommand="gv_RowCommand" DataKeyNames="userID" OnRowCreated="gv_RowCreated">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="����" DataField="userNo">
                    <ItemStyle Width="50px" ForeColor="Black" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="userName">
                    <ItemStyle Width="60px" ForeColor="Black" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="Department">
                    <ItemStyle Width="180px" ForeColor="Black" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="Workshop">
                    <ItemStyle Width="100px" ForeColor="Black" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:ButtonField HeaderText="Wo2(��ʱ)" DataTextField="Wo2_cost" DataTextFormatString="{0:N2}"
                    CommandName="1">
                    <ItemStyle Width="80px" ForeColor="Black" HorizontalAlign="Right" />
                </asp:ButtonField>
                <asp:ButtonField HeaderText="Wo(��ʱ)" DataTextField="Wo_cost" DataTextFormatString="{0:N2}"
                    CommandName="4">
                    <ItemStyle Width="80px" ForeColor="Black" HorizontalAlign="Right" />
                </asp:ButtonField>
                <asp:BoundField HeaderText="�ƻ���(��ʱ)" DataField="Wo_cost_unplan" DataFormatString="{0:N2}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" ForeColor="Black" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:ButtonField HeaderText="�ϼ�(��ʱ)" DataTextField="cost_hours" DataTextFormatString="{0:N2}">
                    <ItemStyle ForeColor="Black" HorizontalAlign="Right" Width="80px" />
                </asp:ButtonField>
                <asp:ButtonField HeaderText="Wo2(����)" DataTextField="wo2_cost_unitPrice" DataTextFormatString="{0:N2}"
                    CommandName="1">
                    <ItemStyle ForeColor="Black" HorizontalAlign="Right" Width="80px" />
                </asp:ButtonField>
                <asp:ButtonField HeaderText="Wo(����)" DataTextField="wo_cost_unitPrice" DataTextFormatString="{0:N2}"
                    CommandName="4">
                    <ItemStyle ForeColor="Black" HorizontalAlign="Right" Width="80px" />
                </asp:ButtonField>
                <asp:BoundField HeaderText="�ƻ���(����)" DataField="wo_cost_unitPrice_unplan" DataFormatString="{0:N2}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" ForeColor="Black" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�ϼ�(����)" DataField="cost_unitPrice" DataFormatString="{0:N2}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" ForeColor="Black" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�������" DataField="cost_adj" DataFormatString="{0:N2}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" ForeColor="Black" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="���ʺϼ�" DataField="cost" DataFormatString="{0:N2}" HtmlEncode="False">
                    <ItemStyle Width="80px" ForeColor="Black" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" Width="1180px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="����" Width="50px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="180px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Wo2(��ʱ)" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Wo(��ʱ)" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�ƻ���(��ʱ)" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�ϼ�(��ʱ)" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Wo2(����)" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Wo(����)" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�ƻ���(����)" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�ϼ�(����)" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���ʺϼ�" Width="80px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        </form>
        <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
        </script>
    </div>
</body>
</html>
