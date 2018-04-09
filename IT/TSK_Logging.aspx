<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_Logging.aspx.cs" Inherits="TSK_Logging" %>

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
        <table style="width: 800px">
            <tr>
                <td align="left">
                    更新日志（*5M以内。提供既定<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Docs/updatting.xls"
                        Target="_blank">模板</asp:HyperLink>
                    ，直接导入。每次重新导入。斜体、删除线时间为新上传Log）：
                </td>
                <td align="center">
                    &nbsp;
                    <asp:LinkButton ID="linkDownload" runat="server" Font-Bold="False" Font-Size="11px"
                        Font-Strikeout="False" Font-Underline="True" ForeColor="#000000" OnClick="linkDownload_Click">下载</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <input id="filename" runat="server" name="filename1" style="width: 500px" type="file" />
                </td>
                <td align="right">
                    <asp:Button ID="btnUpload" runat="server" Text="上传" CssClass="SmallButton3" OnClick="btnUpload_Click"
                        Width="60px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        PageSize="20" Width="800px" 
                        DataKeyNames="tskc_id,tskc_chgSysTime,tskc_isTemp" 
                        onrowdatabound="gv_RowDataBound">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="序号" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="类别" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="操作" Width="40px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="文件夹|数据库" Width="90px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="名称" Width="300px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="开始时间" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="截止时间" Width="100px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="tskc_index" HeaderText="序号" HtmlEncode="False">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskc_chgType" HeaderText="类别" HtmlEncode="False">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskc_operateType" HeaderText="操作" HtmlEncode="False">
                                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskc_chgInPath" HeaderText="文件夹|数据库" HtmlEncode="False">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskc_chgProgName" HeaderText="名称">
                                <HeaderStyle Width="360px" HorizontalAlign="Left" />
                                <ItemStyle Width="360px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskc_uploadTime" HeaderText="上传时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HtmlEncode="False">
                                <HeaderStyle Width="110px" HorizontalAlign="Center" />
                                <ItemStyle Width="110px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskc_last_saveDate" HeaderText="上次更新" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HtmlEncode="False">
                                <HeaderStyle Width="110px" HorizontalAlign="Center" />
                                <ItemStyle Width="110px" HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align: center; height: 15px;">
                </td>
                <td style="text-align: center; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="2">
                    <asp:Button ID="txtBack" runat="server" Text="BACK" CssClass="SmallButton3" OnClick="txtBack_Click"
                        Width="70px" />
                    &nbsp;
                    <asp:Label ID="lbTskNbr" runat="server" Visible="False">0</asp:Label>
                    <asp:Button ID="btnDone" runat="server" Text="TO UPDATE" CssClass="SmallButton3"
                        OnClick="btnDone_Click" Width="70px" />
                    <input id="hidTUpdateEmail" type="hidden" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
