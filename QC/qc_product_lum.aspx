<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_product_lum.aspx.cs" Inherits="QC_qc_product_lum" %>

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
        <table cellspacing="0" cellpadding="2" bgcolor="white" border="0" style="width: 742px">
            <tr>
                <td colspan="1" style="width: 52px;">
                    加工单:
                </td>
                <td colspan="1" style="width: 8px;">
                    &nbsp;<asp:Label ID="lblNbr" runat="server" Text="Label" Width="90px"></asp:Label>
                </td>
                <td colspan="1" style="width: 61px;">
                    ID号:
                </td>
                <td colspan="1" style="width: 81px;">
                    &nbsp;<asp:Label ID="lblLot" runat="server" Text="Label" Width="90px"></asp:Label>
                </td>
                <td colspan="1" style="width: 69px;">
                    QAD号:
                </td>
                <td colspan="1" style="width: 14px;">
                    &nbsp;<asp:Label ID="lblPart" runat="server" Text="Label" Width="78px"></asp:Label>
                </td>
                <td colspan="1" style="width: 66px;">
                    采购数量:
                </td>
                <td colspan="1" style="width: 104px;">
                    <asp:Label ID="lblRcvd" runat="server" Text="Label" Width="78px"></asp:Label>
                </td>
                <td colspan="1" style="width: 64px;">
                </td>
                <td colspan="4" style="width: 130px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="10" valign="top">
                    测试时间:<asp:TextBox ID="txtHour" runat="server" Width="83px"></asp:TextBox>/h &nbsp;
                    &nbsp;测试方式:<asp:DropDownList ID="dropWay" runat="server" Width="67px">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem Value="UP">UP </asp:ListItem>
                        <asp:ListItem Value="DOWN">DOWN</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
            border-bottom: silver 1px solid; width: 744px;">
            <tr>
                <td align="left" style="width: 110px">
                    1、标准文件导入：
                </td>
                <td colspan="3">
                    <input type="file" id="filename" runat="server" style="width: 612px; height: 22px" />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2" style="height: 25px">
                    &nbsp;
                </td>
                <td colspan="2" valign="bottom" align="center" style="height: 25px">
                    <input type="button" value="导入数据" id="uploadBtn" style="width: 120px" class="SmallButton2"
                        runat="server" name="uploadBtn" onserverclick="uploadBtn_ServerClick" validationgroup="Import" />
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 110px">
                    2、Excel文件导入：
                </td>
                <td valign="top" colspan="3">
                    <input type="file" id="filenameExcel" runat="server" style="width: 612px; height: 22px" />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2" style="height: 25px">
                    &nbsp;
                </td>
                <td colspan="2" valign="bottom" align="center" style="height: 25px">
                    <label onclick="submit();">
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        <input type="button" value="导入数据" id="uploadBtnExcel" style="width: 120px" class="SmallButton2"
                            runat="server" validationgroup="Import" onserverclick="uploadBtnExcel_ServerClick" />
                        &nbsp;&nbsp;获取<a href="/quality/luminousFlux.aspx?tm=/docs/luminousFluxDataExcel.xls"><font
                            color="#000099">Excel文件</font></a>样式
                    </label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 20px">
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" CssClass="SmallButton2"
            OnClick="btnBack_Click" Text="返回" Visible="True" Width="80px" /></form>
    </div>
    <script language="javascript" type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
