<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QADandPart.aspx.cs" Inherits="QAD_QADandPart" %>

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
    <form id="form1" runat="server">
    <div align="Center" style="margin-top:20px;">
        <table>
            <tr>
                <td>域</td>
                <td>
                    <asp:DropDownList ID="ddlPlantCode" runat="server">
                        <asp:ListItem Text="SZX" Value="SZX" Selected="True">SZX</asp:ListItem>
                        <asp:ListItem Text="ZQL" Value="ZQL" Selected="False">ZQL</asp:ListItem>
                        <asp:ListItem Text="ZQZ" Value="ZQZ" Selected="False">ZQZ</asp:ListItem>
                        <asp:ListItem Text="YQL" Value="YQL" Selected="False">YQL</asp:ListItem>
                        <asp:ListItem Text="HQL" Value="HQL" Selected="False">HQL</asp:ListItem>
                        <asp:ListItem Text="TCB" Value="TCB" Selected="False">TCB</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>QAD号</td>
                <td>
                    <asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" 
            DataKeyNames="pt_domain,pt_part,pt_ship_wt,pt_size,pt_net_wt,pt_vend"
            onrowcancelingedit="gv_RowCancelingEdit" onrowediting="gv_RowEditing" 
            onrowupdating="gv_RowUpdating" >
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
                    <asp:TableCell HorizontalAlign="center" Text="供应商代码" Width="50px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="重量" Width="70px"></asp:TableCell>
			        <asp:TableCell HorizontalAlign="center" Text="体积" Width="60px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="箱数" Width="80px"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="pt_domain" HeaderText="域" ReadOnly="True">
                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="50px"  Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="pt_part" HeaderText="QAD" ReadOnly="True">
                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="50px"  Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="pt_vend" HeaderText="供应商代码" ReadOnly="True">
                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="50px"  Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="重量" >
                <EditItemTemplate>
                <asp:TextBox ID="txtptshipwt" runat="server" CssClass="SmallTextBox" Text='<%# Bind("pt_ship_wt") %>'
                    Width="80px"></asp:TextBox>
                </EditItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemTemplate>
                    <%#Eval("pt_ship_wt")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="体积" >
                <EditItemTemplate>
                <asp:TextBox ID="txtptsize" runat="server" CssClass="SmallTextBox" Text='<%# Bind("pt_net_wt") %>'
                    Width="80px"></asp:TextBox>
                </EditItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemTemplate>
                    <%#Eval("pt_net_wt")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="套/箱" >
                <EditItemTemplate>
                <asp:TextBox ID="txtptnetwt" runat="server" CssClass="SmallTextBox" Text='<%# Bind("pt_size") %>'
                    Width="80px"></asp:TextBox>
                </EditItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemTemplate>
                    <%#Eval("pt_size")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="pt_ship_wt" HeaderText="重量" ReadOnly="True" Visible="false">
                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="50px"  Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="pt_size" HeaderText="体积" ReadOnly="True" Visible="false">
                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="50px"  Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="pt_net_wt" HeaderText="套/箱" ReadOnly="True" Visible="false">
                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="50px"  Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ControlStyle Font-Bold="False" Font-Size="12px" />
            </asp:CommandField>
        </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>




