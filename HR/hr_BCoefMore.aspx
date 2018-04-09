<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_BCoefMore.aspx.cs" Inherits="hr_BCoefMore" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>�߳��������߹���</title>
    <base target="_self" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table width="410px" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                </td>
                <td style="text-align: right;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    ����:<asp:TextBox ID="txtUserNo" runat="server" Width="65px" MaxLength="7" Enabled="False"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    ����:<asp:TextBox ID="txtUserName" runat="server" Width="65px" MaxLength="7" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;">
                    ����:<asp:DropDownList ID="dropDept" runat="server" AutoPostBack="True" 
                        DataTextField="Name" DataValueField="departmentID" 
                        onselectedindexchanged="dropDept_SelectedIndexChanged" Width="160px">
                    </asp:DropDownList>
                    <input id="hidDept" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;">
                    ����:<asp:DropDownList 
                        ID="dropWorkShop" runat="server" DataTextField="Name" DataValueField="id" 
                        Width="160px">
                    </asp:DropDownList>
                    <input id="hidWorkShop" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    <span style=" display:none;">ϵ��:<asp:TextBox ID="txtCoef" runat="server" Width="65px" MaxLength="7">0</asp:TextBox></span>
                </td>
                <td style="text-align: left;">
                    &nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="����" CssClass="SmallButton3" Width="60px"
                        OnClick="btnSave_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSave0" runat="server" Text="�ر�" CssClass="SmallButton3" Width="50px"
                        OnClientClick="window.close();" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvlist" name="gvlist" runat="server" AutoGenerateColumns="False"
            PageSize="18" OnRowDeleting="gvlist_RowDeleting" CssClass="GridViewStyle" 
            Width="410px" DataKeyNames="bcf_id">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="260px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="����" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ϵ��" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ɾ��" Width="50px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="����" DataField="department">
                    <HeaderStyle Width="150px" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="workshop">
                    <HeaderStyle Width="150px" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ϵ��" DataField="bcf_coef" Visible="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:CommandField ShowDeleteButton="True" DeleteText="&lt;u&gt;ɾ��&lt;/u&gt;">
                    <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" ForeColor="Black" />
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
