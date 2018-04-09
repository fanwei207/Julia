<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_wroRelationEdit.aspx.cs"
    Inherits="wo2_wo2_wroRelationEdit" %>

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
    <div align="center">
        <table cellpadding="0" cellspacing="0" style="margin: 20px auto; width: 500px; background-color: AliceBlue;
            border-right: 1px solid black; border-bottom: 1px solid black; padding: 20px;">
            <tr style="padding: 10px;">
                <td>
                    父工序：
                    <asp:TextBox ID="txbParent" runat="server" CssClass="SmallTextBox" Width="126px"></asp:TextBox>
                </td>
                <td>
                    父工序名称：
                    <asp:TextBox ID="txbParentName" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr style="padding: 10px;">
                <td colspan="2">
                    子工序：
                    <asp:DropDownList ID="dropChild" runat="server" Width="257px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr align="center" style="padding: 10px;">
                <td colspan="2">
                    <asp:Button ID="btnClose" runat="server" Text="返回" CssClass="SmallButton2" Width="70px"
                        OnClick="btnClose_Click" />
                    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="SmallButton2" Width="70px"
                        OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="lblWroID" runat="server" Text="" Visible="false"></asp:Label>
    </form>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
