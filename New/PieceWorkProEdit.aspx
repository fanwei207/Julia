<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PieceWorkProEdit.aspx.cs"
    Inherits="PieceWorkProEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function getStandardPrice() {
            var price = document.getElementById('txtPrice');
            var stprice = document.getElementById('txtSdprice');
            var Coeff = document.getElementById('txtCoeff');
            stprice.value = price.value / Coeff.value;
        }
        
    </script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <br />
        <table cellspacing="5" cellpadding="5" border="0">
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    ����:
                </td>
                <td style="height: 16px">
                    <asp:DropDownList ID="dropType" runat="server" Width="310px" DataTextField="systemCodeName"
                        DataValueField="systemCodeID">
                    </asp:DropDownList>
                    <asp:Label ID="lblType" runat="server" ForeColor="Red" Text="��ѡ��һ��" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 14px" align="right">
                    ����:
                </td>
                <td style="height: 17px">
                    <asp:DropDownList ID="dropWorkKinds" runat="server" Width="152px" DataTextField="Name"
                        DataValueField="ID">
                    </asp:DropDownList>
                    <asp:Label ID="lblWorkKinds" runat="server" ForeColor="Red" Text="��ѡ��һ��" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 14px" align="right">
                    ֧������:
                </td>
                <td style="height: 17px">
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="SmallTextBox" Width="150px" onkeyup="getStandardPrice();">1</asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPrice"
                        ErrorMessage="����Ϊ��" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPrice"
                        ErrorMessage="����Ϊ�������������С��" ValidationExpression="^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$"
                        Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    ϵ��:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="txtCoeff" runat="server" CssClass="SmallTextBox" Width="150px" onkeyup="getStandardPrice();">1</asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCoeff"
                        ErrorMessage="����Ϊ��" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCoeff"
                        ErrorMessage="����Ϊ�������������С��" ValidationExpression="^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$"
                        Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    ��׼����:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="txtSdprice" onkeydown="event.returnValue=false;" onpaste="return false"
                        runat="server" CssClass="SmallTextBox" Width="150px">1</asp:TextBox>&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="(��׼����=֧������/ϵ��)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 100px; height: 16px">
                    ����:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="SmallTextBox Date" Width="150px"
                        onkeydown="event.returnValue=false;" onpaste="return false"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDate"
                        ErrorMessage="���ڲ���Ϊ��" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    ��ע:
                </td>
                <td style="height: 16px">
                    <textarea id="txtComment" runat="server" cols="20" style="height: 86px"></textarea>
                </td>
            </tr>
            <tr>
                <td style="height: 28px" align="center" colspan="2">
                    <asp:Button ID="btnModify" runat="server" CssClass="SmallButton2" Text="�޸�" OnClick="btnModify_Click">
                    </asp:Button>
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" Text="����" OnClick="btnSave_Click">
                    </asp:Button><asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" Text="����"
                        Visible="True" CausesValidation="False" OnClick="btnBack_Click"></asp:Button>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
