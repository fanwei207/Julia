<%@ Page Language="C#" AutoEventWireup="true" CodeFile="chk_importPartDaily.aspx.cs"
    Inherits="part_chk_importPartDaily" %>

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
    <form id="form1" runat="server">
    <div align="center" >
        <table style="width: 800px; margin: 5px auto;">
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="��������"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txbGenerateDate" runat="server" CssClass="SmallTextBox Date"></asp:TextBox>
                    <asp:Label ID="Label5" runat="server" Text="�̵�����"></asp:Label>
                    <asp:TextBox ID="txbCheckedDate" runat="server" CssClass="SmallTextBox Date"></asp:TextBox>
                    <asp:Label ID="Label4" runat="server" Text="����"></asp:Label>
                    <asp:TextBox ID="txbFinance" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                    <asp:Label ID="lblUserID" runat="server" Text="" Visible="False"></asp:Label>
                    <asp:Label ID="lblUserName" runat="server" Text="" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <asp:Label ID="Label1" runat="server" Text="Excel�ļ�"></asp:Label>
                </td>
                <td>
                    <input id="excelFile" type="file" runat="server" style="width: 600px" />
                </td>
                <td>
                    <asp:Button ID="btnImport" runat="server" Text="����" Width="103px" CssClass="SmallButton2"
                        OnClick="btnImport_Click" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <asp:Label ID="Label2" runat="server" Text="Excelģ��"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/docs/���������̵�������ģ��.xls">���������̵�������ģ��</asp:HyperLink>
                </td>
            </tr>
        </table>
    </div>

    </form>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
