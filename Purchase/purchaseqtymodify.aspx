<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.PurchaseQtyModify"
    CodeFile="PurchaseQtyModify.aspx.vb" %>

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
        <asp:Label ID="lblTitle" Style="padding-top: 5px; text-align: center" runat="server"
            Height="30px" Font-Size="12pt" Font-Bold="True" Width="450px"></asp:Label><br>
        <br>
        <asp:Table ID="Table1" runat="server" Width="400px" CellSpacing="0" GridLines="Both"
            BorderWidth="0px" BorderColor="Black">
            <asp:TableRow Height="25px">
                <asp:TableCell Text="日期（年-月-日）：" HorizontalAlign="Right"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox TabIndex="0" runat="server" Width="250px" Wrap="False" ID="txtEnterDate"
                        CssClass="smalltextbox Date"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="25px">
                <asp:TableCell Text="数量：" HorizontalAlign="Right"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox TabIndex="0" runat="server" Width="250px" Wrap="False" ID="txtPartQty"
                        CssClass="smalltextbox Numeric"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="25px">
                <asp:TableCell Text="类型：" HorizontalAlign="Right"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox TabIndex="0" runat="server" Width="250px" Wrap="False" ID="txtType"
                        ReadOnly="True"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="25px">
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label ID="lblCompDept" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox TabIndex="0" runat="server" Width="250px" Wrap="False" ID="txtCompDept"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="25px">
                <asp:TableCell Text="定单号：" HorizontalAlign="Right"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox TabIndex="0" runat="server" Width="250px" Wrap="False" ID="txtOrder"
                        ReadOnly="True"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="25px">
                <asp:TableCell Text="计划单号：" HorizontalAlign="Right"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox TabIndex="0" runat="server" Width="250px" Wrap="False" ID="txtPlan"
                        ReadOnly="True"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="25px">
                <asp:TableCell Text="送货单号：" HorizontalAlign="Right"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox TabIndex="0" runat="server" Width="250px" Wrap="False" ID="txtDelivery"
                        ReadOnly="True"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="25px">
                <asp:TableCell Text="备注：" HorizontalAlign="Right"></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox TabIndex="0" runat="server" Width="250px" Wrap="False" ID="txtNotes"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <asp:Button ID="BtnOK" TabIndex="0" runat="server" Width="100px" CssClass="SmallButton2"
            Text="修改"></asp:Button>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnCancel" TabIndex="0" runat="server" Width="100px" CssClass="SmallButton2"
            Text="取消"></asp:Button>
        <asp:Label runat="server" Width="0" Visible="False" ID="lblCompDeptID"></asp:Label>
        <asp:Label runat="server" Width="0" Visible="False" ID="lblQty"></asp:Label>
        <asp:Label runat="server" Width="0" Visible="False" ID="lblOrderID"></asp:Label>
        <asp:Label runat="server" Width="0" Visible="False" ID="lblPlanID"></asp:Label>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
