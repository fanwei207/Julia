<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_accessApplymstr.aspx.cs"
    Inherits="AccessApply_admin_accessApplymstr" %>

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
        <form id="form1" method="post" runat="server">
        <table style="width: 1000px" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left" style="width: 277px">
                </td>
                <td style="width: 217px">
                </td>
                <td>
                </td>
            </tr>
            <tr valign="middle" style="background-image: url(../images/banner03.jpg);">
                <td align="left" style="width: 650px; height: 26px;">
                    �������:<asp:TextBox ID="txtApplyId" runat="server" Width="51px" CssClass="SmallTextBox"></asp:TextBox>
                    &nbsp; ������:<asp:TextBox ID="txtApplyName" runat="server" Width="75px" CssClass="SmallTextBox"></asp:TextBox>
                    &nbsp;
                    <asp:CheckBox ID="chkb_displayToApprove" runat="server" Text="����ʾ������" Checked="true"
                        OnCheckedChanged="chkb_displayToApprove_CheckedChanged" AutoPostBack="true" Width="158px" />
                    &nbsp;
                </td>
                <td align="left" style="width: 190px; height: 26px;">
                    <asp:Button ID="btnSearch" runat="server" Text="��ѯ" OnClick="btnSearch_Click" CssClass="SmallButton2"
                        Width="50px" />
                    <asp:Button ID="btnApply" runat="server" Text="���������Ȩ��" OnClick="btnApply_Click"
                        Width="100px" CssClass="SmallButton2" />
                </td>
                <td align="left" style="height: 26px">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvAccessPro" AllowPaging="True" AutoGenerateColumns="False" PageSize="25"
            CssClass="GridViewStyle GridViewRebuild" runat="server" Width="1000px" DataKeyNames="aamId,applyUserID,approveUserID,approveLayer"
            OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" OnRowEditing="gv_RowEditing"
            OnRowCommand="gv_RowCommand" OnRowCancelingEdit="gv_RowCancelingEdit" OnPageIndexChanging="gv_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="900px"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="�������" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="������" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="����" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��������"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��������" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="������" Width="90px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="��������" Width="90px"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableFooterRow BackColor="white" ForeColor="Black">
                        <asp:TableCell HorizontalAlign="Center" Text="�޷���������������Ϣ" ColumnSpan="7"></asp:TableCell>
                    </asp:TableFooterRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="�������" DataField="aamId" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="55px" />
                    <ItemStyle HorizontalAlign="Center" Width="55px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="������" DataField="applyUserName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="applyDepartmentName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��������" DataField="applyReason" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="Center" Width="330px" />
                    <ItemStyle HorizontalAlign="Left" Width="330px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����ʱ��" DataField="applyDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="��ϸ">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkLastApp" Text="�鿴" ForeColor="black" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("aamId") %>' CommandName="lookApplyProcess" />
                    </ItemTemplate>
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" Text="Edit" ForeColor="black" Font-Underline="true"
                            Enabled="false" Font-Size="12px" runat="server" CommandArgument='<%# Eval("aamId") %>'
                            CommandName="Edit" />&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkDelete" Text="delete" ForeColor="black" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("aamId") %>' CommandName="delete"
                            Enabled="false" />
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                    <ControlStyle Font-Size="12px" Font-Underline="True" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="��һ������ID" DataField="approveUserID" ReadOnly="True" Visible="False">
                    <HeaderStyle HorizontalAlign="Center" Width="0px" />
                    <ItemStyle HorizontalAlign="Center" Width="0px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="��һ������" DataField="approveUserName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="65px" />
                    <ItemStyle HorizontalAlign="Center" Width="65px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="����">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkFirstApp" Text="����" ForeColor="black" Font-Underline="true"
                            Enabled="false" Font-Size="12px" runat="server" CommandArgument='<%# Eval("aamId") %>'
                            CommandName="startApprove" />
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="����ʱ��" DataField="approveDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
        <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </div>
</body>
</html>
