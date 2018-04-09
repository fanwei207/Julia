<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vc_mstrList.aspx.cs" Inherits="Purchase_vc_mstrList" %>

<!DOCTYPE html>
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
    <form id="form1" runat="server">
    <div align="center">
        <table style="width: 1150px">
            <tr>
                <td>
                    科目
                    <asp:DropDownList ID="ddl_cate" runat="server" Width="120px" DataTextField="vcc_name"
                        DataValueField="vcc_id" CssClass="Param">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    供应商&nbsp;&nbsp<asp:TextBox ID="txtVender" 
                        runat="server" Width="100px" CssClass="Supplier Param"></asp:TextBox>
                </td>
                <td>
                    日期<asp:TextBox ID="txtDate1" 
                        runat="server" Width="70px" CssClass="SmallTextBox EnglishDate Param"></asp:TextBox>--<asp:TextBox
                        ID="txtDate2" runat="server" Width="70px" 
                        CssClass="SmallTextBox EnglishDate Param"></asp:TextBox>
                </td>
                <td>
                    总金额<asp:TextBox runat="server" 
                        ID="txt_total" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox ID="chk_IsCancle" Text="仅取消" runat="server" Visible="false"/>
                </td>
                <td align="right">
                    <asp:Button runat="server" Text="查询" CssClass="SmallButton3" Width="70px" ID="btn_check"
                        OnClick="btn_check_Click" />
                </td>
                <td>
                    <asp:Button runat="server" Text="导出" CssClass="SmallButton3" Width="70px" ID="btn_export"
                        OnClick="btn_export_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView runat="server" ID="gv" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
            Width="1350px" PageSize="20" AllowPaging="True" OnRowCommand="gv_RowCommand"
            DataKeyNames="vc_id,vc_plant,vc_vend,vc_cateName,vc_date,vc_userNameC,vc_email,vc_FactoryName,usr_companyName,vc_amount,vc_emailDate,vc_confirmName,vc_status"
            OnRowDeleting="gv_RowDeleting" OnPageIndexChanging="gv_PageIndexChanging" 
            OnRowDataBound="gv_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="tb1" Width="1050px" runat="server" CellPadding="-1" CellSpacing="0"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="工厂" HorizontalAlign="Center" Width="30px"></asp:TableCell>
                        <asp:TableCell Text="供应商" HorizontalAlign="Center" Width="100px"></asp:TableCell>
                        <asp:TableCell Text="科目" HorizontalAlign="Center" Width="50px"></asp:TableCell>
                        <asp:TableCell Text="金额" HorizontalAlign="Center" Width="40px"></asp:TableCell>
                        <asp:TableCell Text="比率" HorizontalAlign="Center" Width="40px"></asp:TableCell>
                        <asp:TableCell Text="提交人" HorizontalAlign="Center" Width="50px"></asp:TableCell>
                        <asp:TableCell Text="提交时间" HorizontalAlign="Center" Width="50px"></asp:TableCell>
                        <asp:TableCell Text="备注" HorizontalAlign="Center" Width="150px"></asp:TableCell>
                        <asp:TableCell Text="修改" HorizontalAlign="Center" Width="50px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="工厂" DataField="vc_plantname" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="地点" DataField="vc_site" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="供应商" DataField="usr_companyName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="250px" />
                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="科目" DataField="vc_catename" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="170px" />
                    <ItemStyle HorizontalAlign="Center" Width="170px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="金额" DataField="vc_amount" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="比率%" DataField="vc_rate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="状态" DataField="vc_status" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="提交人" DataField="submitter" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="提交时间" DataField="vc_date" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="备注" DataField="vc_remark" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="250px" />
                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                </asp:BoundField>
                <asp:TemplateField>
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDetail" runat="server" Text="Detail" ForeColor="Black" CommandName="myEdit"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Font-Bold="false"></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="再次发送邮件" >
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnConfirm" runat="server" Text='<%# Bind("vc_confirmName") %>' ForeColor="Black" CommandName="Confirm"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Font-Bold="false"></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="8pt" Font-Underline="True" ForeColor="Black" />
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>取消</u>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:CommandField>
                <asp:BoundField HeaderText="邮件发送日期" DataField="vc_emailDate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
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
