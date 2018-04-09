<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CarInformation.aspx.cs" Inherits="HR_CarInformation" %>

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
        <table style="margin-top:10px;">
            <tr>
                <td>公司</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlPlant" AutoPostBack="True" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                        <asp:ListItem Text="全部" Value="0">全部</asp:ListItem>
                        <asp:ListItem Text="上海强凌" Value="1">上海强凌</asp:ListItem>
                        <asp:ListItem Text="镇江强凌" Value="2">镇江强凌</asp:ListItem>
                        <asp:ListItem Text="扬州强凌" Value="5">扬州强凌</asp:ListItem>
                        <asp:ListItem Text="淮安强凌" Value="8">淮安强凌</asp:ListItem>
                        <asp:ListItem Text="上海天灿宝" Value="11">上海天灿宝</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>车号</td>
                <td>
                    <asp:TextBox ID="txtCarNumber" runat="server" Width="100px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>车辆类型</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlCarType">
                        <asp:ListItem Text="全部" Value="0">全部</asp:ListItem>
                        <asp:ListItem Text="货车" Value="1">货车</asp:ListItem>
                        <asp:ListItem Text="客车" Value="2">客车</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>运行状态</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlCarStartType" AutoPostBack="True" OnSelectedIndexChanged="ddlCarStartType_SelectedIndexChanged">
                        <asp:ListItem Text="全部" Value="0">全部</asp:ListItem>
                        <asp:ListItem Text="待发车" Value="1">待发车</asp:ListItem>
                        <asp:ListItem Text="使用中" Value="2">使用中</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton2" Text="查询" OnClick="btnSearch_Click" />
                </td>
                <td>
                    <asp:Button ID="btnNew" runat="server" CssClass="SmallButton2" Text="新增" OnClick="btnNew_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvCarInformation" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" 
        DataKeyNames="id,Plant,CarNumber,CarCurrentGround,CarStartStatus" OnRowDeleting="gvCarInformation_RowDeleting" OnRowUpdating="gvCarInformation_RowUpdating"
             OnRowEditing="gvCarInformation_RowEditing" OnRowCancelingEdit="gvCarInformation_RowCancelingEdit" 
              OnRowDataBound="gvCarInformation_RowDataBound">
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
                    <asp:TableCell HorizontalAlign="center" Text="公司" Width="50px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="车号" Width="70px"></asp:TableCell>
			        <asp:TableCell HorizontalAlign="center" Text="车辆类型" Width="150px"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="Plant" HeaderText="公司" ReadOnly="True">
                <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="70px"  Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="CarNumber" HeaderText="车号" ReadOnly="True">
                <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="CarType" HeaderText="车辆类型" ReadOnly="True">
                <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="车辆当前里程数" >
                <EditItemTemplate>
                <asp:TextBox ID="txtKilometers" runat="server" CssClass="SmallTextBox" Text='<%# Bind("Kilometers") %>'
                    Width="100px"></asp:TextBox>
                </EditItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemTemplate>
                    <%#Eval("Kilometers")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="车辆当前所在地" >
                <EditItemTemplate>
                    <asp:DropDownList runat="server" ID="ddlCarCurrentGround">
                        <asp:ListItem Value="1" Text="上海强凌"></asp:ListItem>
                        <asp:ListItem Value="2" Text="镇江强凌"></asp:ListItem>
                        <asp:ListItem Value="5" Text="扬州强凌"></asp:ListItem>
                        <asp:ListItem Value="8" Text="淮安强凌"></asp:ListItem>
                        <asp:ListItem Value="11" Text="上海天灿宝"></asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemTemplate>
                    <%#Eval("CarCurrentGround")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CarStartStatus" HeaderText="车辆当前状态" ReadOnly="True">
                <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ControlStyle Font-Bold="False" Font-Size="12px" />
            </asp:CommandField>
            <asp:TemplateField HeaderText="Delete">
                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" Text="<u>删除</u>" ForeColor="Black"
                        CommandName="Delete"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
