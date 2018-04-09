<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_ViewLog.aspx.cs" Inherits="TSK_ViewLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>任务日志</title>
    <base target="_self">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(function () {


            //$("#gvMessagereply").find(".GridViewHeaderStyle TH:eq(1)").css({ 'text-align': 'left', 'word-break': 'break-all', 'word-wrap': 'break-word' });
            $("#gvMessagereply").find(".GridViewHeaderStyle").hide();

        })
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="width: 600px">
            <tr>
                <td style="text-align: left; height: 17px;">
                </td>
                <td style="text-align: center; height: 17px;">
                </td>
            </tr>
            <tr>
                <td style="text-align: left; height: 15px;">
                    任务日志：<asp:DropDownList ID="dropType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropType_SelectedIndexChanged">
                        <asp:ListItem>全部</asp:ListItem>
                        <asp:ListItem>分配</asp:ListItem>
                        <asp:ListItem>开发</asp:ListItem>
                        <asp:ListItem>维护</asp:ListItem>
                        <asp:ListItem>分析</asp:ListItem>
                        <asp:ListItem>测试</asp:ListItem>
                        <asp:ListItem>LOG</asp:ListItem>
                        <asp:ListItem>更新</asp:ListItem>
                        <asp:ListItem>跟踪</asp:ListItem>
                    </asp:DropDownList>
                    （不用填写。查看任务的整个开发、维护、分析全过程）
                </td>
                <td style="text-align: center; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left; height: 15px;" colspan="2">
                    <asp:GridView ID="gvMessagereply" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        Width="600px">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table3" Width="100%" CellPadding="-1" CellSpacing="0" runat="server"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell Text="Owner" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Date" Width="60px" HorizontalAlign="center"></asp:TableCell>
                                    <asp:TableCell Text="Message" Width="610px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="tskl_createName" HeaderText="Author">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    Post At: &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("tskl_createDate") %>'></asp:Label>&nbsp;&nbsp;
                                    <hr align="left" style="width: 100%; border-top: 1px dashed #000; border-bottom: 0px dashed #000;
                                        height: 0px">
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("tskl_type") %>' 
                                        Font-Bold="True" Font-Size="13px"></asp:Label><br /><br />
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("tskl_desc") %>' Style="word-break: break-all"
                                        Width="500px" Font-Size="12px"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Font-Bold="False" Font-Size="12px" />
                                <HeaderStyle HorizontalAlign="Left" Width="500px" />
                                <ItemStyle HorizontalAlign="Left" Width="500px" Height="100px" VerticalAlign="Top"
                                    Font-Bold="False" />
                            </asp:TemplateField>
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
                    <input id="btnClose" class="SmallButton3" type="button" value="CLOSE" onclick="window.close();" />
                    <asp:Label ID="lbtskNbr" runat="server" Visible="False">0</asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
