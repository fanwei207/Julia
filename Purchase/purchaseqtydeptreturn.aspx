<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.PurchaseQtyDeptReturn"
    CodeFile="PurchaseQtyDeptReturn.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        function TXclear() {
            var today = new Date();
            window.location.href = "/Purchase/PurchaseQtyReturn.aspx?rm=" + today + Math.random();
        }
    </script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <asp:ValidationSummary ID="cMsg" runat="server" ShowMessageBox="True" ShowSummary="False"
            HeaderText="请注意以下输入项 !"></asp:ValidationSummary>
        <asp:Label ID="Label1" Style="padding-top: 5px; text-align: center" runat="server"
            Width="700px" ForeColor="Black" Font-Bold="True" Font-Size="12pt" Height="30px">材料部门退库</asp:Label><br>
        <asp:Table ID="Table1" runat="server" Width="700px" BorderColor="White" BorderWidth="0px"
            GridLines="None" CellSpacing="0">
            <asp:TableRow Height="30px">
                <asp:TableCell Text="仓库名称"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:DropDownList ID="warehouse" runat="server" Width="550px">
                    </asp:DropDownList>
                    <asp:CompareValidator Display="none" ValueToCompare="0" ControlToValidate="warehouse"
                        ID="cMsg12" Operator="NotEqual" Type="String" runat="server" ErrorMessage="选择仓库" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px" BorderStyle="None" BorderColor="white">
                <asp:TableCell Text="状态"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:DropDownList ID="status" runat="server" Width="550px">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="材料代码"></asp:TableCell>
                <asp:TableCell ColumnSpan="2">
                    <asp:TextBox TabIndex="0" runat="server" AutoPostBack="True" Width="300px" Wrap="False"
                        ID="partCode" OnTextChanged="txtPartDescBinding"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="partCode"
                        ErrorMessage="材料代码不能为空" ID="Requiredfieldvalidator1"></asp:RequiredFieldValidator>
                </asp:TableCell>
                <asp:TableCell Text="材料分类" Width="150px"></asp:TableCell>
                <asp:TableCell ColumnSpan="1">
                    <asp:Label runat="server" ID="lblPartCat"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="材料描述"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:Label runat="server" ID="lblPartDesc"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="部门代码"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:TextBox TabIndex="0" runat="server" AutoPostBack="True" Width="550px" Wrap="False"
                        ID="DepartmentCode" OnTextChanged="txtDeptCodeInputed"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="DepartmentCode"
                        ErrorMessage="部门代码不能为空" ID="Requiredfieldvalidator2"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="部门名称"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:Label runat="server" ID="lblDeptName"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="计划单号"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:TextBox TabIndex="0" runat="server" Width="550px" Wrap="False" ID="txtPlanCode"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="退库日期(年-月-日)"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:TextBox TabIndex="0" runat="server" Width="550px" Wrap="False" ID="txtEnterDate"
                        CssClass="smalltextbox Date"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtEnterDate"
                        ErrorMessage="退库日期不能为空" ID="Requiredfieldvalidator3"></asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" Display="None" Operator="DataTypeCheck" Type="Date"
                        ControlToValidate="txtEnterDate" ErrorMessage="请输入正确的日期格式（年-月-日）" ID="Comparevalidator1"></asp:CompareValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="退库数量"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:TextBox TabIndex="0" runat="server" Width="550px" Wrap="False" ID="txtPartQty"
                        CssClass="smalltextbox Numeric"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ValidationExpression="[0-9.]+" Display="None"
                        ControlToValidate="txtPartQty" ErrorMessage="请输入正确的退库数量" ID="Regularexpressionvalidator1"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" Display="None" ControlToValidate="txtPartQty"
                        ErrorMessage="退库数量不能为空" ID="Requiredfieldvalidator4"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="30px">
                <asp:TableCell Text="备注"></asp:TableCell>
                <asp:TableCell ColumnSpan="4">
                    <asp:TextBox TabIndex="0" runat="server" Width="550px" Wrap="False" ID="txtNotes"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Bottom" Height="30px">
                <asp:TableCell ColumnSpan="1" Width="150px" HorizontalAlign="center">
                    <asp:Button runat="server" Width="100px" Text="退库" OnClick="addRecord" TabIndex="0"
                        CssClass="SmallButton2" ID="BtnAdd"></asp:Button>
                    <asp:Label runat="server" Width="0" Visible="False" ID="partID"></asp:Label>
                    <asp:Label runat="server" Width="0" Visible="False" ID="DepartmentID"></asp:Label>
                    <asp:Label runat="server" Width="0" Visible="False" ID="lblPlanID"></asp:Label>
                </asp:TableCell>
                <asp:TableCell Width="200px" Text="&#160;"></asp:TableCell>
                <asp:TableCell Width="150px" Text="&#160;"></asp:TableCell>
                <asp:TableCell Text="&#160;"></asp:TableCell>
                <asp:TableCell VerticalAlign="Bottom" ColumnSpan="1" Width="200px" HorizontalAlign="center">
							<input type="button" value="&nbsp;清除&nbsp;" onclick="TXclear();" TabIndex="0" class="SmallButton2"
								style="width:100px" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
