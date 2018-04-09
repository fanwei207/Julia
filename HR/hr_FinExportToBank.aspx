<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_FinExportToBank.aspx.cs"
    Inherits="HR_hr_FinExportToBank" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table cellspacing="1" cellpadding="1" width="550px" border="0">
            <tr>
                <td>
                    <asp:TextBox ID="txtYear" runat="server" Width="50px" MaxLength="4"></asp:TextBox>&nbsp;<b>��</b>&nbsp;
                    <asp:DropDownList ID="dropMonth" runat="server" Width="50px" Font-Size="10pt">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;<b>��</b>&nbsp;
                </td>
                <td>
                    <b>����</b> &nbsp;
                    <asp:DropDownList ID="dropBank" runat="server" Width="80px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnBank" runat="server" Text="���ʸ�����EXCEL" CssClass="SmallButton2"
                        Width="100px" OnClick="btnBank_Click" />
                &nbsp;<asp:Button ID="btnBankTest" runat="server" Text="������EXCEL(�����ݴ�)" CssClass="SmallButton2"
                        Width="130px" OnClick="btnBankTest_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="center">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnFintotal" runat="server" Width="100px" Text="������ϸ����" CssClass="SmallButton2"
                        OnClick="btnFintotal_Click" />
                </td>
                <td align="center">
                </td>
                <td align="center">
                    <asp:Button ID="btmSummary" runat="server" Width="100px" Text="���ʻ��ܱ���" CssClass="SmallButton2"
                        OnClick="btmSummary_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnConfirm" runat="server" Width="100px" Text="�Ƽ�����ȷ��" CssClass="SmallButton2"
                        OnClick="btnConfirm_Click" />
                </td>
                <td align="center">
                </td>
                <td align="center">
                    <asp:Button ID="btnConfirmtime" runat="server" Width="100px" Text="��ʱ����ȷ��" CssClass="SmallButton2"
                        OnClick="btnConfirmtime_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Button ID="btnExportSalary" runat="server" Width="100px" Text="���ʷ�������" CssClass="SmallButton2"
                        OnClick="btnExportSalary_Click" />
                </td>
            </tr>
        </table>
        <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
        </form>
    </div>
</body>
</html>
