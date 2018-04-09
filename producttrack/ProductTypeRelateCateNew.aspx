<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductTypeRelateCateNew.aspx.cs"
    Inherits="ProductTypeRelateCateNew" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table style="width: 580px" cellpadding="0" cellspacing="0">
            <tr valign="middle">
                <td align="left">
                    条目类型:<asp:Label 
                        ID="lblProductType" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                    &nbsp;&nbsp; 产品分类:<asp:Label ID="lblPackagingType" runat="server" Text="Label" 
                        Font-Bold="True"></asp:Label>
                    &nbsp;&nbsp; 备注:<asp:Label ID="lblRemark" runat="server" Text="Label" 
                        Font-Bold="True"></asp:Label>
                    &nbsp;&nbsp; 有关联:<asp:Label ID="lblRelated" runat="server" Text="Label" 
                        Font-Bold="True"></asp:Label>
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr valign="middle">
                <td align="left" class="style1">
                    <br />
                    添加对应文档类型:<br />
                    Category<asp:DropDownList 
                        ID="ddlCategory" runat="server" Height="17px" Width="180px" AutoPostBack="True" 
                        onselectedindexchanged="ddlCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp; Type<asp:DropDownList ID="ddlName" runat="server" Height="17px" 
                        Width="180px">
                    </asp:DropDownList>
                </td>
                <td align="right" valign="bottom">
                    <asp:Button ID="btnAdd" runat="server" Text="增加" CssClass="SmallButton3" 
                        Width="50px" onclick="btnAdd_Click" />
                    <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton3" 
                        Width="50px" onclick="btnBack_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False"
            PageSize="25" CssClass="GridViewStyle" runat="server" Width="580px" 
            DataKeyNames="ptt_id" onrowcommand="gv_RowCommand">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="580px"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="NO." Width="30px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Category" Width="120px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="名称" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="创建者" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="创建日期" Width="120px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="" Width="100px"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableFooterRow BackColor="white" ForeColor="Black">
                        <asp:TableCell HorizontalAlign="Center" Text="无符合条件的信息" ColumnSpan="6"></asp:TableCell>
                    </asp:TableFooterRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="NO.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex +1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="Category" DataField="typename" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="名称" DataField="catename" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="创建者" DataField="createName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="创建日期" DataField="createDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" Text="Delete" ForeColor="black" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("ptt_id") + "," + Eval("cate_id") %>' CommandName="MyDelete" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
        </form>
    </div>
</body>
</html>
