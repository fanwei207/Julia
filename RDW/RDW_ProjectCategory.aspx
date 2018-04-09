<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_ProjectCategory.aspx.cs"
    Inherits="RDW_ProjectCategory" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="2" cellpadding="2" width="600px" bgcolor="white" border="0">
            <tr>
                <td>
                    <asp:Label ID="LblCode" runat="server" Width="31px" CssClass="LabelRight" Text="Code:"
                        Font-Bold="False"></asp:Label>
                    <asp:TextBox ID="txtCode" runat="server" Width="53px" TabIndex="1" MaxLength="3"
                        CssClass="smallTextBox"></asp:TextBox>
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="lblCate" runat="server" Width="49px" CssClass="LabelRight" Text="Category:"
                        Font-Bold="False"></asp:Label>
                    <asp:TextBox ID="txtCategory" runat="server" Width="257px" TabIndex="1" MaxLength="30"
                        CssClass="smallTextBox"></asp:TextBox>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="5" Text="Query"
                        Width="40px" OnClick="btnQuery_Click" />&nbsp;
                    <asp:Button ID="btnNew" runat="server" CssClass="SmallButton2" TabIndex="5" Text="New"
                        Width="40px" OnClick="btnNew_Click" />&nbsp;
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton2" TabIndex="5" Text="Export"
                        Width="40px" OnClick="btnExport_Click"/>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle" PageSize="20" OnPageIndexChanging="gv_PageIndexChanging"
            OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" OnRowCancelingEdit="gv_RowCancelingEdit"
             OnRowEditing = "gv_RowEditing" OnRowUpdating ="gv_RowUpdating"
            Width="620px" DataKeyNames="cate_id,cate_linksample,DocPath" 
            onrowcommand="gv_RowCommand">
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
                        <asp:TableCell Text="Code" Width="30px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="Category" Width="300px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="Creator" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="Project Code Length" Width="100px" HorizontalAlign="center"
                            Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="Date" Width="120px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="Del" Width="40px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="cate_code" HeaderText="Code" ReadOnly="true">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="40px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="cate_name" HeaderText="Category">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="cate_ProjCode" HeaderText="ProjCode Serial Number ">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:CommandField ShowEditButton="True">
                    <ControlStyle Font-Bold="False" Font-Underline="True" ForeColor="Black" />
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="Link Sample">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkLinkSample" runat="server" AutoPostBack="True" OnCheckedChanged="chkLinkSample_CheckedChanged" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:BoundField DataField="cate_createName" HeaderText="Creator" ReadOnly="true">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="cate_createDate" HeaderText="Date" ReadOnly="true">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField ReadOnly="true" HeaderText="Template" DataField="TempName" >
                    <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
<%--                <asp:HyperLinkField HeaderText="Template Doc" Text="View" DataNavigateUrlFields="DocPath" ControlStyle-Font-Underline="true"
                 Target="_blank" DataNavigateUrlFormatString="{0}">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:HyperLinkField>--%>
                <asp:ButtonField Text='View' HeaderText='Project Doc' CommandName='Doc' ControlStyle-Font-Underline="true">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:ButtonField>
                <asp:ButtonField Text='DO' HeaderText='Maintenance' CommandName='DO' ControlStyle-Font-Underline="true">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:ButtonField>
                <asp:CommandField ShowDeleteButton="True" DeleteText="Del">
                    <ControlStyle Font-Bold="False" Font-Underline="True" ForeColor="Black" />
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:CommandField>
                
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
