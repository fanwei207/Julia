<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fixas_repairApplyAdd.aspx.cs"
    Inherits="new_Fixas_repairApplyAdd" %>

<%@ Import Namespace="Portal.Fixas" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <asp:Panel ID="panelRepairOrder" runat="server" Width="574px" Visible="false">
            <asp:Label ID="Label3" runat="server" Text="维修单" Font-Bold="True" Font-Size="Large"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblRepairOrder" runat="server" Text="" Font-Bold="True" Font-Size="Large"></asp:Label>
            <br />
        </asp:Panel>
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 600px">
            <tr>
                <td style="width: 120px; text-align: right;">
                    资产编号：
                </td>
                <td style="width: 150px;">
                    <asp:TextBox ID="txbFixasNo" runat="server" CssClass="SmallTextBox4" TabIndex="1"
                        Width="150px" AutoPostBack="True" OnTextChanged="txbFixasNo_TextChanged"></asp:TextBox>
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
                <td colspan="4" style="width: 480px;">
                    <asp:Label ID="lblFixasSupplier" runat="server" CssClass="SmallTextBox5" Width="492px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px; text-align: right">
                    问题描述：
                </td>
                <td colspan="4" style="width: 480px;">
                    <asp:TextBox ID="txbProblemDesc" runat="server" Width="492px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="5">
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton2" TabIndex="6" Text="添加"
                        Width="70px" OnClick="btnAdd_Click" />
                    <asp:Label ID="lblFixasNo" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblFixasTypeID" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblFixasSubTypeID" runat="server" Text="" Visible="false"></asp:Label>
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
