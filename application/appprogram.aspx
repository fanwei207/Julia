<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.appProgram" CodeFile="appProgram.aspx.vb" %>

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
        <asp:Table runat="server" Width="780px" ID="Table1">
            <asp:TableRow>
                <asp:TableCell>
                    请选择:
                    <asp:DropDownList ID="selectTypeDDList" runat="server" AutoPostBack="True">
                        <asp:ListItem Value="0">程序出错报警</asp:ListItem>
                        <asp:ListItem Value="1">更新电脑数据</asp:ListItem>
                        <asp:ListItem Value="2">程序修改</asp:ListItem>
                        <asp:ListItem Value="3">新增程序</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="applicationRB" Checked="True" runat="server" Text="申请" GroupName="app"
                        AutoPostBack="True" OnCheckedChanged="RadioButton_Click"></asp:RadioButton>
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="confirmRB" runat="server" Text="审核" GroupName="app" AutoPostBack="True"
                        OnCheckedChanged="RadioButton_Click"></asp:RadioButton>
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="approveRB" runat="server" Text="批准" GroupName="app" AutoPostBack="True"
                        OnCheckedChanged="RadioButton_Click"></asp:RadioButton>
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="doneRB" runat="server" Text="处理" GroupName="app" AutoPostBack="True"
                        OnCheckedChanged="RadioButton_Click"></asp:RadioButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="allRB" runat="server" Text="所有" GroupName="app" AutoPostBack="True"
                        OnCheckedChanged="RadioButton_Click"></asp:RadioButton>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Button ID="newApp" CssClass="smallbutton3" Width="50" Text="新申请" runat="server">
                    </asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="appList" runat="server" AllowPaging="True" Width="780px" AllowSorting="false"
            CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="appID"></asp:BoundColumn>
                <asp:BoundColumn DataField="appDate" HeaderText="申请日期"  DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="60px"></ItemStyle>
                    <HeaderStyle Width="60px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="appby" HeaderText="申请人"  >
                    <ItemStyle Width="60px"></ItemStyle>
                    <HeaderStyle Width="60px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="appDept" HeaderText="部门" >
                    <ItemStyle Width="134px"></ItemStyle>
                    <HeaderStyle Width="134px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="appReason" HeaderText="原因"  >
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    <HeaderStyle></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AppStatus" HeaderText="状态" >
                    <ItemStyle Width="60px"></ItemStyle>
                    <HeaderStyle Width="60px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn ButtonType="LinkButton" DataTextField="OPStatus" CommandName="Detail"
                    HeaderText="">
                    <ItemStyle Width="60px" Font-Underline="True"></ItemStyle>
                    <HeaderStyle Width="60px"></HeaderStyle>
                </asp:ButtonColumn>
            </Columns> 
        </asp:DataGrid> 
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
