<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.WorkTypechanged" CodeFile="WorkTypechanged.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
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
        <asp:Table ID="Table1" runat="server" CellSpacing="0" BorderColor="Black" Width="780px"
            GridLines="None">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Right" Text="日期" Width="40px"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="year" CssClass="SmallTextBox Date" runat="server" Width="80px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="tolimit" runat="server"></asp:Label>&nbsp;&nbsp;
                    <asp:TextBox ID="total" runat="server" Width="80px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="uplable" runat="server"></asp:Label>&nbsp;&nbsp;
                    <asp:TextBox ID="uplimit" runat="server" Width="80px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" OnClick="searchRecord"
                        CausesValidation="False"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="Table2" runat="server" GridLines="Both" Width="780px" BorderWidth="1px"
            BorderColor="Black" CellSpacing="0">
            <asp:TableRow>
                <asp:TableCell Text="工号&nbsp;" HorizontalAlign="Right"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox TabIndex="1" runat="server" AutoPostBack="True" ID="workerNo" OnTextChanged="workerNo_changed"
                        Width="80px"></asp:TextBox>
                    <asp:RequiredFieldValidator Display="none" ControlToValidate="workerNo" ErrorMessage="工号不能为空"
                        runat="server" ID="Requiredfieldvalidator1" />
                </asp:TableCell><asp:TableCell Text="姓名&nbsp;" HorizontalAlign="Right"></asp:TableCell><asp:TableCell>
                    <asp:Label runat="server" ID="workerName" Width="50px"></asp:Label>
                    <asp:Label ID="userID" runat="server" Visible="False"></asp:Label>
                </asp:TableCell><asp:TableCell Text="类型&nbsp;" HorizontalAlign="Center">
                    <asp:DropDownList ID="Dropdownlist1" runat="server" Width="70px" TabIndex="2">
                    </asp:DropDownList>
                </asp:TableCell><asp:TableCell HorizontalAlign="center" Width="140px">
                    <asp:Button ID="save" TabIndex="3" OnClick="BtnSave_click" runat="server" Width="100px"
                        Text="保存" CssClass="SmallButton2"></asp:Button>
                </asp:TableCell></asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="780px" PageSize="16" AutoGenerateColumns="False"
            AllowPaging="True" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" SortExpression="gsort" HeaderText="序号" ReadOnly="True">
                    <ItemStyle Width="10%" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="usercode" SortExpression="userID" HeaderText="工号" ReadOnly="True">
                    <ItemStyle Width="20%" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="username" SortExpression="username" HeaderText="姓名" ReadOnly="True">
                    <ItemStyle Width="20%" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cDate" SortExpression="cDate" HeaderText="日期" ReadOnly="True">
                    <ItemStyle Width="20%" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ctype" SortExpression="ctype" HeaderText="类型" ReadOnly="True">
                    <ItemStyle Width="20%" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="Delete">
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="id" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid><br />
        </form>
    </div>
    <script language="javascript" type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
