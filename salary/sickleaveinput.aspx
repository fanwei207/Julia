<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.sickleaveinput" CodeFile="sickleaveinput.aspx.vb" %>

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
        <asp:Table ID="Table1" runat="server" CellSpacing="0" BorderColor="Black" Width="960px">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="left" Width="60px" Text="起始日期&nbsp;"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="110px">
                    <asp:TextBox ID="name2value" runat="server" CssClass="smalltextbox Date" Width="70px"
                        onkeydown="event.returnValue =false;" onpaste="return false;"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="left" Width="60px" Text="结束日期&nbsp;"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="110px">
                    <asp:TextBox ID="Textbox1" runat="server" CssClass="smalltextbox Date" Width="70px"
                        onkeydown="event.returnValue =false;" onpaste="return false;"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Right" Width="40px" Text="工号&nbsp;"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="100px">
                    <asp:TextBox runat="server" Width="100px" ID="workerNoSearch"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="Right" Width="40px" Text="姓名&nbsp;"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="100px">
                    <asp:TextBox runat="server" ID="workerNameSearch" Width="100"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="80px" HorizontalAlign="Right">
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" OnClick="searchRecord"
                        CausesValidation="False"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="input" runat="server" Width="960px" BorderColor="black" GridLines="Both">
            <asp:TableRow>
                <asp:TableCell Text="<b>输入栏</b>" Width="160px" HorizontalAlign="Center" VerticalAlign="Bottom"
                    ForeColor="red"></asp:TableCell>
                <asp:TableCell Text="工号" Width="60px" HorizontalAlign="right" VerticalAlign="Bottom"></asp:TableCell>
                <asp:TableCell Width="100px">
                    <asp:TextBox ID="number" Width="100px" runat="server" TabIndex="0" AutoPostBack="True"
                        OnTextChanged="namevalue_change"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Text="姓名" Width="60px" HorizontalAlign="right" VerticalAlign="Bottom"></asp:TableCell>
                <asp:TableCell Width="110px">
                    <asp:Label ID="name" runat="server" Width="100px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                    <asp:Button ID="save" runat="server" Width="100px" CssClass="SmallButton2" OnClick="BtnSave_click"
                        TabIndex="6" Text="保存"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="备注" HorizontalAlign="Right" VerticalAlign="Bottom" ColumnSpan="2"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:TextBox ID="comment" runat="server" Width="500px" TabIndex="3" MaxLength="34"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="960px" PageSize="15" AutoGenerateColumns="False"
            AllowPaging="True" CssClass="GridViewStyle">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" SortExpression="gsort" HeaderText="序号" ReadOnly="True"
                    ItemStyle-Width="40px">
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userID" SortExpression="userID" HeaderText="工号" ReadOnly="True"
                    ItemStyle-Width="80px">
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userName" SortExpression="userName" HeaderText="姓名" ReadOnly="True"
                    ItemStyle-Width="80px">
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="starttime" SortExpression="starttime" HeaderText="起始日期"
                    ItemStyle-Width="80px">
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="endtime" SortExpression="endtime" HeaderText="结束日期" ItemStyle-Width="80px">
                    <ItemStyle HorizontalAlign="center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="sickdays" SortExpression="sickdays" HeaderText="天数" ItemStyle-Width="60px">
                    <ItemStyle HorizontalAlign="right" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="remark" SortExpression="remark" HeaderText="备注" ItemStyle-Width="300px">
                    <ItemStyle HorizontalAlign="left" Width="300px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="inputer" SortExpression="inputer" HeaderText="录入员" ItemStyle-Width="60px"
                    ReadOnly="True">
                    <ItemStyle HorizontalAlign="center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="inputdate" SortExpression="inputdate" HeaderText="录入日期"
                    ItemStyle-Width="80px" ReadOnly="True">
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>更新</u>" CancelText="<u>取消</u>"
                    EditText="<u>编辑</u>" ItemStyle-Width="80px">
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="Delete" ItemStyle-Width="40px">
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="inputID" ReadOnly="True"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <asp:Label ID="Uid" Visible="False" runat="server"></asp:Label>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
