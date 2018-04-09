<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_Updating.aspx.cs" Inherits="TSK_Updating" %>

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

            $(".GridViewRowStyle, .GridViewAlternatingRowStyle").dblclick(function () {

                if ($(this).find("td:eq(1)").html() == "过程") {
                    var _path = $(this).find("td:eq(3)").html();
                    var _progName = $(this).find("td:eq(4)").html();
                    var _src = "../IT/TSK_ViewStoredProcedure.aspx?path=" + _path + "&prog=" + _progName;

                    $.window("查看存储过程", 800, 450, _src);
                }
                else if ($(this).find("td:eq(1)").html() == "表" && $(this).find("td:eq(2)").html() == "新增") {
                    var _path = $(this).find("td:eq(3)").html();
                    var _progName = $(this).find("td:eq(4)").html();
                    var _src = "../IT/TSK_ViewTableDefinition.aspx?path=" + _path + "&table=" + _progName;

                    $.window("查看表定义", 800, 450, _src);
                }
                else {
                    alert('目前只支持查看存储过程！');
                }
                //end if
            })
            //end dblclick

        })
    
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
                    <input id="hidUpdateBy" type="hidden" runat="server" />
                    <input id="hidDesc" type="hidden" runat="server" />
                    <input id="hidTrackEmail" type="hidden" runat="server" />
    <div align="center">
        <table style="width: 910px">
            <tr>
                <td align="left">
                    更新日志：
                    <asp:CheckBox ID="chkNotUpdate" runat="server" Checked="True" Text="仅未更新" AutoPostBack="True"
                        OnCheckedChanged="chkNotUpdate_CheckedChanged" />
                    （双击“过程”，可查看测试库过程明细）&nbsp;&nbsp;&nbsp; <asp:Button ID="txtBack" runat="server" Text="BACK" CssClass="SmallButton3"
                        OnClick="txtBack_Click" Width="70px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDone" runat="server" Text="TO TRACK" CssClass="SmallButton3" OnClick="btnDone_Click"
                        Width="70px" />
                    <asp:CheckBox ID="chkUpdating" runat="server" Visible="False" />
                    <asp:Label ID="lbtskNbr" runat="server" Visible="False">0</asp:Label>
                    </td>
                <td align="right">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
                        PageSize="20" Width="910px" DataKeyNames="tskc_id,tskc_chgSysTime,tskc_isUpdated"
                        OnRowDataBound="gv_RowDataBound" onrowcommand="gv_RowCommand">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" Width="910px" CellPadding="-1" CellSpacing="0" runat="server"
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
                                <ItemStyle Width="40px" HorizontalAlign="Center" Font-Overline="False" Font-Strikeout="False" />
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
                                <HeaderStyle Width="300px" HorizontalAlign="Center" />
                                <ItemStyle Width="300px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkDelete" runat="server" CommandName="myDelete"><u>删除</u></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="tskc_uploadTime" HeaderText="上传时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HtmlEncode="False">
                                <HeaderStyle Width="110px" HorizontalAlign="Center" />
                                <ItemStyle Width="110px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskc_chgSysTime" HeaderText="系统时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HtmlEncode="False">
                                <HeaderStyle Width="110px" HorizontalAlign="Center" />
                                <ItemStyle Width="110px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tskc_last_saveDate" HeaderText="上次时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                                HtmlEncode="False">
                                <HeaderStyle Width="110px" HorizontalAlign="Center" />
                                <ItemStyle Width="110px" HorizontalAlign="Center" />
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
