<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_rep_cp100.aspx.cs" Inherits="qc_rep_cp100" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 354px">
            <tr>
                <td style="width: 87px" align="right">
                    加工单:
                </td>
                <td style="width: 411px">
                    <asp:TextBox ID="txtNbr1" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>—<asp:TextBox
                        ID="txtNbr2" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 87px; height: 24px;">
                    工单日期:
                </td>
                <td style="width: 411px; height: 24px;">
                    <asp:TextBox ID="txtOrdDate1" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>—<asp:TextBox
                        ID="txtOrdDate2" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 87px">
                    截止日期:
                </td>
                <td style="width: 411px">
                    <asp:TextBox ID="txtDueDate1" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>—<asp:TextBox
                        ID="txtDueDate2" runat="server" CssClass="smalltextbox Date" Width="120px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 87px">
                    种类:
                </td>
                <td style="width: 411px">
                    <asp:DropDownList ID="dropType" runat="server" AutoPostBack="True" DataTextField="typeName"
                        DataValueField="typeID" Width="116px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 87px">
                    TCP:
                </td>
                <td style="width: 411px">
                    <asp:CheckBox ID="chkTcp" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 87px" align="right">
                </td>
                <td style="width: 411px">
                    <asp:Button ID="btnDaily" runat="server" CssClass="SmallButton2" TabIndex="0" Text="导出"
                        OnClick="btnDaily_Click" Width="88px" />
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
