<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bigOrderImport.aspx.cs" Inherits="plan_bigOrderImport" %>

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
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="780" bgcolor="white" border="0">
            <tr>
                <td align="center" height="50">
                    <font face="����"></font>
                </td>
            </tr>
        </table>
        <table cellspacing="2" cellpadding="2" width="700" bgcolor="white" border="0">
            <tr>
                <td align="right" style="width: 90px">
                    �ļ�����: &nbsp;
                </td>
                <td valign="top" width="500" colspan="2">
                    <asp:DropDownList ID="ddlFileType" runat="server" Width="200px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90px; height: 23px">
                    ��������: &nbsp;
                </td>
                <td colspan="2" style="height: 23px" valign="top" width="500">
                    <asp:DropDownList ID="ddlImportType" runat="server" Width="200px">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">�󶩵���Ϣ</asp:ListItem>
                        <asp:ListItem Value="2">����������ɢ��</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="5" style="width: 90px">
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 28px; width: 90px;">
                    �����ļ�: &nbsp;
                </td>
                <td valign="top" width="500" style="height: 28px">
                    <input id="filename" style="width: 468px; height: 22px" type="file" name="filename1"
                        runat="server" />
                </td>
                <td style="width: 110px; height: 28px;">
                    <asp:Button ID="uploadPartBtn" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        OnClick="uploadPartBtn_ServerClick" Text="����" Width="80px" />
                </td>
            </tr>
            <tr>
                <td height="5" style="width: 90px">
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 90px; height: 18px;">
                    ���أ�
                </td>
                <td align="left" style="height: 18px" colspan="2">
                    <label id="here" onclick="submit();">
                        <a href="/docs/bigOrder.xls" target="blank"><font color="blue">����󶩵���ģ��</font></a>
                        &nbsp;&nbsp;<a href="/docs/bigOrder_ZS.xls" target="blank"><font color="blue"> &nbsp;
                            &nbsp; &nbsp;&nbsp; ����������ɢ����ģ��</font></a>
                    </label>
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 700px; height: 37px" runat="server" id="tb1">
            <tr>
                <td align="left" colspan="5" style="height: 24px">
                    <font color="red">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;����֮�ڵĶ��������޸ģ���Ҫ��дԭ�򣬲���Ҫ������Ա����������</font>
                </td>
            </tr>
        </table>
        <table style="width: 700px; height: 37px" runat="server" id="tb2">
            <tr>
                <td align="right" style="width: 90px; height: 24px">
                    ��˾��
                </td>
                <td style="width: 155px; height: 24px">
                    <asp:DropDownList ID="ddl_plant" runat="server" Width="152px">
                        <asp:ListItem Value="1">�Ϻ�ǿ�� SZX</asp:ListItem>
                        <asp:ListItem Value="2">��ǿ�� ZQL</asp:ListItem>
                        <asp:ListItem Value="5">����ǿ�� YQL</asp:ListItem>
                        <asp:ListItem Value="8">����ǿ�� HQL</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" style="width: 66px; height: 24px">
                    ���ţ�
                </td>
                <td style="width: 125px; height: 24px">
                    <asp:TextBox ID="txb_userno" runat="server" Height="22" TabIndex="3" Width="74px"></asp:TextBox>
                </td>
                <td style="height: 24px">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="���" Width="80px" OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPerson" runat="server" AllowPaging="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle" DataKeyNames="bo_id" PageSize="5"
            Width="400px" OnRowDataBound="gvPerson_RowDataBound" 
            OnRowDeleting="gvPerson_RowDeleting">
            <RowStyle CssClass="GridViewRowStyle" />
            <Columns>
                <asp:TemplateField>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    <ItemTemplate>
                        <asp:Label ID="label1" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="userNo" HeaderText="����">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="125px" />
                    <ItemStyle HorizontalAlign="Center" Width="125px" />
                </asp:BoundField>
                <asp:BoundField DataField="bo_approveName" HeaderText="����">
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="125px" />
                    <ItemStyle HorizontalAlign="Center" Width="125px" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkdelete" runat="server" CommandArgument='<%# Eval("bo_id") %>'
                            CommandName="Delete" Text="<u>ɾ��</u>"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="0" CellSpacing="0" CssClass="GridViewHeaderStyle"
                    GridLines="Vertical" Width="400px">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="����������" Width="200px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="ɾ��" Width="100px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
