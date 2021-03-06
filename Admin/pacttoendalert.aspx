<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.pactToEndAlert" CodeFile="pactToEndAlert.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <asp:Table runat="server" CellPadding="0" CellSpacing="0" ID="Table1" Width="820px">
            <asp:TableRow>
                <asp:TableCell Width="110px">
                    工号:<asp:TextBox ID="txb_workNo" runat="server" Width="70px" CssClass="TextLeft"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="110px">
                    姓名:
                    <asp:TextBox ID="txb_userName" runat="server" Width="70px" CssClass="TextLeft"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="160px">
                    部门名称:<asp:DropDownList ID="dropDept" runat="server" Width="100px">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" Width="150px">
                    报警类型:<asp:DropDownList ID="dropType" runat="server" Width="80px">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="200px">
                    日期:<asp:TextBox runat="server" ID="txb_ContractDateFrom" Width="70px" CssClass="TextLeft Date">
                    </asp:TextBox>—<asp:TextBox runat="server" ID="txb_ContractDateTo" Width="70px"
                        CssClass="TextLeft Date"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="70px" HorizontalAlign="left">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3"></asp:Button>
                </asp:TableCell>
                <asp:TableCell Width="50px" HorizontalAlign="Right">
                    <asp:Button ID="ButExcel" runat="server" CssClass="SmallButton3" Text="Excel"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="dgList" runat="server" Width="820px" AutoGenerateColumns="false"
            PageSize="18" AllowPaging="True" PagerStyle-Mode="NumericPages" CssClass="GridViewStyle AutoPageSize"
            HeaderStyle-Font-Bold="false">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="userNo"   HeaderText="工号">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userName"  HeaderText="姓名">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="dptName"   HeaderText="部门">
                    <HeaderStyle HorizontalAlign="Center" Width="360px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="360px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="type"   HeaderText="">
                    <ItemStyle HorizontalAlign="center" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="strContractDate"  HeaderText=""
                    DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NumDate"   HeaderText="剩余天数">
                    <ItemStyle HorizontalAlign="center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
			Form1.txb_workNo.focus();
    </script>
</body>
</html>
