<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SampleNotesReceiveLists.aspx.cs"
    Inherits="supplier_SampleNotesReceiveLists" %>

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
    <div>
        <form id="Form1" method="post" runat="server">
        <table width="1000px">
            <tr>
                <td>
                    打样单：<asp:TextBox ID="txt_bosnbr" runat="server" Width="108px"></asp:TextBox>(*)
                    供应商：<asp:DropDownList ID="ddl_vend" runat="server" Height="21px" Width="213px" DataTextField="ad_name"
                        DataValueField="ad_addr">
                    </asp:DropDownList>
                    &nbsp; &nbsp;&nbsp;
                    <asp:DropDownList ID="ddl_ReceiveState" runat="server">
                        <asp:ListItem Value="2">全部</asp:ListItem>
                        <asp:ListItem Value="0" Selected="True">未收货</asp:ListItem>
                        <asp:ListItem Value="1">已收货</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;<asp:Button ID="btn_Search" runat="server" Text="查询" CssClass="SmallButton2"
                        OnClick="btn_Search_Click" Width="40px" />
                    &nbsp;<asp:Button ID="btn_Cancel" runat="server" Text="取消" CssClass="SmallButton2"
                        Width="38px" OnClick="btn_Cancel_Click" />
                    &nbsp;&nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gv_bos_mstr" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" OnPageIndexChanging="gv_bos_mstr_PageIndexChanging"
                        OnRowDataBound="gv_bos_mstr_RowDataBound" OnRowCommand="gv_bos_mstr_RowCommand"
                        PageSize="20" DataKeyNames="bos_nbr,bos_receiptIsConfirm,bos_isCanceled" Width="1000px">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField DataField="bos_nbr" HeaderText="打样单">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_vend" HeaderText="供应商">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_vendName" HeaderText="供应商名称">
                                <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_rmks" HeaderText="备注">
                                <HeaderStyle HorizontalAlign="Center" Width=" " />
                                <ItemStyle HorizontalAlign="Left" Width=" " />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_createddate" HeaderText="生成日期" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="bos_vendIsConfirm" HeaderText="供应商">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="bos_isCanceled" HeaderText="状态" >
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkDoReceive" runat="server" CommandName="DoReceive" Font-Underline="True"
                                        Font-Size="12px" CommandArgument='<%# Eval("bos_nbr") %>' ForeColor="Black">收货</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="bos_receiptDate" HeaderText="收货日期" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
