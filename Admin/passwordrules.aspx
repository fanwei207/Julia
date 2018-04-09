<%@ Page Language="C#" AutoEventWireup="true" CodeFile="passwordrules.aspx.cs" Inherits="admin_passwordrules" %>

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
    <form id="form1" runat="server">
    <div align="center">
        <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" BorderWidth="1" Height="500px"
            Width="620px" BackColor="AliceBlue">
            <table cellspacing="0" cellpadding="0" width="620px" border="0">
                <tr>
                    <td style="height: 30px; width: 120px; text-align: right;">
                        <font style="font-size: 9pt;">�������</font>
                    </td>
                    <td style="width: 35%; height: 30px;">
                        <asp:DropDownList ID="dropRules" runat="server" Width="180px" OnSelectedIndexChanged="dropRules_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 15%; height: 30px;">
                        <asp:Button ID="btnEdit" runat="server" Text="�༭" CssClass="SmallButton2" Width="60px"
                            OnClick="btnEdit_Click" />
                    </td>
                    <td style="width: 15%; height: 30px;">
                        <asp:Button ID="btnAdd" runat="server" Text="����" CssClass="SmallButton2" Width="60px"
                            OnClick="btnAdd_Click" />
                    </td>
                    <td style="width: 15%; height: 30px;">
                        <asp:Button ID="btnDelete" runat="server" Text="ɾ��" CssClass="SmallButton2" Width="60px"
                            OnClick="btnDelete_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px; text-align: right;">
                    </td>
                    <td colspan="4">
                        <asp:CheckBox ID="chkDefaultRule" runat="server" Text="Ĭ�Ϲ���" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px; text-align: right;">
                        <font style="font-size: 9pt;">�������ƣ�</font>
                    </td>
                    <td style="height: 30px" colspan="4">
                        <asp:TextBox ID="txbRuleName" runat="server" Width="180px" Height="20px" CssClass="SmallTextBox"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px; text-align: right;">
                        <font style="font-size: 9pt;">������С���ȣ�</font>
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txbMinLen" runat="server" Width="180px" Height="20px" CssClass="SmallTextBox"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 30px;">
                        <font style="font-size: 9pt;">������󳤶ȣ�</font>
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txbMaxLen" runat="server" Height="20px" Width="180px" CssClass="SmallTextBox"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 30px;">
                        <font style="font-size: 9pt;">�����ظ�������</font>
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txbRepeatCount" runat="server" Height="20px" Width="180px" CssClass="SmallTextBox"
                            ReadOnly="True"></asp:TextBox>
                        <font style="font-size: 9pt;">����ʹ�ü�¼�ɴ洢���������</font>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 30px;">
                        <font style="font-size: 9pt;">������Ч�ڣ�</font>
                    </td>
                    <td style="height: 30px" colspan="4">
                        <asp:TextBox ID="txbValidity" runat="server" Height="20px" Width="180px" CssClass="SmallTextBox"
                            ReadOnly="True"></asp:TextBox>
                        <asp:DropDownList ID="dropValidity" runat="server" Height="20px" Width="64px">
                            <asp:ListItem Value="YEAR">��</asp:ListItem>
                            <asp:ListItem Value="MONTH">��</asp:ListItem>
                            <asp:ListItem Value="DAY">��</asp:ListItem>
                            <asp:ListItem Value="HOUR">ʱ</asp:ListItem>
                            <asp:ListItem Value="MINUTE">��</asp:ListItem>
                            <asp:ListItem Value="SECOND">��</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="height: 120px; text-align: right;">
                        <font style="font-size: 9pt;">���빹�ɣ�</font>
                    </td>
                    <td style="width: 500px; height: 120px; text-align: left;" colspan="4">
                        <p>
                            <asp:CheckBox ID="chkNumber" runat="server" Text="�������" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownList4" runat="server" Width="360px">
                                <asp:ListItem>����0-9</asp:ListItem>
                            </asp:DropDownList>
                        </p>
                        <p>
                            <asp:CheckBox ID="chkLowLetter" runat="server" Text="�������" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownList3" runat="server" Width="360px">
                                <asp:ListItem>Сд��ĸa-z</asp:ListItem>
                            </asp:DropDownList>
                        </p>
                        <p>
                            <asp:CheckBox ID="chkUpLetter" runat="server" Text="�������" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownList2" runat="server" Width="360px">
                                <asp:ListItem>��д��ĸA-Z</asp:ListItem>
                            </asp:DropDownList>
                        </p>
                        <p>
                            <asp:CheckBox ID="chkSpecial" runat="server" Text="�������" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="360px">
                                <asp:ListItem>�����ַ�!@#$%^&amp;*()_+~`[]-=\|:&quot;;',./&lt;&gt;?��</asp:ListItem>
                            </asp:DropDownList>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td style="height: 120px; text-align: right;">
                        <font style="font-size: 9pt;">���빹��������</font>
                    </td>
                    <td style="width: 450px; height: 120px; text-align: left;" colspan="4">
                        <asp:TextBox ID="txbPwdStructureDesc" runat="server" Height="110px" TextMode="MultiLine"
                            Width="450px" CssClass="SmallTextBox" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 15px;" colspan="5">
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
    <script type="text/javascript">
         <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
