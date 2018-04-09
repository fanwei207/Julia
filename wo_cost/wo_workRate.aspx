<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo_workRate.aspx.cs" Inherits="wo_cost_wo_workRate" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table id="table3" cellpadding="0" cellspacing="0" bordercolor="Black" gridlines="Both"
            runat="server" style="width: 780px">
            <tr>
                <td>
                    公司
                </td>
                <td>
                    <asp:DropDownList ID="dropCompany" runat="server" TabIndex="1" Width="160px" OnTextChanged="dropCompany_TextChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                    部门
                </td>
                <td>
                    &nbsp;
                    <asp:DropDownList ID="dropDept" runat="server" TabIndex="2" Width="120px">
                    </asp:DropDownList>
                </td>
                <td>
                    预估工效
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtRate" Width="80px"></asp:TextBox>
                </td>
                <td>
                    年月
                </td>
                <td>
                    <asp:TextBox ID="txtYear" runat="server" Width="50px"></asp:TextBox><asp:DropDownList
                        ID="dropMonth" runat="server" CssClass="smallbutton2" Font-Size="10pt" Width="40px">
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
                </td>
                <td colspan="2" align="center">
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" Width="60px"
                        CausesValidation="False" OnClick="BtnSearch_Click"></asp:Button>
                    &nbsp;
                    <asp:Button ID="btnSave" runat="server" Width="52px" CssClass="SmallButton3" Text="保存"
                        OnClick="btnSave_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgWorkRate" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            OnDeleteCommand="dgCenter_DeleteCommand" OnPageIndexChanged="dgCenter_PageIndexChanged"
            PageSize="22" Width="770px" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="id" ReadOnly="True" Visible="False">
                    <ItemStyle CssClass="hidden" />
                    <HeaderStyle CssClass="hidden" />
                    <FooterStyle CssClass="hidden" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="department" HeaderText="部门" ReadOnly="True">
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workdate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="生效日期">
                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="预估工效" ReadOnly="True" DataField="workRate" DataFormatString="{0:P}">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" />
                </asp:BoundColumn>
                <asp:ButtonColumn CommandName="Delete" Text="<u>删除</u>">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
