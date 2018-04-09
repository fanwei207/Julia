<%@ Page Language="C#" AutoEventWireup="true" CodeFile="restleave.aspx.cs" Inherits="Wage.hr_restleave" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
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
        <table id="Table1" runat="server" cellspacing="0" cellpadding="0" width="960px">
            <tr>
                <td width="120px">
                    工号:<asp:TextBox ID="txtUserNo1" runat="server" Width="80px" OnTextChanged="txtUserNo1_TextChanged"></asp:TextBox>
                </td>
                <td width="120px">
                    姓名:<asp:TextBox ID="txtUserName1" runat="server" Width="90"></asp:TextBox>
                </td>
                <td>
                    部门:<asp:DropDownList ID="dropDept" runat="server" Width="90px" DataTextField="Name"
                        DataValueField="departmentID">
                    </asp:DropDownList>
                </td>
                <td>
                    年 :<asp:TextBox ID="txtYear" runat="server" MaxLength="4" Width="40px"></asp:TextBox>
                </td>
                <td width="60px">
                    月<asp:DropDownList ID="dropMonth" runat="server" CssClass="smallbutton2" Font-Size="10pt"
                        Width="40px">
                        <asp:ListItem Value="0">--</asp:ListItem>
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
                <td colspan="2" width="80px">
                    <asp:Button ID="BtnSearch" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="BtnSearch_Click" Text="查询" Width="50px" />
                    <asp:CheckBox ID="chkDel" runat="server" Visible="False" />
                </td>
            </tr>
        </table>
        <table id="Table2" runat="server" width="960px" bordercolor="Black" cellspacing="0"
            cellpadding="0" style="border: 1px solid #000;">
            <tr>
                <td>
                    日期:<asp:TextBox ID="txtWorkDate" runat="server" Width="80px" TabIndex="1" CssClass="smalltextbox Date"></asp:TextBox>
                </td>
                <td>
                    工号:<asp:TextBox TabIndex="2" runat="server" AutoPostBack="True" ID="txtUserNo" Width="90px"
                        OnTextChanged="txtUserNo_TextChanged"></asp:TextBox>
                </td>
                <td>
                    姓名:<asp:Label runat="server" ID="lblUserName" Width="60px"></asp:Label>
                    <asp:Label ID="lblUserID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblEnterDate" runat="server" Visible="False"></asp:Label>
                </td>
                <td>
                    天数:<asp:TextBox ID="txtNumber" runat="server" Width="33px" TabIndex="3"></asp:TextBox>
                </td>
                <td>
                    <asp:Button TabIndex="4" runat="server" Width="40px" ID="BtnSave" Text="保存" CssClass="SmallButton3"
                        OnClick="BtnSave_Click"></asp:Button>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="export" runat="server" Width="81px" CausesValidation="False" Text="年休假导出"
                        CssClass="SmallButton2" OnClick="export_Click"></asp:Button>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="exportByMonth" runat="server" Width="100px" CausesValidation="False" Text="年休假导出（按月）"
                        CssClass="SmallButton2" OnClick="exportByMonth_Click" ></asp:Button>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <font size="3">下载：</font>
                </td>
                <td align="left" style="width: 135; height: 21px;">
                    <label id="here" onclick="submit();">
                        <a href="/docs/Hr_RestLeave.xls" target="blank"><font color="blue">导入模版</font></a></label>
                </td>
                 <td colspan ="3">
                     导入：
                     <input id="filename1" style="width: 363px; height: 22px" type="file" name="filename1"
                                runat="server" />
                       &nbsp;<asp:Button ID="btn_import" runat="server" 
                                onclick="btn_import_Click" Text="Import" CssClass="SmallButton2" />
                 </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgRest" runat="server" Width="960px" PageSize="26" AutoGenerateColumns="False"
            AllowPaging="True" DataKeyField="ID" CssClass="GridViewStyle AutoPageSize" OnDeleteCommand="dgRest_DeleteCommand"
            OnItemDataBound="dgRest_ItemDataBound" OnPageIndexChanged="dgRest_PageIndexChanged">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn SortExpression="gsort" HeaderText="序号" ReadOnly="True">
                    <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="department" SortExpression="department" HeaderText="部门"
                    ReadOnly="True">
                    <ItemStyle Width="100px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userNo" SortExpression="userID" HeaderText="工号" ReadOnly="True">
                    <ItemStyle Width="70px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="userName" SortExpression="userName" HeaderText="姓名" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="workdate" SortExpression="workdate" HeaderText="日期" ReadOnly="True"
                    DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="number" SortExpression="number" HeaderText="天数" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="enterdate" SortExpression="enterdate1" HeaderText="入公司日期"
                    ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="leavedate" SortExpression="leavedate1" HeaderText="离职日期"
                    ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Cname" SortExpression="Cname" HeaderText="录入人" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="createddate" SortExpression="Cname" HeaderText="录入日期" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="Delete">
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="createdby" HeaderText="创建人" ReadOnly="True" Visible="False">
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
