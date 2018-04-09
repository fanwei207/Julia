<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_CHleave.aspx.cs" Inherits="HR_hr_CHleave" %>

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
        <table cellspacing="0" cellpadding="0" 
            style="border-style: none; border-color: inherit; border-width: 0px; width: 810px;">
            <tr>
                <td align="right">
                    <asp:Label ID="lblStartDate" runat="server" Width="60px" CssClass="LabelRight" Text="起始日期:"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtStartDate" runat="server" Width="80px" CssClass="SmallTextBox Date"
                        onkeydown="event.returnValue=false;" onpaste="return false;" ValidationGroup="chkAll"
                        TabIndex="1"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblEndDate" runat="server" Width="60px" CssClass="LabelRight" Text="结束日期:"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtEndDate" runat="server" Width="80px" CssClass="SmallTextBox Date"
                        onkeydown="event.returnValue=false;" onpaste="return false;" ValidationGroup="chkAll"
                        TabIndex="2"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblUserNo" runat="server" Width="60px" CssClass="LabelRight" Text="员工工号:"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtUserNo" runat="server" Width="80px" TabIndex="3"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblUserName" runat="server" Width="60px" CssClass="LabelRight" Text="员工姓名:"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtUserName" runat="server" Width="80px" TabIndex="4"></asp:TextBox>
                </td>
                <td align="center" style="width: 120px">
                    <asp:Label ID="lblType" runat="server" CssClass="LabelRight" Text="类型:" Width="40px"></asp:Label>&nbsp;
                    <asp:DropDownList ID="ddlType" runat="server" Width="60px" Font-Size="10pt" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                        <asp:ListItem Text="--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="事假" Value="1"></asp:ListItem>
                        <asp:ListItem Text="病假" Value="2"></asp:ListItem>
                        <asp:ListItem Text="婚假" Value="3"></asp:ListItem>
                        <asp:ListItem Text="丧假" Value="4"></asp:ListItem>
                        <asp:ListItem Text="年假" Value="5"></asp:ListItem>
                        <asp:ListItem Text="产假" Value="6"></asp:ListItem>
                        <asp:ListItem Text="工伤" Value="7"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton3" CausesValidation="false"
                        OnClick="btnQuery_Click" />
                &nbsp;<asp:Button ID="btnExport" runat="server" CssClass="SmallButton3" Text="导出" OnClick="btnExport_Click"
                        TabIndex="13" />
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="810px" style="border: 1px solid #000;">
            <tr>
                <td align="center" colspan="2" rowspan="2">
                    <asp:Label ID="lblTitle" runat="server" Width="60px" Text="<b>输入栏</b>" ForeColor="red"></asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="lblLaborNo" runat="server" Width="60px" Text="员工工号:" TabIndex="5"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtLaborNo" runat="server" Width="80px" ValidationGroup="chkAll"
                        OnTextChanged="txtLaborNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblLaborName" runat="server" Width="60px" Text="员工姓名:"></asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblLaborNameValue" runat="server" Width="80px" Text=""></asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="lblDays" runat="server" Width="60px" Text="请假天数:"></asp:Label>
                </td>
                <td align="Left">
                    <asp:TextBox ID="txtDays" runat="server" CssClass="SmallTextBox Numeric" Width="80px"
                        TabIndex="6"></asp:TextBox>
                </td>
                <td align="center" colspan="2" rowspan="2">
                    <asp:Button ID="btnAddNew" runat="Server" Width="80px" Text="增加" CssClass="SmallButton2"
                        TabIndex="8" ValidationGroup="chkAll" CausesValidation="true" OnClick="btnAddNew_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblMemo" runat="server" Width="60px" Text="备注信息:"></asp:Label>
                </td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txtMemo" runat="server" Width="500px" MaxLength="255" TabIndex="7"></asp:TextBox>&nbsp;
                    <asp:Label ID="lblUID" runat="server" Style="visibility: hidden"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvLeave" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="25" DataKeyNames="LeaveID"
            OnRowDeleting="gvLeave_RowDeleting" OnPreRender="gvLeave_PreRender" OnPageIndexChanging="gvLeave_PageIndexChanging"
            Width="810px" OnRowDataBound="gvLeave_RowDataBound">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="780px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="工号" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="开始日期" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="结束日期" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="天数" Width="50px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="创建人" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="备注" Width="300px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="删除" Width="40px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="UserCode" HeaderText="工号">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UserName" HeaderText="姓名">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="StartDate" HeaderText="开始日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="false">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="EndDate" HeaderText="结束日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="false">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Days" HeaderText="天数">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Creater" HeaderText="创建人">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
              
                <asp:BoundField DataField="Comment" HeaderText="备注">
                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField>
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" Text="<u>删除</u>" ForeColor="Black"
                            CommandName="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
