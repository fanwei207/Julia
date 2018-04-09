<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_ChDate.aspx.cs" Inherits="HR_hr_ChDate" %>

<!DOCTYPE html>

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
    <div align="center">
           <table  cellspacing="0" cellpadding="0" >
            <tr>
                <td>
                    年份&nbsp;<asp:TextBox ID="txtYear" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
                    月份&nbsp;
                    <asp:DropDownList ID="dropMonth" runat="server" CssClass="server" Width="40px">
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
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnSearch_Click" />
                    &nbsp
                    <asp:Button ID="btnAttendance" runat="server" Text="生成考勤" CssClass="SmallButton3" OnClick="btnAttendance_Click" />
                    &nbsp
                    <asp:Button ID="btnSalary" runat="server" Text="生成工资" CssClass="SmallButton3" OnClick="btnSalary_Click" />
                </td>
            </tr>
        </table>
            <asp:GridView ID="gvlist" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            runat="server" DataKeyNames="id" OnRowUpdating="gvlist_RowUpdating"
            OnRowCancelingEdit="gvlist_RowCancelingEdit" OnRowEditing="gvlist_RowEditing"
            >
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField DataField="workdate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="日期" ReadOnly="True">
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="工作时长（小时）">
                    <EditItemTemplate>
                        <asp:TextBox ID="txHours" runat="server" CssClass="smalltextbox" Text='<%# Bind("workhours", "{0:F0}") %>'
                            Width="100%"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("workhours", "{0:F0}") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True">
                    <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" ForeColor="Black" />
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:CommandField>
 
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="日期" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工作时长（小时）" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
    </form>
</body>
</html>
