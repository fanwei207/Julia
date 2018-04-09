<%@ Page Language="C#" AutoEventWireup="true" CodeFile="soque_edit.aspx.cs" Inherits="soque_edit" %>

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
    <div align="center">
        <form id="Form1" runat="server">
        <table id="table1" cellpadding="0" cellspacing="0" style="width: 824px">
            <tr>
                <td style="width: 90px">
                    <asp:Label ID="lbl_id" runat="server" Visible="false"></asp:Label>
                </td>
                <td style="text-align: left;">
                    Order:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtNbr" runat="server" TabIndex="1" Width="100px" CssClass="SmallTextBox1"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Line:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtLine" runat="server" TabIndex="2" Width="100px" CssClass="SmallTextBox1"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Degree:
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="dropDegree" runat="server" Width="94px" DataTextField="soque_degreeName"
                        DataValueField="soque_did">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 67px">
                </td>
                <td style="text-align: left;">
                    Product:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtCustPart" runat="server" TabIndex="3" Width="150px" CssClass="SmallTextBox1"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Qty:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtQty" runat="server" TabIndex="4" Width="100px" CssClass="SmallTextBox1"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 67px">
                </td>
                <td style="text-align: left;">
                    QAD:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtPart" runat="server" TabIndex="5" Width="150px" CssClass="SmallTextBox1"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                    Customer:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtCust" runat="server" TabIndex="6" Width="100px" CssClass="SmallTextBox1"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="height: 22px; width: 67px;">
                </td>
                <td style="text-align: left; height: 22px;">
                    Ord Date:
                </td>
                <td style="text-align: left; height: 22px;">
                    <asp:TextBox ID="txtOrdDate" runat="server" TabIndex="7" Width="100" CssClass="smalltextbox Date"></asp:TextBox>
                </td>
                <td style="text-align: left; height: 22px;">
                    Due Date:
                </td>
                <td style="text-align: left; height: 22px;">
                    <asp:TextBox ID="txtShipDate" runat="server" TabIndex="8" Width="100" CssClass="smalltextbox Date"></asp:TextBox>
                </td>
                <td style="height: 22px">
                </td>
                <td style="height: 22px">
                </td>
            </tr>
            <tr>
                <td style="text-align: right; height: 24px;">
                    Submite To£º</td>
                <td colspan="6" style="text-align: left; height: 24px;">
                    <asp:TextBox ID="txb_choose" runat="server" Width="500px" CssClass="SmallTextBox1"></asp:TextBox>
                    <asp:TextBox ID="txb_chooseid" runat="server" Style="display: none" Visible="True"
                        CssClass="SmallTextBox1"></asp:TextBox>
                    <%--<asp:Button ID="btn_choose" runat="server" CssClass="SmallButton2" Text="Select" OnClick="btn_choose_Click" />--%>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 67px;">
                    Copy To£º
                </td>
                <td colspan="6" style="text-align: left;">
                    <asp:TextBox ID="txb_cc" runat="server" Width="500px" CssClass="SmallTextBox1"></asp:TextBox>
                    <asp:TextBox ID="txb_ccid" runat="server" Style="display: none" Visible="True" CssClass="SmallTextBox1"></asp:TextBox>
                    <%--<asp:Button ID="btn_cc" runat="server" CssClass="SmallButton2" Text="Select" OnClick="btn_cc_Click" />--%>
                </td>
            </tr>
            <tr>
                <td style="height: 14px; text-align: right; width: 67px;">
                    Details£º
                </td>
                <td colspan="6" style="height: 14px; text-align: left">
                    <asp:CheckBoxList ID="chklDetails" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                        DataTextField="soques_step" DataValueField="soques_index">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td style="height: 14px; text-align: right; width: 67px;">
                    Due Date£º</td>
                <td colspan="6" style="height: 14px; text-align: left">
                    <asp:TextBox ID="txtDueDate" runat="server" Width="80px" CssClass="SmallTextBox1 Date"></asp:TextBox>
                    (* A date that <strong>Details</strong> will be finished)</td>
            </tr>
            <tr>
                <td valign="top" style="text-align: right; width: 67px;">
                    &nbsp;Remarks£º<br />
                    (Limit 100 Characters)
                </td>
                <td colspan="6" style="text-align: left;">
                    <asp:TextBox ID="txtRmks" runat="server" Height="200px" TextMode="MultiLine" Width="100%"
                        CssClass="SmallTextBox1" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <%--<td style="text-align: right; width: 67px;">
                    &nbsp;E-Mail£º
                </td>
                <td colspan="6" style="text-align: left;">
                    <font style="color: red">Please enter in your e-mail address.</font>
                    <br />
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="SingleLine" Width="500" CssClass="SmallTextBox1"
                        MaxLength="50"></asp:TextBox>
                </td>--%>
            </tr>
            <tr>
                <td align="center" colspan="7">
                    <asp:Button ID="btn_next" runat="server" CssClass="SmallButton2" Text="Submit" Width="80px"
                        OnClick="btn_next_Click" />
                    <asp:Button ID="btn_reject" runat="server" CssClass="SmallButton2" Text="Delete" Visible="False" />
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton2" Text="Back" OnClick="btn_back_Click" />
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
