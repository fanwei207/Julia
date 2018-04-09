<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test_Report.aspx.cs" Inherits="RDW_Test_Report" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>可行性测试项目-列表</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin-top:10px;">
        <table>
            <tr>
                <td>跟踪号</td>
                <td>
                    <asp:TextBox ID="txtProdNo" runat="server" CssClass="SmallTextBox Param"></asp:TextBox>
                </td>
                <td>项目号</td>
                <td>
                    <asp:TextBox ID="txtProjectCode" runat="server" CssClass="SmallTextBox Param"></asp:TextBox>
                </td>
                <td>分类</td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server" Width="60px" CssClass="SmallTextBox Param" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Text="--全部--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="电子" Value="1"></asp:ListItem>
                        <asp:ListItem Text="结构" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>严重程度</td>
                <td>                    
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="60px" CssClass="SmallTextBox Param" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Text="--全部--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>进度</td>
                <td>                    
                    <asp:DropDownList ID="ddlStep" runat="server" Width="80px" CssClass="SmallTextBox Param" AutoPostBack="True" OnSelectedIndexChanged="ddlStep_SelectedIndexChanged">
                        <asp:ListItem Text="--全部--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="跟踪计划" Value="1"></asp:ListItem>
                        <asp:ListItem Text="计划方案" Value="2"></asp:ListItem>
                        <asp:ListItem Text="效果确认" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnSearch_Click" />
                </td>
                <td>
                    <asp:Button ID="btnNew" runat="server" Text="新增" CssClass="SmallButton2" OnClick="btnNew_Click" />
                </td>
                <td>
                    <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton2" OnClick="btnBack_Click" />
                </td>
                <td>
                    <asp:Button ID="btnEXCEL" runat="server" Text="EXCEL" CssClass="SmallButton2" OnClick="btnEXCEL_Click"  />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            DataKeyNames="test_ID,prod_No,prod_ProjectName,prod_Code,CreateBy,CreateName,prod_mid,prod_did
            ,test_Type, test_FailureTime, test_ProblemContent"
             OnRowCommand="gv_RowCommand"
            AllowPaging="True" PageSize="10" OnPageIndexChanging="gv_PageIndexChanging">
            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                    GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="跟踪号" Width="50px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="项目名称" Width="70px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="项目代码" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="分类" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="失效时间" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="严重程度" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="申请人" Width="50px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="申请日期" Width="50px"></asp:TableCell>
                        </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="prod_No" HeaderText="跟踪号">
                    <HeaderStyle Width="100px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="prod_ProjectName" HeaderText="项目名称/ECN编号">
                    <HeaderStyle Width="200px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="prod_Code" HeaderText="项目代码">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="test_Type" HeaderText="分类">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="test_FailureTime" HeaderText="失效时间">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="test_FailureTimeLeave" HeaderText="严重程度">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CreateName" HeaderText="申请人">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CreateDate" HeaderText="申请日期">
                    <HeaderStyle Width="90px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="测试">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDet" runat="server" CommandName="det" Font-Bold="False"
                            Font-Size="12px" Font-Underline="True" ForeColor="Black"><u>详细</u></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </div>
        <asp:HiddenField ID="hidDID" runat="server" />
        <asp:HiddenField ID="hidMID" runat="server" />
        <asp:HiddenField ID="hidProdNo" runat="server" />
        <asp:HiddenField ID="hidProjectName" runat="server" />
        <asp:HiddenField ID="hidProjectCode" runat="server" />
        <asp:HiddenField ID="hidProdID" runat="server" />
    </form>
</body>
</html>
