<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_TestingView.aspx.cs"
    Inherits="TSK_TestingView" %>

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
        <table style="width: 940px">
            <tr>
                <td align="left">
                    测试方案：（仅供查看）
                </td>
                <td align="right">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        PageSize="20" Width="940px" DataKeyNames="tskr_table" 
                        onrowdatabound="gv_RowDataBound">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="940px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="序号" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="测试方案" Width="500px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="创建时间" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="待决" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="结果" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="tskr_index" HeaderText="序号" HtmlEncode="False">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskr_type" HeaderText="类别" HtmlEncode="False">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskr_testSolution" HeaderText="测试方案">
                                <HeaderStyle Width="500px" HorizontalAlign="Center" />
                                <ItemStyle Width="500px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskr_createDate" HeaderText="创建时间" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                                HtmlEncode="False">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskr_resultText" HeaderText="测试结果">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskr_testDate" HeaderText="测试时间" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                                HtmlEncode="False">
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
