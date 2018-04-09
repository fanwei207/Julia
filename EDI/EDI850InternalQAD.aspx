<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDI850InternalQAD.aspx.cs"
    Inherits="EDI_EDI850InternalQAD" %>

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
        <table cellspacing="0" cellpadding="0" border="0" style="background-image: url('../images/bg_tb20.jpg');
            background-repeat: repeat-x; margin-top: 20px; height: 174px; width: 429px;">
            <tr>
                <td rowspan="5" style="width: 4px; background-image: url(../images/bg_tb19.jpg);
                    background-repeat: no-repeat; background-position: top left;">
                </td>
                <td align="right">
                    ��������:
                </td>
                <td>
                    <asp:TextBox ID="txtDateFr" runat="server" Width="70" onkeydown="event.returnValue=false;"
                        onpaste="return false;" CssClass="SmallTextBox4 Date"></asp:TextBox>
                    ����<asp:TextBox ID="txtDateTo" runat="server" Width="70" onkeydown="event.returnValue=false;"
                        onpaste="return false;" CssClass="SmallTextBox4 Date"></asp:TextBox>
                </td>
                <td rowspan="5" style="width: 4px; background-image: url(../images/bg_tb21.jpg);
                    background-repeat: no-repeat; background-position: right top;">
                </td>
            </tr>
            <tr>
                <td align="right">
                    �ɹ�Ա:
                </td>
                <td>
                    <asp:TextBox ID="txtBuyer" runat="server" Width="160" CssClass="SmallTextBox4 Buyer"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    QAD��:
                </td>
                <td>
                    <asp:TextBox ID="txtDomain" runat="server" Width="160" CssClass="SmallTextBox4 Company"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" height="5">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnExportEdi" runat="server" Text="����EDI�ɹ�����" CssClass="SmallButton2"
                        Width="100" OnClick="btnExportEdi_Click" OnClientClick="alert('��������ʱ��Ƚϳ������ĵȴ�.......');"
                        CausesValidation="true" ValidationGroup="validationGrp1" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnExportExcel" runat="server" Text="����Excel����" CssClass="SmallButton2"
                        Width="100" OnClick="btnExportExcel_Click" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnGenEdi" runat="server" Text="����EDI����" CssClass="SmallButton2"
                        Width="100" OnClick="btnGenEdi_Click" OnClientClick="return confirm('������EDI�ɹ�����֮���޷��ٴε���Excel����ȷ�Ϸ���EDI�ɹ�����?')" />
                </td>
            </tr>
        </table>
        <div id="contain" style="width: 600px; height: 100px;">
        </div>
        <asp:Label ID="lblFlag" runat="server" Visible="False"></asp:Label>
        </form>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
