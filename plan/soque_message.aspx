<%@ Page Language="C#" AutoEventWireup="true" CodeFile="soque_message.aspx.cs" Inherits="plan_soque_message" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    </br>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                Width="740px" PageSize="30" AllowPaging="true" 
                onpageindexchanging="gv_PageIndexChanging" >
                <RowStyle CssClass="GridViewRowStyle" Wrap="false" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="540px" CellPadding="0" CellSpacing="0" runat="server" Font-Size="8"
                        CssClass="GridViewHeaderStyle" HorizontalAlign="Center" VerticalAlign="Middle" GridLines="Vertical">
                        <asp:TableRow Height="27px">
                            <asp:TableCell Text="No." Width="40px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Messages" Width="500px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label ID="lblViewNo" runat="server" Text='<%# (Container.DataItemIndex + 1) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                    <asp:BoundField HeaderText="Messages" DataField="soques_msg">
                        <HeaderStyle Width="700px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    
                </Columns>
            </asp:GridView>
    </div>
    </form>
</body>
</html>
