<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app_MattchAppList.aspx.cs" Inherits="HR_app_MattchAppList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin-top:20px;">
        <table>
            <tr>
                <td>应聘时间</td>
                <td>
                    <asp:TextBox ID="txtAppDate" runat="server"  CssClass="SmallTextBox Date" Width="100px"></asp:TextBox></td>
                <td>公司</td>
                <td>
                    <asp:DropDownList ID="ddlCompany" runat="server"  Width="80px" 
                    DataTextField="plantCode" DataValueField="plantID" 
                    onselectedindexchanged="ddlCompany_SelectedIndexChanged" 
                    AutoPostBack="True"></asp:DropDownList>
                </td>
                <td>部门</td>
                <td>
                    <asp:DropDownList ID="ddlDepartment" runat="server"  Width="80px" 
                    DataTextField="name" DataValueField="departmentID" 
                    AutoPostBack="True"></asp:DropDownList>
                </td>
                <td><asp:Button ID="btnReach" runat="server" Text="查询"  CssClass="SmallButton2" 
                        onclick="btnReach_Click"/></td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            onrowcancelingedit="gv_RowCancelingEdit" onrowediting="gv_RowEditing" 
            onrowupdating="gv_RowUpdating" DataKeyNames="id" >
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
                    <asp:TableCell HorizontalAlign="center" Text="应聘时间" Width="70px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="应聘者姓名" Width="70px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="出生年月" Width="70px"></asp:TableCell>
			        <asp:TableCell HorizontalAlign="center" Text="身份证号" Width="60px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="应聘岗位" Width="80px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="公司" Width="80px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="部门" Width="80px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="学历" Width="50px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="专业" Width="50px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="毕业院校" Width="50px"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="True">
                <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="70px"  Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="appDate" HeaderText="应聘时间" ReadOnly="True">
                <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="70px"  Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="name" HeaderText="应聘者姓名" ReadOnly="True">
                <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="70px"  Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="birthDay" HeaderText="出生年月" ReadOnly="True">
                <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="IC" HeaderText="身份证号" ReadOnly="True">
                <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="应聘岗位" >
                <EditItemTemplate>
                <asp:TextBox ID="txtBirthDay" runat="server" CssClass="SmallTextBox" Text='<%# Bind("appProc") %>'
                    Width="80px"></asp:TextBox>
                </EditItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                <ItemTemplate>
                    <%#Eval("appProc")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="company" HeaderText="公司" ReadOnly="True">
                <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="department" HeaderText="部门" ReadOnly="True">
                <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="education" HeaderText="学历" ReadOnly="True">
                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="50px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="professional" HeaderText="专业" ReadOnly="True">
                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="50px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="graduateSchool" HeaderText="毕业院校" ReadOnly="True">
                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="50px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="miaoshu" HeaderText="是否匹配" ReadOnly="True">
                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="50px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
            EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
            <HeaderStyle Width="70px" HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            <ControlStyle Font-Bold="False" Font-Size="12px" />
        </asp:CommandField>
        </Columns>
        </asp:GridView>
    </div>    
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
