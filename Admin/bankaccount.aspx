<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.BankAccount" CodeFile="BankAccount.aspx.vb" %>

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
        <asp:Table ID="TableQuery" runat="server" BorderColor="Black" Width="640px" CellSpacing="0">
            <asp:TableRow>
                <asp:TableCell Width="120px" HorizontalAlign="Right">
                    工号：&nbsp;
                    <asp:TextBox runat="server" Width="60px" ID="txtUserNo" TabIndex="0"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="120px" HorizontalAlign="Right">
                    姓名：&nbsp;
                    <asp:TextBox runat="server" Width="60px" ID="txtUserName" TabIndex="1"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell Width="200px" HorizontalAlign="Right">
                    银行：&nbsp;<asp:DropDownList runat="server" Width="120px" ID="BankType" TabIndex="2">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width="140px" HorizontalAlign="Right">
                    离职<asp:CheckBox ID="leave" runat="server"></asp:CheckBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnShowAll" CssClass="SmallButton3" runat="server" Text="查询" TabIndex="3">
                    </asp:Button>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExport" CssClass="SmallButton3" runat="server" Text="导出" TabIndex="3">
                    </asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="dgBankAccount" runat="server" CssClass="GridViewStyle"
            PageSize="16" AllowPaging="True" AutoGenerateColumns="False" 
            CellPadding="2">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="&lt;b&gt;序号&lt;/b&gt;"
                    Visible="False">
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="UserNo" ReadOnly="True" HeaderText="&lt;b&gt;工号&lt;/b&gt;">
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="UserName" ReadOnly="True" HeaderText="&lt;b&gt;姓名&lt;/b&gt;">
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="EnterDate" ReadOnly="True" HeaderText="&lt;b&gt;入职日期&lt;/b&gt;"
                    DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="LeaveDate" ReadOnly="True" HeaderText="&lt;b&gt;离职日期&lt;/b&gt;"
                    DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="&lt;b&gt;开户银行&lt;/b&gt;" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle Font-Size="8pt" Width="100px"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblBank" runat="server" Text='<%# Container.DataItem("Bank") %>' Width="100px">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" Width="100px" ID="BankDropdownlist" DataSource="<%# BankListSource() %>"
                            DataTextField="name" DataValueField="id" OnPreRender="BankSetDDI">
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="BankAccount" HeaderText="&lt;b&gt;银行账号&lt;/b&gt;">
                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="BankLoc" HeaderText="&lt;b&gt;归属地&lt;/b&gt;">
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>保存</u>" CancelText="<u>取消</u>"
                    EditText="<u>编辑</u>">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="80px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:BoundColumn DataField="userID" Visible="False" ReadOnly="True"></asp:BoundColumn>
            </Columns>
            <PagerStyle Font-Size="11pt" HorizontalAlign="Center" ForeColor="Black" BackColor="White"
                Mode="NumericPages"></PagerStyle>
        </asp:DataGrid>
        <br />
        <table cellspacing="2" cellpadding="2" width="640" bgcolor="#99ffff" border="1" bordercolor="#000099">
            <tr>
                <td colspan="5">
                    <font color="#ff0000">* 注:银行填写只能为中国银行,建设银行,农业银行,工商银行</font>
                </td>
            </tr>
            <tr>
                <td>
                    工号
                </td>
                <td>
                    姓名
                </td>
                <td>
                    银行
                </td>
                <td>
                    银行帐号
                </td>
                <td>
                    归属地
                </td>
            </tr>
            <tr>
                <td>
                    A001
                </td>
                <td>
                    XXXXX
                </td>
                <td>
                    建设银行
                </td>
                <td>
                    2039181723748
                </td>
                <td>
                    本市/异地
                </td>
            </tr>
        </table>
        <table cellspacing="2" cellpadding="2" width="640" bgcolor="#99ffff" border="0">
            <tr>
                <td align="center" width="80">
                    导入文件: &nbsp;
                </td>
                <td valign="top" width="300">
                    <input id="filename" style="width: 400px" type="file" size="45" name="filename" runat="server">
                </td>
                <td align="right">
                    <input class="SmallButton2" id="uploadBtn" style="width: 80px" type="button" value="导入"
                        name="uploadBtn" runat="server" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
