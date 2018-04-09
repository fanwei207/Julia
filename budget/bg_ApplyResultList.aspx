<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bg_ApplyResultList.aspx.cs"
    Inherits="bg_ApplyResultList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table id="tblHead" runat="server" cellspacing="0" cellpadding="0" width="920px"
            height="20px" border="0">
            <tr height="20px">
                <td align="right">
                    <asp:Label ID="lblApplicant" runat="server" Width="50px" CssClass="LabelRight" Text="������:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtApplicant" runat="server" Width="80px" TabIndex="1"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblDept" runat="server" Width="40px" CssClass="LabelRight" Text="����:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtDept" runat="server" Width="80px" TabIndex="2"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblKeyword" runat="server" Width="60px" CssClass="LabelRight" Text="�ؼ���:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtKeyword" runat="server" Width="200px" TabIndex="3"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtYear" runat="server" Width="40px" TabIndex="4" MaxLength="4"></asp:TextBox>��
                    <asp:DropDownList ID="ddlMonth" runat="server" Width="40px" TabIndex="5" Font-Size="10pt">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                    ��
                </td>
                <td align="Center">
                    <asp:Button ID="btnQuery" runat="server" Text="��ѯ" CssClass="SmallButton3" Width="40px"
                        TabIndex="6" OnClick="btnQuery_Click" />
                </td>
                <td style="width: 200px">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvApply" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="25" OnPreRender="gvApply_PreRender"
            OnPageIndexChanging="gvApply_PageIndexChanging" Width="920px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="920px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="�����" Width="80px" HorizontalAlign="center" Font-Bold="false"
                            Visible="false"></asp:TableCell>
                        <asp:TableCell Text="������" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="��������������" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="��������" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="������" Width="70px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="����ʱ��" Width="60px" HorizontalAlign="center" Font-Bold="false"
                            Visible="false"></asp:TableCell>
                        <asp:TableCell Text="����˻�" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="������;" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="�ɱ�����" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="ʵ�ʷ���" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="ApplyNo" HeaderText="�����" Visible="false">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Applicant" HeaderText="������">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Dept" HeaderText="��������������">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Notes" HeaderText="��������" HtmlEncode="false">
                    <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle HorizontalAlign="Left" Wrap="true" />
                </asp:BoundField>
                <asp:BoundField DataField="Amount" HeaderText="������" HtmlEncode="false" DataFormatString="{0:N2}">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="CreatedDate" HeaderText="����ʱ��" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}"
                    Visible="false">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Account" HeaderText="����˻�">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Sub" HeaderText="������;">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="CC" HeaderText="�ɱ�����">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="140px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Actual" HeaderText="ʵ�ʷ���">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
            <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
