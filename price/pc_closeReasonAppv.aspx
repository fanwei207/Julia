<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pc_closeReasonAppv.aspx.cs" Inherits="price_pc_closeReasonAppv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <base target="_self">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(function () {
            var txtCloseReason = $("#txtCloseReason");
            txtCloseReason.load(txtval());
            txtCloseReason.click(
            function () {
                txtCloseReason.val() == "请输入关闭原因：";
                txtcl();

            }
            );


            function txtval() {
                if (txtCloseReason.val() == "") {

                    txtCloseReason.val("请输入关闭原因：");
                    txtCloseReason.attr("color", "#AAAAAA");

                }
            }
            function txtcl() {

                txtCloseReason.val("");

            }

        })
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <div>
                <asp:Label ID="lbDetID" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;
            QAD:&nbsp;&nbsp;<asp:Label ID="lbQAD" runat="server"></asp:Label>&nbsp;&nbsp;
            部件号:&nbsp;&nbsp;<asp:Label ID="lbCode" runat="server"></asp:Label>&nbsp;&nbsp;
            供应商:&nbsp;&nbsp;<asp:Label ID="lbVender" runat="server"></asp:Label>&nbsp;&nbsp;
            供应商名称:&nbsp;&nbsp;<asp:Label ID="lbVenderName" runat="server"></asp:Label>&nbsp;&nbsp;
            <div>
                报价:&nbsp;&nbsp;<asp:Label ID="lbPrice" runat="server"></asp:Label>&nbsp;&nbsp;
            核价:&nbsp;&nbsp;<asp:Label ID="lbCheckPrice" runat="server"></asp:Label>&nbsp;&nbsp;
            </div>
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtCloseReason" Width="400px" CssClass="SmallTextBox"
                    TextMode="MultiLine" Height="100px"></asp:TextBox>
            </div>
            <div id="chkDiv" runat="server">
                <asp:CheckBox ID="chkIsClose" runat="server" Text="保留数据" Checked="true" /><br />
                <div >
                    <table >
                        <tr align="left" >
                            <td>1.当申请关闭的供应商、物料与单位的组合不再会被申请。请勾选
                            </td>
                            

                        </tr>
                        <tr><td>2.当是修改拼版尺寸这样，该供应商、物料与单位的组合会再度被申请价格时。请不要勾选</td></tr>
                    </table>
                </div>
            </div>
            <div>
                <asp:Button ID="btnCloseAppv" runat="server" Text="申请关闭"
                    CssClass="SmallButton2" OnClick="btnCloseAppv_Click" />
                &nbsp;&nbsp;
            <asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="SmallButton2"
                OnClick="btnReturn_Click" />
            </div>
        </div>
    </form>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
