<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_Diff.aspx.cs" Inherits="SID_SID_Diff" %>

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
        <table cellspacing="1" cellpadding="1" width="700px" border="0">
            <tr>
                <td align="right">
                    <asp:Label ID="lblDocumentStart" runat="server" Width="80px" CssClass="LabelRight"
                        Font-Bold="false" Text="��֤��ʼ����:"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtDocumentStart" runat="server" Width="80px" onkeydown="event.returnValue=false;"
                        onpaste="return false;" TabIndex="1" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblDocumentEnd" runat="server" Width="80px" CssClass="LabelRight"
                        Font-Bold="false" Text="��֤��������:"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtDocumentEnd" runat="server" Width="80px" onkeydown="event.returnValue=false;"
                        onpaste="return false;" TabIndex="2" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblDeclarationStart" runat="server" Width="80px" CssClass="LabelRight"
                        Font-Bold="false" Text="������ʼ����:"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtDeclarationStart" runat="server" Width="80px" onkeydown="event.returnValue=false;"
                        onpaste="return false;" TabIndex="3" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblDeclarationEnd" runat="server" Width="80px" CssClass="LabelRight"
                        Font-Bold="false" Text="���ؽ�������:"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtDeclarationEnd" runat="server" Width="80px" onkeydown="event.returnValue=false;"
                        onpaste="return false;" TabIndex="4" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:Button ID="btnQuery" runat="server" Text="��ѯ" CssClass="SmallButton3" CausesValidation="false"
                        OnClick="btnQuery_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvDiff" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="25" OnPreRender="gvDiff_PreRender"
            OnPageIndexChanging="gvDiff_PageIndexChanging" Width="700px">
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
                        <asp:TableCell Text="��֤��Ʊ��" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��֤��Ʊ����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���ط�Ʊ��" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���ط�Ʊ����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��֤��Ʊ���" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���ط�Ʊ���" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��Ʊ������" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="DocumentNo" HeaderText="��֤��Ʊ��">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="DocumentShipDate" HeaderText="��֤��Ʊ����" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="false">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DeclarationNo" HeaderText="���ط�Ʊ��">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="DeclarationShipDate" HeaderText="���ط�Ʊ����" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="false">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DocumentAmount" HeaderText="��֤��Ʊ���" DataFormatString="{0:#0.00}"
                    HtmlEncode="false">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="DeclarationAmount" HeaderText="���ط�Ʊ���" DataFormatString="{0:#0.00}"
                    HtmlEncode="false">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="DiffAmount" HeaderText="��Ʊ������" DataFormatString="{0:#0.00}"
                    HtmlEncode="false">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
