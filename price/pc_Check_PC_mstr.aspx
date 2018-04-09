<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pc_Check_PC_mstr.aspx.cs"
    Inherits="price_pc_Check_PC_mstr" %>

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
    <form id="form1" runat="server">
        <div align="center">
            <div>
                QAD：&nbsp;&nbsp;<asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox5" Width="100"></asp:TextBox>&nbsp;&nbsp;
            供应商：&nbsp;&nbsp;<asp:TextBox ID="txtVender" runat="server" CssClass="SmallTextBox5" Width="80"></asp:TextBox>&nbsp;&nbsp;
            供应商名称：&nbsp;&nbsp;<asp:TextBox ID="txtVenderName" runat="server" CssClass="SmallTextBox5" Width="100"></asp:TextBox>&nbsp;&nbsp;
            域： &nbsp;&nbsp;<asp:DropDownList ID="ddlDomain" runat="server">
                <asp:ListItem Text="all"></asp:ListItem>
                <asp:ListItem Text="SZX" Selected="true"></asp:ListItem>
                <asp:ListItem Text="ZQL"></asp:ListItem>
                <asp:ListItem Text="ZQZ"></asp:ListItem>
                <asp:ListItem Text="YQL"></asp:ListItem>
                <asp:ListItem Text="HQL"></asp:ListItem>
            </asp:DropDownList>&nbsp;&nbsp;
            类型：&nbsp;&nbsp;<asp:DropDownList ID="ddlType" runat="server">
            </asp:DropDownList>&nbsp;&nbsp;
            <asp:CheckBox ID="ckbCheck" Text="已审核" runat="server" AutoPostBack="true"
                OnCheckedChanged="ckbCheck_CheckedChanged" />&nbsp;&nbsp;
                 <asp:CheckBox ID="chkDiff" Text="差异" runat="server" AutoPostBack="true" OnCheckedChanged="chkDiff_CheckedChanged" Checked="true" />&nbsp;&nbsp;
            <asp:CheckBox ID="chkIs100" Text="100系统申请的" runat="server" AutoPostBack="true " OnCheckedChanged="chkIs100_CheckedChanged" Checked="True" />
                &nbsp;&nbsp;
            <asp:Button ID="btnSelect" runat="server" Text="查询" CssClass="SmallButton2"
                OnClick="btnSelect_Click" />
                &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 
            <asp:Button ID="btnExport" runat="server" Text="导出报表" CssClass="SmallButton2" OnClick="btnExport_Click"
                   />
            </div>
            <div>
                <asp:GridView ID="gvInfo" AutoGenerateColumns="false" PageSize="30" AllowPaging="true"
                    runat="server" CssClass="GridViewStyle GridViewRebuild" EmptyDataText="No Data"
                    DataKeyNames="pc_id,pc_part,pc_list,pc_domain,pc_start,pc_expire"
                    OnRowDataBound="gvInfo_RowDataBound" OnPageIndexChanging="gvInfo_PageIndexChanging">
                    <RowStyle CssClass="GridViewRowStyle" />
                    <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <EmptyDataTemplate>
                        <asp:Table ID="Table1" Width="600px" CellPadding="-1" CellSpacing="0" runat="server"
                            CssClass="GridViewHeaderStyle" GridLines="Vertical">
                            <asp:TableRow>
                                <asp:TableCell Text="无数据" Width="600px" HorizontalAlign="center"></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField HeaderText="QAD" DataField="pc_part" Visible="true">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="供应商" DataField="pc_list" Visible="true">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="供应商名称" DataField="ad_name" Visible="true">
                            <HeaderStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="域" DataField="pc_domain" Visible="true">
                            <HeaderStyle Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="单位" DataField="pc_um" Visible="true">
                            <HeaderStyle Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="币种" DataField="pc_curr" Visible="true">
                            <HeaderStyle Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="100系统价格" DataField="price1" Visible="true">
                            <HeaderStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="QAD系统价格" DataField="price2" Visible="true">
                            <HeaderStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="起始日期" DataField="pc_start" Visible="true">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="终止日期" DataField="pc_expire" Visible="true">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="差异" Visible="true">
                            <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            <HeaderStyle HorizontalAlign="Center" Width="50px" Font-Size="12px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lbDiff" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="审核" Visible="true">
                            <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            <HeaderStyle HorizontalAlign="Center" Width="50px" Font-Size="12px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Button ID="btnCheck" runat="server" Text="审核通过" CssClass="SmallButton2" OnClick="btnCheck_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="isCheck" Visible="false">
                            <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                            <HeaderStyle HorizontalAlign="Center" Width="50px" Font-Size="12px" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lbCheck" runat="server" Text=' <%# Eval("isCheck") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
