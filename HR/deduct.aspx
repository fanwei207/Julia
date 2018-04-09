<%@ Page Language="C#" AutoEventWireup="true" CodeFile="deduct.aspx.cs" Inherits="Wage.hr_deduct" %>

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
    <div align="Left">
        <form id="Form1" method="post" runat="server">
        <table id="Table4" runat="server" cellpadding="0" cellspacing="0" gridlines="None">
            <tr>
                <td width="26px">
                    日期
                </td>
                <td width="160px">
                    <asp:TextBox ID="txtStdDate" runat="server" Width="70px" CssClass="SmallTextBox Date"></asp:TextBox>
                    --
                    <asp:TextBox ID="txtEndDate" runat="server" Width="70px" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
                <td width="26px">
                    工号
                </td>
                <td width="40px">
                    <asp:TextBox runat="server" Width="40px" ID="txtUserNo"></asp:TextBox>
                </td>
                <td width="26px">
                    姓名
                </td>
                <td style="width: 41px">
                    <asp:TextBox runat="server" ID="txtUserName" Width="54px" ReadOnly="True"></asp:TextBox>
                </td>
                <td width="26px">
                    性质
                </td>
                <td width="60px">
                    <asp:DropDownList ID="dropType" runat="server" Width="60px" DataTextField="systemCodeName"
                        DataValueField="systemCodeID">
                    </asp:DropDownList>
                </td>
                <td width="26px">
                    类型
                </td>
                <td width="40px">
                    <asp:DropDownList ID="dropStatus" runat="server" Width="40px">
                        <asp:ListItem Value="2">无</asp:ListItem>
                        <asp:ListItem Value="0">内</asp:ListItem>
                        <asp:ListItem Value="1">外</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="26px">
                    部门
                </td>
                <td style="width: 80px">
                    <asp:DropDownList ID="dropDept" runat="server" Width="140px" DataTextField="name"
                        DataValueField="departmentID">
                    </asp:DropDownList>
                </td>
                <td style="width: 60px">
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton2" CausesValidation="False"
                        OnClick="BtnSearch_Click"></asp:Button>
                </td>
                <td width="65px">
                    &nbsp;<asp:Button ID="btnExport" runat="server" CssClass="SmallButton3" Text="导出"
                        OnClick="btnExport_Click" TabIndex="13" />
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="836px" style="border: 1px solid #000;">
            <tr>
                <td width="60">
                    日期
                </td>
                <td width="100">
                    <asp:TextBox ID="txtWorkDate" CssClass="SmallTextBox Date" runat="server" Width="80px"
                        TabIndex="5"></asp:TextBox>
                </td>
                <td width="60">
                    工号
                </td>
                <td width="80">
                    <asp:TextBox ID="txtUserNo2" runat="server" AutoPostBack="True" OnTextChanged="txtUserNo2_TextChanged"
                        TabIndex="6" MaxLength="5" Width="80px"></asp:TextBox>
                </td>
                <td width="60">
                    姓名
                </td>
                <td style="width: 90px">
                    <asp:TextBox ID="txtUserName2" runat="server" ReadOnly="True" Width="90px"></asp:TextBox>
                </td>
                <td width="40">
                    性质
                </td>
                <td width="60">
                    <asp:DropDownList ID="dropType2" runat="server" Width="60px" DataTextField="systemCodeName"
                        DataValueField="systemCodeID" AutoPostBack="True" OnSelectedIndexChanged="dropType2_SelectedIndexChanged"
                        TabIndex="7">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="dropMoneyType" runat="server" Width="86px" DataTextField="name"
                        DataValueField="id" TabIndex="6">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    金额
                </td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" Width="80px" TabIndex="8" CssClass="SmallTextBox Numeric"></asp:TextBox>
                </td>
                <td colspan="5">
                    备注<asp:TextBox ID="txtRmks" runat="server" Width="400px"></asp:TextBox>
                </td>
                <td colspan="2" align="right">
                    <asp:Button ID="btnSave" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        Text="录入" OnClick="btnSave_Click" TabIndex="9" />
                    <asp:Button ID="Search" CssClass="smallbutton2" runat="server" Text="员工查询" Visible="False">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgDeduct" runat="server" CssClass="GridViewStyle AutoPageSize"
            AllowPaging="True" PageSize="19" PagerStyle-Mode="NumericPages" OnDeleteCommand="dgDeduct_DeleteCommand"
            OnPageIndexChanged="dgDeduct_PageIndexChanged" OnItemDataBound="dgDeduct_ItemDataBound"
            AutoGenerateColumns="False" Width="1120px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle Width="40px" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workdate" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle Width="80px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userNo" HeaderText="工号">
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle Width="40px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userName" HeaderText=" 姓名">
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle Width="80px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DeductNum" HeaderText="扣款数量">
                    <ItemStyle Width="70px" HorizontalAlign="Right"></ItemStyle>
                    <HeaderStyle Width="70px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Amount" HeaderText="扣款金额">
                    <ItemStyle Width="60px" HorizontalAlign="Right"></ItemStyle>
                    <HeaderStyle Width="60px" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="扣款分类" DataField="deductname">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="扣款事项" DataField="deductType">
                    <HeaderStyle Width="120px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SystemCodeName" HeaderText="工作性质" Visible="False">
                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle Width="70px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="createName" HeaderText="输入员">
                    <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle Width="50px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CreatedDate" HeaderText="输入日期" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle Width="80px" />
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="Delete">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle Width="50px" />
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="ID" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="Comments" HeaderText="备注">
                    <ItemStyle Width="300px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
