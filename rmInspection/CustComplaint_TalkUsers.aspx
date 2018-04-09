<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustComplaint_TalkUsers.aspx.cs" Inherits="rmInspection_CustComplaint_TalkUsers" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#btnSave").click(function () {
                var userno = $("#txtUserNo").val();
                if (userno == '')
                {
                    alert('请输入工号');
                    return false;
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-top:10px;" align="center">
        <table>
            <tr>
                <td>客诉单号</td>
                <td>
                    <asp:Label ID="labNo" runat="server" Text="Label"></asp:Label>
                    <asp:HiddenField ID="hidNo" runat="server" />
                </td>
            </tr>
            <tr>
                <td>公司</td>
                <td>
                    <asp:DropDownList ID="ddlPlant" Width="70" runat="server">
                        <asp:ListItem Text="SZX" Value="1"></asp:ListItem>
                        <asp:ListItem Text="ZQL" Value="2"></asp:ListItem>
                        <asp:ListItem Text="YQL" Value="5"></asp:ListItem>
                        <asp:ListItem Text="HQL" Value="8"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>工号</td>
                <td>
                    <asp:TextBox ID="txtUserNo" CssClass="SmallTextBox5" Width="70" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" Text="保存" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
        
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            DataKeyNames="ID,CustComp_No,CustComp_UserID,CustComp_UserNo,CustComp_UserName,createBy,createName,createDate"
            OnRowDeleting="gv_RowDeleting"
            AllowPaging="False" PageSize="20">
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
                        <asp:TableCell HorizontalAlign="center" Text="工号" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="姓名" Width="150px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="CustComp_UserNo" HeaderText="工号">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="CustComp_UserName" HeaderText="姓名">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="删除">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" Text="<u>Delete</u>" ForeColor="Black"
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
