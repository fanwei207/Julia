<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo_actualReleaseZqDet.aspx.cs" Inherits="wo_actualReleaseZqDet" %>

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
                <td>
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
                        PageSize="20" Width="1000px" OnRowDataBound="gv_RowDataBound" AllowPaging="True"
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
                                    <asp:TableCell Text="域" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                                    <asp:TableCell Text="工单" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                                    <asp:TableCell Text="ID号" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                                    <asp:TableCell Text="零件" Width="200px" HorizontalAlign="Center"></asp:TableCell>
                                    <asp:TableCell Text="需求量" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                                    <asp:TableCell Text="发料量" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                                    <asp:TableCell Text="库位" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="wod_domain" HeaderText="域" HtmlEncode="False">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wod_nbr" HeaderText="工单" HtmlEncode="False">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wod_lot" HeaderText="ID号" HtmlEncode="False">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wod_part" HeaderText="零件">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pt_desc" HeaderText="描述">
                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wod_qty_req" HeaderText="需求量">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wod_qty_iss" HeaderText="发料量">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wod_loc" HeaderText="库位">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
