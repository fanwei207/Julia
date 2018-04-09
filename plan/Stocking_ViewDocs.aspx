<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stocking_ViewDocs.aspx.cs" Inherits="plan_Stocking_ViewDocs" %>

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
                    <asp:GridView ID="gvUpload" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        Width="800px" CssClass="GridViewStyle" PageSize="20"  OnRowCommand="gvUpload_RowCommand"
                        DataKeyNames="filePath">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="false" />
                        <RowStyle CssClass="GridViewRowStyle" Wrap="false" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table2" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="Attach File Name" Width="540px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Upload User" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="View" Width="50px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="fileName" HeaderText="Attach File Name">
                                <HeaderStyle Width="540px" HorizontalAlign="Center" />
                                <ItemStyle Width="540px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CreateName" HeaderText="Upload User">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:ButtonField Text="View" HeaderText="View" CommandName="View">
                                <ControlStyle Font-Bold="False" Font-Underline="True" />
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                            </asp:ButtonField>
                        </Columns>
                    </asp:GridView>
    </div>
    </form>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
