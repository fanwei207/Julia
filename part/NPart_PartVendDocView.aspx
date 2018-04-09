<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NPart_PartVendDocView.aspx.cs" Inherits="part_NPart_PartVendDocView" %>

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
            <div>
                <asp:Label Font-Size="16px" runat="server">QAD:</asp:Label>&nbsp;&nbsp;
                <asp:Label Font-Size="16px" runat="server" ID="lbQAD"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label Font-Size="16px" runat="server">供应商:</asp:Label>&nbsp;&nbsp;
                <asp:Label Font-Size="16px" runat="server" ID="lbVendor"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
            <asp:GridView ID="gvDet" runat="server" 
                AutoGenerateColumns="False" AllowPaging="true" PageSize="100"
                CssClass="GridViewStyle GridViewRebuild" 
                DataKeyNames="virPath,Level,id,path,typeid,cateid,name,filepath,filename,hiscnt" 
                OnRowCommand="gvDet_RowCommand" OnRowDataBound="gvDet_RowDataBound">
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="1200px"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="Category" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="Type" Width="100px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="DocName" Width="150px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="FileName" Width="220px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="Ver" Width="20px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="Lv" Width="20px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="All" Width="20px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="preview" Width="30px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="P.var" Width="30px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="pictureNo" Width="30px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="description" Width="30px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="11"></asp:TableCell>
                                </asp:TableFooterRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="Category" DataField="typename">
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Type" DataField="catename">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DocName" DataField="name">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="FileName" DataField="filename">
                        <HeaderStyle HorizontalAlign="Center" Width="220px" />
                        <ItemStyle HorizontalAlign="Center" Width="220px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Ver" DataField="version">
                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Lv" DataField="level">
                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="All" DataField="isall">
                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <HeaderTemplate>preview</HeaderTemplate>
                        <HeaderStyle Width="30px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="30px" Font-Underline="true"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbview" runat="server" Text="view" CommandName="view"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>P.var</HeaderTemplate>
                        <HeaderStyle Width="30px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="30px" Font-Underline="true"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbPvar" runat="server" Text="List" CommandName="old"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="pictureNo" DataField="pictureNo">
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="description" DataField="description">
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                    </asp:BoundField>

                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
<script type="text/javascript">
    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
</script>
</html>
