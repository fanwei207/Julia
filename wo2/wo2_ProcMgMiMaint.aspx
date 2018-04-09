<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_ProcMgMiMaint.aspx.cs"
    Inherits="wo2_MgMiMaint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
        .style2
        {
            width: 72px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table cellpadding="0" cellspacing="0" style="width: 592px;">
            <tr>
                <td colspan="2" style="text-align: left;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    请先选择要导入的类别：<asp:DropDownList ID="dropType" runat="server">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>LED</asp:ListItem>
                        <asp:ListItem>组装</asp:ListItem>
                        <asp:ListItem>线路板</asp:ListItem>
                        <asp:ListItem>毛管</asp:ListItem>
                        <asp:ListItem>明管</asp:ListItem>
                    </asp:DropDownList>
                    *
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left;" colspan="2">
                    <input id="filename" runat="server" name="filename1" style="width: 500px" type="file" /><asp:Button
                        ID="btnExport2" runat="server" Text="导入" CssClass="SmallButton2" OnClick="btnExport1_Click" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left;" class="style1">
                    模板：<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Docs/wo2_proInOut.xls">下载</asp:HyperLink>
                </td>
                <td class="style1">
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    批量删除（<font style="color: Red;">请谨慎操作</font>）：
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td class="style2">
                                公司*:
                            </td>
                            <td>
                                <asp:DropDownList ID="dropDomain" runat="server">
                                    <asp:ListItem>--</asp:ListItem>
                                    <asp:ListItem>SZX</asp:ListItem>
                                    <asp:ListItem>ZQL</asp:ListItem>
                                    <asp:ListItem>YQL</asp:ListItem>
                                    <asp:ListItem>HQL</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                日期：</td>
                            <td>
                                <asp:TextBox ID="txtDate" runat="server" Width="100px" CssClass="Date" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                车间：
                            </td>
                            <td>
                                <asp:TextBox ID="txtWorkShop" runat="server" Width="100px" MaxLength="15"></asp:TextBox>
                            </td>
                            <td>
                                原工序：</td>
                            <td>
                                <asp:TextBox ID="txtOrigProc" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                工序：
                            </td>
                            <td>
                                <asp:TextBox ID="txtProc" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
                            </td>
                            <td>
                                生产线：</td>
                            <td>
                                <asp:TextBox ID="txtLine" runat="server" Width="100px" MaxLength="8"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                工单：
                            </td>
                            <td>
                                <asp:TextBox ID="txtNbr" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
                            </td>
                            <td>
                                ID：</td>
                            <td>
                                <asp:TextBox ID="txtLot" runat="server" Width="100px" MaxLength="8"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4" style=" text-align:center;">
                                <asp:Button ID="btnExport3" runat="server" Text="删除" CssClass="SmallButton2" OnClick="btnExport3_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
