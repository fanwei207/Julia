<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.RestForTime" CodeFile="RestForTime.aspx.vb" %>

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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <asp:Table ID="Table1" runat="server" Width="780px" BorderColor="Black" CellSpacing="0">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Bottom" Width="120px">
                    工号
                    <asp:TextBox runat="server" Width="90px" ID="workerNoSearch"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="120px">
                    姓名
                    <asp:TextBox runat="server" ID="workerNameSearch" Width="90"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="120px">
                    部门
                    <asp:DropDownList ID="department" runat="server" Width="90px">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="60px">
                    年
                    <asp:TextBox ID="yeartextbox" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="60px">
                    月
                    <asp:DropDownList ID="month" runat="server" Width="40px" Font-Size="10pt" CssClass="smallbutton2">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="40px" HorizontalAlign="center">
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" OnClick="searchRecord"
                        CausesValidation="False"></asp:Button>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="40px" HorizontalAlign="center" ColumnSpan="2">
                    <asp:Button ID="ButExcel" runat="server" CssClass="SmallButton3" Text="Excel" Width="60px">
                    </asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="Table2" runat="server" Width="780px" BorderColor="Black" CellSpacing="0"
            GridLines="Both" BorderWidth="1px">
            <asp:TableRow>
                <asp:TableCell>
                    工号
                    <asp:TextBox TabIndex="2" runat="server" AutoPostBack="True" ID="workerNo" OnTextChanged="workerNo_changed"
                        Width="100px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    姓名
                    <asp:Label runat="server" ID="workerName" Width="50px"></asp:Label>
                    <asp:Label ID="userID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="enterdate" runat="server" Visible="False"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    加班日期
                    <asp:TextBox ID="chgdate" runat="server" Width="80px" TabIndex="1"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    加班天数
                    <asp:TextBox ID="ovnumber" runat="server" Width="80px" TabIndex="3"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="center" Width="140px" RowSpan="2">
                    <asp:Button TabIndex="9" runat="server" Width="80px" ID="BtnSave" Text="保存" CssClass="SmallButton2"
                        OnClick="BtnSave_click"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    调休日1
                    <asp:TextBox ID="workdate" runat="server" Width="80px" TabIndex="4"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    调休天1
                    <asp:TextBox ID="number" runat="server" Width="80px" TabIndex="5"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    调休日2&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="sdate" runat="server" Width="80px" TabIndex="6"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    调休天2&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="number1" runat="server" Width="80px" TabIndex="7"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="5">
                    备注
                    <asp:TextBox ID="comment" runat="server" Width="570px" TabIndex="8"></asp:TextBox><font
                        color="#ff0033" size="2">* 请先输入加班日期</font>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" PageSize="16" AllowSorting="True" AutoGenerateColumns="False"
            AllowPaging="True" CssClass="GridViewStyle AutoPageSize" Width="1760px">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" SortExpression="gsort" HeaderText="序号" ReadOnly="True"
                    Visible="False">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="department" SortExpression="department" HeaderText="部门"
                    ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="80px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userNo" SortExpression="userID" HeaderText="工号" ReadOnly="True">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userName" SortExpression="userName" HeaderText="姓名" ReadOnly="True">
                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="weekday" SortExpression="weekday" HeaderText="星期" ReadOnly="True">
                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="chgdate" SortExpression="chgdate" HeaderText="加班日期" ReadOnly="True">
                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ovnumber" SortExpression="ovnumber" HeaderText="加班天数"
                    ReadOnly="True">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workdate" SortExpression="workdate" HeaderText="调休日1"
                    ReadOnly="True">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="number1" SortExpression="number1" HeaderText="调休天1" ReadOnly="True">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="sdate" SortExpression="sdate" HeaderText="调休日2" ReadOnly="True">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="number2" SortExpression="number2" HeaderText="调休天2" ReadOnly="True">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="enterdate" HeaderText="入司日期" ReadOnly="True">
                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="leavedate" HeaderText="离职日期" ReadOnly="True">
                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="Delete">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="money" HeaderText="金额" ReadOnly="True">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="50px" HorizontalAlign="Right" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="结算">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSettled" runat="server" OnCheckedChanged="chk_CheckedChanged"
                            Checked='<%# DataBinder.Eval(Container, "DataItem.needSettled") %>' AutoPostBack="True" />
                    </ItemTemplate>
                    <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="systemCodeName" HeaderText="计酬方式" ReadOnly="True">
                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="comment" HeaderText="备注" ReadOnly="True">
                    <HeaderStyle Width="800px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="800px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="id" ReadOnly="True"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
