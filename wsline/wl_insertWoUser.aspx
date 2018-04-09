<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wl_insertWoUser.aspx.cs"
    Inherits="wsline_wl_insertWoUser" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        function doSelect() {
            var dom = document.all;
            var el = event.srcElement;
            if (el.id.indexOf("chkAll") >= 0 && el.tagName == "INPUT" && el.type.toLowerCase() == "checkbox") {
                var ischecked = false;
                if (el.checked)
                    ischecked = true;
                for (i = 0; i < dom.length; i++) {
                    if (dom[i].id.indexOf("chkUsers") >= 0 && dom[i].tagName == "INPUT" && dom[i].type.toLowerCase() == "checkbox")
                        dom[i].checked = ischecked;
                }
            }
        }
    </script>
    <style type="text/css">
        .style1
        {
            height: 27px;
            width: 96px;
        }
        .style2
        {
            height: 27px;
            width: 66px;
        }
        .style3
        {
            height: 27px;
            width: 80px;
        }
        .style4
        {
            height: 27px;
            width: 148px;
        }
        .style5
        {
            height: 27px;
            width: 224px;
        }
    </style>
</head>
<body>
    <div align="Center">
        <form id="Form1" method="post" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="1040px">
            <tr>
                <td align="right" class="style1">
                    �ӹ���:
                </td>
                <td align="left" style="height: 27px; width: 120px;">
                    <asp:TextBox ID="txtWorkOrder" runat="server" Height="20px" Width="100px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td align="right" class="style2">
                    ID:
                </td>
                <td align="left" style="height: 27px; width: 120px;">
                    <asp:TextBox ID="txtID" runat="server" Height="20px" Width="100px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td align="right" class="style3">
                    ����:
                </td>
                <td align="left" class="style4">
                    <asp:DropDownList ID="ddlProcInput" runat="server" Width="140px" DataTextField="wo2_mop_procname"
                        DataValueField="wo2_mop_proc" OnSelectedIndexChanged="ddlProcInput_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td align="right" style="height: 27px; width: 140px;">
                    ��λ:
                </td>
                <td align="left" class="style5">
                    <asp:DropDownList ID="ddlPostionInput" runat="server" Width="140px" DataTextField="wo2_sop_procname"
                        DataValueField="wo2_sop_proc" AutoPostBack="True" OnSelectedIndexChanged="ddlPostionInput_SelectedIndexChanged">
                        <asp:ListItem Value="0">----</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" style="height: 27px; width: 81px;">
                    <asp:Button ID="btnSearch" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        Text="��ѯ" OnClick="btnSearch_Click" />
                </td>
                <td align="right" style="height: 27px; width: 81px;">
                    <asp:Button ID="btnSave" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        Text="����" OnClick="btnSave_Click" />
                </td>
                <td align="right" style="height: 27px; width: 80px;">
                    <asp:Button ID="txtClear" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        Text="���" OnClick="txtClear_Click" />
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    ��Ч����:&nbsp;
                </td>
                <td align="left" style="height: 27px; width: 120px;">
                    <asp:TextBox ID="txtEffecDate" runat="server" Height="20px" Width="100px" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
                <td align="right" class="style2">
                    ����:
                </td>
                <td align="left" style="height: 27px; width: 120px;">
                    <asp:TextBox ID="txtUserNo" runat="server" Height="20px" Width="100px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td align="right" class="style3">
                    ����:
                </td>
                <td align="left" class="style4">
                    <asp:TextBox ID="txtQty" runat="server" Height="20px" Width="100px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td align="right" style="height: 27px;" colspan="2">
                    <asp:Label ID="lblSum" runat="server"></asp:Label>
                </td>
                <td align="right" style="height: 27px; width: 81px;">
                    <asp:Button ID="btnAdd" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        Text="���" OnClick="btnAdd_Click" />
                </td>
                <td align="right" style="height: 27px; width: 81px;">
                    <asp:Button ID="btnAvg" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        Text="ƽ������" OnClick="btnAvg_Click" />
                </td>
                <td align="right" style="height: 27px; width: 80px;">
                    <asp:Button ID="btnDel" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        Text="ɾ��" OnClick="btnDel_Click" Visible="False" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="1090px" OnRowDataBound="gvList_RowDataBound" DataKeyNames="wo2_id,wo2_isBarCodesys,wo2_effDate"
            OnRowCommand="gvList_RowCommand">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                    GridLines="Vertical" Width="1040px">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="�ӹ���" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="ID" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="����" Width="120px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="�ص�" Width="40px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��������" Width="50px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="������" Width="50px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="����" Width="40px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="����" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��λ" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��λϵ��" Width="52px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="����" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="�㱨��" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="�㱨ʱ��" Width="110px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="ɾ��" Width="40px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="wo2_nbr" HeaderText="�ӹ���">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo2_wID" HeaderText="ID">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_part" HeaderText="����">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_site" HeaderText="�ص�">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_qty_ord" HeaderText="��������" DataFormatString="{0:F0}"
                    HtmlEncode="False">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_line" HeaderText="������">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkUsers" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkAll" runat="server" ForeColor="Black" AutoPostBack="False" onclick="doSelect()" />
                    </HeaderTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="wo2_userNo" HeaderText="����">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo2_userName" HeaderText="����">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo2_postName" HeaderText="��λ">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="wo2_postProportion" HeaderText="��λϵ��">
                    <HeaderStyle Width="52px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="52px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="����">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_wo2_line_comp" runat="server" CssClass="TextRight" Width="100%"
                            Text='<%# Bind("wo2_line_comp") %>'></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="60px" Font-Bold="false" />
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                </asp:TemplateField>
                <asp:BoundField DataField="wo2_effdate" HeaderText="��Ч����" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo2_reportedName" HeaderText="�㱨��">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="wo2_reportedDate" HeaderText="�㱨ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    HtmlEncode="False">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkdelete" Text="<u>ɾ��</u>" ForeColor="Black" Font-Underline="True"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("wo2_id") + "," + Eval("wo2_effdate") %>'
                            CommandName="del" />
                    </ItemTemplate>
                    <HeaderStyle Width="40px" Font-Bold="False" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="wo2_proc" HeaderText="����">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <font style="color: Red; font-size: 11px;">ע��:��ձ����㱨��Ա��ɾ���������㱨��Ա</font></form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
