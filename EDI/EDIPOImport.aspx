<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDIPOImport.aspx.cs" Inherits="EDIPOImport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
        <div style="width: 830px; margin: 20px auto; background-color: #d4d4d4; padding-top: 2px;
            padding-bottom: 2px;">
            <fieldset style="width: 800px; padding-left: 3px;">
                <legend style="padding-left: 2px;">�ļ�����</legend>
                <table cellpadding="4" cellspacing="0" style="width: 744px;" border="0">
                    <tr>
                        <td style="height: 5; width: 398px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 398px">
                            �����ļ�:
                        </td>
                        <td valign="top" colspan="2" style="width: 826px">
                            <input id="filename1" style="width: 563px; height: 22px" type="file" name="filename1"
                                runat="server" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click"
                        Text="����" />&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 398px; height: 20px">
                            ����:
                        </td>
                        <td style="height: 20px; text-align: left">
                            <asp:CheckBoxList ID="chkList" runat="server">
                                <asp:ListItem Value="10">���� �ƻ��������(PCD)</asp:ListItem>
                                <asp:ListItem Value="20">���� ��ֹ����</asp:ListItem>
                                <asp:ListItem Value="30">���� ����</asp:ListItem>                                
                                <asp:ListItem Value="40">���� ���۵��ص�</asp:ListItem>
                                <asp:ListItem Value="130">���� �Ƶ�</asp:ListItem>
                                <asp:ListItem Value="50">���� �ͻ�</asp:ListItem>
                                <%--<asp:ListItem Value="60">���� QAD</asp:ListItem>--%>
                                <asp:ListItem Value="70">���� �ͻ����</asp:ListItem>
                                <asp:ListItem Value="80">���� ���۶���</asp:ListItem>
                                <asp:ListItem Value="90">���� ����</asp:ListItem>
                                <asp:ListItem Value="100">���� ��Ʒ(TRUE/FALSE)</asp:ListItem>
                                <asp:ListItem Value="110">BOM����ԭ��</asp:ListItem>
                                <asp:ListItem Value="120">���� ��ŵ��������</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px; width: 398px;">
                            ģ��:
                        </td>
                        <td style="height: 20px; text-align: left;">
                            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="False" Font-Size="11px"
                                Font-Underline="True" NavigateUrl="~/docs/EDIPOImport.xls" Target="_blank">ģ��</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5; width: 398px;">
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
