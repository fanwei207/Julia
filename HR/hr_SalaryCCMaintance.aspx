<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_SalaryCCMaintance.aspx.cs"
    Inherits="Wage.HR_hr_SalaryCCMaintance" %>

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
        <table id="table3" cellpadding="0" cellspacing="0" bordercolor="Black" gridlines="Both"
            runat="server" style="width: 630px">
            <tr>
                <td>
                    部门
                </td>
                <td>
                    <asp:DropDownList ID="dropCostCenter" runat="server" TabIndex="2" Width="120px">
                    </asp:DropDownList>
                </td>
                <td>
                    应出勤
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtAttend" Width="80px"></asp:TextBox>
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
                    <asp:Button ID="btnSave" runat="server" Width="52px" CssClass="SmallButton3" Text="保存"
                        OnClick="btnSave_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgCenter" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CellPadding="1" OnDeleteCommand="dgCenter_DeleteCommand" OnPageIndexChanged="dgCenter_PageIndexChanged"
            PageSize="16" Width="630px" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="cc_cc" HeaderText="成本中心" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle Width="100px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cc_name" HeaderText="名称" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                    <HeaderStyle Width="300px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="cc_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="生效日期">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle Width="100px" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="应出勤" ReadOnly="True" DataField="cc_attendencedays">
                    <ItemStyle HorizontalAlign="Right" Width="120px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <HeaderStyle Width="80px" />
                </asp:BoundColumn>
                <asp:ButtonColumn CommandName="Delete" Text="<u>删除</u>">
                    <ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" />
                    <HeaderStyle Width="50px" />
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="id" ReadOnly="True" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <br />
        <asp:Button ID="btnExport" runat="server" Visible="false" Text="B类人员平均工资导出" Width="180px"
            CssClass="SmallButton2" OnClick="btnExport_Click" />
        </form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
