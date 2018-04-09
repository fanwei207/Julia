<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_QA.aspx.cs" Inherits="SID_QA" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
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
        <table cellspacing="0" cellpadding="0" width="400px" border="0" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td align="right">
                    <asp:Label ID="lblSNO" runat="server" Width="50px" CssClass="LabelRight" Text="系列号:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtSNo" runat="server" Width="230px" TabIndex="1"></asp:TextBox>
                </td>
                <td align="Left">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="2" Text="查询"
                        Width="50px" OnClick="btnQuery_Click" />
                </td>
                <td align="Left">
                    <asp:Button ID="btnAddNew" runat="server" CssClass="SmallButton2" TabIndex="3" Text="添加"
                        Width="50px" OnClick="btnAddNew_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSID" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="20" 
            OnPreRender="gvSID_PreRender" DataKeyNames="SNO"
            OnPageIndexChanging="gvSID_PageIndexChanging" Width="400px" 
            OnRowDeleting="gvSID_RowDeleting">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="400px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="系列号" Width="50px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="系列描述" Width="300px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="删除" Width="50px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="SNO" HeaderText="系列号">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SDesc" HeaderText="系列描述">
                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
