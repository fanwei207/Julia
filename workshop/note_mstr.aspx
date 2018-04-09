<%@ Page Language="C#" AutoEventWireup="true" CodeFile="note_mstr.aspx.cs" Inherits="workshop_note_mstr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
        <table cellspacing="0" cellpadding="0" style="width: 1020px;">
            <tr>
                <td style=" width:30px;">
                    公司:
                </td>
                <td>
                    <asp:DropDownList ID="dropDomain" runat="server">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">SZX</asp:ListItem>
                        <asp:ListItem Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Value="5">YQL</asp:ListItem>
                        <asp:ListItem Value="8">HQL</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style=" width:30px;">
                    日期:
                </td>
                <td>
                    <asp:TextBox ID="txtDate" CssClass="Date Param" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td style=" width:30px;">
                    线长:
                </td>
                <td>
                    <asp:TextBox ID="txtUser" CssClass="Param" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td style=" width:30px;">
                    车间:
                </td>
                <td>
                    <asp:TextBox ID="txtDept" CssClass="Param" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style=" width:50px;">
                    生产线:
                </td>
                <td>
                    <asp:TextBox ID="txtLine" CssClass="Param" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style=" width:30px;">
                    工单:
                </td>
                <td>
                    <asp:TextBox ID="txtNbr" runat="server" Width="100px" CssClass="Param"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnQuery_Click" />
                </td>
                <td colspan="2">
                    <asp:Button ID="btn_export" runat="server" Text="导出" CssClass="SmallButton3" OnClick="btn_export_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="1020px" PageSize="20" DataKeyNames="note_id" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
            OnRowCommand="gv_RowCommand">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="序号" DataField="note_index" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Right" Width="40px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="公司" DataField="note_domain" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="日期" DataField="note_createDate" ReadOnly="True" 
                    DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="车间" DataField="note_workshop" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="130px" />
                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="生产线" DataField="note_line" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="线长" DataField="note_createName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工单" DataField="note_nbr" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="备注" DataField="note_rmks" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="400px" />
                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkDetail" runat="server" CommandName="Detail"><u>明细</u></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="GridViewPagerStyle" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
