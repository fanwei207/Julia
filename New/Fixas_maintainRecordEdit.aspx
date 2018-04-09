<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fixas_maintainRecordEdit.aspx.cs"
    Inherits="new_Fixas_maintainRecordEdit" %>

<%@ Import Namespace="Portal.Fixas" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <asp:Panel ID="panelMaintainOrder" runat="server" Width="600px">
            <asp:Label ID="Label3" runat="server" Text="保养单" Font-Bold="True" Font-Size="Large"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblMaintainOrder" runat="server" Text="" Font-Bold="True" Font-Size="Large"></asp:Label>
            <br />
        </asp:Panel>
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 600px">
            <tr>
                <td style="width: 120px; text-align: right;">
                    资产编号：
                </td>
                <td style="width: 150px;">
                    <asp:Label ID="lblFixasNo" runat="server" CssClass="SmallTextBox5" Width="150px"></asp:Label>
                </td>
                <td style="width: 120px; text-align: right">
                    资产名称：
                </td>
                <td style="width: 210px;" colspan="2">
                    <asp:Label ID="lblFixasName" runat="server" CssClass="SmallTextBox5" Width="210px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px; text-align: right">
                    所在公司：
                </td>
                <td style="width: 150px;">
                    <asp:Label ID="lblDomain" runat="server" CssClass="SmallTextBox5" Width="150px"></asp:Label>
                </td>
                <td style="width: 120px; text-align: right">
                    成本中心：
                </td>
                <td style="width: 210px;" colspan="2">
                    <asp:Label ID="lblCC" runat="server" CssClass="SmallTextBox5" Width="210px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px; text-align: right">
                    规格：
                </td>
                <td style="width: 150px;">
                    <asp:Label ID="lblFixasDesc" runat="server" CssClass="SmallTextBox5" Width="150px"></asp:Label>
                </td>
                <td style="width: 120px; text-align: right">
                    类型：
                </td>
                <td style="width: 100px;">
                    <asp:Label ID="lblFixasType" runat="server" CssClass="SmallTextBox5" Width="100px"></asp:Label>
                </td>
                <td style="width: 105px;">
                    <asp:Label ID="lblFixasSubType" runat="server" CssClass="SmallTextBox5" Width="105px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px; text-align: right">
                    入账公司：
                </td>
                <td style="width: 150px;">
                    <asp:Label ID="lblFixasEntity" runat="server" CssClass="SmallTextBox5" Width="150px"></asp:Label>
                </td>
                <td style="width: 120px; text-align: right">
                    入账日期：
                </td>
                <td style="width: 210px;" colspan="2">
                    <asp:Label ID="lblFixasVouDate" runat="server" CssClass="SmallTextBox5" Width="210px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px; text-align: right">
                    供应商：
                </td>
                <td colspan="4" style="width: 480px">
                    <asp:Label ID="lblFixasSupplier" runat="server" CssClass="SmallTextBox5" Width="492px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">
                    计划保养时间：
                </td>
                <td style="width: 150px;">
                    <asp:Label ID="lblPlanMaintainDate" runat="server" CssClass="SmallTextBox5" Width="150px"></asp:Label>
                </td>
                <td style="text-align: right; width: 120px;">
                </td>
                <td style="width: 210px;" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 120px; text-align: right">
                    保养人：
                </td>
                <td colspan="4" style="width: 480px;">
                    <asp:TextBox ID="txbMaintainedName" runat="server" Width="492px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 120px; text-align: right">
                    保养开始时间：
                </td>
                <td style="width: 150px;">
                    <asp:TextBox ID="txbMaintainBeginDate" runat="server" TabIndex="2" Width="150" 
                        CssClass="Date"></asp:TextBox>
                </td>
                <td style="width: 120px; text-align: right">
                    保养结束时间：
                </td>
                <td style="width: 210px;" colspan="2">
                    <asp:TextBox ID="txbMaintainEndDate" runat="server" TabIndex="3" Width="210px" 
                        CssClass="Date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 120px; text-align: right">
                    正确格式如：
                </td>
                <td style="width: 150px;">
                    <asp:Label ID="lblFormat1" runat="server" Text="2013-09-16 10:08" ForeColor="Red"></asp:Label>
                </td>
                <td style="width: 120px; text-align: right">
                    正确格式如：
                </td>
                <td style="width: 210px;" colspan="2">
                    <asp:Label ID="lblFormat2" runat="server" Text="2013-09-16 15:23" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px; text-align: right">
                    保养描述：
                </td>
                <td colspan="4" style="width: 480px;">
                    <asp:Label ID="lblMaintainDesc" runat="server" CssClass="SmallTextBox5" Width="492px"
                        Style="height: 80px; overflow-y: scroll;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px; text-align: right">
                    保养记录：
                </td>
                <td colspan="4" style="width: 480px;">
                    <asp:TextBox ID="txbMaintainRecord" runat="server" Width="492px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="5">
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" TabIndex="6" Text="保存"
                        Width="50px" OnClick="btnSave_Click" />
                    <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" TabIndex="6" Text="返回"
                        Width="50px" OnClick="btnBack_Click" />
                    <asp:Label ID="lblMaintainRecord" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td style="width: 120px; text-align: right">
                    确认人：
                </td>
                <td colspan="4" style="width: 480px;">
                    <asp:TextBox ID="txbConfirmName" runat="server" Width="492px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="5">
                    <asp:Button ID="btnComplete" runat="server" CssClass="SmallButton2" TabIndex="6"
                        Text="完成保养" Width="70px" OnClick="btnComplete_Click" OnClientClick="return confirm('你确定已完成保养？')" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
