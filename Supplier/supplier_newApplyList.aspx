<%@ Page Language="C#" AutoEventWireup="true" CodeFile="supplier_newApplyList.aspx.cs" Inherits="Supplier_supplier_newApplyList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table>
            <tr>
                <td>申请编号</td>
                <td>
                    <asp:TextBox ID="txtSupplierNo" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>供应商</td>
                <td>
                    <asp:TextBox ID="txtSupplier" runat="server" CssClass="SmallTextBox Supplier" Width="80px"></asp:TextBox>
                </td>
                <td>供应商名称</td>
                <td>
                    <asp:TextBox ID="txtSupplierName" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>申请部门</td>
                <td>
                    <asp:DropDownList ID="ddlApplyDept" Width="100px" runat="server" 
                        DataTextField="supplier_AppDeptName" DataValueField="supplier_AppDeptID" AutoPostBack="True" CssClass="Param" OnSelectedIndexChanged="ddlApplyDept_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>状态</td>
                <td>
                    <asp:DropDownList ID="ddlStatus" Width="80px" runat="server" AutoPostBack="True" CssClass="Param" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" >
                        <asp:ListItem Value="3">----</asp:ListItem>
                        <asp:ListItem Value="0">进行中</asp:ListItem>
                        <asp:ListItem Value="1">已同意</asp:ListItem>
                        <asp:ListItem Value="2">已拒绝</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>处理进程</td>
                <td>
                    <asp:DropDownList ID="ddlProceStatus" Width="180px" runat="server" CssClass="Param" AutoPostBack="True" OnSelectedIndexChanged="ddlProceStatus_SelectedIndexChanged" >
                        <asp:ListItem Value="0">----</asp:ListItem>
                        <asp:ListItem Value="1">等待主管签字</asp:ListItem>
                        <asp:ListItem Value="2">等待供应商资质文件评估</asp:ListItem>
                        <asp:ListItem Value="3">等待验厂决策</asp:ListItem>
                        <asp:ListItem Value="4">等待验厂意见签字</asp:ListItem>
                        <asp:ListItem Value="5">等待签署文件处理</asp:ListItem>
                        <asp:ListItem Value="6">等待总经理签字</asp:ListItem>
                        <asp:ListItem Value="7">等待生成供应商代码</asp:ListItem>
                        <asp:ListItem Value="8">已生成供应商代码但没有提交</asp:ListItem>
                        <asp:ListItem Value="9">已生成供应商代码已提交</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="查询" CssClass="SmallButton2" OnClick="Button1_Click" />
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="新增" CssClass="SmallButton2" OnClick="Button2_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="FQgvList" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize" 
         PageSize="30" OnPageIndexChanging="FQgvList_PageIndexChanging"
            DataKeyNames="supplier_No,supplier_LeaderIsAgree" OnRowCommand="FQgvList_RowCommand">
        <RowStyle CssClass="GridViewRowStyle" />
        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <FooterStyle CssClass="GridViewFooterStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <Columns>
            <asp:TemplateField HeaderText="新供应商申请编号" Visible="true">
                <ItemStyle HorizontalAlign="Center" Width="120" Font-Underline="True" />
                <ItemTemplate>
                    <asp:LinkButton ID="linkNo" CssClass="no" runat="server" Text='<%# Bind("supplier_No") %>'
                        CommandName="ViewEdit"></asp:LinkButton>
                </ItemTemplate>
                <ControlStyle Font-Underline="True" />
                <HeaderStyle HorizontalAlign="Center" Width="110"></HeaderStyle>
            </asp:TemplateField>
            <asp:BoundField HeaderText="申请公司" DataField="supplier_AppCompanyName">
                <HeaderStyle Width="240px" />
                <ItemStyle Width="240px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="申请日期" DataField="supplier_AppDate">
                <HeaderStyle Width="100px" />
                <ItemStyle Width="100px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="申请人" DataField="supplier_AppUserName">
                <HeaderStyle Width="100px" />
                <ItemStyle Width="100px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="申请部门" DataField="supplier_AppDeptName">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="供应商中文名" DataField="supplier_SuppChineseName">
                <HeaderStyle Width="300px" />
                <ItemStyle Width="300px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="供应商英文名" DataField="supplier_SuppEnglishName">
                <HeaderStyle Width="300px" />
                <ItemStyle Width="300px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="供应商中文地址" DataField="supplier_SuppChineseAddress">
                <HeaderStyle Width="300px" />
                <ItemStyle Width="300px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="供应商英文地址" DataField="supplier_SuppEnglishAddress">
                <HeaderStyle Width="300px" />
                <ItemStyle Width="300px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="联系人" DataField="supplier_SuppContactName">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="职务" DataField="supplier_SuppContactRoleName">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="供应商代码" DataField="supplier_SupplierNum">
                <HeaderStyle Width="120px" />
                <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:ButtonField Text="View" HeaderText="View" CommandName="View">
                <ControlStyle Font-Bold="False" Font-Underline="True" />
                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                <ItemStyle Width="80px" HorizontalAlign="Center" ForeColor="Black" />
            </asp:ButtonField>
        </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
