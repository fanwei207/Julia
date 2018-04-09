<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.addpersonnel" CodeFile="addpersonnel.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 55px;
            height: 17px;
        }
        .style2
        {
            width: 232px;
            height: 17px;
        }
        .style3
        {
            width: 87px;
            height: 17px;
        }
        .style4
        {
            height: 17px;
        }
        .style5
        {
            width: 55px;
            height: 16px;
        }
        .style6
        {
            width: 232px;
            height: 16px;
        }
        .style7
        {
            width: 87px;
            height: 16px;
        }
        .style8
        {
            height: 16px;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <asp:ValidationSummary ID="Validationsummary1" runat="server" ShowSummary="false"
            ShowMessageBox="true" HeaderText="���������ֶ�:"></asp:ValidationSummary>
        <table cellspacing="4" cellpadding="0" style="border:1px solid #d7d7d7; margin-top:2px;">
            <tr>
                <td style="height: 16px; width: 55px;" align="right">
                    �� &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��
                </td>
                <td style="height: 16px; width: 232px;">
                    <asp:TextBox ID="usercode" TabIndex="2" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="20" ReadOnly="false" AutoPostBack="True"></asp:TextBox>
                </td>
                <td style="height: 16px; width: 87px;" align="right">
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="UserName" TabIndex="1" runat="server" Width="183px" CssClass="SmallTextBox" AutoPostBack="true"
                        MaxLength="20"></asp:TextBox>
                </td>
                <td style="height: 4px" valign="middle" align="center" colspan="2" rowspan="6">
                    <asp:Image ID="photo" runat="server" BackColor="#ffffff" Width="120px" Height="130px">
                    </asp:Image>
                </td>
            </tr>
            <tr>
                <td align="right" class="style5">
                    &nbsp;��&nbsp; ��&nbsp; ��
                </td>
                <td class="style6">
                    <asp:TextBox ID="LoginName" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="20"></asp:TextBox>
                </td>
                 <td align="right" class="style7">
                        Ӣ&nbsp; ��&nbsp; ��
                    </td>
                <td class="style8">
                    <asp:TextBox ID="txtEnglishName" TabIndex="2" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="20"></asp:TextBox>
                </td>  
            </tr>
            <tr>
                <td style="height: 14px; width: 55px;" align="right">
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��
                </td>
                <td style="height: 14px; width: 232px;">
                    <asp:TextBox ID="userPWD" TabIndex="3" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="15" TextMode="Password" ReadOnly="true">******</asp:TextBox>
                </td>
                <td
                        style="width: 87px; height: 16px" align="right">
                        ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��
                    </td>
                <td style="height: 16px">
                    <asp:TextBox ID="birthday" TabIndex="4" runat="server" Width="183px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                    ��������
                </td>
                <td class="style2">
                    <asp:TextBox ID="enterdate" TabIndex="6" runat="server" Width="183px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td>
                <asp:CompareValidator ID="Comparevalidator3" runat="server" Display="none" ErrorMessage="ת�����ڱ���Ϊ������"
                    ControlToValidate="employDate" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                <td align="right" class="style3">
                        ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��
                    </td>
                <td class="style4">
                    <asp:DropDownList ID="sex" TabIndex="7" runat="server" Width="60px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ա������ &nbsp;<asp:DropDownList ID="userType" runat="server"
                        Width="40px">
                    </asp:DropDownList>
                </td>    
            </tr>
            <tr>
                <td style="height: 14px; width: 55px;" align="right">
                    ת������
                </td>
                <td style="height: 17px; width: 232px;">
                    <asp:TextBox ID="employDate" TabIndex="9" runat="server" Width="183px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td>
                <asp:CompareValidator ID="Comparevalidator2" runat="server" Display="none" ErrorMessage="�뿪��λ���ڱ���Ϊ������"
                    ControlToValidate="leavedate" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                <td
                        style="width: 87px; height: 16px" align="right">
                        ��&nbsp; ��&nbsp; ֤
                    </td>
                <td style="height: 16px">
                    <asp:TextBox ID="icno" TabIndex="10" runat="server" Width="183px" CssClass="SmallTextBox" AutoPostBack="true"
                        MaxLength="20"></asp:TextBox>
                </td>    
            </tr>
            <tr>
                <td style="height: 14px; width: 55px;" align="right">
                    �뿪����
                </td>
                <td style="height: 17px; width: 232px;">
                    <asp:TextBox ID="leavedate" TabIndex="12" runat="server" Width="183px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td>
                <td
                        style="width: 87px; height: 16px" tabindex="13" align="right">
                        ��ͥ��ַ
                    </td>
                <td style="height: 16px">
                    <asp:TextBox ID="homeaddress" TabIndex="14" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="50"></asp:TextBox>
                </td>

                </tr>
            <tr>
                <td colspan="2" style="height: 1px; width: 287px;">
                    <asp:CheckBox ID="Cleave" runat="server" Text="��˾����"></asp:CheckBox>&nbsp; &nbsp;&nbsp;
                    <asp:CheckBox ID="falelv" runat="server" Text="��������ְ"></asp:CheckBox>&nbsp; &nbsp;&nbsp;
                    <asp:CheckBox ID="unback" runat="server" Text="δ��"></asp:CheckBox>
                </td>
                <td style="height: 1px; width: 87px;" align="right">
                    �� &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��
                </td>
                <td style="height: 1px">
                    <asp:TextBox ID="homezip" TabIndex="17" runat="server" Width="183px" CssClass="SmallTextBox Numeric"
                        MaxLength="10"></asp:TextBox>
                </td>
                <td valign="middle" align="center" colspan="2" rowspan="2">
                    <asp:Button ID="btnUpload" runat="server" Text="��Ƭ�ϴ�" />
                </td>
            </tr>
            <tr>
                <td style="height: 11px; width: 55px;" align="right">
                    ��ͬ����
                </td>
                <td style="height: 11px; width: 232px;">
                    <asp:TextBox ID="contractstartdate" TabIndex="16" runat="server" Width="85px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>��
                    <asp:TextBox ID="contractdate" TabIndex="16" runat="server" Width="85px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td><td style="height: 16px; width: 87px;" align="right">
                    Ŀǰסַ
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="currentAddress" TabIndex="20" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 11px; width: 55px;" align="right">
                    ��ͬ����
                </td>
                <td style="height: 11px; width: 232px;">
                    <asp:DropDownList ID="contract" TabIndex="19" runat="server" Width="80px">
                    </asp:DropDownList>
                    &nbsp; &nbsp;��˾
                    <asp:TextBox ID="comp" runat="server" Width="70px"></asp:TextBox>
                </td>
                <td
                        style="width: 87px; height: 16px" align="right">
                        �� &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="currentzip" TabIndex="23" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="40"></asp:TextBox>
                </td>
                <td style="height: 4px; width: 58px;" align="right">
                    ��������
                </td>
                <td style="height: 4px">
                    <asp:TextBox ID="txb_exchangeDate" TabIndex="8" runat="server" Width="160px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 11px; width: 55px;" align="right">
                    �Ƴ귽ʽ
                </td>
                <td style="height: 11px; width: 232px;">
                    <asp:DropDownList ID="worktype" TabIndex="25" runat="server" Width="76px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;����
                    <asp:TextBox ID="worktypedate" runat="server" Width="72px" CssClass="SmallTextBox Date"
                        Height="20px"></asp:TextBox>
                </td>
                <td style="height: 11px; width: 87px;" align="right">
                    ��&nbsp;&nbsp;��&nbsp;&nbsp;��
                </td>
                <td style="height: 11px">
                    <asp:DropDownList ID="province" TabIndex="26" runat="server" Width="75px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;����
                    <asp:TextBox ID="Pzone" runat="server" Width="73px"></asp:TextBox>
                </td>
                <td style="height: 8px; width: 58px;" align="right">
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��
                </td>
                <td style="height: 8px">
                    <asp:DropDownList ID="Department" TabIndex="5" runat="server" Width="160px" AutoPostBack="True">
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td style="height: 11px; width: 55px;" align="right">
                    �ù�����
                </td>
                <td style="height: 11px; width: 232px;">
                    <asp:DropDownList ID="employtype" TabIndex="27" runat="server" Width="183px">
                    </asp:DropDownList>
                </td>
                <td style="height: 16px; width: 87px;" align="right">
                    ��������
                </td>
                <td style="height: 17px">
                    <asp:DropDownList ID="dropRegister" runat="server" Height="16px" Width="75px">
                        <asp:ListItem Value="NULL">--</asp:ListItem>
                        <asp:ListItem Value="0">��ũҵ����</asp:ListItem>
                        <asp:ListItem Value="1">ũҵ����</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp; ����<asp:DropDownList ID="dropNation" TabIndex="26" runat="server"
                        Width="75px">
                        <asp:ListItem Value="NULL">--</asp:ListItem>
                        <asp:ListItem Value="����">����</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="����">����</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="���Ӷ���">���Ӷ���</asp:ListItem>
                        <asp:ListItem Value="����">����</asp:ListItem>
                        <asp:ListItem Value="�°���">�°���</asp:ListItem>
                        <asp:ListItem Value="����">����</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="���״���">���״���</asp:ListItem>
                        <asp:ListItem Value="����˹��">����˹��</asp:ListItem>
                        <asp:ListItem Value="���¿���">���¿���</asp:ListItem>
                        <asp:ListItem Value="��ɽ��">��ɽ��</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="��������">��������</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="����">����</asp:ListItem>
                        <asp:ListItem Value="��ŵ��">��ŵ��</asp:ListItem>
                        <asp:ListItem Value="����">����</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="�¶�������">�¶�������</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="����">����</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="�����">�����</asp:ListItem>
                        <asp:ListItem Value="����">����</asp:ListItem>
                        <asp:ListItem Value="ë����">ë����</asp:ListItem>
                        <asp:ListItem Value="�Ű���">�Ű���</asp:ListItem>
                        <asp:ListItem Value="�ɹ���">�ɹ���</asp:ListItem>
                        <asp:ListItem Value="����">����</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="ŭ��">ŭ��</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="Ǽ��">Ǽ��</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="���">���</asp:ListItem>
                        <asp:ListItem Value="ˮ��">ˮ��</asp:ListItem>
                        <asp:ListItem Value="��������">��������</asp:ListItem>
                        <asp:ListItem Value="��������">��������</asp:ListItem>
                        <asp:ListItem Value="����">����</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="����">����</asp:ListItem>
                        <asp:ListItem Value="������">������</asp:ListItem>
                        <asp:ListItem Value="���ȱ����">���ȱ����</asp:ListItem>
                        <asp:ListItem Value="����">����</asp:ListItem>
                        <asp:ListItem Value="����">����</asp:ListItem>
                        <asp:ListItem Value="ԣ����">ԣ����</asp:ListItem>
                        <asp:ListItem Value="ԣ����">ԣ����</asp:ListItem>
                        <asp:ListItem Value="ά�����">ά�����</asp:ListItem>
                        <asp:ListItem Value="׳��">׳��</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="height: 8px; width: 58px;" align="right">
                    ����/����
                </td>
                <td style="height: 14px">
                    <asp:DropDownList ID="workshop" TabIndex="11" runat="server" Width="160px" AutoPostBack="True">
                    </asp:DropDownList>
                </td></tr>
            <tr>
                <td style="height: 16px; width: 55px;" align="right">
                    ��ʼ����
                </td>
                <td style="height: 4px; width: 232px;">
                    <asp:TextBox ID="healthCheckDate" TabIndex="29" runat="server" Width="183px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td>
                                <td style="height: 16px; width: 87px;" align="right">
                    ������ò
                </td>
                <td style="height: 11px">
                    <asp:DropDownList ID="dropPoliticalStatus" TabIndex="26" runat="server" Width="75px">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>�й���Ա</asp:ListItem>
                        <asp:ListItem>�й�Ԥ����Ա</asp:ListItem>
                        <asp:ListItem>������Ա</asp:ListItem>
                        <asp:ListItem>Ⱥ��</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp; ѧ��<asp:DropDownList ID="education" TabIndex="28" runat="server" Width="75px">
                    </asp:DropDownList>
                </td>
                <td style="height: 11px; width: 58px;" align="right">
                    ְ��/��λ
                </td>
                <td style="height: 11px">
                    <asp:DropDownList ID="dropRoleType" runat="server" AutoPostBack="True" Width="60px">
                        <asp:ListItem Selected="True" Value="0">�����</asp:ListItem>
                        <asp:ListItem Value="1">���ż�</asp:ListItem>
                        <asp:ListItem Value="2">ְ��</asp:ListItem>
                        <asp:ListItem Value="3">����</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="role" TabIndex="22" runat="server" Width="100px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 16px; width: 55px;" align="right">
                    ���⹤��
                </td>
                <td style="height: 16px; width: 232px;">
                    <asp:DropDownList ID="especialtype" TabIndex="31" runat="server" Width="183px">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>����</asp:ListItem>
                        <asp:ListItem>���ȷ�</asp:ListItem>
                        <asp:ListItem>������ӡ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="height: 16px; width: 87px;" align="right">
                    ְ&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��
                </td>
                <td class="3" style="height: 16px">
                    <asp:DropDownList ID="occupation" TabIndex="30" runat="server" Width="133px">
                    </asp:DropDownList>
                </td>
                <td style="height: 11px; width: 58px;" align="right">
                    ϵ&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��
                </td>
                <td style="height: 11px">
                    <asp:TextBox ID="txtCoef" runat="server" CssClass="SmallTextBox" MaxLength="10" Width="60px"
                        ReadOnly="True"></asp:TextBox>
                    &nbsp;Level
                    <asp:DropDownList ID="ddl_level" runat="server" AutoPostBack="True" Width="60px">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Selected="True" Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                    </asp:DropDownList>
                    <%--<asp:TextBox ID="txt_level" runat="server" Width="60px" MaxLength = 1></asp:TextBox>--%>
                </td>
            </tr>
            <tr>
                <td style="height: 2px; width: 55px;" align="right">
                    �� &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��
                </td>
                <td style="height: 2px; width: 232px;">
                    <asp:TextBox ID="begood" TabIndex="32" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="50"></asp:TextBox>
                </td>
                <td style="height: 4px; width: 87px;" align="right">
                    ֤&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��
                </td>
                <td style="height: 4px">
                    <asp:TextBox ID="certificate" TabIndex="32" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="50"></asp:TextBox>
                </td><td
                        style="width: 58px; height: 2px" align="right">
                        ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��
                    </td>
                <td style="height: 11px">
                    <asp:DropDownList ID="workgroup" TabIndex="15" runat="server" Width="160px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 10px; width: 55px;" valign="top" align="right">
                    ��&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��
                </td>
                <td style="height: 10px; width: 232px;" valign="top">
                    <asp:TextBox ID="fax" TabIndex="34" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="40"></asp:TextBox>
                </td>
                <td style="height: 2px; width: 87px;" align="right">
                    ��&nbsp;&nbsp;��&nbsp;&nbsp;��
                </td>
                <td style="height: 2px">
                    <asp:TextBox ID="introducer" TabIndex="33" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="50"></asp:TextBox>
                </td></asp:RegularExpressionValidator><td
                        style="width: 58px; height: 2px" align="right">
                        ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��
                    </td>
                <td>
                    <asp:DropDownList ID="kindswork" TabIndex="15" runat="server" Width="160px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 8px; width: 55px;" valign="top" align="right">
                    �����ʼ�
                </td>
                <td style="height: 8px; width: 232px;" valign="top">
                    <asp:TextBox ID="Email" TabIndex="36" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="50"></asp:TextBox>
                </td>
                <td style="height: 10px; width: 87px;" align="right">
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��
                </td>
                <td style="height: 10px">
                    <asp:TextBox ID="phone" TabIndex="35" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="20"></asp:TextBox>
                </td>
                <td align="right">
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ע
                </td>
                <td></td>
            </tr>
            <tr>
                <td style="height: 8px; width: 55px;" valign="top" align="right">
                    ��������
                </td>
                <td style="height: 8px; width: 232px;" valign="top">
                    <asp:DropDownList ID="insurance" TabIndex="18" runat="server" Width="183px">
                    </asp:DropDownList>
                </td>
                <td style="width: 87px; text-align: right;">
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��
                </td>
                <td style="height: 10px" align="left">
                    <asp:TextBox ID="Mobile" TabIndex="37" runat="server" Width="183px" CssClass="SmallTextBox"
                        MaxLength="20"></asp:TextBox>
                </td>
                <td rowspan="6" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="comments" runat="server" Width="200px" CssClass="SmallTextBox" TextMode="MultiLine"
                        Height="119px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="height: 1px; width: 232px;" valign="top" align="left">
                    <asp:CheckBox ID="houseFund" runat="server" Text="ס��"></asp:CheckBox>&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="medicalFund" runat="server" Text="ҽ��"></asp:CheckBox>&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="unemployFund" runat="server" Text="ʧҵ"></asp:CheckBox>
                </td>
                <td align="right" style="width: 87px">
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��
                </td>
                <td style="height: 4px" align="Left">
                    <asp:DropDownList ID="marriage" TabIndex="24" runat="server" Width="133px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="height: 1px; width: 232px;" valign="top" align="left">
                    <asp:CheckBox ID="retiredFund" runat="server" Text="����"></asp:CheckBox>&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="sretiredFund" runat="server" Text="����"></asp:CheckBox>
                </td>
                <td style="height: 4px; width: 87px;" align="right">
                    &nbsp;
                </td>
                <td>
                    &nbsp;<asp:CheckBox ID="labourunion" TabIndex="38" runat="server" Enabled="True"
                        Checked="False" Text="�����Ա"></asp:CheckBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="isActive" TabIndex="39" runat="server"
                        Enabled="True" Checked="False" Text="��Ч"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    �ɷ�����
                </td>
                <td style="height: 1px; width: 232px;" valign="middle" align="left">
                    <asp:TextBox ID="txbPayDate" runat="server" Width="72px" CssClass="SmallTextBox"></asp:TextBox>&nbsp;&nbsp;&nbsp;ת������
                    <asp:TextBox ID="txbFinishDate" runat="server" Width="72px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td style="height: 4px; width: 87px;" align="right">
                    �빤������
                </td>
                <td>
                    <asp:TextBox ID="labedate" runat="server" Width="183px" CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    ������ɷ�
                </td>
                <td style="height: 1px; width: 232px;" valign="middle" align="left">
                    <asp:TextBox ID="txbHouseFundPayDate" runat="server" Width="72px" CssClass="SmallTextBox"></asp:TextBox>������ת��
                    <asp:TextBox ID="txbHouseFundFinishDate" runat="server" Width="72px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td style="width: 87px;" valign="middle" align="right">
                    ���ɺ�ͬ����
                </td>
                <td>
                    <asp:TextBox ID="wldate" runat="server" Width="183px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 16px; width: 55px;" align="right">
                    ���ڱ��
                </td>
                <td style="height: 16px; width: 232px;">
                    <asp:TextBox ID="txtFingerprint" TabIndex="32" runat="server" Width="183px" CssClass="SmallTextBox Numeric"
                        MaxLength="10"></asp:TextBox>
                </td><td
                        style="height: 1px; width: 87px;" valign="middle" align="right">
                        ���֤��Ч����
                    </td>
                <td>
                    <asp:TextBox ID="txtIDdate" runat="server" Width="183px" CssClass="SmallTextBox Date"
                        MaxLength="10"></asp:TextBox>
                </td>
            </tr>
        </table>
        &nbsp;
        <table cellspacing="1" cellpadding="1" border="0" style="width: 780px">
            <tr>
                <td style="height: 28px" align="center" width="250">
                    <asp:Button ID="BtnModify" TabIndex="40" runat="server" CssClass="SmallButton2" Visible="False"
                        Text="�޸�"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="BtnSave" TabIndex="41" runat="server" CssClass="SmallButton2" Visible="False"
                        Text="����"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="BtnDelete" TabIndex="42" runat="server" CssClass="SmallButton2" Visible="false"
                        Text="ɾ��" CausesValidation="False"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="BtnReturn" TabIndex="43" runat="server" CssClass="SmallButton2" Visible="True"
                        Text="����" CausesValidation="False"></asp:Button>
                </td>
                <td width="50">
                </td>
                <td style="height: 28px; width: 364px;" align="center">
                    <asp:Button ID="Button1" TabIndex="44" runat="server" CssClass="SmallButton2" Visible="True"
                        Text="�׼�¼" CausesValidation="False"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="Button2" TabIndex="45" runat="server" CssClass="SmallButton2" Visible="True"
                        Text="��һҳ" CausesValidation="False"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="Button3" TabIndex="46" runat="server" CssClass="SmallButton2" Visible="True"
                        Text="��һҳ" CausesValidation="False"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="Button4" TabIndex="47" runat="server" CssClass="SmallButton2" Visible="True"
                        Text="ĩ��¼" CausesValidation="False"></asp:Button>
                </td>
                <td style="height: 28px" align="right" width="100">
                    <asp:Button ID="btn_exportMoveHist" TabIndex="48" runat="server" Width="90px" CssClass="SmallButton2"
                        Visible="True" Text="��ȡ������¼"></asp:Button>
                </td>
                <td align="right" style="height: 28px" width="100">
                    <asp:Button ID="Button5" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        TabIndex="49" Text="����IC����" Visible="True" Width="72px" />
                </td>
            </tr>
        </table>
        <asp:Label ID="datetype" runat="server" Visible="False"></asp:Label><asp:Label ID="kindtype"
            runat="server" Visible="False"></asp:Label>
        <asp:Label ID="treturn" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="foundtype" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="worktypechange" runat="server" Visible="False"></asp:Label>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
