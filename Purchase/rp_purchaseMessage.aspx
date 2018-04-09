<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rp_purchaseMessage.aspx.cs" Inherits="Purchase_rp_purchaseMessage" %>

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
        <asp:HiddenField ID="hidPlanDate" runat="server" />
        <table style="margin-top:20px;">
            <tr>
                <td style="width:100px;" align="right">采购单号</td>
                <td style="width:20px;"></td>
                <td style="width:200px;">
                    <asp:Label ID="labNo" runat="server" Text="Label"></asp:Label>
                </td>
                <%--<td style="width:100px;" align="right">业务部门</td>
                <td style="width:20px;"></td>
                <td style="width:200px;">
                    <asp:Label ID="labCust" runat="server" Text="Label"></asp:Label>
                </td>--%>
            </tr>
            <%--<tr>
                <td align="right">申请人</td>
                <td style="width:20px;"></td>
                <td style="width:100px;">
                    <asp:Label ID="labOrder" runat="server" Text="Label"></asp:Label>
                </td>
                <td align="right">
                    申请部门</td>
                <td style="width:20px;"></td>
                <td style="width:100px;">
                    <asp:TextBox ID="txtPlanDate" runat="server" CssClass="SmallTextBox Date" Width="100px"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <td align="right">留言：<br />
                    （500字以内）
                </td>
                <td style="width:20px;"></td>
                <td colspan="5">
                    <asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" Width="700px" Height="200px" MaxLength="500"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">附件：</td>
                <td style="width:20px;"></td>
                <td colspan="3">
                    <input id="filename" runat="server" style="width:400px;" name="resumename"  CssClass="SmallTextBox"  type="file"/>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="6">
                    <asp:Button ID="btnSaveMsg" runat="server" Text="保存"  CssClass="SmallButton2" OnClick="btnSaveMsg_Click" /></td>
            </tr>
        </table>
    </div>
        <asp:HiddenField ID="hiduID" runat="server" />
        <asp:HiddenField ID="hiduName" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
    </form>
        <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
</body>
</html>
