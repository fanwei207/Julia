<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductTrackingItem.aspx.cs" Inherits="producttrack_ProductTrackingItem" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .SmallTextBox
        {
        }
        .style1
        {
            width: 129px;
        }
    </style>
</head>
<body style="background-color: Window">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table style="width: 750px; height: 28px;">
            <tr>
                <td class="style1">Code：<asp:TextBox ID="txtCode" runat="server" 
                        CssClass="SmallTextBox" Width="80px"></asp:TextBox>
                </td>
                <td>
                    Qad：<asp:TextBox ID="txtQad" runat="server" CssClass="SmallTextBox" Width="150px"></asp:TextBox>
                </td>
                <td>
                    Type：<asp:DropDownList 
                        ID="dropType" runat="server" CssClass="SmallTextBox" 
                        Width="60px" ontextchanged="dropType_changed" AutoPostBack="True"></asp:DropDownList>
                </td>
                <td>
                    Detail：<asp:DropDownList ID="dropDetail" runat="server" CssClass="SmallTextBox" Width="80px"></asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="SmallButton3" Width="50px"
                        OnClick="btnSearch_Click" />
                </td>
                <td>
                    <asp:Button ID="btnAdd" Text="Add" runat="server" CssClass="SmallButton3" Width="50px"
                        OnClick="btnAdd_Click" />
                </td>
                <td>
                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="SmallButton3" Width="50px"
                        OnClick="btnCancel_Click" />
                </td>
                
            </tr>
        </table>
        <asp:GridView ID="gvlist" runat="server" AllowPaging="true" PageSize="20" DataKeyNames="itm_id" AutoGenerateColumns="False" Width="750px" CssClass="GridViewStyle AutoPageSize"
          OnRowDeleting="gvlist_RowDeleting" OnRowCommand="Edit_Command" OnPageIndexChanging="gvlist_PageIndexChanging">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                                <asp:Table ID="Table1" runat="server" GridLines="Vertical" BorderWidth="0">
                                    <asp:TableRow CssClass="GridViewHeaderStyle" HorizontalAlign="Center" >
                                        <asp:TableCell HorizontalAlign="center" Text="Code" Width="150px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="QAD" Width="200px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="Type" Width="150px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="Detail" Width="150px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="Edit" Width="50px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="Delete" Width="50px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="6" HorizontalAlign="Center">没有相关数据</asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="itm_code" HeaderText="Code">
                    <ItemStyle HorizontalAlign="left" Width="150px" />
                    <HeaderStyle HorizontalAlign="center" Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="itm_qad" HeaderText="QAD">
                    <ItemStyle HorizontalAlign="center" Width="200px" />
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="ptt_type" HeaderText="Type">
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="ptt_detail" HeaderText="Detail">
                    <ItemStyle HorizontalAlign="left" Width="150px" />
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEditType" runat="server"  CommandName="TrEdit" Font-Bold="False"
                            CausesValidation="false" Font-Size="10px" Font-Underline="True" Text="Edit" CommandArgument='<%# Bind("itm_id") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelType" Text="Delete" runat="server" CommandName="Delete" CausesValidation="false" Font-Size="10px" Font-Underline="true" />
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblmsg" runat="server" Visible="true" ForeColor="red"></asp:Label>
        </form>
        <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </div>
</body>
</html>
