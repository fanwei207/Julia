<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcu_type_price_section_part.aspx.cs" Inherits="price_pcu_type_price_section_part" %>

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
        <div>
            <div>
                <asp:DropDownList ID="ddlType" runat="server" Width="100"  DataTextField="pcut_type" DataValueField="pcut_type" AutoPostBack ="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Label ID="lbMsg" runat="server" ForeColor="Red">您所选择的类型没有未导入期间！</asp:Label>
                &nbsp;&nbsp;
            供应商：
            <asp:TextBox ID="txtVend" runat="server" CssClass="SmallTextBox5" Width="100"></asp:TextBox>
                &nbsp;&nbsp;
            QAD：
            <asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox5" Width="100"></asp:TextBox>
                &nbsp;&nbsp;
            期间：
            <asp:DropDownList ID="ddlcalendar" runat="server"  Width="150" DataTextField="pcus_calendar" DataValueField="pcus_id" AutoPostBack ="true" OnSelectedIndexChanged="ddlcalendar_SelectedIndexChanged" ></asp:DropDownList>
                &nbsp;&nbsp;
            &nbsp;&nbsp;
            <asp:Button ID="btnSelect" runat="server" Text="查询" CssClass="SmallButton2" Width="80" OnClick="btnSelect_Click" />
                &nbsp;&nbsp;
            <asp:Button ID="btnExport" runat="server" Text="导出价格cimload" CssClass="SmallButton2" Width="100" OnClick="btnExport_Click" />

            </div>
            <asp:GridView ID="gvInfo" runat="server" CssClass="GridViewStyle GridViewRebuild" 
                AutoGenerateColumns="false" OnRowDataBound="gvInfo_RowDataBound"
                AllowPaging="true" PageSize="25" OnPageIndexChanging="gvInfo_PageIndexChanging"  >
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="700px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="没有找到数据" Width="700px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>

            </asp:GridView>

        </div>
    </form>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
         <asp:literal runat="server" id="ltlAlertTS" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
