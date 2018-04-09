<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SampleDeliveryCheckList.aspx.cs" Inherits="SampleDelivery_SampleDeliveryCheckList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
<div>
        <form id="Form1" method="post" runat="server">
        <table width="1000px">
            <tr>
                <td>
                    送样单：<asp:TextBox ID="txt_nbr" runat="server" Width="109px"></asp:TextBox>(*)
                    &nbsp; &nbsp; &nbsp; &nbsp; 接收方：<asp:TextBox ID="txt_receiver" runat="server" Width="109px">
                    </asp:TextBox>
                    &nbsp; &nbsp; &nbsp; &nbsp; 生成日期:<asp:TextBox ID="txt_CreatedDate1" runat="server"
                        Width="82px" CssClass="Date"></asp:TextBox>-<asp:TextBox ID="txt_CreatedDate2" runat="server"
                            Width="79px" CssClass="Date"></asp:TextBox>
                    <asp:TextBox ID="txt_rmks" runat="server" Width="19px" Visible="false"></asp:TextBox>&nbsp;
                    <asp:DropDownList ID="ddl_checkValue" runat="server" Width="84px">
                        <asp:ListItem Value="0" Selected="True">待检测</asp:ListItem>
                        <asp:ListItem Value="1">已检测</asp:ListItem>
                        <asp:ListItem Value="2"> 全部 </asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;<asp:Button ID="btn_Search" runat="server" Text="查询"
                        CssClass="SmallButton2" OnClick="btn_Search_Click" Width="40px" />
                    &nbsp; &nbsp;<asp:Button ID="btn_Cancel" runat="server" Text="取消" CssClass="SmallButton2"
                        Width="38px" OnClick="btn_Cancel_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gv_mstr" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" OnPageIndexChanging="gv_mstr_PageIndexChanging"
                        OnRowDataBound="gv_mstr_RowDataBound" OnRowCommand="gv_mstr_RowCommand"
                        PageSize="20" DataKeyNames="Id,CheckResult,IsSended,IsCanceled"
                        Width="1070px">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="900px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="送样单" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="接收方" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="发送地" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="备注"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="生成日期" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="检测" Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text=" 发送" Width="40px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text=" 状态" Width="40px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有记录" ColumnSpan="8"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="No" HeaderText="送样单">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Receiver" HeaderText="接收方">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Shipto" HeaderText="运达地">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Left" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remarks" HeaderText="备注">
                                <HeaderStyle HorizontalAlign="Center" Width="380px" />
                                <ItemStyle HorizontalAlign="Left" Width="380px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CreatedDate" HeaderText="生成日期" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CheckResult" HeaderText="检测">
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="检测">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkDetail" runat="server" CommandName="Detail" Font-Underline="True"
                                        CommandArgument='<%# Eval("Id") %>' ForeColor="Black">检测</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
<%--                            <asp:TemplateField HeaderText="邮件">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkEmail" runat="server" CommandName="SendDetail" Font-Underline="True"
                                        OnClientClick="return confirm('确定通知供应商已建立此打样单');" CommandArgument='<%# Container.DataItemIndex %>'
                                        ForeColor="Black">发送</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>--%>
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
