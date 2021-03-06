<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.basicalsalary" CodeFile="basicalsalary.aspx.vb" %>

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
        <asp:Table ID="Table1" GridLines="Both" Width="1070px" BorderColor="Black" CellSpacing="0"
            runat="server">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Right" Text="基础工资类型&nbsp;"
                    Width="80px"></asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="typeID" runat="server" Width="60px" AutoPostBack="True" OnSelectedIndexChanged="typechange"
                        TabIndex="1">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年月
                    <asp:TextBox ID="year" runat="server" Width="50px"></asp:TextBox>&nbsp;
                    <asp:DropDownList ID="month" runat="server" Width="40px" AutoPostBack="True" Font-Size="10pt"
                        CssClass="smallbutton2">
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
                <asp:TableCell HorizontalAlign="Right" Text="工号:"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="workNO" runat="server" Width="100px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right" Text="姓名:"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="workName" runat="server" Width="100px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                    <asp:Button ID="Bsearch" CssClass="SmallButton3" runat="server" OnClick="search"
                        Text="查询" Width="60px"></asp:Button>
                        &nbsp<asp:Button ID="ButExcel" runat="server" CssClass="SmallButton3" Text="Excel" Width="60px"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" Text="部门:"></asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="departmentdrop" runat="server" Width="120px" AutoPostBack="True"
                        OnSelectedIndexChanged="workshopchange" TabIndex="2">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right" Text="职位:"></asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="roledrop" runat="server" Width="100px" TabIndex="3">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right" Text="分类:"></asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="Dropdownlist1" runat="server" Width="100px" TabIndex="4">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right" Text="工段:"></asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="workshop" runat="server" Width="100px" Enabled="true" TabIndex="5">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" Text="工号:"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="textbox2" TabIndex="6" runat="server" Width="100px" AutoPostBack="True"
                        OnTextChanged="namevalue_change"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right" Text="姓名:"></asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="name" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right" Text="金额:"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="allowance" CssClass="SmallTextBox Numeric" TabIndex="7" runat="server" Width="100px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" RowSpan="2">
                    <asp:Button ID="save" TabIndex="8" OnClick="BtnSave_click" runat="server" 
                        Text="保存" CssClass="SmallButton3" Width ="60px"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" Text="备注"></asp:TableCell>
                <asp:TableCell ColumnSpan="5">
                    <asp:TextBox ID="txtComment" runat="server" Width="520px" Enabled="false"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="1070px" AllowPaging="True" AutoGenerateColumns="False"
            PageSize="16" CssClass="GridViewStyle AutoPageSize" >
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" SortExpression="gsort" HeaderText="序号" ReadOnly="True">
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ctype" SortExpression="ctype" HeaderText="计酬方式" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="department" SortExpression="department" HeaderText="部门"
                    ReadOnly="True">
                    <ItemStyle Width="150px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="rolename" SortExpression="rolename" HeaderText="职务" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userNo" SortExpression="userID" HeaderText="工号" ReadOnly="True">
                    <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userName" SortExpression="userName" HeaderText="姓名" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="kinds" SortExpression="kinds" HeaderText="金额">
                    <ItemStyle Width="80px" HorizontalAlign="right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="leavedate" SortExpression="leavedate" ReadOnly="True"
                    HeaderText="离职日期">
                    <ItemStyle Width="80px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workyear" SortExpression="workyear" HeaderText="工龄" ReadOnly="True">
                    <ItemStyle Width="40px" HorizontalAlign="right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="comment" SortExpression="comment" HeaderText="备注" ReadOnly="True">
                    <ItemStyle Width="260px" HorizontalAlign="right"></ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>更新</u>" HeaderText="<u>编辑</u>" CancelText="<u>取消</u>"
                    EditText="<u>编辑</u>" Visible="false">
                    <ItemStyle HorizontalAlign="center" Width="80px"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="Delete">
                    <ItemStyle HorizontalAlign="center" Width="40px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <asp:Label ID="Uid" runat="server" Visible="False"></asp:Label>
        </form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
