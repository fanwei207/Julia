<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.dept_partsCat" CodeFile="dept_partsCat.aspx.vb" %>

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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <asp:Table runat="server" Width="680px" ID="Table1" GridLines="None" BorderWidth="0"
            CellPadding="3" CellSpacing="0">
            <asp:TableRow Height="25px" BackColor="LightGrey">
                <asp:TableCell ColumnSpan="2">
                    请输入部门代码:
                    <asp:TextBox ID="deptTextBox" CssClass="TextLeft" runat="server" Width="100px" AutoPostBack="True"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 部门名称:
                    <asp:Label runat="server" ID="lbl_deptName"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Button ID="btn_back" runat="server" Text="返回" CssClass="SmallButton2" Width="80px">
                    </asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="20px">
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow BackColor="LightGrey" VerticalAlign="Bottom">
                <asp:TableCell HorizontalAlign="Left" Width="300px">所过滤的类别：</asp:TableCell>
                <asp:TableCell Width="80px"></asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" Width="300px">可添加的类别：</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow BackColor="LightGrey">
                <asp:TableCell HorizontalAlign="Center">
                    <asp:Panel runat="server" Style="overflow-y: scroll; overflow: auto" HorizontalAlign="Left"
                        Width="300px" Height="400px" BorderColor="Black" BackColor="White" BorderWidth="1px"
                        ID="Panel1">
                        <asp:DataGrid ID="dg_CategoryIn" runat="server" Width="280px" BorderWidth="1px" BorderStyle="None"
                            BorderColor="#999999" ShowHeader="true" GridLines="Vertical" CellPadding="0"
                            BackColor="White" AutoGenerateColumns="False" HeaderStyle-Font-Bold="false" PagerStyle-HorizontalAlign="Center"
                            AllowPaging="false" AllowSorting="True">
                            <ItemStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                            <Columns>
                                <asp:BoundColumn DataField="categoryID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="name" HeaderText="类别">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="type" HeaderText="类型">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="选项">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ckb_CategoryIn" runat="server" Width="30px"></asp:CheckBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center"></PagerStyle>
                        </asp:DataGrid>
                    </asp:Panel>
                    <br />
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle">
                    <asp:Button ID="addBtn" Text="<<增加" CssClass="SmallButton2" Width="100px" runat="server">
                    </asp:Button>
                    <br />
                    <br />
                    <asp:Button ID="deleteBtn" Text="删除>>" CssClass="SmallButton2" Width="100px" runat="server">
                    </asp:Button>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                <asp:Panel runat="server" Style="overflow-y: scroll; overflow: auto" HorizontalAlign="Left"
                        Width="300px" Height="400px" BorderColor="Black" BackColor="White" BorderWidth="1px"
                        ID="Panel2">
                    <asp:DataGrid ID="dg_CategoryOut" runat="server" Width="300px" BorderWidth="1px"
                        BorderStyle="None" BorderColor="#999999" ShowHeader="true" GridLines="Vertical"
                        CellPadding="0" BackColor="White" AutoGenerateColumns="False" HeaderStyle-Font-Bold="false"
                        PagerStyle-HorizontalAlign="Center" AllowPaging="true" AllowSorting="True">
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundColumn DataField="categoryID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="name" HeaderText="类别">
                                <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="type" HeaderText="类型">
                                <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="选项">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckb_CategoryOut" runat="server" Width="30px"></asp:CheckBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center"></PagerStyle>
                    </asp:DataGrid>
                    </asp:Panel>
                    <br />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </form>
    </div>
    <script language="vbscript" type="text/vbscript"> 
			<!-- 
			Sub document_onkeydown 
				if window.event.keyCode=13 then 
					window.event.keyCode=9
				end if 
			End Sub
			//--> 
    </script>
    <script language="javascript" type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
			if(document.getElementById("deptTextBox").value=="")
			  document.getElementById("deptTextBox").focus();   
    </script>
</body>
</html>
