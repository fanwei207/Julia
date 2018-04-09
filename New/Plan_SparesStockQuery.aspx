<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plan_SparesStockQuery.aspx.cs"
    Inherits="Plan_SparesStockQuery" %>

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
        <table cellspacing="0" cellpadding="0">
            <tr class="main_top">
                <td>
                    <asp:TextBox ID="txtNo" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="100px"></asp:TextBox><asp:TextBox
                        ID="txtDesc" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="300px"></asp:TextBox><asp:TextBox
                            ID="txtDevice" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="102px"></asp:TextBox><asp:Label
                                ID="Label1" runat="server" Width="101px"></asp:Label><asp:DropDownList ID="dropDept"
                                    runat="server" DataTextField="Name" DataValueField="departmentID" Width="100px">
                                </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="5" Text="Query"
                        OnClick="btnQuery_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="21" OnPreRender="gvRDW_PreRender"
            DataKeyNames="ID" OnRowDataBound="gvRDW_RowDataBound" 
            OnPageIndexChanging="gvRDW_PageIndexChanging">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="700px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="备品编号" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="备品描述" Width="300px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="用于设备" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="库存数量" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="所属部门" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="NO" HeaderText="备品编号" ReadOnly="True">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="备品描述" ReadOnly="True">
                    <HeaderStyle Width="300px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Device" HeaderText="用于设备" ReadOnly="True">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Stock" HeaderText="库存数量" ReadOnly="True">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Floor" HeaderText="所属部门" ReadOnly="True">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
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
