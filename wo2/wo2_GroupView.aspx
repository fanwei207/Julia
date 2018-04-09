<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_GroupView.aspx.cs" Inherits="wo2_GroupView" %>

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
        <table width="680px">
            <tr>
                <td>
                    <asp:Label ID="lblInfo" runat="server" CssClass="LabelLeft" Width="620px" Font-Bold="false"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnClose" runat="server" Width="40" CssClass="SmallButton3" Text="����"
                        OnClick="btnClose_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvDetail" runat="server" AllowPaging="True"
            AutoGenerateColumns="False" Width="680px" 
            CssClass="GridViewStyle AutoPageSize" PageSize="25"
            OnPreRender="gvDetail_PreRender" DataKeyNames="DetailID" OnPageIndexChanging="gvDetail_PageIndexChanging"
            OnRowDeleting="gvDetail_RowDeleting" 
            OnRowDataBound="gvDetail_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="680px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="Ա������" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Ա������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��λ����" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��λ����" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��λϵ��" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ɾ��" Width="50px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="UserNo" HeaderText="Ա������">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UserName" HeaderText="Ա������">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="MOPProc" HeaderText="�������">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="MOPName" HeaderText="��������">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SOPProc" HeaderText="��λ����">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SOPName" HeaderText="��λ����">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SOPRate" HeaderText="��λϵ��" DataFormatString="{0:N2}">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:TemplateField>
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" Text="<u>ɾ��</u>" ForeColor="Black" CommandName="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script>
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
