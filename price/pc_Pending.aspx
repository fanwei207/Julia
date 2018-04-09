<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pc_Pending.aspx.cs" Inherits="price_pc_Pending" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(function () {
            var txtPending = $("#txtPending");
            txtPending.load(txtval());
            txtPending.click(
            function () {
                txtPending.val() == "请输入挂起原因：";
                txtcl();

            }
            );


            function txtval() {
                if (txtPending.val() == "") {

                    txtPending.val("请输入挂起原因：");
                    txtPending.attr("color", "#AAAAAA");

                }
            }
            function txtcl() {

                txtPending.val("");

            }

        })
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <div>
            <asp:Label ID="lbDetID" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;
            <asp:Label ID="lbIsPrice" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;
            <asp:Label ID="lbStatus" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;
            QAD:&nbsp;&nbsp;<asp:Label ID="lbQAD" runat="server"></asp:Label>&nbsp;&nbsp;
            部件号:&nbsp;&nbsp;<asp:Label ID="lbCode" runat="server"></asp:Label>&nbsp;&nbsp;
            供应商:&nbsp;&nbsp;<asp:Label ID="lbVender" runat="server"></asp:Label>&nbsp;&nbsp;
            <div>
            报价:&nbsp;&nbsp;<asp:Label ID="lbPrice" runat="server"></asp:Label>&nbsp;&nbsp;
            核价:&nbsp;&nbsp;<asp:Label ID="lbCheckPrice" runat="server"></asp:Label>&nbsp;&nbsp;
            </div>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txtPending" Width="400px" CssClass="SmallTextBox"
                TextMode="MultiLine" Height="100px"  ></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnPending" runat="server" Text="挂起" CssClass="SmallButton2" OnClick="btnPending_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="SmallButton2" OnClick="btnReturn_Click" />
        </div>
    </div>
    </form>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
