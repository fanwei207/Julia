<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userApproveList.aspx.cs"
    Inherits="PM_HeaderList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body runat="server">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="870px" border="0">
            <tr>
                <td>
                    公司：
                </td>
                <td>
                    <asp:DropDownList ID="dropCompany" runat="server" Width="60px">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">SZX</asp:ListItem>
                        <asp:ListItem Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Value="5">YQL</asp:ListItem>
                        <asp:ListItem Value="8">HQL</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right">
                    工号：
                </td>
                <td>
                    <asp:TextBox ID="txtUserNo" runat="server" Width="80px" TabIndex="3" 
                        CssClass="SmallTextBox" MaxLength="6"></asp:TextBox>
                </td>
                <td  align="right">
                    姓名：</td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" Width="80px" TabIndex="3" 
                        CssClass="SmallTextBox" MaxLength="6"></asp:TextBox>
                </td>
                <td  align="right">
                    <asp:Label ID="lblReqDate" runat="server" Width="60px" CssClass="LabelRight" Text="申请日期:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtApprDate" runat="server" Width="80px" TabIndex="3" 
                        CssClass="SmallTextBox Date" MaxLength="10"></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox ID="isHandle" runat="server" Checked="True" Text="只显示未处理的记录" 
                        AutoPostBack="True" oncheckedchanged="isHandle_CheckedChanged">
                    </asp:CheckBox>
                </td>
                <td  align="right">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" TabIndex="7" Text="查询"
                        Width="50px" OnClick="btnQuery_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle" PageSize="23" OnPageIndexChanging="gv_PageIndexChanging"
            OnRowCommand="gv_RowCommand" Width="870px" DataKeyNames="ResumePath,isCreator" 
            onrowdatabound="gv_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="870px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="公司" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工号" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="部门" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工段" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="简历" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="申请人" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="下一处理人" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="申请日期" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="明细" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="处理" Width="40px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="plantCode" HeaderText="公司">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="userNo" HeaderText="工号">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="userName" HeaderText="姓名">
                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="departmentName" HeaderText="部门">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="workshopName" HeaderText="工段">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                 <asp:TemplateField HeaderText="简历">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkresume" Text="<u>下载</u>" Font-Underline="true" Font-Size="12px"
                            runat="server" CommandArgument='<%# Eval("ResumePath") %>' CommandName="myResume" />
                    </ItemTemplate>
                    <HeaderStyle Width="40px" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="createdName" HeaderText="申请人">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="nextApprove" HeaderText="下一处理人">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="createdDate" HeaderText="申请日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkdetail" Text="<u>明细</u>" Font-Underline="true" Font-Size="12px"
                            runat="server" CommandArgument='<%# Eval("id") %>' CommandName="myDetail" />
                    </ItemTemplate>
                    <HeaderStyle Width="40px" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnHandle" Text="<u>处理</u>" Font-Underline="true" Font-Size="12px"
                            runat="server" CommandArgument='<%# Eval("id") %>' CommandName="myHandle" />
                    </ItemTemplate>
                    <HeaderStyle Width="40px" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
