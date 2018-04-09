<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_LoggingPre.aspx.cs"
    Inherits="TSK_LoggingPre" %>

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
        <table style="width: 770px">
            <tr>
                <td align="left">
                    LOG上传任务列表（测试通过的任务，必须一次性上传Log，允许多次上传，后上传的将覆盖之前上传的）：
                </td>
                <td align="right">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        PageSize="20" DataKeyNames="tskd_id"
                        Width="770px" OnRowDataBound="gv_RowDataBound">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="770px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="状态" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="序号" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="类型" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="任务内容" Width="600px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="tskd_statu" HeaderText="状态" HtmlEncode="False">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskd_index" HeaderText="序号" HtmlEncode="False">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskd_type" HeaderText="类型" HtmlEncode="False">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskd_desc" HeaderText="任务内容">
                                <HeaderStyle Width="600px" HorizontalAlign="Center" />
                                <ItemStyle Width="600px" HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="2">
                    &nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="BACK" CssClass="SmallButton3" OnClick="btnBack_Click"
                        Width="60px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDone" runat="server" Text="NEXT" CssClass="SmallButton3" OnClick="btnDone_Click"
                        Width="60px" />
                    <input id="hidNbr" type="hidden" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
