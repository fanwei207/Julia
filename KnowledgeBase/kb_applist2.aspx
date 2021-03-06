<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.KB_applist2" CodeFile="KB_applist2.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="1000">
            <tr>
                <td>
                    <asp:Label ID="countLabel" runat="server">&nbsp;类型</asp:Label><asp:DropDownList ID="SelectTypeDropDown"
                        runat="server" AutoPostBack="true" Width="160px">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="Label1" runat="server">状态</asp:Label><asp:DropDownList ID="SelectStatusDropDown"
                        runat="server" AutoPostBack="false" Width="70px">
                    </asp:DropDownList>
                    &nbsp; 关键字<asp:TextBox ID="txtCode" TabIndex="0" runat="server" CssClass="SmallTextBox"
                        Width="200px" MaxLength="50"></asp:TextBox>&nbsp;申请日期
                    <asp:TextBox ID="txtDate1" TabIndex="0" runat="server" CssClass="SmallTextBox Date" Width="65px"
                        MaxLength="12"></asp:TextBox>--
                    <asp:TextBox ID="txtDate2" TabIndex="0" runat="server" CssClass="SmallTextBox Date" Width="65px"
                        MaxLength="12"></asp:TextBox>&nbsp;
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="False" AutoPostBack="True">
                    </asp:CheckBox>
                </td>
                <td>
                    <asp:Button ID="btnQuery" TabIndex="0" runat="server" Text="查询" CssClass="SmallButton3">
                    </asp:Button>&nbsp;
                    <asp:Button ID="Button1" TabIndex="0" runat="server" Text="新建申请" CssClass="SmallButton3"
                        Width="50"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <table id="table2" cellspacing="0" cellpadding="0" width="1000">
            <tr>
                <td>
                    <asp:DataGrid ID="Datagrid1" runat="server" Width="980px" BorderWidth="1px" BorderColor="#999999"
                        GridLines="Vertical" BackColor="White" BorderStyle="None" AutoGenerateColumns="False"
                        HeaderStyle-Font-Bold="false" PagerStyle-HorizontalAlign="Center" AllowSorting="True" PageSize="15" AllowPaging="True"
                        CellPadding="0" CssClass="GridViewStyle">
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="typeid"></asp:BoundColumn>
                            <asp:BoundColumn DataField="docid" HeaderText="ID">
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn Visible="False" DataField="docuserid"></asp:BoundColumn>
                            <asp:BoundColumn DataField="docstatus" SortExpression="docstatus" HeaderText="状态">
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="docuser" SortExpression="docuser" HeaderText="申请人">
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="doccont" SortExpression="doccont" HeaderText="申请内容">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="docdate" SortExpression="docdate" HeaderText="申请日期">
                                <HeaderStyle Width="60px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="doctaken" SortExpression="doctaken" HeaderText="会签意见">
                                <HeaderStyle Width="350px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" Width="350px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:ButtonColumn Text="&lt;u&gt;详细&lt;/u&gt;" CommandName="docview">
                                <HeaderStyle Width="30px"></HeaderStyle>
                                <ItemStyle Width="30px"></ItemStyle>
                            </asp:ButtonColumn>
                            <asp:ButtonColumn Text="&lt;u&gt;处理&lt;/u&gt;" CommandName="doctake">
                                <HeaderStyle Width="30px"></HeaderStyle>
                                <ItemStyle Width="30px"></ItemStyle>
                            </asp:ButtonColumn>
                        </Columns>
                        <PagerStyle Font-Size="12pt" HorizontalAlign="Center" ForeColor="#330099" BackColor="Silver"
                            Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
