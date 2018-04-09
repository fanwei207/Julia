<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_SystemLogs.aspx.cs" Inherits="TSK_SystemLogs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        $(function () {

            //显示日志明细
            $(".GridViewRowStyle, .GridViewAlternatingRowStyle").dblclick(function (e) {

                var text = $(".Message", $(this)).val();

                $.window("事务日志", 800, 450, "", text);

            })
            //end dblclick


        })

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="width: 900px;">
            <tr>
                <td align="left">
                    日期:<asp:TextBox ID="txtDate" runat="server" CssClass="Date" Width="100px"></asp:TextBox>
                    &nbsp;
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" OnClick="btnQuery_Click"
                        Text="Query" />
                </td>
                <td align="right">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
                        PageSize="20" Width="900px" OnRowDataBound="gv_RowDataBound" AllowPaging="True"
                        OnPageIndexChanging="gv_PageIndexChanging">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="900px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="Type" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                                    <asp:TableCell Text="Date" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                                    <asp:TableCell Text="Time" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                                    <asp:TableCell Text="Source" Width="200px" HorizontalAlign="Center"></asp:TableCell>
                                    <asp:TableCell Text="Category" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                                    <asp:TableCell Text="Event" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                                    <asp:TableCell Text="User" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                                    <asp:TableCell Text="Computer" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="Type" HeaderText="Type" HtmlEncode="False">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Date" HeaderText="Date" HtmlEncode="False">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Time" HeaderText="Time" HtmlEncode="False">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Source" HeaderText="Source">
                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Category" HeaderText="Category">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Event" HeaderText="Event">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="User" HeaderText="User">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Computer">
                                <ItemTemplate>
                                    <asp:Label ID="lblComputer" runat="server" Text='<%# Bind("Computer") %>'></asp:Label>
                                    <input id="hidMessage" type="hidden" class="Message" runat="server" value='<%# Bind("Message") %>' disabled="disabled" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
