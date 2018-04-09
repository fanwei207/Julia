<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bg_ApplyEvaluate.aspx.cs"
    Inherits="bg_ApplyEvaluate" %>

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
        <table cellspacing="2" cellpadding="2" width="750px" bgcolor="white" border="0">
            <tr>
                <td align="right">
                    <asp:Label ID="lblApplicant" runat="server" Width="100px" CssClass="LabelRight" Text="申请人:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="Left" colspan="6">
                    <asp:Label ID="lblApplicantValue" runat="server" Width="650px" CssClass="LabelLeft"
                        Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    <asp:Label ID="lblContent" runat="server" Width="100px" CssClass="LabelRight" Text="申请内容:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="Left" colspan="6">
                    <asp:TextBox ID="txtContentValue" runat="server" CssClass="TextLeft" TextMode="MultiLine"
                        Width="650px" BorderWidth="0px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblAmount" runat="server" Width="100px" CssClass="LabelRight" Text="申请金额:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="6">
                    <asp:Label ID="lblAmountValue" runat="server" Width="650px" CssClass="LabelLeft"
                        Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblAccount" runat="server" Width="100px" CssClass="LabelRight" Text="账户信息:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="6">
                    <asp:Label ID="lblAccountValue" runat="server" Width="650px" CssClass="LabelLeft"
                        Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblBudget" runat="server" Width="100px" CssClass="LabelRight" Text="预测费用:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="6">
                    <asp:Label ID="lblBudgetValue" runat="server" Width="650px" CssClass="LabelLeft"
                        Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCumulation" runat="server" Width="100px" CssClass="LabelRight"
                        Text="累计申请:" Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="6">
                    <asp:Label ID="lblCumulationValue" runat="server" Width="650px" CssClass="LabelLeft"
                        Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="7">
                    <asp:GridView ID="gvEvaluate" runat="server" AllowPaging="False" AllowSorting="False"
                        AutoGenerateColumns="False" CssClass="GridViewStyle" Width="750px">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="Notes" HeaderText="审核内容" HtmlEncode="false">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="true" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblReceipt" runat="server" Width="100px" CssClass="LabelRight" Text="提交给:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="Left" colspan="5">
                    <asp:TextBox ID="txtReceipt" runat="server" CssClass="SmallTextBox" Width="600px"
                        TabIndex="1" ReadOnly="true"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:Button ID="btnReceipt" runat="server" CssClass="SmallButton2" Width="40px" TabIndex="4"
                        Text="选择" OnClick="btnReceipt_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblCopyTo" runat="server" Width="100px" CssClass="LabelRight" Text="抄送给:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="Left" colspan="5">
                    <asp:TextBox ID="txtCopyTo" runat="server" CssClass="SmallTextBox" Width="600px"
                        TabIndex="2" ReadOnly="true"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:Button ID="btnCopyTo" runat="server" CssClass="SmallButton2" Width="40px" TabIndex="5"
                        Text="选择" OnClick="btnCopyTo_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblNotes" runat="server" Width="100px" CssClass="LabelRight" Text="审核备注:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" colspan="6">
                    <asp:TextBox ID="txtNotes" runat="server" CssClass="SmallTextBox" Width="650px" TabIndex="3"
                        TextMode="MultiLine" MaxLength="500" Height="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 100px">
                    <asp:TextBox ID="txtReceiptID" runat="server" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="txtReceiptEmail" runat="server" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="txtReceiptValue" runat="server" Style="display: none"></asp:TextBox>
                </td>
                <td align="center" style="width: 100px">
                    <asp:TextBox ID="txtCopyToID" runat="server" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="txtCopyToEmail" runat="server" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="txtCopyToValue" runat="server" Style="display: none"></asp:TextBox>
                </td>
                <td align="center" style="width: 100px">
                    <asp:Button ID="btnPass" runat="server" CssClass="SmallButton2" TabIndex="6" Text="通过"
                        Width="60px" OnClick="btnPass_Click" />
                </td>
                <td align="center" style="width: 100px">
                    <asp:Button ID="btnPassClose" runat="server" CssClass="SmallButton2" TabIndex="6"
                        Text="通过并关闭" Width="80px" OnClick="btnPassClose_Click" />
                </td>
                <td align="center" style="width: 100px">
                    <asp:Button ID="btnNoPass" runat="server" CssClass="SmallButton2" TabIndex="6" Text="不通过"
                        Width="60px" OnClick="btnNoPass_Click" />
                </td>
                <td align="center" style="width: 200px">
                    <asp:Button ID="btnCancel" runat="server" CssClass="SmallButton2" TabIndex="8" Text="返回"
                        Width="60px" OnClick="btnCancel_Click" />
                </td>
                <td>
                    <asp:HiddenField ID="EvaluateID" runat="server" />
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
