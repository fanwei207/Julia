<%@ Page Language="VB" AutoEventWireup="false" CodeFile="computerQuery.aspx.vb" Inherits="computermanage_computerQuery" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="left">
        <form id="form1" runat="server" method="post">
        <table id="table1" width="1000" border="0">
            <tr>
                <td>
                    ��&nbsp;&nbsp;&nbsp;&nbsp;��<asp:DropDownList ID="drptype" runat="server" Width="120">
                    </asp:DropDownList>
                </td>
                <td>
                    �ʲ����<asp:TextBox ID="txbassetno" runat="server" Width="120" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td>
                    �������<asp:DropDownList ID="drpnet" runat="server" Width="120">
                        <asp:ListItem Value="0" Text="----"></asp:ListItem>
                        <asp:ListItem Value="1" Text="����"></asp:ListItem>
                        <asp:ListItem Value="2" Text="δ����"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    ״̬<asp:DropDownList ID="drpstatus" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    ��&nbsp;&nbsp;&nbsp;&nbsp;��<asp:DropDownList ID="drpdepartment" runat="server" Width="120px"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td>
                    Ա&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��<asp:DropDownList ID="drpusername"
                        runat="server" Width="120px">
                    </asp:DropDownList>
                </td>
                <td colspan="2" align="center">
                    <asp:Button ID="btnser" Text="��ѯ" runat="server" CssClass="SmallButton2" Width="50"
                        CausesValidation="False"></asp:Button>
                    &nbsp;<asp:Button ID="btncel" Text="ȡ��" runat="server" CssClass="SmallButton2" Width="50"
                        CausesValidation="False"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="datagrid1" runat="server" Width="1780px" PageSize="20" AllowPaging="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="typename" HeaderText="����">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="assetno" HeaderText="�ʲ����">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cpu" HeaderText="CPU">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="memory" HeaderText="�ڴ�">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="harddisk" HeaderText="Ӳ��">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="display" HeaderText="��ʾ��">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="keyboard" HeaderText="����">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mouse" HeaderText="���">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ip" HeaderText="IP��ַ">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mac" HeaderText="MAC��ַ">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="os" HeaderText="����ϵͳ">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="net" HeaderText="������">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="departmentname" HeaderText="����">
                    <HeaderStyle Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="username" HeaderText="Ա��">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="begindate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="enddate" HeaderText="�黹����" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="status" HeaderText="״̬">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="description" HeaderText="����">
                    <HeaderStyle Width="500px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server" EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
