<%@ Page Language="C#" AutoEventWireup="true" CodeFile="note_det.aspx.cs" Inherits="note_det" %>

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
        <table cellspacing="0" cellpadding="0" style="width: 600px" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td style=" text-align:right;">
                    &nbsp;<asp:Button ID="btnBack" 
                        runat="server" CssClass="SmallButton3" OnClick="btnBack_Click"
                        TabIndex="0" Text="返回" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="600px" OnRowDataBound="gv_RowDataBound"
            PageSize="20" DataKeyNames="ntt_sel" AllowPaging="True" 
            OnPageIndexChanging="gv_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="序号" DataField="ntt_index" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Right" Width="40px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="事项" DataField="ntp_type" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="工作内容">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ntt_desc") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txDesc" runat="server" MaxLength="100" Text='<%# Bind("ntt_desc") %>'
                            Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="400px" />
                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="GridViewPagerStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
