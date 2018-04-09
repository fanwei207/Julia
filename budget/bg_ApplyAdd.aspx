<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bg_ApplyAdd.aspx.cs" Inherits="bg_ApplyAdd" %>

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
        <table cellspacing="2" cellpadding="2" width="450px" bgcolor="white" border="0">
            <tr>
                <td align="right">
                    <asp:Label ID="lblReceipt" runat="server" Width="100px" CssClass="LabelRight" Text="提交给:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="Left" colspan="3">
                    <asp:TextBox ID="txtReceipt" runat="server" CssClass="SmallTextBox" Width="300px"
                        TabIndex="1" ReadOnly="true"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:Button ID="btnReceipt" runat="server" CssClass="SmallButton2" Width="40px" TabIndex="7"
                        Text="选择" OnClick="btnReceipt_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCopyTo" runat="server" Width="100px" CssClass="LabelRight" Text="抄送给:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="Left" colspan="3">
                    <asp:TextBox ID="txtCopyTo" runat="server" CssClass="SmallTextBox" Width="300px"
                        TabIndex="2" ReadOnly="true"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:Button ID="btnCopyTo" runat="server" CssClass="SmallButton2" Width="40px" TabIndex="8"
                        Text="选择" OnClick="btnCopyTo_Click" />
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 23px">
                    <asp:Label ID="lblSub" runat="server" Width="100px" CssClass="LabelRight" Text="费用用途:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="Left" colspan="4" style="height: 23px">
                    <asp:DropDownList ID="ddlSub" runat="server" Width="350px" AutoPostBack="true" TabIndex="3"
                        DataValueField="SubCode" DataTextField="SubDesc" OnSelectedIndexChanged="ddlSub_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCC" runat="server" Width="100px" CssClass="LabelRight" Text="成本中心:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="Left" colspan="4">
                    <asp:DropDownList ID="ddlCC" runat="server" Width="350px" AutoPostBack="true" TabIndex="4"
                        DataValueField="CCCode" DataTextField="CCDesc" OnSelectedIndexChanged="ddlCC_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblDept" runat="server" Width="100px" CssClass="LabelRight" Text="部门:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="Left" style="width: 150px">
                    <asp:Label ID="lblDeptValue" runat="server" Width="150px" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="lblAccount" runat="server" Width="100px" CssClass="LabelRight" Text="相关账户:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="Left" colspan="2">
                    <asp:Label ID="lblAccountValue" runat="server" Width="150px" CssClass="LabelLeft"
                        Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCumulation" runat="server" Width="100px" CssClass="LabelRight"
                        Text="累计申请:" Font-Bold="false"></asp:Label>
                </td>
                <td align="Left" style="width: 150px">
                    <asp:Label ID="lblCumulationValue" runat="server" Width="150px" CssClass="LabelLeft"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="lblBudget" runat="server" Width="100px" CssClass="LabelRight" Text="预测费用:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="Left" colspan="2">
                    <asp:Label ID="lblBudgetValue" runat="server" Width="150px" CssClass="LabelLeft"
                        Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblNotes" runat="server" Width="100px" CssClass="LabelRight" Text="申请内容:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="4">
                    <asp:TextBox ID="txtNotes" runat="server" CssClass="SmallTextBox" Width="350px" TabIndex="5"
                        TextMode="MultiLine" MaxLength="500" Height="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblAmount" runat="server" Width="100px" CssClass="LabelRight" Text="申请金额:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="4">
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="SmallTextBox Numeric" Width="350px"
                        TabIndex="6"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="center">
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" TabIndex="9" Text="保存"
                        Width="50px" OnClick="btnSave_Click" />
                </td>
                <td>
                    <asp:TextBox ID="txtReceiptID" runat="server" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="txtReceiptEmail" runat="server" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="txtReceiptValue" runat="server" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="txtCopyToID" runat="server" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="txtCopyToEmail" runat="server" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="txtCopyToValue" runat="server" Style="display: none"></asp:TextBox>
                </td>
                <td align="left" style="width: 200px">
                    <asp:Button ID="btnCancel" runat="server" CssClass="SmallButton2" TabIndex="8" Text="返回"
                        Width="50px" OnClick="btnCancel_Click" />
                </td>
                <td>
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
