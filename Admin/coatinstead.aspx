<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.CoatInstead" CodeFile="CoatInstead.aspx.vb" %>

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
        <asp:ValidationSummary ID="Validationsummary1" runat="server" ShowSummary="false"
            ShowMessageBox="true" HeaderText="请检查以下字段:"></asp:ValidationSummary>
        <asp:Table ID="Table1" runat="server" Width="750px" BorderColor="Black" CellSpacing="0">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="left" Width="140px">
                    工号:
                    <asp:TextBox runat="server" Width="80px" ID="workerNoSearch"></asp:TextBox></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="180px">
                    姓名：<asp:TextBox runat="server" ID="workerNameSearch" Width="80px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="180px">
                    部门:<asp:DropDownList ID="department" runat="server" Width="80px">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="180px">
                    <asp:CheckBox runat="server" ID="chk_LeaveStaff" Text="包括离职员工"></asp:CheckBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="70px" HorizontalAlign="right">
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" OnClick="searchRecord"
                        CausesValidation="False"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="table2" runat="server" Width="750px" BorderColor="Black" CellSpacing="0"
            GridLines="Both">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Middle" HorizontalAlign="right" Width="40px" Text="工号:"
                    RowSpan="2"></asp:TableCell>
                <asp:TableCell VerticalAlign="Middle" Width="60px" RowSpan="2">
                    <asp:TextBox ID="wcode" runat="server" Width="60px" AutoPostBack="True" OnTextChanged="namevalue_change"
                        TabIndex="1"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Middle" HorizontalAlign="right" Text="姓名:" Width="40px"
                    RowSpan="2"></asp:TableCell>
                <asp:TableCell VerticalAlign="Middle" Width="70px" RowSpan="2">
                    <asp:Label ID="wname" runat="server" Width="70px"></asp:Label>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="right" Width="50px" Text="日期:"
                    CssClass="smalltextbox Date"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="130px">
                    <asp:TextBox ID="name2value" runat="server" Width="70px" ReadOnly="false" TabIndex="2"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="right" Width="50px" Text="日期:"
                    CssClass="smalltextbox Date"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="110px">
                    <asp:TextBox ID="Textbox1" runat="server" Width="70px" ReadOnly="false" TabIndex="4"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="right" Width="50px" Text="日期:"
                    CssClass="smalltextbox Date"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="120px">
                    <asp:TextBox ID="Textbox2" runat="server" Width="70px" ReadOnly="false" TabIndex="6"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:Button ID="Btn_Cancel" runat="server" CssClass="SmallButton3" Text="取消" Width="60px"
                        OnClick="Btn_CancelOnClick" TabIndex="8"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="right" Width="50px" Text="长夹克:"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="120px">
                    <asp:TextBox ID="wear" runat="server" Width="40px" TabIndex="3"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="right" Width="50px" Text="短夹克:"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="120px">
                    <asp:TextBox ID="shorte" runat="server" Width="40px" TabIndex="5"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" HorizontalAlign="right" Width="50px" Text="白大褂:"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" Width="120px">
                    <asp:TextBox ID="white" runat="server" Width="40px" TabIndex="7"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center">
                    <asp:Button ID="Btsave" runat="server" CssClass="SmallButton3" Text="新增" Width="60px"
                        OnClick="Save_uniform" TabIndex="8"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br>
        <table id="table3" bordercolor="appworkspace" cellspacing="0" cellpadding="0" width="750px"
            border="1" runat="server">
            <tr bgcolor="#006699" height="5" bordercolor="appworkspace">
                <td bordercolor="appworkspace" background="#006699" style="width: 238px">
                    &nbsp;
                </td>
                <td valign="bottom" bordercolor="appworkspace" align="center" style="width: 141px">
                    <b><font color="white">长夹克</font></b>
                </td>
                <td valign="bottom" align="center" style="width: 144px">
                    <b><font color="white">短夹克</font></b>
                </td>
                <td valign="bottom" align="center" style="width: 144px">
                    <b><font color="white">白大褂</font></b>
                </td>
                <td bordercolor="appworkspace" align="center" background="#006699">
                    <b><font color="white">操作</font></b>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="750px" AllowPaging="True" AutoGenerateColumns="False"
            PageSize="16" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="userNo" ReadOnly="True" HeaderText="&lt;b&gt;工号&lt;/b&gt;">
                    <HeaderStyle Width="60px" HorizontalAlign="center"></HeaderStyle>
                    <ItemStyle Width="60px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn DataTextField="name" HeaderText="&lt;b&gt;姓名&lt;/b&gt;" ItemStyle-Font-Underline="true"
                    CommandName="Detail">
                    <HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="departmentName" HeaderText="&lt;b&gt;部门&lt;/b&gt;">
                    <HeaderStyle Width="110px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="110px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ljackdate" HeaderText="&lt;b&gt;申领日期&lt;/b&gt;">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="lnum" HeaderText="&lt;b&gt;件数&lt;/b&gt;">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="sjackdate" HeaderText="&lt;b&gt;申领日期&lt;/b&gt;">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="snum" HeaderText="&lt;b&gt;件数&lt;/b&gt;">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wcoatdate" HeaderText="&lt;b&gt;申领日期&lt;/b&gt;">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wnum" HeaderText="&lt;b&gt;件数&lt;/b&gt;">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" EditText="<u>编辑</u>" ItemStyle-Width="40px">
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </asp:EditCommandColumn>
                <asp:BoundColumn DataField="hname" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="userID" Visible="false"></asp:BoundColumn>
                <asp:ButtonColumn ButtonType="LinkButton" ItemStyle-Font-Underline="true" CommandName="Detail"
                    Text="<u>详细</u>">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        <asp:Label ID="reger" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="uid" runat="server" Visible="false"></asp:Label>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
