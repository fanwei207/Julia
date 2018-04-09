<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuestComplaint_Approach.aspx.cs" Inherits="rmInspection_GuestComplaint_Approach" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>客诉-投诉解决方式维护</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .LabelRight
        {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table cellspacing="2" cellpadding="2" width="600px" bgcolor="white" border="0">
            <tr> 
                <td>
                    <asp:Label ID="lblApproach" runat="server" Width="55px" CssClass="LabelRight" Text="解决方式:"
                        Font-Bold="False"></asp:Label>
                    <asp:TextBox ID="txtApproach" runat="server" Width="257px" TabIndex="1" MaxLength="30"
                        CssClass="smallTextBox"></asp:TextBox>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="5" Text="查询"
                        Width="40px" OnClick="btnQuery_Click" />&nbsp;
                    <asp:Button ID="btnNew" runat="server" CssClass="SmallButton2" TabIndex="5" Text="新增"
                        Width="40px" OnClick="btnNew_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle" PageSize="20" OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" OnRowCancelingEdit="gv_RowCancelingEdit"
             OnRowEditing = "gv_RowEditing" 
            Width="620px" DataKeyNames="ApproachID,CreatedBy" OnRowUpdating="gv_RowUpdating">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="620px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="解决方式" Width="30px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="创建人" Width="300px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="创建时间" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell> 
                        <asp:TableCell Text="Del" Width="40px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns> 
                <asp:BoundField DataField="ApproachName" HeaderText="解决方式">
                    <HeaderStyle Width="340px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="340px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="CreatedName" HeaderText="创建者" ReadOnly="true">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>  
                <asp:BoundField DataField="Createdtime" HeaderText="创建时间" ReadOnly="true">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                    <%--<asp:TemplateField HeaderText="截止时间">
                        <ItemStyle HorizontalAlign="Center" Width="70"/>
                        <ItemTemplate>
                            <asp:Textbox ID="txtEndtime" runat="server" CssClass="Date Param"></asp:Textbox>
                        </ItemTemplate>
                        <ControlStyle Font-Underline="false" />
                        <HeaderStyle HorizontalAlign="Center" Width="70"></HeaderStyle>
                    </asp:TemplateField>--%>
                <asp:CommandField ShowEditButton="True">
                    <ControlStyle Font-Bold="False" Font-Underline="True" ForeColor="Black" />
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:CommandField> 
                <asp:CommandField ShowDeleteButton="True" DeleteText="Del">
                    <ControlStyle Font-Bold="False" Font-Underline="True" ForeColor="Black" />
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
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
