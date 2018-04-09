<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_bi_mstr.aspx.cs" Inherits="HR_hr_bi_mstr" %>

<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
        <table cellspacing="0" cellpadding="0" width="700" border="0">
            <tr>
                <td rowspan="27" valign="top">
                    <asp:TextBox ID="txtYear" runat="server" Width="50px" AutoPostBack="true" MaxLength="4"
                        OnTextChanged="txtYear_TextChanged"></asp:TextBox>&nbsp;<b>��</b>&nbsp;
                    <asp:DropDownList ID="ddlMonth" runat="server" Width="50px" AutoPostBack="true" Font-Size="10pt"
                        OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
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
                    &nbsp;<b>��</b>
                </td>
                <%--����������--%>
                <td colspan="4" style="height: 25px" align="left" valign="bottom">
                    <u><b>���������� :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b></u>
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    ÿ�³�������:
                </td>
                <td>
                    <asp:TextBox ID="txtWorkDays" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="ÿ�³�������"></asp:TextBox>��
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    ��ʱ�̶�������:
                </td>
                <td>
                    <asp:TextBox ID="txtFixedDays" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="��ʱ�̶�������"></asp:TextBox>��
                </td>
            </tr>
            <%--END ����������--%><%--��ҹ������--%>
            <tr>
                <td colspan="4" style="height: 25px" align="left" valign="bottom">
                    <u><b>��ҹ������ :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b></u>
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    ��ҹ�����:
                </td>
                <td>
                    <asp:TextBox ID="txtMidNightSubsidy" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="��ҹ�����"></asp:TextBox>Ԫ
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    ҹ�����:
                </td>
                <td>
                    <asp:TextBox ID="txtNightSubsidy" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="ҹ�����"></asp:TextBox>Ԫ
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    ȫҹ����:
                </td>
                <td>
                    <asp:TextBox ID="txtWholeNightSubsidy" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="ȫҹ����"></asp:TextBox>Ԫ
                </td>
            </tr>
            <%--END ��ҹ������--%><%--������Ϣ����--%>
            <tr>
                <td colspan="4" style="height: 25px" align="left" valign="bottom">
                    <u><b>������Ϣ���� :&nbsp;&nbsp;&nbsp;&nbsp;</b></u>
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    ��������:
                </td>
                <td>
                    <asp:TextBox ID="txtBasePrice" runat="server" Width="150px" CssClass="SmallTextBox"
                        Enabled="False" ValidationGroup="��������"></asp:TextBox>Ԫ
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    ��������:
                </td>
                <td>
                    <asp:TextBox ID="txtBasicWage" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="��������"></asp:TextBox>Ԫ
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    ������������:
                </td>
                <td>
                    <asp:TextBox ID="txtWorkHours" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="������������"></asp:TextBox>Сʱ
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    ��ƽ��������:
                </td>
                <td>
                    <asp:TextBox ID="txtAvgDays" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="��ƽ��������"></asp:TextBox>��
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    �������:
                </td>
                <td>
                    <asp:TextBox ID="txtLaborRate" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="�������"></asp:TextBox>%
                </td>
                <td style="width: 101px; height: 16px" align="right">
                    �ۿ����:
                </td>
                <td>
                    <asp:TextBox ID="txtDeductRate" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="�ۿ����"></asp:TextBox>%
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    ��˰������:
                </td>
                <td>
                    <asp:TextBox ID="txtTex" runat="server" Width="150px" CssClass="SmallTextBox" ValidationGroup="��˰������"></asp:TextBox>Ԫ
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    �ڼ��չ�����:
                </td>
                <td>
                    <asp:TextBox ID="txtHolidayRate" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="�ڼ��չ�����"></asp:TextBox>��
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    �Ӱ๤����:
                </td>
                <td>
                    <asp:TextBox ID="txtOverTimeRate" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="�Ӱ๤����"></asp:TextBox>��
                </td>
                <td style="width: 101px; height: 16px" align="right">
                    ������������:
                </td>
                <td>
                    <asp:TextBox ID="txtSaturdayRate" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="������������"></asp:TextBox>��
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    ���ٹ�����:
                </td>
                <td>
                    <asp:TextBox ID="txtSickleaveRate" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="���ٹ�����"></asp:TextBox>%
                </td>
                <td style="width: 101px; height: 16px" align="right">
                    ��������:
                </td>
                <td>
                    <asp:TextBox ID="txtSickLeaveDay" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="��������"></asp:TextBox>��
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    ��������:
                </td>
                <td>
                    <asp:TextBox ID="txtOverPrice" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="���ٹ�����"></asp:TextBox>Ԫ
                </td>
                <td style="width: 101px; height: 16px" align="right">
                    ��������:</td>
                <td>
                    <asp:TextBox ID="txtBasicWageNew" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="���ٹ�����"></asp:TextBox>Ԫ</td>
            </tr>
            <%--END ������Ϣ����--%><%--�籣��Ϣ����--%>
            <tr>
                <td colspan="4" style="height: 25px" align="left" valign="bottom">
                    <u><b>�籣��Ϣ���� :&nbsp;&nbsp;&nbsp;&nbsp;</b></u>
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    ��ᱣ�ջ���:
                </td>
                <td>
                    <asp:TextBox ID="txtSocial" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="��ᱣ�ջ���"></asp:TextBox>Ԫ
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    �����:
                </td>
                <td>
                    <asp:TextBox ID="txtUnionfee" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="�����"></asp:TextBox>%
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    ���ϱ���:
                </td>
                <td>
                    <asp:TextBox ID="txtOldAge" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="���ϱ���"></asp:TextBox>%
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    ʧҵ����:
                </td>
                <td>
                    <asp:TextBox ID="txtUnemploy" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="ʧҵ����"></asp:TextBox>%
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    ���˱���:
                </td>
                <td>
                    <asp:TextBox ID="txtInjury" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="���˱���"></asp:TextBox>%
                </td>
                <td style="width: 101px; height: 16px" align="right">
                    ��������:
                </td>
                <td>
                    <asp:TextBox ID="txtMaternity" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="��������"></asp:TextBox>%
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    ҽ�Ʊ���:
                </td>
                <td>
                    <asp:TextBox ID="txtHealth" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="ҽ�Ʊ���"></asp:TextBox>%
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    ס��������:
                </td>
                <td>
                    <asp:TextBox ID="txtHousingFund" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="ס��������"></asp:TextBox>%
                </td>
            </tr>
            <%--ũ����Ϣ����--%>
            <tr>
                <td colspan="4" style="height: 25px" align="left" valign="bottom">
                    <u><b>ũ����Ϣ���� :&nbsp;&nbsp;&nbsp;&nbsp;</b></u>
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    ���ϱ���:
                </td>
                <td>
                    <asp:TextBox ID="txtAOldAge" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="���ϱ���"></asp:TextBox>Ԫ
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    ҽ�Ʊ���:
                </td>
                <td>
                    <asp:TextBox ID="txtAHealth" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="ҽ�Ʊ���"></asp:TextBox>%
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    ���˱���:
                </td>
                <td>
                    <asp:TextBox ID="txtAInjury" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="���˱���"></asp:TextBox>Ԫ
                </td>
            </tr>
            <%--������������--%>
            <tr>
                <td colspan="4" style="height: 25px" align="left" valign="bottom">
                    <u><b>������������ :&nbsp;&nbsp;&nbsp;&nbsp;</b></u>
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    ȫ�ڽ�����:
                </td>
                <td>
                    <asp:TextBox ID="txtMaxAttbonus" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="ȫ�ڽ�����"></asp:TextBox>Ԫ
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    ȫ�ڽ�����:
                </td>
                <td>
                    <asp:TextBox ID="txtMinAttbonus" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="ȫ�ڽ�����"></asp:TextBox>Ԫ
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    ȫ�ڽ�%
                </td>
                <td>
                    <asp:TextBox ID="txtPercentAttbonus" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="ȫ�ڽ�����"></asp:TextBox>%
                </td>
                <td align="right">
                    ÿ�³���������:</td>
                <td>
                    <asp:TextBox ID="txtMinWorkDays" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="ȫ�ڽ�����"></asp:TextBox>��</td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    ��������������:
                </td>
                <td>
                    <asp:TextBox ID="txtMaxWYbonus" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="��������������"></asp:TextBox>Ԫ
                </td>
                <td style="width: 100px; height: 16px" align="right">
                    ��������������:
                </td>
                <td>
                    <asp:TextBox ID="txtMinWYbonus" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="��������������"></asp:TextBox>Ԫ
                </td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    &nbsp;����������:</td>
                <td>
                    <asp:TextBox ID="txtWorkYearbonus" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="����������"></asp:TextBox>Ԫ
                </td>
                <td align="right">
                    ���佱%</td>
                <td>
                    <asp:TextBox ID="txtPercentWYbonus" runat="server" Width="150px" CssClass="SmallTextBox"
                        ValidationGroup="ȫ�ڽ�����"></asp:TextBox>%</td>
            </tr>
            <tr>
                <td style="width: 101px; height: 16px" align="right">
                    <span lang="ZH-CN">ֻ�ܵ���</span></td>
                <td>
                    <asp:CheckBox ID="chkMinus" runat="server" Checked="True" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="height: 28px" align="center" colspan="5">
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" Text="����" OnClick="btnSave_Click"
                        ValidationGroup="chkAll" CausesValidation="true"></asp:Button>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript" language="javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
