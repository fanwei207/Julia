<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDIReports.aspx.cs" Inherits="EDI_EDIReports" %>

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
            height: 14px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table cellpadding="0" cellspacing="0" style="width: 500px;">
            <tr>
                <td colspan="3" style="text-align: left;">
                    <strong>EDI、QAD客户订单不匹配：</strong>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 判断依据是客户订单号，即EDI订单号不在销售单列表中。由于数据同步时间问题，故该报表每天只生成一份。
                </td>
            </tr>
            <tr>
                <td>
                    接收日期：
                </td>
                <td>
                    <asp:TextBox ID="txtPoRecDate1" runat="server" Width="100px" CssClass="Date"></asp:TextBox>
                    -<asp:TextBox ID="txtPoRecDate2" runat="server" Width="100px" CssClass="Date"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnExport1" runat="server" Text="导出" CssClass="SmallButton2" OnClick="btnExport1_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left;">
                    <strong>EDI、QAD销售订单不匹配：</strong>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left;" class="style1">
                    &nbsp;&nbsp;&nbsp;&nbsp; 判断依据是销售订单号。&nbsp;可能存在修改EDI订单情况，这会导致不匹配。可在&lt;EDI 850 
                    Update&gt;中更新。
                    如果是在EDI中，但没EDI销售单的，可能是数据还未同步过来。</td>
            </tr>
            <tr>
                <td>
                    订单日期：
                </td>
                <td>
                    <asp:TextBox ID="txtOrdDate1" runat="server" Width="100px" CssClass="Date"></asp:TextBox>
                    -<asp:TextBox ID="txtOrdDate2" runat="server" Width="100px" CssClass="Date"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="导出" CssClass="SmallButton2" OnClick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left;">
                    <strong>JDE、EDI销售订单不匹配：</strong>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left;" class="style1">
                    &nbsp;&nbsp;&nbsp;&nbsp; JDE订单是US的全部未结订单，并理应全部通过EDI到达CHN。这张报表反映的是JDE中存在，但却不在EDI中的订单列表
                </td>
            </tr>
            <tr>
                <td>
                    订单日期：
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width="100px" CssClass="Date"></asp:TextBox>
                    -<asp:TextBox ID="TextBox2" runat="server" Width="100px" CssClass="Date"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="导出" CssClass="SmallButton2" 
                        OnClick="Button2_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left;" >
                    E<strong>DI、Hist不匹配：</strong></td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left;" >
                    &nbsp;&nbsp; 每次EDI订单到达时，系统会自动备份。可能会因为服务器宕机导致JOB未及时执行，导致EDI订单到达时未及时解析&nbsp;</td>
            </tr>
            <tr>
                <td>
                    到达日期：</td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" Width="100px" CssClass="Date"></asp:TextBox>
                    -<asp:TextBox ID="TextBox4" runat="server" Width="100px" CssClass="Date"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button3" runat="server" Text="导出" CssClass="SmallButton2" 
                        OnClick="Button3_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
