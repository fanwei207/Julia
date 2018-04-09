<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_BCoefficient.aspx.cs"
    Inherits="Wage.HR_hr_BCoefficient" %>

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
            runat="server" style="width: 710px">
            <tr>
                <td>
                    年月
                </td>
                <td>
                    <asp:TextBox ID="txtYear" runat="server" Width="50px"></asp:TextBox>&nbsp;
                    <asp:DropDownList ID="dropMonth" runat="server" Width="40px" Font-Size="10pt" CssClass="smallbutton2">
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
                <td style="width: 34px">
                    工号
                </td>
                <td>
                    <asp:TextBox ID="txtSUserNo" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    姓名
                </td>
                <td>
                    <asp:TextBox ID="txtSUserName" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    部门
                    <asp:DropDownList ID="dropDept" runat="server" TabIndex="2" Width="120px" DataTextField="Name"
                        DataValueField="departmentID">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button runat="server" ID="BtnSearch" Text="查询" CssClass="SmallButton3" Width="60px"
                        CausesValidation="False" OnClick="BtnSearch_Click"></asp:Button>
                    <asp:Button runat="server" ID="Button1" Text="导出" CssClass="SmallButton3" Width="60px"
                        CausesValidation="False" OnClick="Button1_Click"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    工号
                </td>
                <td>
                    <asp:TextBox ID="txtUserNo" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="txtUserNo_TextChanged"></asp:TextBox>
                </td>
                <td style="width: 34px">
                    姓名
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtUserName" Width="80px" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    系数
                </td>
                <td>
                    <asp:TextBox ID="txtCoef" runat="server" Width="100px" CssClass="SmallTextBox Numeric"></asp:TextBox>
                </td>
                <td colspan="2" align="center">
                    平均（部门）<asp:CheckBox ID="chkDepartment" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Width="52px" CssClass="SmallButton3" Text="保存"
                        OnClick="btnSave_Click"></asp:Button>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgBCoef" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CellPadding="1" OnDeleteCommand="dgBCoef_DeleteCommand" OnPageIndexChanged="dgBCoef_PageIndexChanged"
            PageSize="23" CssClass="GridViewStyle AutoPageSize" 
            onitemcommand="dgBCoef_ItemCommand">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="bc_userno" HeaderText="工号" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="60px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <HeaderStyle Width="60px" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="姓名">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkMore" runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.bc_username", "<u>{0}</u>") %>' 
                            CommandName="MoreLine"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="部门" ReadOnly="True" DataField="name">
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                    <HeaderStyle Width="120px" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="工段" ReadOnly="True" DataField="workshop">
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                    <HeaderStyle Width="120px" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="班组" ReadOnly="True" DataField="workgroup">
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                    <HeaderStyle Width="120px" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="工种" ReadOnly="True" DataField="kindswork">
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                    <HeaderStyle Width="120px" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="类型" ReadOnly="True" DataField="systemcodename">
                    <ItemStyle HorizontalAlign="center" Width="50px" />
                    <HeaderStyle Width="50px" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="职务" ReadOnly="True" DataField="rolename">
                    <ItemStyle HorizontalAlign="center" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_coef" HeaderText="系数" SortExpression="kinds">
                    <ItemStyle HorizontalAlign="Right" Width="60px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <HeaderStyle Width="60px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_startdate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="生效日期"
                    SortExpression="enter">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                    <HeaderStyle Width="70px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_depart" HeaderText="平均（部门）">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle Width="80px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_createby" HeaderText="创建人" SortExpression="cleave"
                    Visible="False">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bc_createdate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="创建日期"
                    Visible="False"></asp:BoundColumn>
                <asp:ButtonColumn CommandName="Delete" Text="<u>删除</u>">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Width="50px" />
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="bc_id" ReadOnly="True" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
