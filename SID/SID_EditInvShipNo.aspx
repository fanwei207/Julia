<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_EditInvShipNo.aspx.cs" Inherits="SID_EditInvShipNo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
        <table cellspacing="0" cellpadding="0" width="590px" border="0" class="main_top">
            <tr>
                <td align="right" width="90px">
                    <asp:Label ID="lblSysPKNo" runat="server" Width="100px" CssClass="LabelRight" Text="财务发票号:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" width="120px">
                    <asp:TextBox ID="txt_inv" runat="server" Width="100px" TabIndex="1" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="right" width="90px">
                    <asp:Label ID="lblShipNo" runat="server" Width="55px" CssClass="LabelRight" Text="报关单号:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" width="120px">
                    <asp:TextBox ID="txt_shipno" runat="server" Width="110px" TabIndex="2" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="Left" width="80px">
                    &nbsp;&nbsp;<asp:Button ID="btn_add" runat="server"  TabIndex="3"
                        Text="新增" Width="50px" OnClick="btn_add_Click" />
                </td>

                <td align="Left" width="80px">
                    &nbsp;&nbsp;<asp:Button ID="btn_save" runat="server"  TabIndex="3"
                        Text="保存" Width="50px" OnClick="btn_save_Click" />
                        <input type="hidden" id ="hid_id" runat="server"/>
                </td>
                <td align="Left" width="80px">
                    &nbsp;&nbsp;<asp:Button ID="btn_cancel" runat="server"  TabIndex="3"
                        Text="取消" Width="50px" OnClick="btn_cancel_Click" />
                </td>
                <td align="Left" width="90px">
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnQuery" runat="server" TabIndex="4"
                        Text="查询" Width="50px" OnClick="btnQuery_Click" />
                </td>
                <td align="Left" width="300px">
                    &nbsp;&nbsp;<asp:Button ID="btn_export" runat="server"  TabIndex="5"
                        Text="导出" Width="50px" OnClick="btn_export_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvInv" runat="server" AllowPaging="True" PageSize="18" AutoGenerateColumns="False"
            DataKeyNames="sid_id" OnRowCommand="gvInv_RowCommand" OnPageIndexChanging="gvInv_PageIndexChanging"
            OnRowDataBound="gvInv_RowDataBound" Width="400px" CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="sid_inv_nbr" HeaderText="财务发票号">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="sid_shipno" HeaderText="报关单号">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit1"
                       Text="<u>编辑</u>" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle ForeColor="Black" HorizontalAlign="Center" Width="50px" />
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
